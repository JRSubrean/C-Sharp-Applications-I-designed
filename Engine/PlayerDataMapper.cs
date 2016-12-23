using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
/*using System.Data; and using System.Data.SqlClient; are namespaces, where the SqlConnection,
 SqlCommand and SqlDataReader classes exists, and are classes needed to access the SQL server.*/

namespace Engine
{
    public static class PlayerDataMapper
    {
        private static readonly string theConnectionString = "Data Source=(local);Initial Catalog=Pilot;Integrated Security=True";

        public static Player CreateFromDatabase()
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(theConnectionString))
                /*This connects to the database*/
                {
                    connection.Open();
                    /*Opens the connection, so we can perform SQL commands*/

                    Player playerOne;

                    using(SqlCommand savedGameCommand = connection.CreateCommand())
                    /*Creates a SQL command object that uses the connection to our database.
                     The SqlCommand object is where we create our SQL statement*/
                    {
                        savedGameCommand.CommandType = CommandType.Text;
                        /*This SQL statement reads the first rows in the SavedGame table.*/

                        savedGameCommand.CommandText = "SELECT TOP 1 * FROM SavedGame";
                        /*For this program, we should only ever have 1 row, but this will
                         ensure we only get 1 record in our SQL query results.*/

                        SqlDataReader reader = savedGameCommand.ExecuteReader();
                        /*You ExecuteReader when you expect the query to return a row, or rows*/

                        if(!reader.HasRows)
                        /*Checks if the query didn't return a row/record of data*/
                        {
                            return null;
                            /*If there is no data in the SavedGame table, then return null 
                            (no saved player data)*/
                        }

                        reader.Read();
                        /*Gets the row/record from the data reader*/

                        int currentHitPoints = (int)reader["CurrentHitPoints"];
                        int maximumHitPoints = (int)reader["MaximumHitPoints"];
                        int goldAmount = (int)reader["Gold"];
                        int expPoints = (int)reader["ExperiencePoints"];
                        int idOfCurrentLocation = (int)reader["CurrentLocation"];
                        /*Gets the column values for the row/record*/

                        playerOne = Player.CreatePlayerFromDatabase(currentHitPoints, maximumHitPoints, goldAmount, expPoints, idOfCurrentLocation);
                    }

                    using(SqlCommand inventoryCommand = connection.CreateCommand())
                    /*Read the rows/record from the Inventory table, and add them to the player*/
                    {
                        inventoryCommand.CommandType = CommandType.Text;
                        inventoryCommand.CommandText = "SELECT * FROM Inventory";

                        SqlDataReader reader = inventoryCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while(reader.Read())
                            {
                                int inventoryItemId = (int)reader["InventoryItem"];
                                int quantityOfItem = (int)reader["quantity"];

                                playerOne.AddItemToInventory(World.ItemByID(inventoryItemId), quantityOfItem);
                                /*Adds the item to the player's inventory*/
                            }
                        }
                    }

                    using (SqlCommand questCommand = connection.CreateCommand())
                    {
                        questCommand.CommandType = CommandType.Text;
                        questCommand.CommandText = "SELECT * FROM Quest";

                        SqlDataReader reader = questCommand.ExecuteReader();

                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                int idOfQuest = (int)reader["questId"];
                                bool isQuestDone = (bool)reader["questCompletedOrFailed"];

                                PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(idOfQuest));
                                playerQuest.questCompletedOrFailed = isQuestDone;
                                /*Build the PlayerQuest item for this row*/

                                playerOne.currentQuests.Add(playerQuest);
                            }
                        }
                    }

                        return playerOne;
                    /*Now that the player has been built from the database, return it.*/
                }
            }
            catch(Exception error)
            {
                /*Ignore errors. If there is an error, this function will return a "null" player.
                 
                 A try/catch block is used, as there can be problems with the code here, such as
                 the database might not be running, or the connection string might not be correct, etc.
                 The function will "try" to execute the code in the "try" section, and if there is an
                 error, it will run the "catch" section.*/
            }
            return null;
        }

        public static void SaveToDatabase(Player playerOne)
        {
            try
            {
                using(SqlConnection connection = new SqlConnection(theConnectionString))
                {
                    connection.Open();
                    /*Opens connection, so we can perform SQL commands*/

                    using(SqlCommand existingRowCountCommand = connection.CreateCommand())
                    {
                        existingRowCountCommand.CommandType = CommandType.Text;
                        existingRowCountCommand.CommandText = "SELECT count(*) FROM SavedGame";

                        int existingRowCount = (int)existingRowCountCommand.ExecuteScalar();
                        /*Use ExecuteScalar when your query will return only one value*/

                        if(existingRowCount == 0)
                        {
                            using (SqlCommand insertSavedGame = connection.CreateCommand())
                            /*There is no existing row, so do an INSERT*/
                            {
                                insertSavedGame.CommandType = CommandType.Text;
                                insertSavedGame.CommandText = "INSERT INTO SavedGame" +
                                    "(CurrentHitPoints, MaximumHitPoints, Gold, ExperiencePoints, CurrentLocation)"
                                    + "VALUES " + "(@CurrentHitPoints, @MaximumHitPoints, @Gold, @ExperiencePoints, @CurrentLocation)";

                                insertSavedGame.Parameters.Add("@CurrentHitPoints", SqlDbType.Int);
                                insertSavedGame.Parameters["@CurrentHitPoints"].Value = playerOne.currentHitPoints;
                                insertSavedGame.Parameters.Add("@MaximumHitPoints", SqlDbType.Int);
                                insertSavedGame.Parameters["@MaximumHitPoints"].Value = playerOne.maximumHitPoints;
                                insertSavedGame.Parameters.Add("@Gold", SqlDbType.Int);
                                insertSavedGame.Parameters["@Gold"].Value = playerOne.amountOfGold;
                                insertSavedGame.Parameters.Add("@ExperiencePoints", SqlDbType.Int);
                                insertSavedGame.Parameters["@ExperiencePoints"].Value = playerOne.experiencePoints;
                                insertSavedGame.Parameters.Add("@CurrentLocation", SqlDbType.Int);
                                insertSavedGame.Parameters["@CurrentLocation"].Value = playerOne.currentLocation.locationId;
                                /*The following insertSavedGame codes here perform the SQL command.*/

                                insertSavedGame.ExecuteNonQuery();
                                /*Use ExecuteNonQuery, because this query doesn't return any results.*/
                            }
                        }
                        else//If there is an existing row, then run this code
                        {
                            using(SqlCommand updateSavedGame = connection.CreateCommand())
                            /*There is an existing row, so do an UPDATE*/
                            {
                                updateSavedGame.CommandType = CommandType.Text;
                                updateSavedGame.CommandText = "UPDATE SavedGame " + "SET CurrentHitPoints = @CurrentHitPoints, " +
                                    "MaximumHitPoints = @MaximumHitPoints, " + "Gold = @Gold, " + "ExperiencePoints = @ExperiencePoints, " +
                                    "CurrentLocation = @CurrentLocation";

                                updateSavedGame.Parameters.Add("@CurrentHitPoints", SqlDbType.Int);
                                updateSavedGame.Parameters["@CurrentHitPoints"].Value = playerOne.currentHitPoints;
                                updateSavedGame.Parameters.Add("@MaximumHitPoints", SqlDbType.Int);
                                updateSavedGame.Parameters["@MaximumHitPoints"].Value = playerOne.maximumHitPoints;
                                updateSavedGame.Parameters.Add("@Gold", SqlDbType.Int);
                                updateSavedGame.Parameters["@Gold"].Value = playerOne.amountOfGold;
                                updateSavedGame.Parameters.Add("@ExperiencePoints", SqlDbType.Int);
                                updateSavedGame.Parameters["@ExperiencePoints"].Value = playerOne.experiencePoints;
                                updateSavedGame.Parameters.Add("@CurrentLocation", SqlDbType.Int);
                                updateSavedGame.Parameters["@CurrentLocation"].Value = playerOne.currentLocation;

                                /*This passes the values from the player object to the SQL query, using parameters.
                                 By using parameters, it helps make the program more secure, as well as prevent SQL
                                 injection attacks.
                                 
                                 @ signifies that the value is a parameter to pass into the SQL statement.*/

                                updateSavedGame.ExecuteNonQuery();
                                /*This performs the SQL command. Again, ExecuteNonQuery is used, as this
                                 query doesn't return any results.*/
                            }
                        }
                    }
                    using(SqlCommand deleteQuestsCommand = connection.CreateCommand())
                    /*This deletes existing Quest rows*/
                    {
                        deleteQuestsCommand.CommandType = CommandType.Text;
                        deleteQuestsCommand.CommandText = "DELETE FROM Quest";

                        deleteQuestsCommand.ExecuteNonQuery();
                    }

                    foreach(PlayerQuest playerQuest in playerOne.currentQuests)
                    {
                        using(SqlCommand insertQuestCommand = connection.CreateCommand())
                        {
                            insertQuestCommand.CommandType = CommandType.Text;
                            insertQuestCommand.CommandText = "INSERT INTO Quest (questId, questCompletedOrFailed) VALUES (@questId, @questCompletedOrFailed)";

                            insertQuestCommand.Parameters.Add("@questId", SqlDbType.Int);
                            insertQuestCommand.Parameters["@questId"].Value = playerQuest.questInfo.questId;
                            insertQuestCommand.Parameters.Add("@questCompletedOrFailed", SqlDbType.Int);
                            insertQuestCommand.Parameters["@questCompletedOrFailed"].Value = playerQuest.questCompletedOrFailed;

                            insertQuestCommand.ExecuteNonQuery();
                        }
                    }

                    using(SqlCommand deleteInventoryCommand = connection.CreateCommand())
                    /*Deletes existing Inventory rows*/
                    {
                        deleteInventoryCommand.CommandType = CommandType.Text;
                        deleteInventoryCommand.CommandText = "DELETE FROM Inventory";

                        deleteInventoryCommand.ExecuteNonQuery();
                    }

                    foreach(InventoryItem inventoryItem in playerOne.theInventory)
                    {
                        using(SqlCommand insertInventoryCommand = connection.CreateCommand())
                        {
                            insertInventoryCommand.CommandType = CommandType.Text;
                            insertInventoryCommand.CommandText = "INSERT INTO Inventory (InventoryItem, quantity) VALUES (@InventoryItem, @quantity)";

                            insertInventoryCommand.Parameters.Add("@InventoryItem", SqlDbType.Int);
                            insertInventoryCommand.Parameters["@InventoryItem"].Value = inventoryItem.itemInfo.itemId;
                            insertInventoryCommand.Parameters.Add("@quantity", SqlDbType.Int);
                            insertInventoryCommand.Parameters["@quantity"].Value = inventoryItem.quantity;

                            insertInventoryCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch(Exception error)
            {
                //Ignore errors for now.
            }
        }
    }
}

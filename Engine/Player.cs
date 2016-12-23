using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.ComponentModel;

namespace Engine
{
    public class Player : LivingCreature
    {
        public event EventHandler<MessageEventArgs> onMessage;
        /*The above line signifies that the Player class will send an event notification with a
         MessageEventArgs object - the object with the message text we want to display.*/

        private void RaiseMessage(string message, bool addExtraNewLine = false)
        {
            if(onMessage != null)
            {
                onMessage(this, new MessageEventArgs(message, addExtraNewLine));
            }
        }
        /*RaiseMessage raises the event. When that happens, it passes a MessageEventArgs, with the values the
         UI code needs.*/

        private int playerGold;
        private int playerExperiencePoints;

        private Monster theCurrentMonster;
        /*Holds the monster the player's fighting at the current location.*/
        public int amountOfGold
        {
            get { return playerGold; }
            set
            {
                playerGold = value;
                WhenPropertyChanged("Gold");
            }
        }
        public int experiencePoints
        {
            get { return playerExperiencePoints; }
            private set
            {
                playerExperiencePoints = value;
                WhenPropertyChanged("ExperiencePoints");
                WhenPropertyChanged("Level");
            }
        }
        /*Set is set to private here, so that the value can only be set by other methods in the 
        Player class. Gold and ExperiencePoints use databinding.
        
        The experiencePoints property raises a WhenPropertyChanged event for both
        ExperiencePoints and Level, since the Level value is never set and always calculated
        from ExperiencePoints.
        
        Now, everytime the ExperiencePoints property value changes, it'll also send a
        notification to update the level.
        
        The Player class is using the WhenPropertyChanged() function in LivingCreature, since
        the Player class inherits from the LivingCreature class. This, it can use any
        functions, properties and events from LivingCreature, as long as the are visible as
        "public", "internal", or "protected".*/

        public int playerLevel
        {
            get { return ((experiencePoints / 100) + 1); }
        }
        /*Set is removed, since we're never going to put a value into the level
         property. get; was also changed, since it was an auto-property, meaning
         get; would get the value it was previously set to.
         
             1 is added to the function, so the player will start out at level 1, and
             not 0. The int property will automatically round the answer down, so that's
             why the + 1 is added in.*/
        public BindingList<InventoryItem> theInventory { get; set; }
        /*To bind a List property, you need to change its datatype to either "BindingList" or
         "ObservableCollection". BindingList, however, gives more options than ObservableCollection, such as searching
         and sorting*/
        public BindingList<PlayerQuest> currentQuests { get; set; }

        /*These are properties of the player class

        These 2 list properties can hold lists containing items from the InventoryItem
        and PlayerQuest objects.*/

        //public Location currentLocation { get; set; }


        private Location theCurrentLocation;

        public Location currentLocation
        {
            get { return theCurrentLocation; }
            set
            {
                theCurrentLocation = value;
                WhenPropertyChanged("CurrentLocation");
            }
        }
        /*The auto-property currentLocation has been changed up, to where it uses a backing variable, and this will
         raise an event, when location is changed.*/


        public Weapon currentWeapon { get; set; }

        public List<Weapon> Weapons
        {
            get { return theInventory.Where(x => x.itemInfo is Weapon).Select(x => x.itemInfo as Weapon).ToList(); }
        }

        public List<HealingPotion> Potions
        {
            get { return theInventory.Where(x => x.itemInfo is HealingPotion).Select(x => x.itemInfo as HealingPotion).ToList(); }
        }

        /*The lists of Weapons and Potions are created to bind them to the comboboxes. LINQ is used here to create
         a new list of Inventory items, where the "itemInfo" property is the datatype "Weapon" (Or "HealingPotion").
         
        "Is" is used to check the datatype of an object.
        
        The "Select(x => x.Details)" returns only the Details property of the InventoryItem object.
        
        "ToList()" converts the results of the LINQ query into a new list.*/

        private void RaiseInventoryChangedEvent(Item aItem)
        {
            if(aItem is Weapon)
            {
                WhenPropertyChanged("Weapons");
            }
            if(aItem is HealingPotion)
            {
                WhenPropertyChanged("Potions");
            }
        }
        /*When we add/remove anything in the inventory list, this method gets called.
         
        If the item was a weapon, it'll raise an event saying that the "Weapons" property has been changed,
        likewise with whether potions are added or removed.*/

        public void RemoveItemFromInventory(Item removeAnItem, int quantity = 1)
        {
            InventoryItem anItem = theInventory.SingleOrDefault(ii => ii.itemInfo.itemId == removeAnItem.itemId);

            if(anItem == null)
            {
                /*The item is not in the player's inventory, so ignore it. We might want to raise an errorr for
                this situation.*/
            }
            else
            {
                anItem.quantity -= quantity;
                /*They have the item in their inventory, so decrease the quantity.*/

                if(anItem.quantity < 0)
                {
                    anItem.quantity = 0;
                }
                /*We don't want to allow negative quantities, so we'd want to raise an error, in case that
                 happens.*/

                if(anItem.quantity == 0)
                {
                    theInventory.Remove(anItem);
                }
                /*If the quantity is 0, then remove the item from the list.*/

                RaiseInventoryChangedEvent(removeAnItem);
                /*This notifies the UI that the inventory has changed.*/
            }
        }
        /*When this method is called, we pass the item we want to remove, along with the quanity.
         
             For the int quantity parameter, is has an "= 1" after it, meaning it's an optional parameter. If
             we call this function with only an item,, it'll assume we want to remove one of it, and if we
             want to delete more than 1 of that item, we can pass in the number to remove for this parameter.*/

        private Player(int currentHP, int maximumHP, int goldAmount, int totalExp) : base(currentHP, maximumHP)
        /*(currentHitPoints, maximumHitPoints, amountOfGold, experiencePoints) : (currentHitPoints, maximumHitPoints)*/
        {
            amountOfGold = goldAmount;
            experiencePoints = totalExp;

            theInventory = new BindingList<InventoryItem>();
            currentQuests = new BindingList<PlayerQuest>();

            /*These 2 lines set the value of the new properties to empty lists. Should
             this not be done, the values of these 2 properties would be "null."*/
        }

        public static Player CreateDefaultPlayer()
        {
            Player player = new Player(20, 20, 100, 0);
            player.theInventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));
            player.currentLocation = World.LocationByID(World.LOCATION_ID_HOME);

            return player;
        }

        public void AddExperiencePoints(int experiencePointsToAdd)
        {
            experiencePoints += experiencePointsToAdd;
            maximumHitPoints = (playerLevel * 10);
        }

        public static Player CreatePlayerFromXmlString(string xmlPlayerData)
        {
            try
            {
                XmlDocument playerData = new XmlDocument();

                playerData.LoadXml(xmlPlayerData);

                int currentHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentHitPoints").InnerText);
                int maximumHitPoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/MaximumHitPoints").InnerText);
                int amountOfGold = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/Gold").InnerText);
                int experiencePoints = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/ExperiencePoints").InnerText);
                /*InnerText always returns a string, so Convert.ToInt32 is used to get the numbers.
                 An XPath is given for the SelectSingleNode function.*/

                Player player = new Player(currentHitPoints, maximumHitPoints, amountOfGold, experiencePoints);

                int currentLocationId = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentLocation").InnerText);
                player.currentLocation = World.LocationByID(currentLocationId);

                if(playerData.SelectSingleNode("/Player/Stats/CurrentWeapon") != null)
                {
                    int currentWeaponId = Convert.ToInt32(playerData.SelectSingleNode("/Player/Stats/CurrentWeapon").InnerText);
                    player.currentWeapon = (Weapon)World.ItemByID(currentWeaponId);
                }
                /*The above code will load the player's currently selected weapon, if one exists in the save
                 game file.*/

                foreach(XmlNode node in playerData.SelectNodes("/Player/InventoryItems/InventoryItem"))
                /*SelectNodes gets every node that matches the XPath (i.e. Nodes that have multiple items).*/
                {
                    int itemId = Convert.ToInt32(node.Attributes["ID"].Value);//ItemID
                    int quantity = Convert.ToInt32(node.Attributes["Quantity"].Value);

                    for(int i = 0; i < quantity; i++)
                    {
                        player.AddItemToInventory(World.ItemByID(itemId));
                    }
                }

                foreach(XmlNode node in playerData.SelectNodes("/Player/PlayerQuests/PlayerQuest"))
                {
                    int questId = Convert.ToInt32(node.Attributes["ID"].Value);//QuestID
                    bool questCompletedOrFailed = Convert.ToBoolean(node.Attributes["IsCompleted"].Value);//questCompletedOrFailed

                    PlayerQuest playerQuest = new PlayerQuest(World.QuestByID(questId));
                    playerQuest.questCompletedOrFailed = questCompletedOrFailed;

                    player.currentQuests.Add(playerQuest);
                }

                return player;
            }
            catch
            {
                return Player.CreateDefaultPlayer();
                /*If there's an error with the XML data, return a default player object,
                 * rather than an error message.*/
            }
        }


        /*Since the Player class inherits from the LivingCreature class, the base class, if
         the base class has a constructor in it, the classes that inherit from that base
         class will also have to have a constructor as well.*/
        public bool HasRequiredItemToEnterThisLocation(Location location)
        {
            if (location.itemNeededToEnter == null)
            {
                // There is no required item for this location, so return "true"

                return true;
            }

            // See if the player has the required item in their inventory

            return theInventory.Any(ii => ii.itemInfo.itemId == location.itemNeededToEnter.itemId);
            /*When using BindingLists, the .Exists() method is not available, so instead of BindingLists, the
             .Any() method is used.
             
             The .Any() method will return a "true", if any of the items in the BindingList match the criteria we
             are looking for, like what the .Exists() method does for a List property or variable.*/

            /*This demonstrates the use of LINQ. The foreach loop that was used here was
             all done in one line using LINQ. This above code does the same thing, where
             the foreach loop checks the player's inventory to see if the player has the
             required item(s) in their inventory to go to a new area through the Exists()
             function.

            => is a lambda expression.

            ii is the variable name I gave it, so LINQ will use that for each item in the
            list, much like how the foreach loop did.
             
             LINQ stands for "Language-Integrated Query." This helps to make your code
             more compact, cleaner, and easier to understand. There are several different
             ways you can work with LINQ as well.
             
             Make sure using System.Linq; is there before you use LINQ!*/
        }

        public bool HasThisQuest(Quest quest)
        {
            return currentQuests.Any(pq => pq.questInfo.questId == quest.questId);
            
            /*Another instance, where LINQ was used to simplify code, this time, public bool
             HasThisQuest.*/
        }

        public bool CompletedThisQuest(Quest quest)
        {
            foreach (PlayerQuest playerQuest in currentQuests)
            {
                if (playerQuest.questInfo.questId == quest.questId)
                {
                    return playerQuest.questCompletedOrFailed;
                }
            }

            return false;
        }

        public bool HasAllQuestCompletionItems(Quest quest)
        {
            // See if the player has all the items needed to complete the quest here
            foreach (QuestCompletionItem qci in quest.questItemWon)
            {
                if(!theInventory.Any(ii => ii.itemInfo.itemId == qci.itemWonInfo.itemId && ii.quantity >= qci.itemQty))
                /*Check each item in the player's inventory, to see if they have it, 
                and enough of it.
                    
                The expression will see, if the item exists in the player's inventory
                (ii.intemInfo.itemId == qci.itemWonInfo.itemId) and if the quantity in the
                player's inventory is greater than, or equal to, the quantity required to
                complete the quest (ii.quantity >= qci.itemQty).
                
                 If the program doesn't find an item in the list that matches both conditions, we'll
                 stop checking and return false for the method. If it gets through all the items
                 required to complete the quest, the method returns true in the end.*/
                {
                    return false;
                }
            }

            // If we got here, then the player must have all the required items, and enough of them, to complete the quest.
            return true;
        }

        /*public void RemoveQuestCompletionItems(Quest quest)
        {
            foreach (QuestCompletionItem qci in quest.questItemWon)
            {
                InventoryItem anItem = theInventory.SingleOrDefault(ii => ii.itemInfo.itemId == qci.itemWonInfo.itemId);

                if(anItem != null)
                {*/
                    /*Subtracts the quantity from the player's inventory that was needed to
                    complete the quest.*/
                    /*anItem.quantity -= qci.itemQty;
                }
            }            
        }*/

        /*This is another instance of LINQ used here. Here, the SingleOrDefault method is
         used, where it's used as another way to also get a specific item from a list.
         However, you'll need to check if it returned "null", since nothing matched the
         condition.
         
         The SingleOrDefault method only works, if you'll only ever have one item in the list
         that matches the condition. You'll need to use a different LINQ method if you want
         to get more than one item from the list.*/

        /*public void AddItemToInventory(Item itemToAdd)
        {
            InventoryItem anItem = theInventory.SingleOrDefault(ii => ii.itemInfo.itemId == itemToAdd.itemId);

            if(anItem == null)
            {*/
                /*The player didn't have the item, so add it to their inventory, with a quantity
                 of 1.*/
                /*theInventory.Add(new InventoryItem(itemToAdd, 1));
            }
            else
            {*/
                /*The player has the item in his/her inventory, so increase the quantity of
                 that item by 1.*/
                /*anItem.quantity++;
            }
        }*/

        /*Another instance of LINQ used here. This is another way the SingleOrDefault method
         can be used.*/

        public void RemoveQuestCompletionItems (Quest theQuest)
        {
            foreach(QuestCompletionItem qci in theQuest.questItemWon)
            {
                InventoryItem anItem = theInventory.SingleOrDefault(ii => ii.itemInfo.itemId == qci.itemWonInfo.itemId);
                /*This subtracts the quantity from the player's inventory that was needed
                 to complete the quest.*/

                if(anItem != null)
                {
                    RemoveItemFromInventory(anItem.itemInfo, qci.itemQty);
                }
            }
        }

        public void AddItemToInventory(Item addItem, int quantity = 1)
        {
            InventoryItem anItem = theInventory.SingleOrDefault(ii => ii.itemInfo.itemId == addItem.itemId);

            if(anItem == null)
            {
                theInventory.Add(new InventoryItem(addItem, quantity));
                /*If the player doesn't have the item, then add it to the inventory.*/
            }
            else
            /*If the player has the item in his/her inventory, then increase the 
            quantity.*/
            {
                anItem.quantity += quantity;
            }

            RaiseInventoryChangedEvent(addItem);
            /*Notify the UI that the inventory has changed*/
        }

        public void MarkQuestCompleted(Quest quest)
        {
            // Find the quest in the player's quest list
            PlayerQuest playerQuest = currentQuests.SingleOrDefault(pq => pq.questInfo.questId == quest.questId);

            if(playerQuest != null)
            {
                playerQuest.questCompletedOrFailed = true;
            }
        }

        /*Another instance of LINQ used here. This shows another way the SingleOrDefault
         method can be used.*/

        public void MoveTo(Location newLocation)
        {
            //Does the location have any required items
            if(!HasRequiredItemToEnterThisLocation(newLocation))
            {
                RaiseMessage("You must have a " + newLocation.itemNeededToEnter +
                    " to enter this location.");
                return;
            }

            // Update the player's current location
            currentLocation = newLocation;

            // Show/hide available movement buttons
            /*ButtonNorth.Visible = (newLocation.northRegion != null);
            ButtonEast.Visible = (newLocation.eastRegion != null);
            ButtonSouth.Visible = (newLocation.southRegion != null);
            ButtonWest.Visible = (newLocation.westRegion != null);*/

            // Display current location name and description
            /*rtbLocation.Text = newLocation.locationName + Environment.NewLine;
            rtbLocation.Text += newLocation.locationDesc + Environment.NewLine;*/
            currentLocation = newLocation;

            // Completely heal the player
            //thePlayer.currentHitPoints = thePlayer.maximumHitPoints;
            currentHitPoints = maximumHitPoints;

            // Update Hit Points in UI
            //lblHitPoints.Text = thePlayer.currentHitPoints.ToString();

            // Does the location have a quest?
            if (newLocation.questsToDo != null)
            {
                // See if the player already has the quest, and if they've completed it
                bool playerAlreadyHasQuest = HasThisQuest(newLocation.questsToDo);
                bool playerAlreadyCompletedQuest = CompletedThisQuest(newLocation.questsToDo);

                // See if the player already has the quest
                if (playerAlreadyHasQuest)
                {
                    // If the player has not completed the quest yet
                    if (!playerAlreadyCompletedQuest)
                    {
                        // See if the player has all the items needed to complete the quest
                        bool playerHasAllItemsToCompleteQuest = HasAllQuestCompletionItems(newLocation.questsToDo);

                        // The player has all items required to complete the quest
                        if (playerHasAllItemsToCompleteQuest)
                        {
                            // Display message
                            RaiseMessage("");
                            RaiseMessage( "You complete the '" + newLocation.questsToDo.questName + "' quest.");

                            // Remove quest items from inventory
                            RemoveQuestCompletionItems(newLocation.questsToDo);

                            // Give quest rewards
                            RaiseMessage("You receive: ");
                            RaiseMessage(newLocation.questsToDo.questExperiencePointsRewarded.ToString() + " experience points");
                            RaiseMessage( newLocation.questsToDo.questGoldRewarded.ToString() + " gold");
                            RaiseMessage(newLocation.questsToDo.itemRewarded.itemName, true);

                            AddExperiencePoints(newLocation.questsToDo.questExperiencePointsRewarded);
                            /*AddExperiencePoints method is used here*/

                            amountOfGold += newLocation.questsToDo.questGoldRewarded;

                            // Add the reward item to the player's inventory
                            AddItemToInventory(newLocation.questsToDo.itemRewarded);

                            // Mark the quest as completed
                            MarkQuestCompleted(newLocation.questsToDo);

                            // Find the quest in the player's quest list
                            /*foreach (PlayerQuest pq in currentQuests)
                            {
                                if (pq.questInfo.questId == newLocation.questsToDo.questId)
                                {
                                    // Mark it as completed
                                    pq.questCompletedOrFailed = true;

                                    break;
                                }
                            }*/
                        }
                    }
                }

                else
                {
                    // The player does not already have the quest

                    // Display the messages
                    RaiseMessage("You receive the " + newLocation.questsToDo.questName + " quest.");
                    RaiseMessage(newLocation.questsToDo.questDesc);
                    RaiseMessage("To complete it, return with:");
                    foreach (QuestCompletionItem qci in newLocation.questsToDo.questItemWon)
                    {
                        if (qci.itemQty == 1)
                        {
                            RaiseMessage(qci.itemQty.ToString() + " " + qci.itemWonInfo.itemName);
                        }
                        else
                        {
                            RaiseMessage(qci.itemQty.ToString() + " " + qci.itemWonInfo.namePlural);
                        }
                    }
                    RaiseMessage("");

                    // Add the quest to the player's quest list
                    currentQuests.Add(new PlayerQuest(newLocation.questsToDo));
                }
            }

            // Does the location have a monster?
            if (newLocation.monstersThatLiveHere != null)
            {
                RaiseMessage("You see a " + newLocation.monstersThatLiveHere.monsterName);

                // Make a new monster, using the values from the standard monster in the World.Monster list
                Monster standardMonster = World.MonsterByID(newLocation.monstersThatLiveHere.monsterId);

                theCurrentMonster = new Monster(standardMonster.currentHitPoints, standardMonster.maximumHitPoints,
                    standardMonster.monsterId, standardMonster.monsterName, standardMonster.monsterDamage,
                    standardMonster.monsterExperiencePointsRewarded, standardMonster.monsterGoldRewarded);

                foreach (LootItem lootItem in standardMonster.lootTable)
                {
                    theCurrentMonster.lootTable.Add(lootItem);
                }

                /*ComboWeapons.Visible = true;
                ComboPotions.Visible = true;
                ButtonUseWeapon.Visible = true;
                ButtonUsePotion.Visible = true;*/

            }
            else
            {
                theCurrentMonster = null;
            }

            //Refresh player's weapons combobox
            // UpdateWeaponListInUI();

            //Refresh player's potions combobox
            //UpdatePotionListInUI();

            //ScrollToBottomOfMessages();
        }       

        public void UseWeapon(Weapon weapon)
        {
            //Get the currently selected weapon from the comboWeapons ComboBox
            //Weapon currentWeapon = (Weapon)ComboWeapons.SelectedItem;

            //Determine the amount of damage to do to the monster
            /*int damageToMonster = RandomNumberGenerator.NumberBetween(currentWeapon.minimumDamage,
                currentWeapon.maximumDamage);*/
            int damageToMonster = RandomNumberGenerator.NumberBetween(weapon.minimumDamage,
            weapon.maximumDamage);
            /*Turns out all you needed to do was the change currentWeapon to weapon. The method UseWeapon has
             weapon as a parameter, so you needed to change the naming of it, so that was why the old int
             damageToMonster was giving you a NullReferenceException exception.*/

            //Apply the damage to the monster's current hit points
            theCurrentMonster.currentHitPoints -= damageToMonster;

            //Display message
            RaiseMessage("You hit the " + theCurrentMonster.monsterName +
                " for " + damageToMonster.ToString() + "points.");

            //Check if the monster is dead
            if (theCurrentMonster.currentHitPoints <= 0)
            {
                //Monster is dead
                RaiseMessage("");
                RaiseMessage("You defeated the " + theCurrentMonster.monsterName + "!");

                //Reward experience points for killing the monster
                AddExperiencePoints(theCurrentMonster.monsterExperiencePointsRewarded);
                /*AddExperiencePoints method is used here*/

                RaiseMessage(" You receive " + theCurrentMonster.monsterGoldRewarded.ToString() + " experience points!");

                //Reward gold for killing the monster
                amountOfGold += theCurrentMonster.monsterGoldRewarded;
                RaiseMessage(" You receive " + theCurrentMonster.monsterGoldRewarded.ToString() + "gold!");

                //Get random items for killing the monster
                List<InventoryItem> itemsLooted = new List<InventoryItem>();

                //Add items to the lootedItems list, comparing a random number to the drop percentage
                foreach (LootItem lootItem in theCurrentMonster.lootTable)
                {
                    if (RandomNumberGenerator.NumberBetween(1, 100) <= lootItem.dropProbability)
                    {
                        itemsLooted.Add(new InventoryItem(lootItem.itemLootedInfo, 1));
                    }
                }

                //If no items were randomly selected, then add the default loot item(s)
                if (itemsLooted.Count == 0)
                {
                    foreach (LootItem lootItem in theCurrentMonster.lootTable)
                    {
                        if (lootItem.defaultItem)
                        {
                            itemsLooted.Add(new InventoryItem(lootItem.itemLootedInfo, 1));
                        }
                    }
                }

                //Add the looted items to the player's inventory
                foreach (InventoryItem inventoryItem in itemsLooted)
                {
                    AddItemToInventory(inventoryItem.itemInfo);

                    if (inventoryItem.quantity == 1)
                    {
                        RaiseMessage("You loot " + inventoryItem.quantity.ToString() +
                            " " + inventoryItem.itemInfo.itemName);
                    }
                    else
                    {
                        RaiseMessage("You loot " + inventoryItem.quantity.ToString() +
                            " " + inventoryItem.itemInfo.namePlural);
                    }
                }

                //Refresh player information and inventory controls
                /*lblHitPoints.Text = thePlayer.currentHitPoints.ToString();
                lblGold.Text = thePlayer.amountOfGold.ToString();
                lblExperience.Text = thePlayer.experiencePoints.ToString();
                lblLevel.Text = thePlayer.playerLevel.ToString();*/

                //UpdateWeaponListInUI();
                //UpdatePotionListInUI();

                //Add a blank line to the messages box, just for appearance.
                RaiseMessage("");

                //Move player to the current locatio (to heal player and create a new monster to fight)
                MoveTo(currentLocation);
            }
            else
            {
                //Monster is still alive

                //Determine the amount of damage the monster does to the player
                int playerTakesDamage = RandomNumberGenerator.NumberBetween(0, theCurrentMonster.monsterDamage);

                //Display message
                RaiseMessage("The " + theCurrentMonster.monsterName + " did " + playerTakesDamage.ToString() +
                    " to the player!");

                //Subtract damage from player
                currentHitPoints -= playerTakesDamage;

                //Refresh player data in UI
                //lblHitPoints.Text = thePlayer.currentHitPoints.ToString();

                if (currentHitPoints <= 0)
                {
                    //Display message
                    RaiseMessage("The " + theCurrentMonster.monsterDamage + "killed you.");

                    //Move player to "Home"
                    //MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                    moveBackToHome();
                }
            }
        }

        public void usePotion(HealingPotion potion)
        {
            //Get the currently selected potion from the combobox
            //HealingPotion typePotion = (HealingPotion)ComboPotions.SelectedItem;

            //Add healing amount to the player's current hit points
            currentHitPoints = (currentHitPoints + potion.healingAmount);

            //currentHitPoints can't exceed the player's maximumHitPoints
            if (currentHitPoints > maximumHitPoints)
            {
                currentHitPoints = maximumHitPoints;
            }

            //Remove the potion from the player's inventory
            /*foreach(InventoryItem itemInventory in thePlayer.theInventory)
            {
                if(itemInventory.itemInfo.itemId == typePotion.itemId)
                {
                    itemInventory.quantity--;
                    break;
                }
            }*/

            RemoveItemFromInventory(potion, 1);
            /*This removes the potion from the player's inventory.
             
             Now, we aren't directly changing the player's inventory from the UI.
             It will now have to pass through the function created in the Player
             object,which will send up a notification that the player's inventory
             has changed.*/

            //Display message
            RaiseMessage("You drink a " + potion.itemName);

            //Monster gets their return to attack

            //Determine the amount of damage the monster does to the player
            int damageToPlayer = RandomNumberGenerator.NumberBetween(0, theCurrentMonster.monsterDamage);

            //Display message
            RaiseMessage("The " + theCurrentMonster.monsterDamage + " did " + damageToPlayer.ToString()
                + " points of damage.");

            //Subtract damage from player
            currentHitPoints -= damageToPlayer;

            if (currentHitPoints <= 0)
            {
                //Display message
                RaiseMessage("The " + theCurrentMonster.monsterName + " killed you.");

                //Move player to "Home"
                //MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
                moveBackToHome();
            }

            //Refresh player data in UI
            //lblHitPoints.Text = thePlayer.currentHitPoints.ToString();
            //UpdatePotionListInUI();
        }

        private void moveBackToHome()
        {
            MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
        }

        public void GoNorth()
        {
            if (currentLocation.northRegion != null)
            {
                MoveTo(currentLocation.northRegion);
            }
        }

        public void GoEast()
        {
            if (currentLocation.eastRegion != null)
            {
                MoveTo(currentLocation.eastRegion);
            }
        }

        public void GoSouth()
        {
            if (currentLocation.southRegion != null)
            {
                MoveTo(currentLocation.southRegion);
            }
        }

        public void GoWest()
        {
            if (currentLocation.westRegion != null)
            {
                MoveTo(currentLocation.westRegion);
            }
        }

        public string ToXmlString()
        {
            XmlDocument playerData = new XmlDocument();

            /*The above object lets us add nodes in a safe way, so we can't break the most
             common XML formatting rules.*/

            XmlNode player = playerData.CreateElement("Player");
            playerData.AppendChild(player);
            /*Creates the top-level XML node*/

            XmlNode stats = playerData.CreateElement("Stats");
            player.AppendChild(stats);
            /*Create the "Stats" child node to hold the other player statistics nodes*/

            XmlNode CurrentHitPoints = playerData.CreateElement("CurrentHitPoints");
            CurrentHitPoints.AppendChild(playerData.CreateTextNode(this.currentHitPoints.ToString()));
            stats.AppendChild(CurrentHitPoints);

            XmlNode MaximumHitPoints = playerData.CreateElement("MaximumHitPoints");
            MaximumHitPoints.AppendChild(playerData.CreateTextNode(this.maximumHitPoints.ToString()));
            stats.AppendChild(MaximumHitPoints);

            XmlNode AmountOfGold = playerData.CreateElement("Gold");
            AmountOfGold.AppendChild(playerData.CreateTextNode(this.amountOfGold.ToString()));
            stats.AppendChild(AmountOfGold);

            XmlNode ExperiencePoints = playerData.CreateElement("ExperiencePoints");
            ExperiencePoints.AppendChild(playerData.CreateTextNode(this.experiencePoints.ToString()));
            stats.AppendChild(ExperiencePoints);

            XmlNode CurrentLocation = playerData.CreateElement("CurrentLocation");
            CurrentLocation.AppendChild(playerData.CreateTextNode(this.currentLocation.locationId.ToString()));
            stats.AppendChild(CurrentLocation);

            /*These five nodes above are the child nodes for the "Stats" node. There is no
            node to store the Level value, as Level is always calculated from
            ExperiencePoints.*/

            if(currentWeapon != null)
            {
                XmlNode CurrentWeapon = playerData.CreateElement("CurrentWeapon");
                CurrentWeapon.AppendChild(playerData.CreateTextNode(this.currentWeapon.itemId.ToString()));
                stats.AppendChild(CurrentWeapon);
            }
            /*This saves the ID of the player's currently selected weapon (if they have one).*/

            XmlNode inventoryItems = playerData.CreateElement("InventoryItems");
            player.AppendChild(inventoryItems);

            /*The "InventoryItems" child node holds each "InventoryItem" node*/

            foreach(InventoryItem items in this.theInventory)
            {
                XmlNode inventoryItem = playerData.CreateElement("InventoryItem");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = items.itemInfo.itemId.ToString();
                inventoryItem.Attributes.Append(idAttribute);

                XmlAttribute quantityAttribute = playerData.CreateAttribute("Quantity");
                quantityAttribute.Value = items.quantity.ToString();
                inventoryItem.Attributes.Append(quantityAttribute);

                inventoryItems.AppendChild(inventoryItem);
            }

            /*This foreach loop creates a "InventoryItem" node for each item in the player's
             inventory*/
            
            XmlNode playerQuests = playerData.CreateAttribute("PlayerQuests");
            player.AppendChild(playerQuests);

            /*Creates the "PlayerQuests" node to hold each "PlayerQuest" node*/

            foreach(PlayerQuest quests in this.currentQuests)
            {
                XmlNode playerQuest = playerData.CreateElement("PlayerQuest");

                XmlAttribute idAttribute = playerData.CreateAttribute("ID");
                idAttribute.Value = quests.questInfo.questId.ToString();
                playerQuest.Attributes.Append(idAttribute);

                XmlAttribute questCompletedAttribute = playerData.CreateAttribute("IsCompleted");//QuestCompleted
                questCompletedAttribute.Value = quests.questCompletedOrFailed.ToString();
                playerQuest.Attributes.Append(questCompletedAttribute);

                playerQuests.AppendChild(playerQuest);
            }

            /*Creates a "PlayerQuest" node for each active quest the player has*/

            return playerData.InnerXml;

            /*The XML document, as a string, so we can save the data to a disk*/
        }

        public static Player CreatePlayerFromDatabase(int currentHitPoints, int maximumHitPoints, int goldAmount, int expPoints, int idOfCurrentLocation)
        {
            Player playerOne = new Player(currentHitPoints, maximumHitPoints, goldAmount, expPoints);

            playerOne.MoveTo(World.LocationByID(idOfCurrentLocation));

            return playerOne;
        }
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Engine;
using System.IO;
using System.Xml;

namespace Pilot
{
    public partial class Pilot : Form
    /*The code below has a number of functions that handle the player's
     actions in battle, including a number of if and foreach loops.*/
    {
        private Player thePlayer;
        /*You're pulling from the Player class at Engine. It's set to private, since we
         don't need anything outside of this screen to use the variable.
         
        The datatype is "Player", since we want to store a player object in it.*/        

        private const string PLAYER_DATA_FILE_NAME = "PlayerData.xml";
        /*This object is constant, because it doesn't change. Const is also a class-level
         variable.*/

        public Pilot()
        {
            InitializeComponent();

            thePlayer = PlayerDataMapper.CreateFromDatabase();

            if(thePlayer == null)
            {
                if(File.Exists(PLAYER_DATA_FILE_NAME))
                {
                    thePlayer = Player.CreatePlayerFromXmlString(File.ReadAllText(PLAYER_DATA_FILE_NAME));
                }
                else
                {
                    thePlayer = Player.CreateDefaultPlayer();
                }
            }
            /*This will try to load the player from the PlayerDataMapper class. If there's an error,
             or if the SavedGameTable is empty, PlayerDataMapper.CreateFromDatabase() will return a
             null. We check the XML file for the saved game. If the XML file isn't seen, then we will
             create a new player.*/

            lblHitPoints.DataBindings.Add("Text", thePlayer, "CurrentHitPoints");
            /*The databinding "subscription" here connects to the "Text" property of
             lblHitPoints to the "CurrentHitPoints" property of the "thePlayer" object.
             
             The databindings are added after the object is created, as the object needs to be
             instantiated before you can bind to it.*/
            
            lblGold.DataBindings.Add("Text", thePlayer, "Gold");
            lblExperience.DataBindings.Add("Text", thePlayer, "ExperiencePoints");
            lblLevel.DataBindings.Add("Text", thePlayer, "Level");

            dgvInventory.RowHeadersVisible = false;
            dgvInventory.AutoGenerateColumns = false;
            /*We set AutoGenerateColumns to "false", so we can manually configure the columns. Otherwise, the
             datagrid would create a column for each property of InventoryItem, and we don't want the binding to
             automatically generate the data grid's columns.*/

            dgvInventory.DataSource = thePlayer.theInventory;

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            /*We add the new DataGridViewTextBoxColumn columns, since we want to display text in them.*/
            {
                HeaderText = "Item Name",
                Width = 197,
                DataPropertyName = "Description"//Don't mess with this
                /*The DataPropertyName value is the property of InventoryItem that we want to display in
                 InventoryItem.*/
            });

            dgvInventory.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Item Quantity",
                DataPropertyName = "Quantity"//Don't mess with this
            });
            /*The stuff done with dgvInventory here goes to configure the dgvInventory datagrid view, as well as
             * the columns of the datagridview.*/

            dgvQuests.RowHeadersVisible = false;
            dgvQuests.AutoGenerateColumns = false;

            dgvQuests.DataSource = thePlayer.currentQuests;

            dgvQuests.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Quest Name",
                Width = 197,
                DataPropertyName = "NameOfQuest"//Don't mess with this
            });

            dgvQuests.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Is Quest Done?",
                DataPropertyName = "IsCompleted"//Don't mess with this
            });

            ComboWeapons.ValueMember = "Id";//ID
            ComboWeapons.DisplayMember = "Name";//WeaponName
            ComboWeapons.DataSource = thePlayer.Weapons;

            /*I think the reason this works is because the constructor for the Weapons class has the ItemId first,
             then itemName second, same deal with the HealingPotion class.*/

            if (thePlayer.currentWeapon != null)
            {
                ComboWeapons.SelectedItem = thePlayer.currentWeapon;
            }

            ComboWeapons.SelectedIndexChanged += ComboWeapons_SelectedIndexChanged;
            
            ComboPotions.ValueMember = "Id";//ID
            ComboPotions.DisplayMember = "Name";//PotionName
            ComboPotions.DataSource = thePlayer.Potions;


            thePlayer.PropertyChanged += PlayerWhenPropertyChanged;

            /*The lines here start off as saying to the program to use the Weapons and
             Potions properties as the data sources for the comboboxes. The
             DisplayMember for the ComboBox says what property to display, and the
             ValueMember says what property to use as the value when the program
             checks for the currently selected item.
             
             SelectedItem is also set to the player's CurrentWeapons, if the player
             has one.*/

            thePlayer.onMessage += displayMessage;
            /*The above line watches for events from the player.cs class.*/

            thePlayer.MoveTo(thePlayer.currentLocation);

            //UpdatePlayerStats();
            /*The UpdatePlayerStats() function is no longer needed, since the databinding does
             what the UpdatePlayerStats() does.*/

            /*When the game starts, it will look for the PlayerData.xml file.
             
             If the file exists, it will read it and create the Player object with the
             values in the file. If it doesn't find the file, it will create the default
             player object.*/

            //Location location = new Location(1, "Home", "This is your house.");
            /*The initialisation above passes the following data in the parameters 
            to the location constructor in the location class.*/

            //thePlayer = new Player(20, 20, 100, 0);
            /*These five parameters are being passed to the constructor in the Player class.*/

            //MoveTo(World.LocationByID(World.LOCATION_ID_HOME));
            /*The above line moves the player to their home.*/

            //thePlayer.theInventory.Add(new InventoryItem(World.ItemByID(World.ITEM_ID_RUSTY_SWORD), 1));
            /*Gives the player a starting weapon for the beginning of the player's journey.*/

            //UpdatePlayerStats();
        }

        private void displayMessage(object sender, MessageEventArgs messageEventArgs)
        {
            rtbMessages.Text += messageEventArgs.aMessage + Environment.NewLine;

            if (messageEventArgs.addAnExtraNewLine)
            {
                rtbMessages.Text += Environment.NewLine;
            }

            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void PlayerWhenPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs)
        /*This function updates the combobox data when the player's inventory changes.*/
        {
            if (propertyChangedEventArgs.PropertyName == "Weapons")
            /*propertyChangedEventArgs.PropertyName tells us which property was
             changed on the Player object. This value comes from the
             Player.RaiseInventoryChangedEvent function, where it says
             WhenPropertyChanged("Weapons") or WhenPropertyChanged("Potions").*/
            {
                ComboWeapons.DataSource = thePlayer.Weapons;
                /*This rebinds the combobox to the weapons DataSource property.*/

                if (!thePlayer.Weapons.Any())
                {
                    ComboWeapons.Visible = false;
                    ButtonUseWeapon.Visible = false;
                }
                /*This if statement checks if the lists are empty, with the Any()
                 method telling us if there are any items in this list, "true" if
                 there are, "false" if none.*/
            }

            if (propertyChangedEventArgs.PropertyName == "Potions")
            {
                ComboPotions.DataSource = thePlayer.Potions;

                if (!thePlayer.Potions.Any())
                {
                    ComboPotions.Visible = false;
                    ButtonUsePotion.Visible = false;
                }
            }

            if (propertyChangedEventArgs.PropertyName == "CurrentLocation")
            {
                ButtonNorth.Visible = (thePlayer.currentLocation.northRegion != null);
                ButtonEast.Visible = (thePlayer.currentLocation.eastRegion != null);
                ButtonSouth.Visible = (thePlayer.currentLocation.southRegion != null);
                ButtonWest.Visible = (thePlayer.currentLocation.westRegion != null);
                /*These 4 lines will show.hide available movement buttons*/

                rtbLocation.Text = thePlayer.currentLocation.locationName + Environment.NewLine;
                rtbLocation.Text += thePlayer.currentLocation.locationDesc + Environment.NewLine;
                /*These 2 lines display current location name and description*/

                ButtonTrade.Visible = (thePlayer.currentLocation.vendorLocatedHere != null);
                /*This shows the "Trade" button only when there is a vendor at the location.*/


                if (thePlayer.currentLocation.monstersThatLiveHere == null)
                {
                    ComboWeapons.Visible = false;
                    ComboPotions.Visible = false;
                    ButtonUseWeapon.Visible = false;
                    ButtonUsePotion.Visible = false;
                }
                else
                {
                    ComboWeapons.Visible = thePlayer.Weapons.Any();
                    ComboPotions.Visible = thePlayer.Potions.Any();
                    ButtonUseWeapon.Visible = thePlayer.Weapons.Any();
                    ButtonUsePotion.Visible = thePlayer.Potions.Any();

                    /*These 4 lines use the Weapons property and the Potions property to
                     show/hide the comboboxes and buttons when the player moves to a new
                     location and encounters a monster.*/
                }
            }
        }

        private void Pilot_Load(object sender, EventArgs e)
        {

        }

        private void ButtonNorth_Click(object sender, EventArgs e)
        {
            //MoveTo(thePlayer.currentLocation.northRegion);
            thePlayer.GoNorth();
            /*This calls the GoNorth() function in the Player.cs class. The other three things do the
             same thing.*/
        }

        private void ButtonEast_Click(object sender, EventArgs e)
        {
            //MoveTo(thePlayer.currentLocation.eastRegion);
            thePlayer.GoEast();

        }

        private void ButtonSouth_Click(object sender, EventArgs e)
        {
            //MoveTo(thePlayer.currentLocation.southRegion);
            thePlayer.GoSouth();            
        }

        private void ButtonWest_Click(object sender, EventArgs e)
        {
            //MoveTo(thePlayer.currentLocation.westRegion);
            thePlayer.GoWest();
        }

        private void ButtonUseWeapon_Click(object sender, EventArgs e)
        {
            Weapon currentWeapon = (Weapon)ComboWeapons.SelectedItem;
            /*This gets the currently selected weapon from the ComboWeapons Combobox. 
             
            The new Player functions accept the weapon (or potion, for the other) as a parameter, instead of
            reading it from the combobox, because the Player class cannot read the combobox controls of
            Pilot.cs.
             */

            thePlayer.UseWeapon(currentWeapon);
            //thePlayer.RemoveItemFromInventory(potion, 1);
        }

        private void ButtonUsePotion_Click(object sender, EventArgs e)
        {
            HealingPotion potion = (HealingPotion)ComboPotions.SelectedItem;
            /*This gets the currently selected potion from the ComboPotions Combobox.*/

            thePlayer.usePotion(potion);
            //thePlayer.RemoveItemFromInventory(potion, 1);
        }

        private void dgvInventory_CellContentClick(object sender, EventArgs e)
        {

        }

        private void ScrollToBottomOfMessages()
        {
            rtbMessages.SelectionStart = rtbMessages.Text.Length;
            rtbMessages.ScrollToCaret();
        }

        private void Pilot_FormClosing(object sender, FormClosingEventArgs e)
        {
            File.WriteAllText(PLAYER_DATA_FILE_NAME, thePlayer.ToXmlString());

            PlayerDataMapper.SaveToDatabase(thePlayer);
            /*With the above line of code added in, when the player exits the game, it will save the
             data to the XML file and the database.*/
        }
        /*When the program closes, using this method, it will write the player's data,
         in our XML format, to the PlayerData.xml file.*/

        private void ComboWeapons_SelectedIndexChanged(object sender, EventArgs e)
        {
            thePlayer.currentWeapon = (Weapon)ComboWeapons.SelectedItem;
        }
        /*(Weapon) is in front of ComboWeapons.SelectedItem because a dropdown can hold different types of
         objects, and ComboWeapons_SelectedIndexChanged could be any datatype. By adding (Weapon) in front of it,
         we are casting it to the Weapon datatype.*/

        private void ButtonTrade_Click(object sender, EventArgs e)
        {
            PilotTradingScreen tradingScreen = new PilotTradingScreen();
            tradingScreen.StartPosition = FormStartPosition.CenterParent;
            tradingScreen.theCurrentPlayer = thePlayer;
            /*By instantiating and creating the form object, the theCurrentPlayer property in
             PilotTradingScreen.cs will be able to work with the thePlayer object*/

            tradingScreen.ShowDialog(this);
        }
    }
}
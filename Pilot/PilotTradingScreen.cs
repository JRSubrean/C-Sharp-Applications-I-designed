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

namespace Pilot
{
    public partial class PilotTradingScreen : Form
    {
        public Player theCurrentPlayer { get; set; }
        public PilotTradingScreen()
        {
            InitializeComponent();
        }

        private void ButtonClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public PilotTradingScreen(Player currentPlayer)
        {
            theCurrentPlayer = currentPlayer;

            InitializeComponent();

            DataGridViewCellStyle rightAlignedCellStyle = new DataGridViewCellStyle();
            rightAlignedCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            /*Styles to display numeric column values. These objects can be used to define
             special formatting for a data grid's columns. The above code also creates a style
             to align the text to the right, instead of the default alignment to the left. It
             will be used for the numeric columns, quantity and price.*/

            dgvMyItems.RowHeadersVisible = false;
            dgvMyItems.AutoGenerateColumns = false;
            /*Populates the datagrid for the player's inventory*/

            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "ItemID",
                Visible = false
            });
            /*This hidden column holds the item ID, so we know which item to sell. The ItemID
             columns are hidden in this way, by setting their "Visible" status to false. While
             we need to include the ItemID column so that we know which item to buy or sell,
             we don't want to display it, as that number won't mean anything much to the player.*/

            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Name",
                Width = 100,
                DataPropertyName = "Description"
            });

            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Qty",
                Width = 30,
                DefaultCellStyle = rightAlignedCellStyle,
                DataPropertyName = "Quantity"
            });

            dgvMyItems.Columns.Add(new DataGridViewTextBoxColumn
            {
                HeaderText = "Price",
                Width = 35,
                DefaultCellStyle = rightAlignedCellStyle,
                DataPropertyName = "Price"
            });

            dgvMyItems.Columns.Add(new DataGridViewButtonColumn
            {
                Text = "Sell 1",
                UseColumnTextForButtonValue = true,
                Width = 50,
                DataPropertyName = "ItemID"
            });

            dgvVendorItems.DataSource = theCurrentPlayer.currentLocation.vendorLocatedHere.theInventory;
            /*The above line of code binds the vendor's inventory to the datagridview*/

            dgvMyItems.CellClick += dgvMyItems_CellClick;
            /*When the user clicks on a row, call this function.*/

            dgvVendorItems.CellClick += dgvVendorItems_CellClick;
            /*When the user clicks on a row, call this function.*/
        }

        private void dgvMyItems_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            /*The first column of a datagridview has a ColumnIndex = 0. This is known as a
             "zero-based" array/collection/list, where it starts by counting with 0. The 5th
             column (ColumnIndex = 4) is the column with the button, and so, if the player
             clicked the button column, we will sell an item from that row.*/
            if(e.ColumnIndex == 4)
            {
                var idOfTheItem = dgvMyItems.Rows[e.RowIndex].Cells[0].Value;
                /*This gets the ID value of the item, from the hidden 1st column. The 1st
                column is ColumnIndex = 0.*/

                Item itemSold = World.ItemByID(Convert.ToInt32(idOfTheItem));
                /*The above line gets the Item object for the selected item row.*/

                if(itemSold.itemPrice == World.UNSELLABLE_ITEM_PRICE)
                {
                    MessageBox.Show("You can't sell the " + itemSold.itemName);
                }
                else
                {
                    theCurrentPlayer.RemoveItemFromInventory(itemSold);
                    /*Remove one of that item(s) from the player's inventory*/

                    theCurrentPlayer.amountOfGold += itemSold.itemPrice;
                    /*Give the player the gold for the item that was sold.*/
                }
            }
        }

        private void dgvVendorItems_CellClick(object sender, DataGridViewCellEventArgs e)
        /*The e parameter gives us info about what cell (row/column) was clicked. It's an
         automatic parameter that is created and passed when the CellClick event happens.*/
        {
            /*The 4th column (ColumnIndex = 3) has the "Buy 1" button.*/
            if (e.ColumnIndex == 3)
            {
                var idOfTheItem = dgvVendorItems.Rows[e.RowIndex].Cells[0].Value;
                /*This gets the ID value of the item from the hidden 1st column.*/

                Item itemBought = World.ItemByID(Convert.ToInt32(idOfTheItem));
                /*Get the Item object for the selected item row*/

                if (theCurrentPlayer.amountOfGold >= itemBought.itemPrice)
                /*This if statement checks if the player has enough gold to buy the item.*/
                {
                    theCurrentPlayer.AddItemToInventory(itemBought);
                    /*Add item(s) to the player's inventory*/

                    theCurrentPlayer.amountOfGold -= itemBought.itemPrice;
                    /*Subtract the player's gold by the price of the item the player bought*/
                }
                else
                {
                    MessageBox.Show("You don't have enough gold to buy the " + itemBought.itemName);
                }
            }
        }
    }
}

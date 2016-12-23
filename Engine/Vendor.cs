using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class Vendor : INotifyPropertyChanged
    {
        public string vendorName { get; set; }
        //public BindingList<InventoryItem> theInventory { get; private set; }
        public BindingList<InventoryItem> theInventory { get; set; }

        public Vendor(string nameOfVendor)
        {
            vendorName = nameOfVendor;
            theInventory = new BindingList<InventoryItem>();
        }

        public void addAnItemToInventory(Item addAnItem, int quantity = 1)
        {
            InventoryItem aItem = theInventory.SingleOrDefault(ii => ii.itemInfo.itemId == addAnItem.itemId);

            if(aItem == null)
            {
                theInventory.Add(new InventoryItem(addAnItem, quantity));
                //The player doesn't have the item, so add it to his/her inventory
            }
            else
            {
                aItem.quantity += quantity;
                //The player has the item in his/her inventory, so increase the quantity by number of item bought
            }

            WhenPropertyChanged("Inventory");
        }

        public void removeAnItemFromInventory(Item removeAnItem, int quantity = 1)
        {
            InventoryItem aItem = theInventory.SingleOrDefault(ii => ii.itemInfo.itemId == removeAnItem.itemId);

            if(aItem == null)
            {
                /*The item is not in the player's inventory, so do nothing. An error could be raised here.*/
            }
            else
            {
                aItem.quantity -= quantity;
                /*The item is present in the player's inventory, so decrease the quantity*/

                if(aItem.quantity < 0)
                /*We shouldn't allow negative quantities, so we have this code.
                We could raise an error for this one.*/
                {
                    aItem.quantity = 0;
                }

                if(aItem.quantity == 0)
                /*If the item quantity is zero, then remove the item from the player's inventory list*/
                {
                    theInventory.Remove(aItem);
                }

                WhenPropertyChanged("Inventory");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void WhenPropertyChanged(string nameOfItem)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(nameOfItem));
            }
        }
    }
}

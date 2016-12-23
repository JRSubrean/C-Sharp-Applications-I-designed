using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Engine
{
    public class InventoryItem : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        protected void WhenPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        private Item itemDetails;

        private int itemQuantity;
        public Item itemInfo
        {
            get { return itemDetails; }
            set
            {
                itemDetails = value;
                WhenPropertyChanged("Details");//Don't mess with this
            }
        }
        public int quantity
        {
            get { return itemQuantity; }
            set
            {
                itemQuantity = value;
                WhenPropertyChanged("Quantity");//Don't mess with this
                WhenPropertyChanged("Description");//Don't mess with this
            }
        }

        public string Description
        {
            get { return quantity > 1 ? itemInfo.namePlural : itemInfo.itemName; }
        }
        /*We want the datagridview to display a property of InventoryItem's details property.*/

        public InventoryItem(Item details, int qty)
        {
            itemInfo = details;
            quantity = qty;
        }

        public int IdOfItem
        {
            get { return itemDetails.itemId; }
        }

        public int priceOfItem
        {
            get { return itemDetails.itemPrice; }
        }
        /*IdOfItem and priceOfItem */
    }
}

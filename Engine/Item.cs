using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Item
    /*This is the base class for the HealingPotion class and the Weapon class. Both of
     those classes inherit from this class.*/
    {
        public int itemId { get; set; }
        public string itemName { get; set; }
        public string namePlural { get; set; }
        public int itemQty { get; set; }

        public int itemPrice { get; set; }

        public Item(int iId, string iName, string nPlural, int iPrice)
        {
            itemId = iId;
            itemName = iName;
            namePlural = nPlural;
            itemPrice = iPrice;
        }
    }
}

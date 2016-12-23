using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class HealingPotion : Item
    {
        public int healingAmount { get; set; }

        public HealingPotion(int iId, string iName, string nPlural, int hPrice, int healAmount) : base(iId, iName, nPlural, hPrice)
        /*When a constructor you've created is inheriting from another constructor, such as
         how the HealingPotion constructor in the HealingPotion class is inheriting from the 
         Item constructor in the Item class, then the parameters have to have the same name as
         the parameters in the parent constructor.
         
         In this way, the HealingPotion constructor derives from the base constructor of the
         base class it inherits from.*/
        {
            healingAmount = healAmount;
        }
    }
}

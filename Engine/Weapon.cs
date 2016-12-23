using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Weapon : Item
    {
        public int minimumDamage { get; set; }
        public int maximumDamage { get; set; }

        public Weapon(int iId, string iName, string nPlural, int wPrice, int minDamage, int maxDamage) : base(iId, iName, nPlural, wPrice)
        {
            minimumDamage = minDamage;
            maximumDamage = maxDamage;
        }
    }
}

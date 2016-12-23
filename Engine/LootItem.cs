using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class LootItem
    {
        public Item itemLootedInfo { get; set; }
        public int dropProbability { get; set; }
        public bool defaultItem { get; set; }

        public LootItem(Item lDetails, int droppedPercent, bool isItemDefault)
        {
            itemLootedInfo = lDetails;
            dropProbability = droppedPercent;
            defaultItem = isItemDefault;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class QuestCompletionItem
    {
        public Item itemWonInfo { get; set; }
        public int itemQty { get; set; }

        public QuestCompletionItem(Item iDetails, int iQty)
        {
            itemWonInfo = iDetails;
            itemQty = iQty;
        }
    }
}

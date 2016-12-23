using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Quest
    {
        public int questId { get; set; }
        public string questName { get; set; }
        public string questDesc { get; set; }
        public int questExperiencePointsRewarded { get; set; }
        public int questGoldRewarded { get; set; }
        public Item itemRewarded { get; set; }
        public List<QuestCompletionItem> questItemWon { get; set; }

        public Quest(int qId, string qName, string qDesc, int qExp, int qGold)
        /*This is a constructor, quest.*/
        {
            questId = qId;
            questName = qName;
            questDesc = qDesc;
            questExperiencePointsRewarded = qExp;
            questGoldRewarded = qGold;

            questItemWon = new List<QuestCompletionItem>();
        }
    }
}
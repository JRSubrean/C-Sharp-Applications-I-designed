using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Monster : LivingCreature
    {
        public int monsterId { get; set; }
        public string monsterName { get; set; }
        public int monsterDamage { get; set; }
        public int monsterExperiencePointsRewarded { get; set; }
        public int monsterGoldRewarded { get; set; }
        public List<LootItem> lootTable { get; set; }

        public Monster(int currentHP, int maxHP, int mId, string mName, int mDamage, int mExp, int mGold) : base(currentHP, maxHP)
        {
            monsterId = mId;
            monsterName = mName;
            monsterDamage = mDamage;
            monsterExperiencePointsRewarded = mExp;
            monsterGoldRewarded = mGold;
            lootTable = new List<LootItem>();
        }
    }
}

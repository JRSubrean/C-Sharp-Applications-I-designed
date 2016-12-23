using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
    public class Location
    {
        public int locationId { get; set; }
        public string locationName { get; set; }
        public string locationDesc { get; set; }
        public Item itemNeededToEnter { get; set; }
        public Quest questsToDo { get; set; }
        public Monster monstersThatLiveHere { get; set; }
        public Location northRegion { get; set; }
        public Location southRegion { get; set; }
        public Location eastRegion { get; set; }
        public Location westRegion { get; set; }
        /*You can also use your classes as datatypes.*/

        public Vendor vendorLocatedHere { get; set; }

        public Location (int id, string name, string description, 
            Item itemRequirementToEnter = null, Quest quests = null, 
            Monster monsterEncounters = null)
        /*This is a class constructor, Location.
         
         The Item, Quest, and Monster parameters have null at the end of them, as some
         locations won't have an item required to enter them, a quest available to them, or
         a monster living there. This allows us to call the Location constructor without
         passing these 3 values.*/
        {
            locationId = id;
            locationName = name;
            locationDesc = description;
            itemNeededToEnter = itemRequirementToEnter;
            questsToDo = quests;
            monstersThatLiveHere = monsterEncounters;
        }
    }
}

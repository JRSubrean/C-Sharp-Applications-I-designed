using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine
{
        public static class World
        /*The purpose of the World class is to have one place to hold
         everything that exists in the game world.*/
        {
            public static readonly List<Item> Items = new List<Item>();
            public static readonly List<Monster> Monsters = new List<Monster>();
            public static readonly List<Quest> Quests = new List<Quest>();
            public static readonly List<Location> Locations = new List<Location>();
            /*These are static list variables.*/

            public const int ITEM_ID_RUSTY_SWORD = 1;
            public const int ITEM_ID_RAT_TAIL = 2;
            public const int ITEM_ID_PIECE_OF_FUR = 3;
            public const int ITEM_ID_SNAKE_FANG = 4;
            public const int ITEM_ID_SNAKESKIN = 5;
            public const int ITEM_ID_CLUB = 6;
            public const int ITEM_ID_HEALING_POTION = 7;
            public const int ITEM_ID_SPIDER_FANG = 8;
            public const int ITEM_ID_SPIDER_SILK = 9;
            public const int ITEM_ID_ADVENTURER_PASS = 10;

            public const int MONSTER_ID_RAT = 1;
            public const int MONSTER_ID_SNAKE = 2;
            public const int MONSTER_ID_GIANT_SPIDER = 3;

            public const int QUEST_ID_CLEAR_ALCHEMIST_GARDEN = 1;
            public const int QUEST_ID_CLEAR_FARMERS_FIELD = 2;

            public const int LOCATION_ID_HOME = 1;
            public const int LOCATION_ID_TOWN_SQUARE = 2;
            public const int LOCATION_ID_GUARD_POST = 3;
            public const int LOCATION_ID_ALCHEMIST_HUT = 4;
            public const int LOCATION_ID_ALCHEMISTS_GARDEN = 5;
            public const int LOCATION_ID_FARMHOUSE = 6;
            public const int LOCATION_ID_FARM_FIELD = 7;
            public const int LOCATION_ID_BRIDGE = 8;
            public const int LOCATION_ID_SPIDER_FIELD = 9;
        /*These are constants. const can never have their values
         changed. This is done, so that we don't have to remember the numeric
         IDs for each of the different game objects.*/

            public const int UNSELLABLE_ITEM_PRICE = -1;
        /*This is set to -1, so it'll be userd as a "flag" (indicator) value. This
         will indicate that it's unsellable. In other words, The item that has this variable as a parameter
         is unsellable, or in this case, ITEM_ID_ADVENTURER_PASS.
         
         This is done here, so that the reason why ITEM_ID_ADVENTURER_PASS has a variable that has a -1
         will be seen.*/



        static World()
            /*This is a static constructor. This method runs other methods, and that's it.*/
            {
                PopulateItems();
                PopulateMonsters();
                PopulateQuests();
                PopulateLocations();
            }

            private static void PopulateItems()
            /*Private means that this method can only be run by other code inside this class. If it was public,
             then it could be run from other classes.*/
            {
                Items.Add(new Weapon(ITEM_ID_RUSTY_SWORD, "Rusty sword", "Rusty swords", 0, 5, 5));
                Items.Add(new Item(ITEM_ID_RAT_TAIL, "Rat tail", "Rat tails", 1));
                Items.Add(new Item(ITEM_ID_PIECE_OF_FUR, "Piece of fur", "Pieces of fur", 2));
                Items.Add(new Item(ITEM_ID_SNAKE_FANG, "Snake fang", "Snake fangs", 2));
                Items.Add(new Item(ITEM_ID_SNAKESKIN, "Snakeskin", "Snakeskins", 3));
                Items.Add(new Weapon(ITEM_ID_CLUB, "Club", "Clubs", 3, 10, 7));
                Items.Add(new HealingPotion(ITEM_ID_HEALING_POTION, "Healing potion", "Healing potions", 5, 3));
                Items.Add(new Item(ITEM_ID_SPIDER_FANG, "Spider fang", "Spider fangs", 1));
                Items.Add(new Item(ITEM_ID_SPIDER_SILK, "Spider silk", "Spider silks", 1));
                Items.Add(new Item(ITEM_ID_ADVENTURER_PASS, "Adventurer pass", "Adventurer passes", UNSELLABLE_ITEM_PRICE));

            /*Add() method adds an object to the list.*/
        }

        private static void PopulateMonsters()
            {
                Monster rat = new Monster(10, 10, MONSTER_ID_RAT, "Rat", 3, 10, 10);
                rat.lootTable.Add(new LootItem(ItemByID(ITEM_ID_RAT_TAIL), 75, false));
                rat.lootTable.Add(new LootItem(ItemByID(ITEM_ID_PIECE_OF_FUR), 75, true));

                Monster snake = new Monster(20, 20, MONSTER_ID_SNAKE, "Snake", 2, 5, 5);
                snake.lootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKE_FANG), 75, false));
                snake.lootTable.Add(new LootItem(ItemByID(ITEM_ID_SNAKESKIN), 75, true));

                Monster giantSpider = new Monster(40, 40, MONSTER_ID_GIANT_SPIDER, "Giant spider", 4, 20, 20);
                giantSpider.lootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_FANG), 75, true));
                giantSpider.lootTable.Add(new LootItem(ItemByID(ITEM_ID_SPIDER_SILK), 25, false));

                Monsters.Add(rat);
                Monsters.Add(snake);
                Monsters.Add(giantSpider);
            }

            private static void PopulateQuests()
            {
                Quest clearAlchemistGarden =
                    new Quest(
                        QUEST_ID_CLEAR_ALCHEMIST_GARDEN,
                        "Clear the alchemist's garden",
                        "Kill rats in the alchemist's garden and bring back 3 rat tails. You will receive a healing potion and 10 gold pieces.", 20, 10);

                clearAlchemistGarden.questItemWon.Add(new QuestCompletionItem(ItemByID(ITEM_ID_RAT_TAIL), 3));

                clearAlchemistGarden.itemRewarded = ItemByID(ITEM_ID_HEALING_POTION);

                Quest clearFarmersField =
                    new Quest(
                        QUEST_ID_CLEAR_FARMERS_FIELD,
                        "Clear the farmer's field",
                        "Kill snakes in the farmer's field and bring back 3 snake fangs. You will receive an adventurer's pass and 20 gold pieces.", 20, 20);

                clearFarmersField.questItemWon.Add(new QuestCompletionItem(ItemByID(ITEM_ID_SNAKE_FANG), 3));

                clearFarmersField.itemRewarded = ItemByID(ITEM_ID_ADVENTURER_PASS);

                Quests.Add(clearAlchemistGarden);
                Quests.Add(clearFarmersField);
            }

            private static void PopulateLocations()
            {
                // Create each location
                Location home = new Location(LOCATION_ID_HOME, "Home", "Your house. You really need to clean up the place.");

                Location townSquare = new Location(LOCATION_ID_TOWN_SQUARE, "Town square", "You see a fountain.");

                Vendor LeoRatCatcher = new Vendor("Leo, the Rat Catcher");
                LeoRatCatcher.addAnItemToInventory(ItemByID(ITEM_ID_PIECE_OF_FUR), 5);
                LeoRatCatcher.addAnItemToInventory(ItemByID(ITEM_ID_RAT_TAIL), 3);

                townSquare.vendorLocatedHere = LeoRatCatcher;

                Location alchemistHut = new Location(LOCATION_ID_ALCHEMIST_HUT, "Alchemist's hut", "There are many strange plants on the shelves.");
                alchemistHut.questsToDo = QuestByID(QUEST_ID_CLEAR_ALCHEMIST_GARDEN);

                Location alchemistsGarden = new Location(LOCATION_ID_ALCHEMISTS_GARDEN, "Alchemist's garden", "Many plants are growing here.");
                alchemistsGarden.monstersThatLiveHere = MonsterByID(MONSTER_ID_RAT);

                Location farmhouse = new Location(LOCATION_ID_FARMHOUSE, "Farmhouse", "There is a small farmhouse, with a farmer in front.");
                farmhouse.questsToDo = QuestByID(QUEST_ID_CLEAR_FARMERS_FIELD);

                Location farmersField = new Location(LOCATION_ID_FARM_FIELD, "Farmer's field", "You see rows of vegetables growing here.");
                farmersField.monstersThatLiveHere = MonsterByID(MONSTER_ID_SNAKE);

                Location guardPost = new Location(LOCATION_ID_GUARD_POST, "Guard post", "There is a large, tough-looking guard here.", ItemByID(ITEM_ID_ADVENTURER_PASS));

                Location bridge = new Location(LOCATION_ID_BRIDGE, "Bridge", "A stone bridge crosses a wide river.");

                Location spiderField = new Location(LOCATION_ID_SPIDER_FIELD, "Forest", "You see spider webs covering covering the trees in this forest.");
                spiderField.monstersThatLiveHere = MonsterByID(MONSTER_ID_GIANT_SPIDER);

                // Link the locations together
                home.northRegion = townSquare;

                townSquare.northRegion = alchemistHut;
                townSquare.southRegion = home;
                townSquare.eastRegion = guardPost;
                townSquare.westRegion = farmhouse;

                farmhouse.eastRegion = townSquare;
                farmhouse.westRegion = farmersField;

                farmersField.eastRegion = farmhouse;

                alchemistHut.southRegion = townSquare;
                alchemistHut.northRegion = alchemistsGarden;

                alchemistsGarden.southRegion = alchemistHut;

                guardPost.eastRegion = bridge;
                guardPost.westRegion = townSquare;

                bridge.westRegion = guardPost;
                bridge.eastRegion = spiderField;

                spiderField.westRegion = bridge;

                // Add the locations to the static list
                Locations.Add(home);
                Locations.Add(townSquare);
                Locations.Add(guardPost);
                Locations.Add(alchemistHut);
                Locations.Add(alchemistsGarden);
                Locations.Add(farmhouse);
                Locations.Add(farmersField);
                Locations.Add(bridge);
                Locations.Add(spiderField);
            }

            public static Item ItemByID(int id)
            /*This method, and the other methods below, are ones we can call to
             get values from the static lists.
             
            This method, and the other below, return something. The method is public, since we're going to need
            to call this method from other parts of the game.*/
            {
                foreach (Item item in Items)
                {
                    if (item.itemId == id)
                    {
                        return item;
                    }
                }

                return null;
            }

            public static Monster MonsterByID(int id)
            {
                foreach (Monster monster in Monsters)
                {
                    if (monster.monsterId == id)
                    {
                        return monster;
                    }
                }

                return null;
            }

            public static Quest QuestByID(int id)
            {
                foreach (Quest quest in Quests)
                {
                    if (quest.questId == id)
                    {
                        return quest;
                    }
                }

                return null;
            }

            public static Location LocationByID(int id)
            {
                foreach (Location location in Locations)
                {
                    if (location.locationId == id)
                    {
                        return location;
                    }
                }

                return null;
            }
    }
}
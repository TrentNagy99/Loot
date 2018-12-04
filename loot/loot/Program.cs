/**
 * Loot
 * 
 * Original game mady by Trent Nagy
 */


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loot
{
    class Program
    {
        public static List<string> playerInventory = new List<string> {"sword"};
        public static List<string> possibleLoot = new List<string> {"health crystal", "potion"};
        public static int playerHealth = 5;
        public static int playerMaxHealth = 5;

        //----Stats----
        public static int explorationsLasted = 0;
        public static int enemiesSlain = 0;
        public static int potionsDrank = 0;
        public static int crystalsUsed = 0;
        //----Stats----

        /**
         *  Main 
         */
        static void Main(string[] args)
        {
            MainMenu();
        }

        /**
         * Fancy menu screen
         */
        public static void MainMenu()
        {
            Console.Clear();
            playerHealth = 5;
            playerMaxHealth = 5;
            explorationsLasted = 0;
            enemiesSlain = 0;
            potionsDrank = 0;
            crystalsUsed = 0;
            playerInventory.Clear();
            playerInventory.Add("sword");

        string title = "==============================================\n" +
                           "||||||||||||||||||||||||||||||||||||||||||||||\n" +
                           "==============================================\n\n" +
                           "  L           OOO          OOO       TTTTTTT  \n" +
                           "  L          O   O        O   O         T     \n" +
                           "  L         O     O      O     O        T     \n" +
                           "  L         O     O      O     O        T     \n" +
                           "  L          O   O        O   O         T     \n" +
                           "  LLLLLL      OOO          OOO          T     \n\n" +
                           "==============================================\n" +
                           "||||||||||||||||||||||||||||||||||||||||||||||\n" +
                           "==============================================\n";

            Console.WriteLine(title);

            PromptMenu();
        }

        /**
         * Handles menu selection
         */
        public static void PromptMenu()
        {
            Console.WriteLine("New Game");
            Console.WriteLine("Continue");
            Console.WriteLine("Exit\n");

            string menuChoice = Console.ReadLine();

            switch (menuChoice.ToLower())
            {
                //New Game
                case "new game":
                case "new":
                    Console.Clear();
                    Console.WriteLine("While eavesdropping on a conversation in town, you hear of The Dungeon that contains" +
                    "\ntreasure of immeasurable wealth. With the last few gold you have, you buy a sword and armor." +
                    "\nHaving nothing to lose, you enter The Dungeon whilist clutching your sword close to you.\n");

                    PromptUser();
                    break;
                //Exit game
                case "exit":
                    Environment.Exit(0);
                    break;
                case "continue":
                    Console.WriteLine("Coming soon.\n");
                    PromptMenu();
                    break;
                //The program will break if this isn't here.
                case "":
                    Console.Clear();
                    MainMenu();
                    break;
                //Unknown selection
                default:
                    Console.WriteLine("\nThat feature hasn't been put in yet!\n");
                    PromptMenu();
                    break;
            }
        }

        /**
         * Asks the player what they want to do.
         */ 
        public static void PromptUser()
        {
            if(playerHealth >= 1)
            {
                Console.WriteLine("You have " + playerHealth + " health\n");
                Console.WriteLine("1) View Inventory\n" +
                                  "2) Explore\n" +
                                  "3) View Stats\n" +
                                  "4) Exit\n");
                string input = Console.ReadLine();

                if (input == "1")
                {
                    // Catch an exception if there's a problem with printing out the inventory list.
                    try
                    {
                        Console.WriteLine("\n-----Inventory-----");
                        for (int i = 0; i < playerInventory.Count; i++)
                            Console.WriteLine(playerInventory[i]);

                        Console.WriteLine("-------------------\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("\nSomething bad happened when showing the inventory. \nPlease Contact Trent about this.");
                    }
                    
                    Console.WriteLine("Use item? (y/n)");
                    string useItemYorN = Console.ReadLine();

                    if(useItemYorN.ToLower() == "y")
                    {
                        Console.WriteLine("\nWhich one? (use the item name)");
                        string itemChoice = Console.ReadLine();

                        switch (itemChoice.ToLower())
                        {
                            //Potion
                            case "potion":
                                Console.WriteLine("\nYou drink the potion, and feel your wounds begin to heal immediately." +
                                              "\nYour health is restored to " + playerMaxHealth);
                                playerHealth = playerMaxHealth;
                                playerInventory.Remove("potion");
                                potionsDrank++;
                                break;
                            //Health crystal
                            case "health crystal":
                            case "crystal":
                                Console.WriteLine("\nThe crystal responds to your desire for greatness.");
                                Console.WriteLine("Your max health is increased by 1.");
                                playerMaxHealth++;
                                playerInventory.Remove("health crystal");
                                crystalsUsed++;
                                break;
                            //Sword
                            case "sword":
                                Console.WriteLine("You swing the sword at the air, hoping to hit something. Nothing happens.\n");
                                break;
                            //Unknown
                            default:
                                Console.WriteLine("You look, but cannot find " + itemChoice + " in your inventory.");
                                break;
                        }
                    }
                    else if (useItemYorN.ToLower() == "n")
                    {
                        Console.WriteLine("\nYou decide not to use anything.");
                    }

                    PromptUser();
                }
                else if(input == "2")
                {
                    Console.WriteLine("\nYou explore the dungeon further.\n");
                    explorationsLasted++;

                    Random rand = new Random();
                    int chance = rand.Next(50);

                    if(chance >= 0 && chance <= 20)
                        FindChest();
                    else if (chance >= 21 && chance <= 40)
                        FindEnemy();
                    else if (chance >= 41 && chance <= 50)
                        FindTrap();

                    PromptUser();
                }
                else if(input == "3")
                {
                    Console.WriteLine("\nExplorations lasted: " + explorationsLasted +
                                      "\nEnemies slain: " + enemiesSlain + 
                                      "\nPotions Drank: " + potionsDrank + 
                                      "\nCrystals used: " + crystalsUsed + "\n");
                    PromptUser();
                }
                else if(input == "4")
                {
                    Console.WriteLine("\nYou decide to retire your days of adventuring.");
                    Console.WriteLine("\n\nPress enter to leave the dungeon.");
                    Console.Read();
                    MainMenu();
                }
                else
                {
                    Console.WriteLine("You don't think \"" + input + "\" is a viable option.\n");
                    PromptUser();
                }
            }
            else
            {
                Console.WriteLine("\nYour days of adventuring are cut short as you collapse on the floor.");
                Console.WriteLine("Hopefully a new adventurer will be strong enough to explore the dungeon further.");
                Console.WriteLine("\n\nPress Enter to exit.");
                Console.Read();
                MainMenu();
            }
        }

        /**
         * This method activates if the player finds a chest while exploring
         */
        public static void FindChest()
        {
            Random rand = new Random();
            int chance = rand.Next(50);

            if(chance >= 0 && chance <= 10)
            {
                Console.WriteLine("You find a health crystal!");
                playerInventory.Add(possibleLoot[0]);
            }
            else if(chance >= 11)
            {
                Console.WriteLine("You find a potion!");
                playerInventory.Add(possibleLoot[1]);
            }
        }

        /**
         * This method activates if the player finds an enemy while exploring
         */
        public static void FindEnemy()
        {
            Random rand = new Random();
            int chance = rand.Next(30);

            if (chance >= 0 && chance <= 10)
                InitiateCombat("Goblin");
            else if (chance >= 11 && chance <= 20)
                InitiateCombat("Spider");
            else if (chance >= 21 && chance <= 30)
                InitiateCombat("Skeleton");
        }

        /**
         * This method activated when the player fights an enemy.
         */
        public static void InitiateCombat(string enemy)
        {
            Console.WriteLine("You start a fight with an enemy " + enemy + "!");

            Random rand = new Random();
            int chance = rand.Next(50);

            if(chance >= 0 && chance <= 25)
            {
                Console.WriteLine("Your natural speed lets you attack first.");
                Console.WriteLine("The enemy has been vanquished to oblivion.\n");
                enemiesSlain++;
            }
            else
            {
                Console.WriteLine("The enemy's speed gives it an advantage.");
                Console.WriteLine("You take 1 point of damage");
                playerHealth--;
            }
        }

        public static void FindTrap()
        {
            Console.WriteLine("You accidentally trip a wire trap! Arrows shoot out and hit you for 2 health.");
            playerHealth -= 2;
        }
    }
}
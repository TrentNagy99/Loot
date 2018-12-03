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
        /**
         *  Main 
         */
        static void Main(string[] args)
        {
            Console.WriteLine("You enter a dungeon with only a sword and 5 health.\n");
            PromptUser();
        }

        /**
         * Asks the player what they want to do.
         */ 
        public static void PromptUser()
        {
            Console.WriteLine("You have " + playerHealth + " health\n");
            Console.WriteLine("1) View Inventory\n" +
                              "2) Explore\n" +
                              "3) Exit\n");
            string input = Console.ReadLine();
            if(playerHealth > 1)
            {
                if(input == "1")
                {
                    // Catch an exception if there's a problem with printing out the inventory list.
                    try
                    {
                        Console.WriteLine("\n-----Inventory-----");
                        for (int i = 0; i < playerInventory.Count; i++)
                        {
                            Console.WriteLine(playerInventory[i]);
                        }
                        Console.WriteLine("-------------------\n");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.WriteLine("\nSomething happened when showing inventory. \nPlease Contact Trent about this.");
                    }
                    
                    Console.WriteLine("Use item? (y/n)");
                    string useItemYorN = Console.ReadLine();

                    if(useItemYorN.ToLower() == "y")
                    {
                        Console.WriteLine("\nWhich one? (use the item name)");
                        string itemChoice = Console.ReadLine();
                        
                        if(itemChoice == "potion")
                        {
                            Console.WriteLine("\nYou drink the potion, and are ready for battle!");
                            playerHealth = playerMaxHealth;
                            playerInventory.Remove(itemChoice);
                        }
                        else if (itemChoice == "health crystal")
                        {
                            Console.WriteLine("\nThe crystal responds to your desire for greatness.");
                            Console.WriteLine("Your max health is increased by 1.");
                            playerMaxHealth++;
                            playerInventory.Remove(itemChoice);
                        }
                        else if (itemChoice == "sword")
                        {
                            Console.WriteLine("You swing the sword at the air. Nothing happens.");
                        }
                        else
                        {
                            Console.WriteLine("You look, but cannot find " + itemChoice + " in your inventory.");
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

                    Random rand = new Random();
                    int chance = rand.Next(50);

                    if(chance >= 0 && chance <= 20)
                    {
                        FindChest();
                    }
                    else if (chance >= 21 && chance <= 40)
                    {
                        FindEnemy();
                    }
                    else if (chance >= 41 && chance <= 50)
                    {
                        FindTrap();
                    }

                    PromptUser();
                }
                else if(input == "3")
                {
                    Console.WriteLine("\nYou decide to retire your days of adventuring.");
                    Console.WriteLine("\n\nPress enter to leave the dungeon.");
                    Console.Read();
                    Environment.Exit(0);
                }
            }
            else
            {
                Console.WriteLine("\nYour days of adventuring are cut short as you collapse on the floor.");
                Console.WriteLine("Hopefully a new adventurer will be strong enough to explore the dungeon further.");
                Console.WriteLine("\n\nPress Enter to exit.");
                Console.Read();
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
         * This method activates if the player finds a chest while exploring
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
            }
            else
            {
                Console.WriteLine("The enemy's speed gives it an advantage.");
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

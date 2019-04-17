using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace loot
{
    class Program
    {
        public static IDictionary<string, int> allItems = new Dictionary<string, int>()
        {
            {"sword", 5}, {"wakizashi", 15}, {"iron shortsword", 25}, {"iron greatsword", 30},
            {"potion", 50}, {"health crystal", 200},
        };

        public static IDictionary<string, int> itemDamage = new Dictionary<string, int>()
        {
            {"fists", 1}, {"sword", 2}, {"wakizashi", 3}, {"iron shortsword", 4}, {"iron greatsword", 5}
        };

        public static IDictionary<int, int> levels = new Dictionary<int, int>()
        {
            {1, 0}, {2, 300}, {3, 900}, {4, 2700}, {5, 6500}, {6, 14000}, {7, 23000}, {8, 34000}, {9, 48000}, {10, 64000}
        };

        public static List<string> playerInventory = new List<string> { "sword" };
        public static List<string> possibleLoot = new List<string> { "health crystal", "potion" };

        //Blacksmith
        public static List<string> blacksmithInventory = new List<string> { "wakizashi", "iron greatsword"};
        public static List<string> blacksmithBuyableItems = new List<string> { "sword", "wakizashi", "iron shortsword", "iron greatsword" };

        //Alchemist
        public static List<string> alchemistInventory = new List<string> { "potion", "potion", "potion", "health crystal"};
        public static List<string> alchemistBuyableItems = new List<string> { "potion", "health crystal" };

        public static Player player = new Player();
        public static int currentDepth = 0;
        public static int nextMilestone = levels[player.Level + 1];

        //----Stats----
        public static int explorationsLasted = 0;
        public static int enemiesSlain = 0;
        public static int potionsDrank = 0;
        public static int crystalsUsed = 0;
        public static int goldObtained = 0;
        //----Stats----

        //Main 
        static void Main(string[] args)
        {
            MainMenu();
        }

        //Fancy menu screen
        public static void MainMenu()
        {
            Console.Clear();
            player.Health = 5;
            player.MaxHealth = 5;
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
            Prompt.PromptMenu();
        }

        public static void GenerateCharacter()
        {

        }

        //This method activates if the player finds a chest while exploring
        public static void FindChest()
        {
            Random rand = new Random();
            int chance = rand.Next(100);
            Console.Clear();

            //Health Crystal
            if (chance >= 0 && chance <= 5)
            {
                Console.WriteLine("You find a health crystal.\n");
                playerInventory.Add(possibleLoot[0]);
            }
            //Potion
            else if (chance >= 11 && chance <= 40)
            {
                Console.WriteLine("You find a potion.\n");
                playerInventory.Add(possibleLoot[1]);
            }
            //Gold
            else
            {
                int chestGold = (rand.Next(5) + player.Level);
                Console.WriteLine("You found " + chestGold + " gold.\n");
                player.Gold += chestGold;
            }
        }

        //This method activates if the player finds an enemy while exploring
        public static void FindEnemy()
        {
            Enemy enemy = new Enemy();
            Console.Clear();
            Random rand = new Random();
            int chance = rand.Next(30);

            InitiateCombat(enemy);
        }

        //This method activates if the player encounters a trap.
        public static void FindTrap()
        {
            Console.Clear();
            Console.WriteLine("You accidentally trip a wire trap! Arrows shoot out and hit you for 2 health.\n");
            player.Health -= 2;
        }

        //This method activates if the player finds a page of a journal
        public static void FindJournal()
        {
            Console.Clear();
            Console.WriteLine("You found a journal. Would you like to read it? (y/n)");

            string choice = Console.ReadLine();

            if(choice.ToLower() == "y")
            {
                Journal journal = new Journal();
                Random rand = new Random();
                int chance = rand.Next(Journal.entries.Count);

                Console.Clear();
                Console.WriteLine(Journal.entries[chance]);
                Prompt.PromptUser();
            }
            else if(choice.ToLower() == "n")
            {
                Console.Clear();
                Console.WriteLine("You decide not to read it.\n");
                Prompt.PromptUser();
            }
            else
            {
                Console.Clear();
                FindJournal();
            }
        }

        //This method activates if the player finds nothing while adventuring
        public static void FindNothing()
        {
            Console.Clear();
            Console.WriteLine("You delve further into the dungeon.\n");
            Prompt.PromptUser();
        }

        public static void FindMerchant()
        {
            Console.Clear();
            Console.WriteLine("Well, you found me. Congratulations. Was it worth it?\n" +
                              "Because despite your violent behavior, the only thing you managed\n" +
                              "to break so far was my heart. Maybe you could settle for that and\n" +
                              "we'll just call it a day. I guess we both know that isn't going to happen.\n" +
                              "You chose this path, and now I have a surprise for you.\n");
            Prompt.PromptMerchant();
        }

        //This method activated when the player fights an enemy.
        public static void InitiateCombat(Enemy enemy)
        {
            Console.WriteLine("You start a fight with an enemy " + enemy.Name + "!");

            Random rand = new Random();
            int chance = rand.Next(50);

            if (chance >= 0 && chance <= 25)
            {
                Console.WriteLine("Your natural speed lets you attack first.");
                int value = 0;
                itemDamage.TryGetValue(player.Equipped, out value);
                Console.WriteLine("You hit your enemy for " + value + " damage!");
                enemy.Health -= value;
                Prompt.PromptBattle(enemy);
            }
            else
            {
                Console.WriteLine("The enemy's speed gives it an advantage.");
                Console.WriteLine("You take 1 point of damage\n");
                player.Health--;

                if(player.Health <= 0)
                    Player.Die();

                Prompt.PromptBattle(enemy);
            }
        }

        //This method is activated when a player slays an enemy.
        public static void ObtainGold()
        {
            Random rand = new Random();
            int chance = rand.Next(10);
            int totalGold = chance + (10 * player.Level);
            player.Gold += totalGold;
            goldObtained += totalGold;
            Console.WriteLine("You've obtained " + totalGold + " gold.\n");
        }

        //This method activates when the player obtains exp
        public static void ObtainEXP()
        {
            Console.WriteLine("You obtained 50 EXP.\n");
            player.Experience += 50;

            if(player.Experience >= nextMilestone)
            {
                Console.WriteLine("Congratulations! You have leveled up.\n");
                player.Level++;
                nextMilestone = levels[player.Level + 1];
                Prompt.PromptUser();
            }
        }

        private static byte[] key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
        private static byte[] iv = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };

        public static string Crypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateEncryptor(key, iv);
            byte[] inputbuffer = Encoding.Unicode.GetBytes(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Convert.ToBase64String(outputBuffer);
        }

        public static string Decrypt(string text)
        {
            SymmetricAlgorithm algorithm = DES.Create();
            ICryptoTransform transform = algorithm.CreateDecryptor(key, iv);
            byte[] inputbuffer = Convert.FromBase64String(text);
            byte[] outputBuffer = transform.TransformFinalBlock(inputbuffer, 0, inputbuffer.Length);
            return Encoding.Unicode.GetString(outputBuffer);
        }
    }

    class Prompt
    {
        //Asks the player what they want to do.
        public static void PromptUser()
        {
            if (Program.player.Health >= 1)
            {

                Console.WriteLine("1) View Inventory\n" +
                                  "2) Explore\n" +
                                  "3) View Stats\n" +
                                  "4) Save\n" +
                                  "5) Leave Dungeon\n");
                string input = Console.ReadLine();

                //Show the inventory, and ask player  if they want to use an item.
                switch (input)
                {
                    //Show the player's inventory, ask if they want to use an item
                    case "1":
                        // Catch an exception if there's a problem with printing out the inventory list.
                        try
                        {
                            Console.Clear();
                            Console.WriteLine("You have " + Program.player.Health + " health");
                            Console.WriteLine("You have " + Program.player.Gold + " gold");

                            Console.WriteLine("\n-----Inventory-----");
                            for (int i = 0; i < Program.playerInventory.Count; i++)
                                Console.WriteLine(Program.playerInventory[i]);

                            Console.WriteLine("-------------------\n");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            Console.WriteLine("\nSomething bad happened when showing the inventory. Please contact me about this.");
                        }

                        Console.WriteLine("Use item? (y/n)");
                        string useItemYorN = Console.ReadLine();

                        if (useItemYorN.ToLower() == "y")
                        {
                            Console.WriteLine("\nWhich one? (use the item name)");
                            string itemChoice = Console.ReadLine();
                            Console.Clear();

                            switch (itemChoice.ToLower())
                            {
                                //Potion
                                case "potion":
                                    Console.WriteLine("You drink the potion, and feel your wounds begin to heal immediately." +
                                                  "\nYour health is restored to " + Program.player.MaxHealth + "\n");
                                    Program.player.Health = Program.player.MaxHealth;
                                    Program.playerInventory.Remove("potion");
                                    Program.potionsDrank++;
                                    break;
                                //Health crystal
                                case "health crystal":
                                case "crystal":
                                    Console.WriteLine("The crystal responds to your desire for greatness.");
                                    Console.WriteLine("Your max health is increased by 1.\n");
                                    Program.player.MaxHealth++;
                                    Program.playerInventory.Remove("health crystal");
                                    Program.crystalsUsed++;
                                    break;
                                default:
                                    if (Program.itemDamage.ContainsKey(itemChoice))
                                    {
                                        if(Program.player.Equipped == itemChoice)
                                        {
                                            Console.WriteLine("You already have that item equipped\n");
                                            PromptUser();
                                        }
                                        else
                                        {
                                            Console.WriteLine("You decide to equip the " + itemChoice + "\n");
                                            Program.player.Equipped = itemChoice;
                                            PromptUser();
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("You look, but cannot find " + itemChoice + " in your inventory.\n");
                                        PromptUser();
                                    }
                                break;
                            }
                        }
                        else if (useItemYorN.ToLower() == "n")
                        {
                            Console.Clear();
                            Console.WriteLine("You decide not to use anything.\n");
                        }

                        PromptUser();
                        break;
                    //Explore the dungeon
                    case "2":
                        Console.WriteLine("\nYou explore the dungeon further.\n");
                        Program.explorationsLasted++;
                        Program.currentDepth++;
                        Random rand = new Random();
                        int chance = rand.Next(100);

                        if (chance >= 0 && chance <= 25)
                            Program.FindChest();
                        else if (chance >= 26 && chance <= 44)
                            Program.FindEnemy();
                        else if (chance >= 45 && chance <= 50)
                            Program.FindTrap();
                        else
                            Program.FindNothing();

                        PromptUser();
                        break;
                    //Show player stats
                    case "3":
                        Console.Clear();
                        Console.WriteLine("Explorations lasted: " + Program.explorationsLasted +
                                          "\nEnemies slain: " + Program.enemiesSlain +
                                          "\nPotions drank: " + Program.potionsDrank +
                                          "\nCrystals used: " + Program.crystalsUsed +
                                          "\nGold obtained: " + Program.goldObtained + 
                                          "\nPlayer level: " + Program.player.Level + "\n");
                        PromptUser();
                        break;
                    //Save the game
                    case "4":
                        try
                        {
                            //First, clear the console.
                            Console.Clear();
                            //Then, tell the player the game is trying to save
                            Console.WriteLine("You scribble down your adventure so far onto a piece of parchment...");

                            //Create a BinaryWriter
                            BinaryWriter writer = new BinaryWriter(new FileStream("savestate", FileMode.Create));

                            //Write the stats
                            writer.Write(Program.explorationsLasted);
                            writer.Write(Program.enemiesSlain);
                            writer.Write(Program.goldObtained);
                            writer.Write(Program.potionsDrank);
                            writer.Write(Program.crystalsUsed);

                            //Write player information
                            writer.Write(Program.player.MaxHealth);
                            writer.Write(Program.player.Health);
                            writer.Write(Program.player.Gold);
                            writer.Write(Program.player.Equipped);
                            writer.Write(Program.player.Level);

                            //Write player inventory
                            foreach (string item in Program.playerInventory)
                            {
                                writer.Write(Program.Crypt(item));
                            }

                            //Tell the user it succeeded.
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Game saved successfully!\n");
                            Console.ResetColor();

                            //Close the writer, prompt the user.
                            writer.Close();
                            PromptUser();
                        }
                        catch (IOException ex)
                        {
                            Console.WriteLine(ex.Message + "\n Cannot create file.");
                            Console.Read();
                        }
                        break;
                    //Return to menu
                    case "5":
                        double calculateHealth(double health, double maxHealth)
                        {
                            double calculated = (health / maxHealth)*100;
                            return calculated;
                        }
                        double healthPercentage = calculateHealth(Program.player.Health, Program.player.MaxHealth);
                        Console.Clear();

                        if(healthPercentage > 69 && Program.currentDepth < 360)
                        {
                            Console.WriteLine("You are " + Program.currentDepth + " minutes away from the exit.\n" +
                                          "Based on your health, you will survive the journey back.\n");

                            Console.WriteLine("Will you leave? (y/n)");

                            string choice = Console.ReadLine();

                            if (choice.ToLower() == "y")
                            {
                                Program.currentDepth = 0;
                                PromptTown();
                            }
                            else
                            {
                                PromptUser();
                            }
                        }
                        else if(healthPercentage > 69 || Program.currentDepth > 360)
                        {
                            Console.WriteLine("You are " + Program.currentDepth + " minutes away from the exit.\n" +
                                          "Based on your depth, you may starve to death.\n");

                            Console.WriteLine("Will you leave? (y/n)");

                            string choice = Console.ReadLine();

                            if (choice.ToLower() == "y")
                            {
                                //Change this
                                Program.currentDepth = 0;
                                PromptTown();
                            }
                            else
                            {
                                PromptUser();
                            }
                        }
                        else if (healthPercentage <= 69 && Program.currentDepth <= 10)
                        {
                            Console.WriteLine("You are " + Program.currentDepth + " minutes away from the exit.\n" +
                                          "You are wounded, but the exit is close by.\n");

                            Console.WriteLine("Will you leave? (y/n)");

                            string choice = Console.ReadLine();

                            if (choice.ToLower() == "y")
                            {
                                Program.currentDepth = 0;
                                PromptTown();
                            }
                            else
                            {
                                PromptUser();
                            }
                        }
                        else if(healthPercentage <= 69)
                        {
                            Console.WriteLine("You are " + Program.currentDepth + " minutes away from the exit.\n" +
                                          "Based on your health, you have a " + healthPercentage + "% chance to survive.\n");

                            Console.WriteLine("Will you leave? (y/n)");

                            string choice = Console.ReadLine();

                            if (choice.ToLower() == "y")
                            {
                                Random random = new Random();
                                int chance2 = random.Next(100);

                                if(chance2 > healthPercentage)
                                {
                                    Console.Clear();
                                    Console.WriteLine("Unfortunately, you have succumbed to your wounds.");
                                    Player.Die();
                                }
                                else
                                {
                                    Program.currentDepth = 0;
                                    PromptTown();
                                }
                            }
                            else
                            {
                                PromptUser();
                            }
                        }
                        break;
                    case "6":
                        Console.Clear();
                        Program.FindJournal();
                        break;
                    //Unknown
                    default:
                        Console.Clear();
                        Console.WriteLine("You don't think \"" + input + "\" is a viable option.\n");
                        PromptUser();
                        break;
                }
            }
            else
            {
                Player.Die();
            }
        }

        //This method is used when a player is in combat.
        public static void PromptBattle(Enemy enemy)
        {
            
            int value = 0;
            Program.itemDamage.TryGetValue(Program.player.Equipped, out value);

            if (enemy.Health > 0) //Only prompt battle if enemy's HP is 0
            {
                if (Program.player.Health <= 0)
                    Player.Die();

                Console.WriteLine("\nWhat do you do?\n" +
                              "1) Attack enemy\n" +
                              "2) Run Away\n");
                string choice = Console.ReadLine();

                if (choice == "1")
                {
                    Console.Clear();
                    Random rand = new Random();
                    int chance = rand.Next(20);

                    if (chance > 10)
                    {
                        Console.WriteLine("You hit your enemy for " + (value + Program.player.Level) + " damage\n");
                        enemy.Health -= (value + Program.player.Level);

                        chance = rand.Next(100);

                        if (enemy.Health <= 0)
                            Enemy.Die();

                        if (chance > 70)
                        {
                            Console.WriteLine("The enemy hits you for " + enemy.Damage + " damage.");
                            Program.player.Health -= enemy.Damage;
                            PromptBattle(enemy);
                        }
                        else
                        {
                            Console.WriteLine("You evade the enemy's attack.");
                            PromptBattle(enemy);
                        }
                    }
                    else
                    {
                        Console.WriteLine("The enemy dodges your attack.\n");

                        chance = rand.Next(100);

                        if (chance > 70)
                        {
                            Console.WriteLine("The enemy hits you for " + enemy.Damage + " damage.");
                            Program.player.Health -= enemy.Damage;
                            PromptBattle(enemy);
                        }
                        else
                        {
                            Console.WriteLine("You evade the enemy's attack.");
                            PromptBattle(enemy);
                        }
                    }
                }
                else
                {
                    Console.Clear();
                    Random rand = new Random();
                    int chance = rand.Next(100);

                    if (chance > 50)
                    {
                        Console.WriteLine("You successfully run away.\n");
                        PromptUser();
                    }
                    else
                    {
                        Console.WriteLine("You failed to escape!");
                        Console.WriteLine("The enemy hits you for " + enemy.Damage + " damage.\n");
                        Program.player.Health -= enemy.Damage;
                        PromptBattle(enemy);
                    }
                }
            }
            else //If the enemy has 0 health upon prompting battle
            {
                Enemy.Die();
            }
        }

        //Handles menu selection
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
                    //Console.WriteLine("While eavesdropping on a conversation in town, you hear of The Dungeon that contains" +
                    //"\ntreasure of immeasurable wealth. With the last few gold you have, you buy a sword and armor." +
                    //"\nHaving nothing to lose, you enter The Dungeon whilist clutching your sword close to you.\n");

                    //PromptUser();
                    

                    GenerateCharacter();
                    break;
                //Exit game
                case "exit":
                    Environment.Exit(0);
                    break;
                case "continue":
                    try
                    {
                        //Create a new BinaryReader
                        BinaryReader reader = new BinaryReader(new FileStream("savestate", FileMode.Open));

                        //Read the statistics
                        Program.explorationsLasted = reader.ReadInt32();
                        Program.enemiesSlain = reader.ReadInt32();
                        Program.goldObtained = reader.ReadInt32();
                        Program.potionsDrank = reader.ReadInt32();
                        Program.crystalsUsed = reader.ReadInt32();

                        //Read player information
                        Program.player.MaxHealth = reader.ReadInt32();
                        Program.player.Health = reader.ReadInt32();
                        Program.player.Gold = reader.ReadInt32();
                        Program.player.Equipped = reader.ReadString();
                        Program.player.Level = reader.ReadInt32();

                        //Read player inventory
                        while (reader.BaseStream.Position != reader.BaseStream.Length)
                        {
                            Program.playerInventory.Add(Program.Decrypt(reader.ReadString()));
                        }

                        //Clear the console and close the reader
                        Console.Clear();
                        Program.playerInventory.Remove("sword");
                        reader.Close();

                        //Prompt the user.
                        Console.WriteLine("You have " + Program.player.Health + " health");
                        Console.WriteLine("You have " + Program.player.Gold + " gold\n");

                        PromptUser();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.Read();
                        Program.MainMenu();
                    }
                    break;
                //The program will break if this isn't here.
                case "":
                    Console.Clear();
                    Program.MainMenu();
                    break;
                //Unknown selection
                default:
                    Console.WriteLine("\nThat feature hasn't been put in yet!\n");
                    PromptMenu();
                    break;
            }
        }

        private static void GenerateCharacter()
        {
            string occupation;
            string luck;
            string town;
            string reason;
            string discover;

            Random rand = new Random();

            occupation = Generation.occupation[rand.Next(Generation.occupation.Count)];
            luck = Generation.luck[rand.Next(Generation.luck.Count)];
            town = Generation.town[rand.Next(Generation.town.Count)];
            reason = Generation.reason[rand.Next(Generation.reason.Count)];
            //discover = Generation.discover[rand.Next(Generation.discover.Count)];

            Console.WriteLine("You are a " + occupation + " with " + luck + " luck.");
            Console.WriteLine("Your hometown is " + town + ", but you moved to Easthallow " + reason + ".\n");
            //Console.WriteLine("While walking around, you happen upon" + discover + ".");

            PromptTown();
        }

        //Prompt the user for what shop they want (unfinished)
        public static void PromptTown()
        {
            Console.WriteLine("The town is full of people going in and out of shops.\n");
            Console.WriteLine("1) Blacksmith\n" +
                              "2) Alchemist Shop\n" +
                              "3) Enter The Dungeon");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Clear();
                    PromptBlacksmith();
                    break;
                case "2":
                    Console.Clear();
                    PromptAlchemist();
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("You arrive back at The Dungeon.\n");
                    PromptUser();
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("You cannot find that here.\n");
                    PromptTown();
                    break;
            }
        }

        //Prompt the user what they want to do in the blacksmith (unfinished)
        public static void PromptBlacksmith()
        {
            Console.WriteLine("Swords of all shapes and sizes line the walls of the blacksmith.\n");

            Console.WriteLine("1) Buy item\n" +
                              "2) Sell item\n" +
                              "3) Leave shop\n");
            string choice = Console.ReadLine();

            switch (choice.ToLower())
            {
                case "1":
                    Console.WriteLine("\n\"Alright, here's what I got.\"\n");

                    Console.WriteLine("-----Inventory-----");
                    for (int i = 0; i < Program.blacksmithInventory.Count; i++)
                        Console.WriteLine(Program.blacksmithInventory[i]);
                    Console.WriteLine("-------------------\n");

                    Console.WriteLine("\"So, what will you buy?\"");
                    choice = Console.ReadLine();

                    if (Program.blacksmithInventory.Contains(choice))
                    {
                        int value = 0;
                        Program.allItems.TryGetValue(choice, out value);
                        Console.Clear();
                        Console.WriteLine("\"Hmm, you know what? I'll charge you " + value + " for my " + choice + "\"");

                        Console.WriteLine("\nDo you accept? (y/n)");
                        string input = Console.ReadLine();

                        if(input.ToLower() == "y")
                        {
                            if (Program.player.Gold < value)
                            {
                                Console.Clear();
                                Console.WriteLine("\"Come back when you get more gold for it.\"\n");
                                PromptTown();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\"It's a done deal, then.\"\n");
                                Program.playerInventory.Add(choice);
                                Program.player.Gold -= Program.allItems[choice];
                                Program.blacksmithInventory.Remove(choice);
                                PromptBlacksmith();
                            }
                        }
                        else if(input.ToLower() == "n")
                        {
                            Console.WriteLine("\"Another time, perhaps?\"\n");
                            PromptBlacksmith();
                        }
                        else
                        {
                            Console.WriteLine("\"It's a simple \'Yes\' or \'No\'.\"");
                            PromptBlacksmith();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\"I don't sell that item.\"\n");
                        PromptBlacksmith();
                    }
                    break;

                case "2":
                    Console.WriteLine("\"Alright, what do you have?\"\n");

                    Console.WriteLine("-----Inventory-----");
                    for (int i = 0; i < Program.playerInventory.Count; i++)
                        Console.WriteLine(Program.playerInventory[i]);
                    Console.WriteLine("-------------------\n");

                    Console.WriteLine("What do you want to sell? (use item name)\n");
                    choice = Console.ReadLine();

                    if (Program.playerInventory.Contains(choice))
                    {
                        Console.Clear();
                        if (!Program.blacksmithBuyableItems.Contains(choice))
                        {
                            Console.WriteLine("\"I don't buy that kind of item.\"\n");
                            Console.Read();
                            PromptBlacksmith();
                        }
                        else
                        {
                            int value = 0;
                            Program.allItems.TryGetValue(choice, out value);

                            Console.WriteLine("\"For you, i'll give you " + value + " for that " + choice + ".\"\n");
                            Console.WriteLine("Do you accept? (y/n)");
                            string input = Console.ReadLine();

                            if (input.ToLower() == "y")
                            {
                                Console.Clear();
                                Console.WriteLine("\"It's a done deal, then.\"\n");
                                if(Program.player.Equipped == choice)
                                {
                                    Program.player.Equipped = "fists";
                                }
                                Program.playerInventory.Remove(choice);
                                Program.player.Gold += Program.allItems[choice];
                                Program.blacksmithInventory.Add(choice);
                                PromptBlacksmith();
                            }
                            else if (input.ToLower() == "n")
                            {
                                Console.WriteLine("\"Another time, perhaps?\"\n");
                                PromptBlacksmith();
                            }
                            else
                            {
                                Console.WriteLine("\"It's a simple \'Yes\' or \'No\'.\"");
                                PromptBlacksmith();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You look for a " + choice + ", but cannot find one.\n");
                        PromptBlacksmith();
                    }
                    break;

                case "3":
                    Console.Clear();
                    Console.WriteLine("\"Good day.\"\n");
                    PromptTown();
                    break;

                default:
                    Console.Clear();
                    Console.WriteLine("\"I don't know about that.\"\n");
                    PromptBlacksmith();
                    
                    break;
            }
        }

        //Prompt the user what they want to do in the alchemist's shop (unfinished)
        public static void PromptAlchemist()
        {
            
            Console.WriteLine("In the corner, a cauldron bubbles as the smell of ingredients invades your nose.\n");

            Console.WriteLine("1) Buy item\n" +
                              "2) Sell item\n" +
                              "3) Leave shop\n");
            string choice = Console.ReadLine();

            switch (choice.ToLower())
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("\"I've got plenty of potions for you.\"\n");

                    Console.WriteLine("-----Inventory-----");
                    for (int i = 0; i < Program.alchemistInventory.Count; i++)
                        Console.WriteLine(Program.alchemistInventory[i]);
                    Console.WriteLine("-------------------\n");

                    Console.WriteLine("\"What will it be?\"");
                    choice = Console.ReadLine();

                    if (Program.alchemistInventory.Contains(choice))
                    {
                        int value = 0;
                        Program.allItems.TryGetValue(choice, out value);

                        Console.WriteLine("\n\"It'll cost you " + value + " for this " + choice + "\"");

                        Console.WriteLine("\nIs that okay? (y/n)");
                        string input = Console.ReadLine();

                        if (input.ToLower() == "y")
                        {
                            Console.Clear();
                            if (Program.player.Gold < value)
                            {
                                Console.WriteLine("\"Im sorry, but you're going to need more gold for this.\"\n");
                                PromptAlchemist();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\"Thank you for your business.\"\n");
                                Program.playerInventory.Add(choice);
                                Program.player.Gold -= Program.allItems[choice];
                                Program.alchemistInventory.Remove(choice);
                                Console.Read();
                                
                                PromptAlchemist();
                            }
                        }
                        else if (input.ToLower() == "n")
                        {
                            Console.Clear();
                            Console.WriteLine("\"It's okay, take your time.\"\n");
                            PromptAlchemist();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("\"Was that a \'Yes\' or \'No\'?\"");
                            PromptAlchemist();
                        }
                    }
                    else
                    {
                        Console.WriteLine("\"I'm sorry, but I don't have any.\"\n");
                        PromptAlchemist();
                    }
                    break;
                case "2":
                    Console.WriteLine("\"Okay, let's see what you have.\"\n");

                    Console.WriteLine("-----Inventory-----");
                    for (int i = 0; i < Program.playerInventory.Count; i++)
                        Console.WriteLine(Program.playerInventory[i]);
                    Console.WriteLine("-------------------\n");

                    Console.WriteLine("What do you want to sell? (use item name)\n");
                    choice = Console.ReadLine();

                    if (Program.playerInventory.Contains(choice))
                    {
                        Console.Clear();
                        if (!Program.alchemistBuyableItems.Contains(choice))
                        {
                            Console.WriteLine("\"Oh, i'm sorry, i'm not in the market for that.\"\n");
                            PromptAlchemist();
                        }
                        else
                        {
                            int value = 0;
                            Program.allItems.TryGetValue(choice, out value);

                            Console.WriteLine("\"I can spare " + value + " gold for your " + choice + ".\"\n");
                            Console.WriteLine("Is that okay? (y/n)");
                            string input = Console.ReadLine();

                            if (input.ToLower() == "y")
                            {
                                Console.Clear();
                                Console.WriteLine("\"Thank you!\"\n");
                                Program.playerInventory.Remove(choice);
                                Program.player.Gold += Program.allItems[choice];
                                Program.alchemistInventory.Add(choice);
                                PromptAlchemist();
                            }
                            else if (input.ToLower() == "n")
                            {
                                Console.Clear();
                                Console.WriteLine("\"Another time, perhaps?\"\n");
                                PromptAlchemist();
                            }
                            else
                            {
                                Console.Clear();
                                Console.WriteLine("\"Was that a \'Yes\' or \'No\'?\"");
                                PromptAlchemist();
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("You look for a " + choice + ", but cannot find one.\n");
                        PromptBlacksmith();
                    }
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("\"Have a good one!\"\n");
                    PromptTown();
                    break;
                default:
                    PromptAlchemist();
                    break;
            }
        }

        public static void PromptMerchant()
        {
            PromptUser();
        }
    }
}
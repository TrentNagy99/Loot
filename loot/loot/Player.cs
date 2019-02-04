﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loot
{
    class Player
    {
        public Player()
        {
            MaxHealth = 5;
            Health = 5;
            Gold = 0;
            Level = 1;
            Experience = 0;
            Equipped = "sword";
        }

        public int Health { get; set; }

        public int Gold { get; set; }

        public int MaxHealth { get; set; }

        public int Level { get; set; }

        public int Experience { get; set; }

        public string Equipped { get; set; }

        public static void Hit(int value)
        {
            Console.WriteLine("You hit your enemy for " + value + " damage.\n");
            Program.enemyHealth -= value;
        }

        public static void Die()
        {
            Console.WriteLine("Your days of adventuring are cut short as you collapse to the floor.\n\n");
            Console.WriteLine("Press enter to continue.");
            Console.Read();
            Program.MainMenu();
        }
    }
}

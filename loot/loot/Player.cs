using System;
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
        }

        public int Health { get; set; }

        public int Gold { get; set; }

        public int MaxHealth { get; set; }

        public static void Hit()
        {
            Console.WriteLine("You hit your enemy for 2 damage.\n");
            Program.enemyHealth -= 2;
        }

        public static void Die()
        {
            Console.WriteLine("Your days of adventuring are cut short as you collapse on the floor.\n\n");
            Console.WriteLine("Press enter to continue.");
            Console.Read();
            Program.MainMenu();
        }
    }
}

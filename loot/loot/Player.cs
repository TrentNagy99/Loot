using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loot
{
    class Player
    {
        private int _maxHealth;
        private int _health;
        private int _gold;

        public Player()
        {
            _maxHealth = 5;
            _health = 5;
            _gold = 0;
        }

        public int Health
        {
            get { return _health; }
            set { _health = value; }
        }

        public int Gold
        {
            get { return _gold; }
            set { _gold = value; }
        }

        public int MaxHealth
        {
            get { return _maxHealth; }
            set { _maxHealth = value; }
        }

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

using System;

namespace loot
{
    class Enemy
    {
        private int health;
        private string name;

        public Enemy()
        {
            Health = 2 + (Program.player.Level + Program.player.Level);
            Name = "Skeleton";
        }

        public static void Die()
        {
            Console.WriteLine("The enemy is defeated!\n");
            Program.ObtainGold();
            Program.enemiesSlain++;
            Program.ObtainEXP();
            Prompt.PromptUser();
        }

        public int Health { get; set; }
        public string Name { get; set; }
    }
}

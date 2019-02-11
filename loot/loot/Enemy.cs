using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loot
{
    class Enemy
    {
        public static void Die()
        {
            Console.WriteLine("The enemy is defeated!\n");
            Program.ObtainGold();
            Program.enemiesSlain++;
            Program.ObtainEXP();
            Prompt.PromptUser();
        }

        public static void Hit()
        {
            Console.WriteLine("The enemy hits you for 1 damage.\n");
            Program.player.Health--;
            Prompt.PromptBattle();
        }
    }
}

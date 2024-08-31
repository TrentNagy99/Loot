using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loot
{
    public class Weapon : Item
    {
        public Weapon()
        {
            BaseDamage = 0;
        }

        public int BaseDamage { get; set; }

        protected override void OnUse()
        {
            Console.WriteLine("You equip the " + Name + "\n");
            Program.player.Equipped = this;
        }
    }
}

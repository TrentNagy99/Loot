using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loot
{
    class Generation
    {
        public static List<string> occupation = new List<string>
        {
            "beggar", "nobleman", "laborer", "pit fighter", "knight"
        };

        public static List<string> luck = new List<string>
        {
            "great", "good", "average", "bad", "terrible"
        };

        public static List<string> town = new List<string>
        {
            "Colonia", "Hemsworth", "Alverton", "Garthram", "Hampstead", "Rivermuth", "Solaria"
        };

        public static List<string> reason = new List<string>
        {
            "to get away from family", "due to unforseen consequences", "to explore new places", "to find a job"
        };

        public static List<string> discover = new List<string>
        {
            //Add Content
        };
    }
}

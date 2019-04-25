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
            "beggar", "nobleman", "laborer", "ring fighter", "knight"
        };

        public static List<string> town = new List<string>
        {
            "Colonia", "Hemsworth", "Alverton", "Garthram", "Hampstead", "Rivermuth", "Solaria"
        };

        public static List<string> fNameMale = new List<string>
        {
            "Reinald", "Barry", "Nathaniel", "Ermin", "Solomon", "Gilram", "Thelnur", "Doldrak"
        };

        public static List<string> lNameMale = new List<string>
        {
            "Kharmus", "Thalmiir", "Thelren", "Amasu", "Mormar", "Galmiir", "Murdrom"
        };

        public static List<string> fNameFemale = new List<string>
        {
            "Tislen", "Reynwin", "Daeleth", "Erana", "Zyleth", "Cairel", "Aeroph", "Mei"
        };

        public static List<string> lNameFemale = new List<string>
        {
            "Amasu", "Vallynn", "Olathana", "Reywell", "Mormar", "Kharmus"
        };

        public static List<string> reason = new List<string>
        {
            "to get away from family", "due to unforseen consequences", "to explore new places", "to find a job"
        };

        public static IDictionary<int, string> lawfulness = new Dictionary<int, string>
        {
            {1, "You are unlawful."}, {2, "You are mostly unlawful."}, {3, "You show average lawfulness."}, {4, "You respect and admire lawfulness."}, {5, "You make sure to uphold the law when necessary."}
        };

        public static IDictionary<int, string> happiness = new Dictionary<int, string>
        {
            {1, "You are depressed."}, {2, "You are mostly unhappy."}, {3, "You are contempt with life."}, {4, "You are mostly happy."}, {5, "You are always happy."}
        };

        public static IDictionary<int, string> productivity = new Dictionary<int, string>
        {
            {1, "You are not productive at all."}, {2, "You are mostly unproductive."}, {3, "You are productive at times, and others, you aren't."}, {4, "You are mostly productive."}, {5, "You are always productive."}
        };

        public static List<string> discover = new List<string>
        {
            //Add Content
        };
    }
}

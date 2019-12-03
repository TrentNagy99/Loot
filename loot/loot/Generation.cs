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
            {1, "You tend to turn your back to the law, mostly for your own gain."}, {2, "You dabble in your unlawfulness, either careful not to get caught or to try and maintian some order."}, {3, "You are constantly on the edge of lawfulness and unlawfulness."}, {4, "You respect and admire lawfulness."}, {5, "You make sure to uphold the law when necessary."}
        };

        public static IDictionary<int, string> happiness = new Dictionary<int, string>
        {
            {1, "You spend your days in a constant depression."}, {2, "You tend to sulk more than others."}, {3, "You are neither sad or happy with how your life worked out."}, {4, "You are quite contempt with most things in your life."}, {5, "You have a greater happiness with your life than all others, and wouldn't trade it for anything.."}
        };

        public static IDictionary<int, string> productivity = new Dictionary<int, string>
        {
            {1, "You find work and effort to be insulting."}, {2, "You tend to avoid work when there's a better solution."}, {3, "You neither like or dislike work, you just do what you must.."}, {4, "You find productivity to be more beneficial in the long run."}, {5, "You think that productivity and hard work are desireable in every person."}
        };

        public static List<string> discover = new List<string>
        {
            //Add Content
        };
    }
}

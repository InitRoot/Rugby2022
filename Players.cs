using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC4Editor
{
    class Players
    {
        //Create the variables with getters and setters so that the Objectlistview can access them
        public string PlayerName { get; set; }
        public string PlayerNation { get; set; }
        public string PlayerIndex { get; set; }
        public string StarStatus { get; set; }
        public string Primary15Position { get; set; }
        public string Primary7Position { get; set; }
        public string PlayerID { get; set; }

        public Players(string plID,string name, string nation, string Indexs, string Star)
        {
            PlayerID = plID;
            PlayerName = name;
            PlayerNation = nation;
            PlayerIndex = Indexs;
            StarStatus = Star;
        }

        public Players(string plID, string name, string nation, string Indexs, string p15Position,string p7Position)
        {
            PlayerID = plID;
            PlayerName = name;
            PlayerNation = nation;
            PlayerIndex = Indexs;
            Primary15Position = p15Position;
            Primary7Position = p7Position;
        } 
    }
}

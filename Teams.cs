using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC4Editor
{
    class Teams
    {
        //Create the variables with getters and setters so that the Objectlistview can access them
        public string TeamName { get; set; }
        public int TeamIndex { get; set; }
        public string TeamID { get; set; }

        public Teams(string tmID, string name, int Indexs)
        {
            TeamName = name;
            TeamIndex = Indexs;
            TeamID = tmID;

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RC4Editor
{
    class Kits
    {
        //Create the variables with getters and setters so that the Objectlistview can access them
        public string KitName { get; set; }
        public int KitIndex { get; set; }
        public string KitID { get; set; }

        public Kits(string ktID, string name, int Indexs)
        {
            KitName = name;
            KitIndex = Indexs;
            KitID = ktID;

        }

    }
}

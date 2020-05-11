using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SystAnalys_lr1.Classes
{
    public class Grid
    {
        public int left { get; set; }
        public int up { get; set; }
        public int right { get; set; }
        public int down { get; set; }
        [XmlIgnore, JsonIgnore]
        public int gridWidth { get; set; }
        [XmlIgnore, JsonIgnore]
        public int gridHeight { get; set; }

        public Grid() { }
        public Grid(int l, int u, int r, int d, int gW, int gH)
        {
            left = l;
            up = u;
            right = r;
            down = d;
            gridWidth = gW;
            gridHeight = gH;
        }    
    }

}

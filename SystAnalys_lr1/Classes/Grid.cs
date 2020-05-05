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
        public int left;
        public int up;
        public int right;
        public int down;
        [XmlIgnore, JsonIgnore]
        public int gridWidth;
        [XmlIgnore, JsonIgnore]
        public int gridHeight;
    }
}

//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
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
        private int _left;
        private int _up;
        private int _right;
        private int _down;
        private int _gridWidth;
        private int _gridHeight;

        public int Left { get => _left; set => _left = value; }
        public int Up { get => _up; set => _up = value; }
        public int Right { get => _right; set => _right = value; }
        public int Down { get => _down; set => _down = value; }
        [XmlIgnore, JsonIgnore]
        public int GridWidth { get => _gridWidth; set => _gridWidth = value; }
        [XmlIgnore, JsonIgnore]
        public int GridHeight { get => _gridHeight; set => _gridHeight = value; }

        public Grid() { }
        public Grid(int l, int u, int r, int d, int gW, int gH)
        {
            Left = l;
            Up = u;
            Right = r;
            Down = d;
            GridWidth = gW;
            GridHeight = gH;
        }
    }

}

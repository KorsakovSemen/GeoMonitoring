//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public class Vertex
    {
        private int _gridNum;
        private int _y;
        private int _x;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public int GridNum { get => _gridNum; set => _gridNum = value; }

        public Vertex()
        { }

        public Vertex(int x, int y)
        {
            X = x;
            Y = y;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (!(obj is Vertex objAsPart)) return false;
            else return Equals(objAsPart);
        }

        public bool Equals(Vertex other)
        {
            if (other == null) return false;
            return (this.X.Equals(other.X) && this.Y.Equals(other.Y));
        }

        public override int GetHashCode()
        {
            var hashCode = -1577951254;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + GridNum.GetHashCode();
            return hashCode;
        }
    }
}

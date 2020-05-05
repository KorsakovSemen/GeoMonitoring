using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public class Vertex //: ICloneable
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int gridNum;

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
            hashCode = hashCode * -1521134295 + gridNum.GetHashCode();
            return hashCode;
        }
    }
}

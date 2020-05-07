using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public class BusStop : Vertex
    {
        public static int stopTime { get; } = 200;

        public BusStop(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

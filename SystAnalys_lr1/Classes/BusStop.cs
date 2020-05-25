using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public class BusStop : Vertex
    {
        private static readonly int s_stopTime = 400;

        public static int StopTime => s_stopTime;

        public BusStop() { }

        public BusStop(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}

//This is a personal academic project. Dear PVS-Studio, please check it.
//PVS-Studio Static Code Analyzer for C, C++, C#, and Java: http://www.viva64.com
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SystAnalys_lr1.Classes
{
    public class Edge
    {
        private int _v1;
        private int _v2;

        public int V1 { get => _v1; set => _v1 = value; }
        public int V2 { get => _v2; set => _v2 = value; }

        public Edge()
        { }

        public Edge(int v1, int v2)
        {
            this.V1 = v1;
            this.V2 = v2;
        }
    }
}

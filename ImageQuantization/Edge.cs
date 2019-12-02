using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class Edge
    {
        public int to, from;
        double cost;
        public Edge(int to , int from , double cost)
        {
            this.to = to;
            this.from = from;
            this.cost = cost;
        }
    }
}

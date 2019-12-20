using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
 
    class Edge : IComparable<Edge>
    {
        public int to, from;
        public double cost;
        public Edge(int to , int from , double cost)
        {
            this.to = to;
            this.from = from;
            this.cost = cost;
        }
       public int CompareTo(Edge other)
        {
            
            return this.cost.CompareTo(other.cost);
        }
       
    }
}

namespace ImageQuantization
{
    class Edge
    {
        public int to, from;
        public double cost;
        public Edge(int to , int from , double cost)
        {
            this.to = to;
            this.from = from;
            this.cost = cost;
        }
    }
}

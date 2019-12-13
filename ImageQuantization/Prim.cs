using System;
using System.Collections.Generic;

namespace ImageQuantization
{
    class Prim
    {
        private int _VertixSize;
        private double[] _key;
        private int[] _parent;
        private bool[] _visted;
        public List<Edge> edges;

        public Prim(int n)  // Θ(n)
        {
            _VertixSize = n;        // Θ(1)
            _key = new double[n];   // Θ(1)
            _parent = new int[n];   // Θ(1)
            _visted = new bool[n];  // Θ(1)
            edges = new List<Edge>(); // Θ(1)

            for (int i = 0; i < n; i++) // Θ(n * body)
            {
                _key[i] = Int32.MaxValue;   // Θ(1)
                _parent[i] = -1;            // Θ(1)
            }
        }

        public double MstPrim(List<RgbPixel> distinctColors)    // Θ(V^2)
        {
            var mstCost = 0d;   // Θ(1)
            int startNode = 0, nextNode = 0; // Θ(1)
            _key[0] = 0;    // Θ(1)
            _parent[0] = 0;    // Θ(1)
            for (var i = 0; i < _VertixSize - 1; i++) // Θ(v * body)
            {
                _visted[startNode] = true;  // Θ(1)
                double miniCost = Int32.MaxValue;   // Θ(1)
                for (var j = 0; j < _VertixSize; j++)   // Θ(v * body)
                {

                    if (_visted[j] == false)    // Θ(1)
                    {
                        var cost = RgbPixel.EuclideanDistance(distinctColors[startNode], distinctColors[j]);    // Θ(1)
                        if (cost < _key[j]) // Θ(1)
                        {
                            _parent[j] = startNode; // Θ(1)
                            _key[j] = cost; // Θ(1)
                        }
                        if (_key[j] < miniCost) // Θ(1)
                        {
                            nextNode = j;    // Θ(1)
                            miniCost = _key[j]; // Θ(1)
                        }
                    }
                }
                startNode = nextNode;    // Θ(1)
                mstCost += _key[startNode]; // Θ(1)
                edges.Add(new Edge(startNode, _parent[startNode], _key[startNode])); // Θ(1)
            }
            
            return Math.Round(mstCost, 2);  // Θ(1)
        }
    }
}

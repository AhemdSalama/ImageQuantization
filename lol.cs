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

        public Prim(int n)
        {
            _VertixSize = n;
            _key = new double[n];
            _parent = new int[n];
            _visted = new bool[n];

            for (int i = 0; i < n; i++)
            {
                _key[i] = Int32.MaxValue;
                _parent[i] = -1;
            }

            _key[0] = 0;
            _parent[0] = -1;
        }

        private int minKey()
        {
            double min = Int32.MaxValue; int minIndex = -1;
            for (int v = 0; v < _VertixSize; v++)
            {
                if (_visted[v] == false && _key[v] < min)
                {
                    min = _key[v];
                    minIndex = v;
                }
            }
            return minIndex;
        }

        public double MstPrim(List<RgbPixel>distinctColors)
        {
          int u = 0, curntNode = 0;
          var mstCost = 0d;
          _key[0] = 0;
          _parent[0]=-1;
            for (int i = 0; i < _VertixSize; i++)
            {
                _visted[u] = true;
                int miniCost = Int32.MaxValue;
                for (int j = 0; j < _VertixSize; j++)
                {

                    if (_visted[j] == false)
                    {
                      var cost = RgbPixel.EuclideanDistance(distinctColors[u], distinctColors[j]);
                      if( cost < _key[j] )
                      {
                        _parent[j] = u;
                        _key[j] = cost;
                      }
                      if(_key[j]<miniCost)
                        {
                          curntNode = j;
                          miniCost = _key[j];
                        }
                    }
                }
                u = curntNode;
                mstCost+=miniCost;
            }

            return Math.Round(mstCost, 1);
        }
    }
}

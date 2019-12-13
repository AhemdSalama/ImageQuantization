﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            for (int i = 0; i < _VertixSize; i++)
            {
                var u = minKey();
                _visted[u] = true;

                for (int j = 0; j < _VertixSize; j++)
                {
                    var cost = RgbPixel.EuclideanDistance(distinctColors[u], distinctColors[j]);

                    if (_visted[j] == false && cost < _key[j] && u != j)
                    {
                        _parent[j] = u;
                        _key[j] = cost;
                    }
                }
            }

            var mstCost = 0d;

            foreach (var d in _key)
            {
                mstCost += Math.Round(d, 6);
            }

            return Math.Round(mstCost, 1);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class _1stBonus
    {
        public static int GetK_1stBonus(Edge[] edges)
        {
            int k = 2;
            Array.Sort(edges);
            double STDdevPre = StandardDeviation(edges, 0, edges.Length);
            double STDdevCur = StandardDeviation(edges, 0, edges.Length - 1);
            if (Math.Abs(STDdevPre - STDdevCur) < 0.0001)
                return k;
            while (Math.Abs( STDdevPre - STDdevCur) >= 0.0001)
            {
                STDdevPre = STDdevCur;
                STDdevCur = StandardDeviation(edges, 0, edges.Length - k);
                k++;
            }
            return k;
        }

        public static double Mean(Edge[] edges , int start ,  int end)
        {
            double s = 0;

            for (int i = start; i < end; i++)
            {
                s += edges[i].cost;
            }

            return s / (end-start);
        }

        public static double Variance(Edge[] edges, double mean , int start ,int end)
        {
            double variance = 0;

            for (int i = start; i < end; i++)
            {
                variance += ((edges[i].cost - mean)* (edges[i].cost - mean));
        }

            int n = end - start;
            if (start > 0)
                n -= 1;
            return variance / (n);
        }
        public static double StandardDeviation(Edge []edges , int start, int end)
        {
            double mean = Mean(edges, start, end);
            double variance = Variance(edges, mean, start, end);
            return Math.Sqrt(variance);
        }
    }
}

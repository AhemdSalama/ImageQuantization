using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ImageQuantization
{
    class KthLargest_Algo2
    {
        static int Dcmp(double a, double b)
        {
            if (Math.Abs(a - b) <= -1e9)
                return 0;
            if (a < b)
                return -1;
            return 1;
        }
        public static Edge KthElement(Edge[] arr, int l, int r, int k)
        {
            var n = r - l + 1;

            if (n < 6)
            {
                Array.Sort(arr, l, n);
                return arr[l + k - 1];
            }

            var medOfMeds = GetMom(arr, l, r);

            var idx = Partition(arr, l, r, medOfMeds);

            if (idx - l == k - 1)
                return arr[idx];

            if (idx - l > k - 1)
                return KthElement(arr, l, idx - 1, k);

            return KthElement(arr, idx + 1, r, k - idx + l - 1);
        }

        static int Partition(Edge[] arr, int l, int r, Edge x)
        {
            int i;
            for (i = l; i < r; i++)
            {
                if (arr[i].cost == x.cost) break;
            }
            Swap(ref arr[i], ref arr[r]);

            i = l;
            for (var j = l; j < r; j++)
                if (arr[j].cost <= x.cost)
                    Swap(ref arr[i++], ref arr[j]);

            Swap(ref arr[i], ref arr[r]);

            return i;
        }

        private static void Swap(ref Edge i, ref Edge j)
        {
            var temp = i;
            i = j;
            j = temp;
        }

        static Edge GetMom(Edge[] arr, int l, int r)
        {
            var n = r - l + 1;
            if (n < 6)
            {
                Array.Sort(arr, l, n % 5);
                return arr[n / 2];
            }


            var medians = new Edge[(n + 4) / 5];

            for (int i = 0; i < n / 5; i++)
            {
                Array.Sort(arr, l + i * 5, 5);
                medians[i] = arr[l + i * 5 + 2];
            }

            if (n % 5 != 0)
            {
                Array.Sort(arr, l + (n / 5) * 5, n % 5);
                medians[(n + 4) / 5 - 1] = arr[l + (n / 5) * 5 + (n % 5) / 2];
            }

            return GetMom(medians, 0, medians.Length - 1);
        }
    }
}
/*
 * /*
int FindKthSmallest(int[] a, int k)
{

    int value = 0;
    int n = a.Length;
    int c = 5;  // Constant used to divide the array into columns

    while (true)
    {

        // Extract median of medians and take it as the pivot
        int pivot = FindPivot(a, n, c);

        // Now count how many smaller and larger elements are there
        int smallerCount = 0;
        int largerCount = 0;
        CountElements(a, n, pivot, out smallerCount, out largerCount);

        // Finally, partition the array
        if (k < smallerCount)
        {
            Partition(a, ref n, pivot, true);
        }
        else if (k < n - largerCount)
        {
            value = pivot;
            break;
        }
        else
        {
            k -= n - largerCount;
            Partition(a, ref n, pivot, false);
        }

    }

    return value;

}

int FindPivot(int[] a, int n, int c)
{

    while (n > 1)
    {

        int pos = 0;
        int tmp = 0;

        for (int start = 0; start < n; start += c)
        {

            int end = start + c;
            if (end > n)    // Last column may have
                end = n;    // less than c elements

            // Sort the column
            for (int i = start; i < end - 1; i++)
                for (int j = i + 1; j < end; j++)
                    if (a[j] < a[i])
                    {
                        tmp = a[i];
                        a[i] = a[j];
                        a[j] = tmp;
                    }

            // Pick the column's median and promote it
            // to the beginning of the array
            end = (start + end) / 2;    // Median position
            tmp = a[end];
            a[end] = a[pos];
            a[pos++] = tmp;

        }

        n = pos;    // Reduce the array and repeat recursively

    }

    return a[0];    // Last median of medians is the pivot

}

void Partition(int[] a, ref int n, int pivot, bool extractSmaller)
{
    int pos = 0;
    for (int i = 0; i < n; i++)
        if ((extractSmaller && a[i] < pivot) ||
            (!extractSmaller && a[i] > pivot))
        {
            int tmp = a[i];
            a[i] = a[pos];
            a[pos++] = tmp;
        }
    n = pos;
}
  */

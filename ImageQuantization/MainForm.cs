using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        RGBPixel[,] ImageMatrix;        // our picture
        public RgbPixel[] ColorsAvg = new RgbPixel[16777217];

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                //Open the browsed image and display it
                string OpenedFilePath = openFileDialog1.FileName;
                ImageMatrix = ImageOperations.OpenImage(OpenedFilePath);
                ImageOperations.DisplayImage(ImageMatrix, pictureBox1);
            }
            txtWidth.Text = ImageOperations.GetWidth(ImageMatrix).ToString();
            txtHeight.Text = ImageOperations.GetHeight(ImageMatrix).ToString();
        }

        private void btnGaussSmooth_Click(object sender, EventArgs e)
        {
            double sigma = double.Parse(txtGaussSigma.Text);
            int maskSize = (int)nudMaskSize.Value;
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        // Return the function with the smallest cost



        private void btnQ_Click(object sender, EventArgs e)
        {
            var wholeProgram = new Stopwatch();
            wholeProgram.Start();
            var distTime = new Stopwatch();
            distTime.Start();
            var distinctColors = CountDistinctColors(); // Exact(N*M)
            distTime.Stop();
            txtDistinctColors.Text = distinctColors.Count.ToString();
            txtDistinctColorsTime.Text = distTime.Elapsed.ToString();

            var mstTime = new Stopwatch();
            mstTime.Start();
            var prim = new Prim(distinctColors.Count);  // Exact(1)
            var ans = prim.MstPrim(distinctColors); // Exact(V^2)
            mstTime.Stop();
            txtMstCost.Text = ans.ToString();
            txtMstCostTime.Text = mstTime.Elapsed.ToString();
            int k = int.Parse(kvalue.Text);
            var KTHtime = new Stopwatch();
            KTHtime.Start();
            //Array.Sort(prim.edges);
            var graph = GetK_LargestEdges(k , prim.edges , distinctColors.Count);
            // bonus 1
            //int k = _1stBonus.GetK_1stBonus(prim.edges);
            kvalue.Text = k.ToString();
            KTHtime.Stop();
            SortBox.Text = KTHtime.Elapsed.ToString();
            //var graph = Constract(prim.edges, k, distinctColors.Count);
            var dfsTime = new Stopwatch();
            dfsTime.Start();
            var dfs = new DFS(distinctColors.Count, graph, distinctColors);
            var Pallete = dfs.Get_Palette(k, ref ColorsAvg);
            dfsTime.Stop();
            DFSbox.Text = dfsTime.Elapsed.ToString();
            var Quantize = new Stopwatch();
            Quantize.Start();
            Quantize_TheImage();
            Quantize.Stop();
            QuantizeBOX.Text = Quantize.Elapsed.ToString();
            wholeProgram.Stop();
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
            txtWholeTime.Text = wholeProgram.Elapsed.ToString();
        }

        private List<RgbPixel> CountDistinctColors()  // Max between [ Exact(N*M) ,  Exact(#Unique Colors) ] -> Exact(N*M)
        {
            var uniqColors = new SortedSet<int>(); // Exact(1)
            var uColors = new List<RgbPixel>();    // Exact(1)

            // Exact(N*M*log)
            foreach (var pixel in ImageMatrix)     // Exact(N*M) * (Body)
            {
                var color = RgbPixel.ConvertToRgbPixel(pixel).RGBToInt();   // Exact(1)
                uniqColors.Add(color);  // Exact(log)
            }   //Body -> Exact(1)

            // Exact(#Unique Colors)
            foreach (var uniqColor in uniqColors)   // Exact(#Unique Colors) * Body
            {
                var color = RgbPixel.IntToRGB(uniqColor);   // Exact(1)
                uColors.Add(color);   // Exact(1)
            }   //Body -> Exact(1)

            return uColors;  // Exact(1)
        }
        private void Quantize_TheImage()
        {
            for (int i = 0; i < ImageOperations.GetHeight(ImageMatrix); i++)
                for (int j = 0; j < ImageOperations.GetWidth(ImageMatrix); j++)
                {
                    var color = RgbPixel.ConvertToRgbPixel(ImageMatrix[i, j]).RGBToInt();
                    RGBPixel Avg;
                    Avg.red = ColorsAvg[color].red;
                    Avg.green = ColorsAvg[color].green;
                    Avg.blue = ColorsAvg[color].blue;
                    ImageMatrix[i, j] = Avg;
                }
        }
        private List<int>[] GetK_LargestEdges(int k, Edge[] edges, int n)
        {
            bool[] arr = new bool[edges.Length];
            for (int i = 0; i < k-1; i++)
            {
                var idx = -1;
                var val = 0d;
                for (int j = 0; j < edges.Length; j++)
                {
                    if (val < edges[j].cost && arr[j] != true)
                    {
                        idx = j;
                        val = edges[j].cost;
                    }
                }
                arr[idx] = true;
            }


            var graph = new List<int>[n];
            int edgeCount = 0;
            foreach (var ed in edges)
            {
                if (arr[edgeCount] == false)
                {
                    if (graph[ed.from] == null)
                        graph[ed.from] = new List<int>();
                    graph[ed.from].Add(ed.to);
                    if (graph[ed.to] == null)
                        graph[ed.to] = new List<int>();
                    graph[ed.to].Add(ed.from);
                }
                else
                {
                    if (graph[ed.from] == null)
                        graph[ed.from] = new List<int>();
                    if (graph[ed.to] == null)
                        graph[ed.to] = new List<int>();
                }

                edgeCount++;
            }
            return graph;


        }
        private List<int>[] Constract (Edge[]edges , int k , int distinctColors)
        {
            var graph = new List<int>[distinctColors];
            int edgeCount = 0;
            foreach (var ed in edges)
            {
                if (edgeCount <= edges.Length - k)
                {
                    if (graph[ed.from] == null)
                        graph[ed.from] = new List<int>();
                    graph[ed.from].Add(ed.to);
                    if (graph[ed.to] == null)
                        graph[ed.to] = new List<int>();
                    graph[ed.to].Add(ed.from);
                }
                else
                {
                    if (graph[ed.from] == null)
                        graph[ed.from] = new List<int>();
                    if (graph[ed.to] == null)
                        graph[ed.to] = new List<int>();
                }

                edgeCount++;
            }
            return graph;

        }
        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void maskedTextBox1_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
/*
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

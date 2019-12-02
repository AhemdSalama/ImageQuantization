using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Diagnostics;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        public struct rgbPixel
        {
            public int red, green, blue;
            public rgbPixel(int red , int green , int blue)
            {
                this.red = red;
                this.green = green;
                this.blue = blue;
            }
        }

        RGBPixel[,] ImageMatrix;
        HashSet<RGBPixel> hashcolors = new HashSet<RGBPixel>();
        SortedSet<int> uniqeColors = new SortedSet<int>();
        List<Edge> edges = new List<Edge>();
        List<RGBPixel> ucolors = new List<RGBPixel>();
        int height;
        int width;
        public static rgbPixel IntToRGB (int value)
        {
            var red = (value >> 0) & 255;
            var green = (value >> 8) & 255;
            var blue = (value >>16) & 255 ;
            return new rgbPixel(red, green, blue);

        }
        public int RGBToInt (int r , int g , int b)
        {
            return (r << 0) | (g << 8) | (b << 16);
        }

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
            int maskSize = (int)nudMaskSize.Value ;
            //********************

            // STEP[1] Put Uniqe Colors in set "uniqeColoes"
           
            height = ImageOperations.GetHeight(ImageMatrix);
            width = ImageOperations.GetWidth(ImageMatrix);

            //TIME : O(H*W)
            //SPACE : O(D^2)
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < height;i++ )
            {
                for (int j = 0 ; j <width;j++)
                {
                    int value = RGBToInt(ImageMatrix[i, j].red, ImageMatrix[i, j].green, ImageMatrix[i, j].blue);
                    uniqeColors.Add(value);
                }
            }
            stopwatch.Stop();
            TimeSpan stopwatchElapsed = stopwatch.Elapsed;
            MessageBox.Show((Convert.ToInt32(stopwatchElapsed.TotalSeconds)).ToString());
            

            //TIME: Exact SIZE OF HASHSET
            Stopwatch s2 = new Stopwatch();
            s2.Start();
            foreach (var c in uniqeColors)
            {
                rgbPixel rgb = IntToRGB(c);
                RGBPixel rgbcolor = new RGBPixel();
                rgbcolor.red = int.Parse(rgb);

                //ucolors.Add();
            }
            s2.Stop();
            TimeSpan se2 = s2.Elapsed;
            MessageBox.Show((Convert.ToInt32(se2.TotalSeconds)).ToString());
           /*
            // STEP[2] Get Distance between each color 
            Edge edge ;
            double cost=0;
            double sum = 0;
            int dred, dgreen, dblue; // contain deffrence 
            int size = ucolors.Count;

            //TIME O(SIZE^2)
            Stopwatch s3 = new Stopwatch();
            s3.Start();
            for (int i = 0; i < size; i++)
            {
                for (int j = i+1; j < size; j++)
                {
                    dred = ucolors[i].red - ucolors[j].red;
                    dgreen = ucolors[i].green - ucolors[j].green;
                    dblue = ucolors[i].blue - ucolors[j].blue;
                    sum = (dred * dred) + (dgreen * dgreen) + (dblue * dblue);
                    cost = Math.Sqrt(sum);
                    edge = new Edge(i, j, cost);
                    edges.Add(edge);
                }
            }
            s3.Stop();
            TimeSpan se3 = s3.Elapsed;
            MessageBox.Show((Convert.ToInt32(se3.TotalSeconds)).ToString());
            */

                //********************
                ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

       
       
    }
}
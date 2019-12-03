using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
            for (int i = 0; i < height;i++ )
            for (int j = 0; j < width; j++)
            {
                int value = RGBToInt(ImageMatrix[i, j].red, ImageMatrix[i, j].green, ImageMatrix[i, j].blue);
                uniqeColors.Add(value);
            }

            //TIME: Exact SIZE OF HASHSET
            foreach (var c in uniqeColors)
            {
                rgbPixel rgb = IntToRGB(c);
                RGBPixel rgbcolor = new RGBPixel();
                rgbcolor.red = (byte)(rgb.red);
                rgbcolor.green = (byte)(rgb.green);
                rgbcolor.blue = (byte)(rgb.blue);

                ucolors.Add(rgbcolor);
            }
           
            // STEP[2] Get Distance between each color 
            Edge edge ;
            double cost=0;
            double sum = 0;
            int dred, dgreen, dblue; // contain deffrence 
            int size = ucolors.Count;

            //TIME O(SIZE^2)
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

                //********************
                ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

       
       
    }
}
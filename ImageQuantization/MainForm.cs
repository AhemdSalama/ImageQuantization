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
            ImageMatrix = ImageOperations.GaussianFilter1D(ImageMatrix, maskSize, sigma);
            ImageOperations.DisplayImage(ImageMatrix, pictureBox2);
        }

        // Return the function with the smallest cost
        


        private void btnQ_Click(object sender, EventArgs e)
        {
            var distTime = new Stopwatch();
            distTime.Start();
            var distinctColors = CountDistinctColors();
            distTime.Stop();
            txtDistinctColors.Text = distinctColors.Count.ToString();
            txtDistinctColorsTime.Text = distTime.Elapsed.ToString();

            var mstTime = new Stopwatch();
            mstTime.Start();
            var prim = new Prim(distinctColors.Count);
            var ans = prim.MstPrim(distinctColors);
            mstTime.Stop();
            txtMstCost.Text = ans.ToString();
            txtMstCostTime.Text = mstTime.Elapsed.ToString();
        }

        private List<RgbPixel> CountDistinctColors()
        {
            var uniqColors = new SortedSet<int>();
            var uColors = new List<RgbPixel>();

            foreach (var pixel in ImageMatrix)
            {
                var color = RgbPixel.ConvertToRgbPixel(pixel).RGBToInt();
                uniqColors.Add(color);
            }

            foreach (var uniqColor in uniqColors)
            {
                var color = RgbPixel.IntToRGB(uniqColor);
                uColors.Add(color);
            }

            return uColors;
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ImageQuantization
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }
        

        //***************************************************************
        // DATA DECLERATION 
        //***************************************************************
        RGBPixel[,] ImageMatrix;        // our picture
        private List<Edge> edges = new List<Edge>();

        //**************************************************************
        

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

            var distinctColors = CountDistinctColors();

            var prim = new Prim(distinctColors.Count);
            var ans = prim.MstPrim(distinctColors);

        }

        private List<RgbPixel> CountDistinctColors()
        {
            var uniqeColors = new SortedSet<int>();
            var uColors = new List<RgbPixel>();

            foreach (var pixel in ImageMatrix)
            {
                var color = RgbPixel.ConvertToRgbPixel(pixel).RGBToInt();
                uniqeColors.Add(color);
            }

            foreach (var uniqeColor in uniqeColors)
            {
                var color = RgbPixel.IntToRGB(uniqeColor);
                uColors.Add(color);
            }

            return uColors;
        }
    }
}
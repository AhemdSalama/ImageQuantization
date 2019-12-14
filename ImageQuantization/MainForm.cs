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

            var distinctColors = CountDistinctColors(); // Exact(N*M)
            var prim = new Prim(distinctColors.Count);    // Exact(1)
            var ans = prim.MstPrim(distinctColors);      // Exact(V^2) 
        
        }
        private List<RgbPixel> CountDistinctColors()  // Max between [ Exact(N*M) ,  Exact(#Unique Colors) ] -> Exact(N*M)
        {
            var uniqeColors = new SortedSet<int>(); // Exact(1)
            var uColors = new List<RgbPixel>();     // Exact(1)

            // Exact(N*M)
            foreach (var pixel in ImageMatrix)     // Exact(N*M) * (Body)
            {
                var color = RgbPixel.ConvertToRgbPixel(pixel).RGBToInt();    // Exact(1)
                uniqeColors.Add(color);   // Exact(1)
            }   //Body -> Exact(1)

            // Exact(#Unique Colors)
            foreach (var uniqeColor in uniqeColors)    // Exact(#Unique Colors) * Body
            {
                var color = RgbPixel.IntToRGB(uniqeColor);    // Exact(1)
                uColors.Add(color);   // Exact(1)
            }     //Body -> Exact(1)

            return uColors;  // Exact(1)
        }
    }
}
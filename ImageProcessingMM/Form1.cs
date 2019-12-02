using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ImageProcessingMM.EngineClasses;
using System.Windows.Forms.DataVisualization.Charting;

namespace ImageProcessingMM
{
    public partial class Form1 : Form
    {
       
        ImageProcessingEngine imageProcessingEngine { get; set; }
        Boolean isGrey { get; set; }

        
        
        public Form1()
        {
            InitializeComponent();
            isGrey = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            isGrey = false;
            var fileContent = string.Empty;
            var filePath = string.Empty;
            Image image;

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "BMP files (*.bmp)|*.bmp|PNG files (*.png)|*.png|JPG files (*.jpg)|*.jpg|JPEG files (*.jpeg)|*.jpeg|GIF files (*.gif)|*.gif";
                openFileDialog.FilterIndex = 0;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path
                    filePath = openFileDialog.FileName;
                    var fileStream = openFileDialog.OpenFile();
                    ImageBox.ImageLocation = filePath;
                    

                    ImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    imageProcessingEngine = new ImageProcessingEngine(fileStream);
                    imageProcessingEngine.generateHistogram();
                    ImagePostBox.Image = null;
                }

            }

        
            
        }

        private void ShowCharts_Click(object sender, EventArgs e)
        {
            HistogramData histogramData = imageProcessingEngine.hitogramsData;


            RedChannel.Series.Clear();

            var seriesRed = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = isGrey ? "seriesGray" : "seriesRed",
                Color = isGrey ? Color.Gray : System.Drawing.Color.Red,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };
          
            var seriesGreen = new Series
            {
                Name = "seriesGreen",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var seriesBlue = new Series
            {
                Name = "seriesBlue",
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            RedChannel.Series.Add(seriesRed);
            if (!isGrey)
            {
                RedChannel.Series.Add(seriesGreen);
                RedChannel.Series.Add(seriesBlue);
            }
        
            for (int i = 0; i < 256; i++)
            {
                seriesRed.Points.AddXY(i, histogramData.redHisto[i]);
                if (!isGrey)
                {
                    seriesGreen.Points.AddXY(i, histogramData.greenHisto[i]);
                    seriesBlue.Points.AddXY(i, histogramData.blueHisto[i]);
                }
            }
            

            ChartGroup.Visible = true;

            
        }

        private void RedChannel_Click(object sender, EventArgs e)
        {

        }

        private void GreyBtn_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.convertGrey();
            ImageBox.Image = Image.FromHbitmap(imageProcessingEngine.getPreImage());
            isGrey = true;
        }

        private void NormalizeBtn_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.generateDistribution();
            imageProcessingEngine.normalizeHistogram();
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void ShowPostCharts_Click(object sender, EventArgs e)
        {
            HistogramData histogramData = imageProcessingEngine.hitogramsData;


            RedChannel.Series.Clear();


            var seriesRed = new System.Windows.Forms.DataVisualization.Charting.Series
            {
                Name = isGrey ? "seriesGray" : "seriesRed",
                Color = isGrey ? Color.Gray : System.Drawing.Color.Red,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var seriesGreen = new Series
            {
                Name = "seriesGreen",
                Color = System.Drawing.Color.Green,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            var seriesBlue = new Series
            {
                Name = "seriesBlue",
                Color = System.Drawing.Color.Blue,
                IsVisibleInLegend = true,
                IsXValueIndexed = true,
                ChartType = SeriesChartType.Column
            };

            RedChannel.Series.Add(seriesRed);
            if (!isGrey)
            {
                RedChannel.Series.Add(seriesGreen);
                RedChannel.Series.Add(seriesBlue);
            }

            for (int i = 0; i < 256; i++)
            {
                seriesRed.Points.AddXY(i, histogramData.histoTransformed[i]);
                if (!isGrey)
                {
                    seriesGreen.Points.AddXY(i, histogramData.greenHisto[i]);
                    seriesBlue.Points.AddXY(i, histogramData.blueHisto[i]);
                }
            }


            ChartGroup.Visible = true;
        }

        private void ScretchBtn_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.stretchHistogram();
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void NeagteBtn_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.imageNegationLinear();
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            trackBar.Value = (int)trackBarUpDown.Value;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            trackBarUpDown.Value = trackBar.Value;
           
        }

        private void TresholdBtn_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.thresholding(trackBar.Value);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void tresholdGray_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.thresholdingWithGreyScale((int)lowThershold.Value, (int)highTreshold.Value);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void tresholdNeg_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.thresholdingWithGreyScaleNegation((int)lowThershold.Value, (int)highTreshold.Value);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
           
            imageProcessingEngine.stretchingWithRange((int)lowThershold.Value, (int)highTreshold.Value);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Posterization_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.posterization((int)trackBarUpDown.Value);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] maskA = MASK.Text.Split(' ');
            int[] mask = maskA.Select(x => Convert.ToInt32(x)).ToArray();
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void Smoth014_Click(object sender, EventArgs e)
        {
            int[] mask = { 0, 1, 0, 1, 4, 1, 0, 1, 0 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth111_Click(object sender, EventArgs e)
        {
            int[] mask = { 1, 1, 1, 1, 1, 1, 1, 1, 1};
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth121_Click(object sender, EventArgs e)
        {
            int[] mask = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth1k_Click(object sender, EventArgs e)
        {
            int[] mask = { 1, 1, 1, 1, (int)KValue.Value, 1, 1, 1, 1 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth0k_Click(object sender, EventArgs e)
        {
            int[] mask = { 0, 1, 0, 1, (int)KValue.Value, 1, 0, 1, 0 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin14_Click(object sender, EventArgs e)
        {
            int[] mask = { 0, -1, 0, -1, 4, -1, 0, -1, 0 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin18_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -1, -1, -1, 8, -1, -1, -1, -1 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Det1Min24_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -2, -1, -1, 4, -1, -1, -2, -1 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin19_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin15_Click(object sender, EventArgs e)
        {
            int[] mask = { 0, -1, 0, -1, 5, -1, 0, -1, 0 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin215_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -2, -1, -1, 5, -1, -1, -2, -1 };
            imageProcessingEngine.neighborhoodOperation(mask);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.medianOperation(5);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }
    }
}

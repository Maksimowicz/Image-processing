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
using ImageProcessingMM.DataTypes;

using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using Emgu.CV.XImgproc;
using System.Drawing.Imaging;
using System.IO;

namespace ImageProcessingMM
{
    public partial class Form1 : Form
    {
        Image<Bgr, byte> imageEmgu;
        ImageProcessingEngine imageProcessingEngine { get; set; }
        Boolean isGrey { get; set; }
        string filePathInner { get; set; }
        SccalingMethod scallingMethod { get; set; }
        KernelMethod kernelMethod { get; set; }
        ElementShape elementShapee { get; set; }
        int trackBarValue { get; set; }
        
        public Form1()
        {
            InitializeComponent();
            isGrey = false;

            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Graphics gr = Graphics.FromImage(ImageBox.Image);
            //using (Pen pen = new Pen(Color.Green, 2))
            //{
            //    gr.Draw
            //}
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
                    //Dilation added using emguCV
                    imageEmgu = new Image<Bgr, byte>(filePath);
                    filePathInner = filePath;

                    ImageBox.SizeMode = PictureBoxSizeMode.StretchImage;
                    imageProcessingEngine = new ImageProcessingEngine(fileStream);
                    imageProcessingEngine.generateHistogram();
                    ImagePostBox.Image = null;

                  //  ImageBox.Paint += 
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
            trackBarValue = (int)trackBar.Value;
        }

        private void trackBar_Scroll(object sender, EventArgs e)
        {
            trackBarUpDown.Value = trackBar.Value;
            trackBarValue = (int)trackBar.Value;
           
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
            imageProcessingEngine.neighborhoodOperation(mask,kernelMethod,scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth111_Click(object sender, EventArgs e)
        {
            int[] mask = { 1, 1, 1, 1, 1, 1, 1, 1, 1};
            imageProcessingEngine.neighborhoodOperation(mask,kernelMethod,scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth121_Click(object sender, EventArgs e)
        {
            int[] mask = { 1, 2, 1, 2, 4, 2, 1, 2, 1 };
            imageProcessingEngine.neighborhoodOperation(mask,kernelMethod,scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth1k_Click(object sender, EventArgs e)
        {
            int[] mask = { 1, 1, 1, 1, (int)KValue.Value, 1, 1, 1, 1 };
            imageProcessingEngine.neighborhoodOperation(mask,kernelMethod,scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Smoth0k_Click(object sender, EventArgs e)
        {
            int[] mask = { 0, 1, 0, 1, (int)KValue.Value, 1, 0, 1, 0 };
            imageProcessingEngine.neighborhoodOperation(mask,kernelMethod,scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin14_Click(object sender, EventArgs e)
        {
            int[] mask = { 0, -1, 0, -1, 4, -1, 0, -1, 0 };
            imageProcessingEngine.neighborhoodOperation(mask,kernelMethod, scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin18_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -1, -1, -1, 8, -1, -1, -1, -1 };
            imageProcessingEngine.neighborhoodOperation(mask, kernelMethod, scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Det1Min24_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -2, -1, -1, 4, -1, -1, -2, -1 };
            imageProcessingEngine.neighborhoodOperation(mask, kernelMethod, scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin19_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -1, -1, -1, 9, -1, -1, -1, -1 };
            imageProcessingEngine.neighborhoodOperation(mask, kernelMethod, scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin15_Click(object sender, EventArgs e)
        {
            int[] mask = { 0, -1, 0, -1, 5, -1, 0, -1, 0 };
            imageProcessingEngine.neighborhoodOperation(mask, kernelMethod, scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void DetMin215_Click(object sender, EventArgs e)
        {
            int[] mask = { -1, -2, -1, -1, 5, -1, -1, -2, -1 };
            imageProcessingEngine.neighborhoodOperation(mask, kernelMethod, scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int medianSize = (int)trackBarUpDown.Value;
            imageProcessingEngine.medianOperation(medianSize,kernelMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.robertsOperation(kernelMethod, scallingMethod);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.sobelPrewwitOperation(kernelMethod, scallingMethod, DataTypes.DirectionEdgeMask.Prewwit);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            imageProcessingEngine.sobelPrewwitOperation(kernelMethod, scallingMethod, DataTypes.DirectionEdgeMask.Sobel);
            ImagePostBox.Image = Image.FromHbitmap(imageProcessingEngine.getPostImage());
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }


        //Buttons with EmguCV, will be replaced with own implementation and Emgu to compare results and controls
        private void DilatationBtn_Click(object sender, EventArgs e)
        {
            Mat mat = imageEmgu.Mat;
            Mat metGrey = new Mat();
            Mat metGeryOutput = new Mat();
            BorderType borderType = BorderType.Default;

            switch(kernelMethod)
            {
                case KernelMethod.CloneBorder:
                    borderType = BorderType.Replicate;
                    break;
                case KernelMethod.NoBorders:
                    borderType = BorderType.Default;
                    break;
                case KernelMethod.UseExisting:
                    borderType = BorderType.Isolated;
                    break;
            }


            ElementShape elementShape = elementShapee;



            CvInvoke.CvtColor(mat, metGrey, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            Mat structuringElement = CvInvoke.GetStructuringElement(elementShape, new Size(3, 3), new Point(1, 1));
         
            CvInvoke.Dilate(metGrey, metGeryOutput, structuringElement, new System.Drawing.Point(1, 1), trackBarValue, borderType, CvInvoke.MorphologyDefaultBorderValue);
            ImagePostBox.Image = metGeryOutput.ToImage<Bgr, byte>().Bitmap;
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;

        }

        private void Erode_Click(object sender, EventArgs e)
        {
            Mat mat = imageEmgu.Mat;
            Mat metGrey = new Mat();
            Mat metGeryOutput = new Mat();
            BorderType borderType = BorderType.Default;
            ElementShape elementShape = elementShapee;

            switch (kernelMethod)
            {
                case KernelMethod.CloneBorder:
                    borderType = BorderType.Replicate;
                    break;
                case KernelMethod.NoBorders:
                    borderType = BorderType.Default;
                    break;
                case KernelMethod.UseExisting:
                    borderType = BorderType.Isolated;
                    break;
            }

            CvInvoke.CvtColor(mat, metGrey, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            Mat structuringElement = CvInvoke.GetStructuringElement(elementShape, new Size(3, 3), new Point(1, 1));

            CvInvoke.Erode(metGrey, metGeryOutput, structuringElement, new System.Drawing.Point(1, 1), trackBarValue, borderType, CvInvoke.MorphologyDefaultBorderValue);
            ImagePostBox.Image = metGeryOutput.ToImage<Bgr, byte>().Bitmap;
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Close_Click(object sender, EventArgs e)
        {
            Mat mat = imageEmgu.Mat;
            Mat metGrey = new Mat();
            Mat metGeryOutput = new Mat();
            BorderType borderType = BorderType.Default;
            ElementShape elementShape = elementShapee;


            switch (kernelMethod)
            {
                case KernelMethod.CloneBorder:
                    borderType = BorderType.Replicate;
                    break;
                case KernelMethod.NoBorders:
                    borderType = BorderType.Default;
                    break;
                case KernelMethod.UseExisting:
                    borderType = BorderType.Isolated;
                    break;
            }

            CvInvoke.CvtColor(mat, metGrey, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            Mat structuringElement = CvInvoke.GetStructuringElement(elementShape, new Size(3, 3), new Point(1, 1));

            CvInvoke.MorphologyEx(metGrey, metGeryOutput, MorphOp.Close, structuringElement, new System.Drawing.Point(1, 1), trackBarValue, borderType, CvInvoke.MorphologyDefaultBorderValue);
            ImagePostBox.Image = metGeryOutput.ToImage<Bgr, byte>().Bitmap;
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Open_Click(object sender, EventArgs e)
        {
            Mat mat = imageEmgu.Mat;
            Mat metGrey = new Mat();
            Mat metGeryOutput = new Mat();
            BorderType borderType = BorderType.Default;
            ElementShape elementShape = elementShapee;

            switch (kernelMethod)
            {
                case KernelMethod.CloneBorder:
                    borderType = BorderType.Replicate;
                    break;
                case KernelMethod.NoBorders:
                    borderType = BorderType.Default;
                    break;
                case KernelMethod.UseExisting:
                    borderType = BorderType.Isolated;
                    break;
            }

            CvInvoke.CvtColor(mat, metGrey, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            Mat structuringElement = CvInvoke.GetStructuringElement(elementShape, new Size(3, 3), new Point(1, 1));

            CvInvoke.MorphologyEx(metGrey, metGeryOutput, MorphOp.Open, structuringElement, new System.Drawing.Point(1, 1), trackBarValue, borderType, CvInvoke.MorphologyDefaultBorderValue);
            ImagePostBox.Image = metGeryOutput.ToImage<Bgr, byte>().Bitmap;
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void Thinning_Click(object sender, EventArgs e)
        {
            Mat mat = imageEmgu.Mat;
            Mat metGrey = imageEmgu.Mat;
            Mat metGeryOutput = new Mat();

            ElementShape elementShape = elementShapee;


            CvInvoke.CvtColor(mat, metGrey, Emgu.CV.CvEnum.ColorConversion.Bgr2Gray);

            Mat structuringElement = CvInvoke.GetStructuringElement(elementShape, new Size(3, 3), new Point(1, 1));

            XImgprocInvoke.Thinning(metGrey, metGeryOutput, ThinningTypes.GuoHall);
           // CvInvoke.MorphologyEx(metGrey, metGeryOutput, MorphOp.Open, structuringElement, new System.Drawing.Point(1, 1), 2, BorderType.Isolated, CvInvoke.MorphologyDefaultBorderValue);
            ImagePostBox.Image = metGeryOutput.ToImage<Bgr, byte>().Bitmap;
            ImagePostBox.SizeMode = PictureBoxSizeMode.StretchImage;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox1.SelectedIndex)
            {
                case 0:
                    scallingMethod = SccalingMethod.Scale;
                    break;
                case 1:
                    scallingMethod = SccalingMethod.Cut;
                    break;
                case 2:
                    scallingMethod = SccalingMethod.TriValue;
                    break;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox2.SelectedIndex)
            {
                case 0:
                    kernelMethod = KernelMethod.UseExisting;
                    break;
                case 1:
                    kernelMethod = KernelMethod.CloneBorder;
                    break;
                case 2:
                    kernelMethod = KernelMethod.NoBorders;
                    break;
            }
        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch(comboBox3.SelectedIndex)
            {
                case 0:
                    elementShapee = ElementShape.Cross;
                    break;
                case 1:
                    elementShapee = ElementShape.Rectangle;
                    break;
                    
            }
        }

        private void saveBtn_Click(object sender, EventArgs e)
        {
            string path = Directory.GetCurrentDirectory() + @"\postImage.jpeg";
            ImagePostBox.Image.Save(path, ImageFormat.Jpeg);
        }
    }
}

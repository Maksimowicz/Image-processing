using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;



namespace ImageProcessingMM.EngineClasses
{

    public enum KernelMethod
    {
        NoBorders,
        CloneBorder,
        UseExisting
    };



    public class HistogramData
    {
        public int[] redHisto { get; set; }
        public int[] greenHisto { get; set; }
        public int[] blueHisto { get; set; }

 

        public int[] histoTransformed { get; set; }
        public float[] histoCumulative { get; set; }
        public Boolean isMonochromatic { get; set; }

        
        public HistogramData()
        {
            redHisto = new int[256];
            greenHisto = new int[256];
            blueHisto = new int[256];

            histoTransformed = new int[256];
            histoCumulative = new float[256];
        }
    }

    public class MaskRep
    {
        int[] mask;
        string name;

        MaskRep(int[] mask, string name)
        {
            this.mask = mask;
            this.name = name;
        }
    }



    public class ImageProcessingEngine
    {
        private Image image { get; set; }
        //private Bitmap bitmapPre { get; set; }
        //private Bitmap bitmapPost { get; set; }
        int pixelAmount { get; set; }

        public HistogramData hitogramsData { get; set; }

        DirectBitmap directBitmapPre;
        DirectBitmap directBitmapPost;





        public ImageProcessingEngine(Stream stream)
        {
            image = Image.FromStream(stream);
            Bitmap innerBitmap = new Bitmap(image);

            directBitmapPre = new DirectBitmap(innerBitmap);
            directBitmapPost = new DirectBitmap(innerBitmap);

            hitogramsData = new HistogramData();

            pixelAmount = directBitmapPre.Height * directBitmapPre.Width;
        }


        public HistogramData generateHistogram()
        {
            hitogramsData = new HistogramData();

            if (directBitmapPre.Bitmap.PixelFormat == PixelFormat.Format16bppGrayScale)
            {
                hitogramsData.isMonochromatic = true;
            }

            Color helpPixel;

            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    helpPixel = directBitmapPre.GetPixel(x, y);
                    hitogramsData.redHisto[helpPixel.R]++;
                    hitogramsData.blueHisto[helpPixel.B]++;
                    hitogramsData.greenHisto[helpPixel.G]++;
                }
            }

            return hitogramsData;
        }


        public IntPtr getPreImage()
        {
            return directBitmapPre.Bitmap.GetHbitmap();
        }

        public IntPtr getPostImage()
        {
            return directBitmapPost.Bitmap.GetHbitmap();
        }


        public void convertGrey()
        {
            int resultPixelValue = 0;

            int[] newRedHisto = new int[256];
            int[] newGreenHisto = new int[256];
            int[] newBlueHisto = new int[256];

            Color helpPixel;

            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    helpPixel = directBitmapPre.GetPixel(x, y);
                    resultPixelValue = (helpPixel.R + helpPixel.G + helpPixel.B) / 3;

                    directBitmapPre.SetPixel(x, y, Color.FromArgb(resultPixelValue, resultPixelValue, resultPixelValue));

                    newRedHisto[resultPixelValue]++;
                    newGreenHisto[resultPixelValue]++;
                    newBlueHisto[resultPixelValue]++;

                }


            }

            hitogramsData.redHisto = newRedHisto;
            hitogramsData.greenHisto = newGreenHisto;
            hitogramsData.blueHisto = newBlueHisto;


        }


        public void generateDistribution()
        {
            int sum = 0;


            for (int i = 0; i < 256; i++)
            {
                sum += hitogramsData.redHisto[i];
                hitogramsData.histoCumulative[i] = (float)sum / pixelAmount;
            }
        }
        public void normalizeHistogram()
        {
            int calculatedPixel = 0;
            int firstNonZeroIndex = Array.FindIndex(hitogramsData.histoCumulative, x => x > 0);
            int[] LUT = new int[256];
            int pixels = 0;
            float valueOffirstNonZeroIndex = hitogramsData.histoCumulative[firstNonZeroIndex];

            Color halp;

            for (int i = 0; i < 256; i++)
            {
                LUT[i] = (int)(((hitogramsData.histoCumulative[i] - valueOffirstNonZeroIndex) / (1 - valueOffirstNonZeroIndex)) * 255);
            }

            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = LUT[halp.R];
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    pixels++;
                }
            }

        }

        public void stretchHistogram()
        {
            int calculatedPixel = 0;

            int[] LUT = new int[256];
            int pixels = 0;


            int minIndex = directBitmapPre.getMin();
            int maxIndex = directBitmapPre.getMax();

            Color halp;

            for (int i = 0; i < 256; ++i)
            {
                LUT[i] = (int)(255 / (maxIndex - minIndex)) * (i - minIndex);
            }

            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = LUT[halp.R];
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    pixels++;
                }
            }

        }


        public void imageNegationLinear()
        {
            Color halp;
            int calculatedPixel;
            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = 255 - halp.R;
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    // pixels++;
                }
            }
        }


        public void thresholding(int threshold)
        {
            Color halp;
            int calculatedPixel;
            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = halp.R > threshold ? 255 : 0;
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    // pixels++;
                }
            }

        }

        public void thresholdingWithGreyScale(int thresholdMin, int thresholdMax)
        {
            Color halp;
            int calculatedPixel;
            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = halp.R < thresholdMin || halp.R > thresholdMax ? 0 : halp.R;
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    // pixels++;
                }
            }

        }


        public void thresholdingWithGreyScaleNegation(int thresholdMin, int thresholdMax)
        {
            Color halp;
            int calculatedPixel;
            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = halp.R < thresholdMin || halp.R > thresholdMax ? 0 : 255 - halp.R;
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    // pixels++;
                }
            }

        }

        public void stretchingWithRange(int lMin, int lMax)
        {
            Color halp;
            int min = directBitmapPre.getMin();
            int max = directBitmapPre.getMax();
            int calculatedPixel;
            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = (halp.R - min) * ((lMax - lMin) / (max - min)) + lMin;
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    // pixels++;
                }
            }

        }

        public void posterization(int levels)
        {
            Color halp;
            int[] LUT = new int[256];
            int stepL = (int)Math.Floor((decimal)(256 / (levels - 1)));
            int stepW = (int)Math.Ceiling((decimal)(256 / levels));

            int calculatedPixel;

            int lower = 0;
            int higher = 0;

            for (int i = 0; i < 256; ++i)
            {
                LUT[i] = 255;
            }

            for (int i = 0; i < levels; i++)
            {

                for (int j = stepW * i; j < stepW * (i + 1); ++j)
                {

                    LUT[j] = stepL * i;
                    if (LUT[j] > 255)
                    {
                        LUT[j] = 255;
                    }
                }
            }


            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = LUT[halp.R];
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    // pixels++;
                }
            }
        }


        public void posterizationPost(int levels)
        {
            Color halp;
            int[] LUT = new int[256];
            int stepL = (int)Math.Floor((decimal)(256 / (levels - 1)));
            int stepW = (int)Math.Ceiling((decimal)(256 / levels));

            int calculatedPixel;

            int lower = 0;
            int higher = 0;

            for (int i = 0; i < 256; ++i)
            {
                LUT[i] = 255;
            }

            for (int i = 0; i < levels; i++)
            {

                for (int j = stepW * i; j < stepW * (i + 1); ++j)
                {

                    LUT[j] = stepL * i;
                    if (LUT[j] > 255)
                    {
                        LUT[j] = 255;
                    }
                }
            }


            for (int x = 0; x < directBitmapPre.Width; ++x)
            {
                for (int y = 0; y < directBitmapPre.Height; ++y)
                {
                    //x columns
                    //y rows
                    halp = directBitmapPre.GetPixel(x, y);

                    calculatedPixel = LUT[halp.R];
                    hitogramsData.histoTransformed[calculatedPixel]++;
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    // pixels++;
                }
            }
        }



        public void neighborhoodOperation(int[] mask, KernelMethod _method = KernelMethod.NoBorders)
        {
            int maskSize = (int)Math.Sqrt(mask.Length);
            int calculatedPixel = 0;
            int maskSum = mask.Min() < 0 ? 1 : mask.Sum();
            int maskValue;

            int xo;
            int yo;


           // int xOnStart = 

            Color helpColor;

            int overlap = maskSize / 2;

            //Iterate through image
            for (int x = overlap; x < directBitmapPre.Width - overlap; ++x)
            {
                for (int y = overlap; y < directBitmapPre.Height - overlap; ++y)
                {
                    //Iterate through image

                    //Iterate through mask
                    for (int xi = 0; xi < maskSize; xi++)
                    {
                        for (int yi = 0; yi < maskSize; yi++)
                        {

                            maskValue = mask[xi + (yi * maskSize)];
                            xo = x + xi - overlap;
                            yo = y + yi - overlap;

                            calculatedPixel += (directBitmapPre.GetPixel(x + xi - overlap, y + yi - overlap).R * mask[xi + (yi * maskSize)]);


                        }
                    }


                    calculatedPixel /= maskSum;
                    if (calculatedPixel > 255)
                    {
                        calculatedPixel = 255;
                    }
                    else if (calculatedPixel < 0)
                    {
                        calculatedPixel = 0;
                    }
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                    calculatedPixel = 0;


                }
            }

        }

        public void medianOperation(int maskSize)
        {

            int[] maskObject = new int[maskSize * maskSize];
            Array.Sort(maskObject);
            int calculatedPixel;
            int elementttt;


            Color helpColor;

            int overlap = maskSize / 2;

            //Iterate through image
            for (int x = overlap; x < directBitmapPre.Width - overlap; ++x)
            {
                for (int y = overlap; y < directBitmapPre.Height - overlap; ++y)
                {
                    //Iterate through image

                    //Iterate through mask
                    for (int xi = 0; xi < maskSize; xi++)
                    {
                        for (int yi = 0; yi < maskSize; yi++)
                        {
                            maskObject[xi + (yi * maskSize)] = directBitmapPre.GetPixel(x + xi - overlap, y + yi - overlap).R;
                        }
                    }

                    Array.Sort(maskObject);

                    elementttt = maskSize*maskSize / 2;
                    calculatedPixel = maskObject[elementttt];

                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                   


                }
            }












        }
    }
}

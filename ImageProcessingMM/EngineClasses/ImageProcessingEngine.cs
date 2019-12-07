using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;
using ImageProcessingMM.LINQExtensions;
using ImageProcessingMM.DataTypes;

//TODO: REFEACTOR DAT 

namespace ImageProcessingMM.EngineClasses
{

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

        Image image;
        public static int[] RobertsFirst = { 1, 0, 0, -1 };
        public static int[] RobertsSecond = { 0, -1, 1, 0 };

        public static int[] SobelFirst = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };
        public static int[] SobelSecond = { -1, 0, 1, -2, 0, 2, -1, 0, 1 };

        public static int[] PrewwitFirst = { 1, 0, -1, 1, 0, -1, 1, 0, -1 };
        public static int[] PrewwitSecond = { 1, 1, 1, 0, 0, 0, -1, -1, -1 };
       
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


        private int ScalePixelValue(int pixel, SccalingMethod method, int min = 0, int max = 255)
        {
            int returnValue = 0;

            switch (method)
            {
                case SccalingMethod.Cut:
                    if (pixel > 255)
                    {
                        returnValue = 255;
                    }
                    else if (pixel < 0)
                    {
                        returnValue = 0;
                    }
                  
                    break;

                case SccalingMethod.Scale:
                    returnValue = (int)(((decimal)(pixel - min) / (decimal)(max - min)) * 255);
                    break;

                case SccalingMethod.TriValue:
                    if (pixel > 255)
                    {
                        returnValue = 255;
                    }
                    else if (pixel < 0)
                    {
                        returnValue = 0;
                    }
                    else
                    {
                        returnValue = 128; // 255/2
                    }
                    break;
            }

            return returnValue;
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



        public void neighborhoodOperation(int[] mask, KernelMethod _method = KernelMethod.NoBorders, SccalingMethod sccalingMethod = SccalingMethod.Cut, Boolean directionEdge = false)
        {
            int maskSize = (int)Math.Sqrt(mask.Length);
            int calculatedPixel = 0;
            int maskSum = mask.Min() < 0 ? 1 : mask.Sum();
            int maskSumInner = 0;

            int[] bitMapToScale = null;
            if (sccalingMethod == SccalingMethod.Scale)
            {
                bitMapToScale = new int[directBitmapPre.Height * directBitmapPre.Width];
            }



            int minPixel = directBitmapPre.getMin();
            int maxPixel = directBitmapPre.getMax();


            int maskValue;

            int xo;
            int yo;

            int lowerXOverlap = 0;
            int higherXOverlap = 0;
            int lowerYOverlap = 0;
            int higherYOverlap = 0;

            int yiInner = 0;

            int calculatedPixelIndexX = 0;
            int calculatedPixelIndexY = 0;

            Color helpColor;

            int overlap = 0; // = maskSize / 2;
            int maskOverlap = maskSize / 2;
            switch(_method)
            {
                case KernelMethod.NoBorders:
                    overlap = maskSize / 2;
                    break;
                case KernelMethod.CloneBorder:
                    overlap = 0;
                    break;
                case KernelMethod.UseExisting:
                    overlap = 0;
                    break;

            }
            


            //Iterate through image
            for (int x = overlap; x < directBitmapPre.Width - overlap; ++x)
            {
                for (int y = overlap; y < directBitmapPre.Height - overlap; ++y)
                {
                    //Iterate through image

                    //Iterate through mask
                    for (int xi = 0 ; xi < maskSize; xi++)
                    {
                        for (int yi = 0; yi < maskSize; yi++)
                        {

                            maskValue = mask[xi + (yi * maskSize)];
                            xo = x + xi - maskOverlap;
                            yo = y + yi - maskOverlap;

                            calculatedPixelIndexX = x + xi - maskOverlap;
                            calculatedPixelIndexY = y + yi - maskOverlap;

                            if((x == 0  && y ==0) || (x == directBitmapPre.Width && y == directBitmapPre.Height))
                            {
                                higherXOverlap = 0;
                            }

                             
                            if(_method == KernelMethod.UseExisting && (calculatedPixelIndexX < 0 || calculatedPixelIndexY < 0 || calculatedPixelIndexX >= directBitmapPre.Width || calculatedPixelIndexY >= directBitmapPre.Height))
                            {
                                continue;
                            }

                            if(_method == KernelMethod.CloneBorder && (calculatedPixelIndexX < 0 || calculatedPixelIndexY < 0 || calculatedPixelIndexX >= directBitmapPre.Width || calculatedPixelIndexY >= directBitmapPre.Height))
                            {
                                calculatedPixel += (directBitmapPre.GetPixel(x, y).R * mask[xi + (yi * maskSize)]);

                                continue;
                            }
                            maskSumInner += mask[xi + (yi * maskSize)];
                            calculatedPixel += (directBitmapPre.GetPixel(calculatedPixelIndexX, calculatedPixelIndexY).R * mask[xi + (yi * maskSize)]);


                        }
                    }

                    if (_method == KernelMethod.UseExisting)
                    {
                        calculatedPixel = maskSum == 1 ? calculatedPixel : calculatedPixel / maskSumInner;
                    }
                    else
                    {
                        calculatedPixel /= maskSum;
                    }

                    int newPixel = calculatedPixel;

                    if (sccalingMethod != SccalingMethod.Scale)
                    {
                        newPixel = this.ScalePixelValue(newPixel, sccalingMethod, sccalingMethod == SccalingMethod.Scale ? minPixel : 0, sccalingMethod == SccalingMethod.Scale ? maxPixel : 255);

                    }


                    if (sccalingMethod != SccalingMethod.Scale)
                    {
                        directBitmapPost.SetPixel(x, y, Color.FromArgb(newPixel, newPixel, newPixel));
                    }
                    else
                    {
                        bitMapToScale[x + (y * directBitmapPre.Width)] = newPixel;
                    }
                    maskSumInner = 0;
                    calculatedPixel = 0;
                }
            }

            if(sccalingMethod == SccalingMethod.Scale)
            {
                int minValue = bitMapToScale.Min();
                int maxValue = bitMapToScale.Max();
                int pixelScalled;
                List<int> checkList = new List<int>();



                for (int x = 0; x < directBitmapPre.Width - overlap; ++x)
                {
                    for (int y = 0; y < directBitmapPre.Height - overlap; ++y)
                    {
                        pixelScalled = (int)(((decimal)(bitMapToScale[x + (y * directBitmapPre.Width)] - minValue)/ (decimal)(maxValue - minValue)) * 255);
                        checkList.Add(pixelScalled);
                        directBitmapPost.SetPixel(x, y, Color.FromArgb(pixelScalled, pixelScalled, pixelScalled));
                    }
                }
            }

        }

        public void medianOperation(int maskSize, KernelMethod _method = KernelMethod.NoBorders)
        {
            List<int> maskList = null; 
            
            int[] maskObject = new int[maskSize * maskSize]; ///for easier median search using list

            //Array.Sort(maskObject);
            int calculatedPixel;
            int elementttt;
            int overlap = 0;
            int maskSizeInner = 0; //mask size used for UseExisting method

            switch (_method)
            {
                case KernelMethod.NoBorders:
                    overlap = maskSize / 2;
                    break;
                case KernelMethod.CloneBorder:
                    overlap = 0;
                    break;
                case KernelMethod.UseExisting:
                    overlap = 0;
                    break;

            }

            Color helpColor;
            int maskOverlap = maskSize / 2;

            int calculatedPixelXIndex = 0;
            int calculatedPixelYindex = 0;

            //Iterate through image
            for (int x = overlap; x < directBitmapPre.Width - overlap; ++x)
            {
                for (int y = overlap; y < directBitmapPre.Height - overlap; ++y)
                {
                    //Iterate through image

                    //Iterate through mask
                    maskList = new List<int>();
                    for (int xi = 0; xi < maskSize; xi++)
                    {
                        for (int yi = 0; yi < maskSize; yi++)
                        {
                            calculatedPixelXIndex = x + xi - maskOverlap;
                            calculatedPixelYindex = y + yi - maskOverlap;

                            if(_method == KernelMethod.UseExisting)
                            {
                                if ((calculatedPixelXIndex < 0 || calculatedPixelYindex < 0 || calculatedPixelXIndex >= directBitmapPre.Width || calculatedPixelYindex >= directBitmapPre.Height))
                                {
                                    continue;
                                }
                                else
                                {
                                    maskSizeInner++;
                                }
                               
                            }

                            if (_method == KernelMethod.CloneBorder && (calculatedPixelXIndex < 0 || calculatedPixelYindex < 0 || calculatedPixelXIndex >= directBitmapPre.Width || calculatedPixelYindex >= directBitmapPre.Height))
                            {
                                calculatedPixelXIndex = x;
                                calculatedPixelYindex = y;
                                
                            }

                            maskList.Add(directBitmapPre.GetPixel(calculatedPixelXIndex, calculatedPixelYindex).R);
                        }
                    }

                    calculatedPixel = maskList.Median();                   
                    directBitmapPost.SetPixel(x, y, Color.FromArgb(calculatedPixel, calculatedPixel, calculatedPixel));
                   
                }
            }












        }


        public void directEdgeOperation(DirectionEdgeMask directionEdgeMask, SccalingMethod sccalingMethod = SccalingMethod.Cut, KernelMethod kernelMethod = KernelMethod.NoBorders)
        {
            DirectBitmap directBitMapFinal = null;

            //Bitmaps for performing operations
            int[] bitmapPreScaleFirst = new int[directBitmapPre.Width * directBitmapPre.Height];
            int[] bitmapPreScaleSecond = new int[directBitmapPre.Width * directBitmapPre.Height];



            int[] maskFirst;
            int[] maskSecond;

            switch(directionEdgeMask)
            {
                case DirectionEdgeMask.Roberts:
                    maskFirst = ImageProcessingEngine.RobertsFirst;
                    maskSecond = ImageProcessingEngine.RobertsSecond;
                    
                    break;
                case DirectionEdgeMask.Sobel:
                    maskFirst = ImageProcessingEngine.SobelFirst;
                    maskSecond = ImageProcessingEngine.SobelSecond;
                    break;
                case DirectionEdgeMask.Prewwit:
                    maskFirst = ImageProcessingEngine.PrewwitFirst;
                    maskSecond = ImageProcessingEngine.PrewwitSecond;
                    break;
            }
   

        }

        public int[] robertsOperation(KernelMethod kernelMethod = KernelMethod.NoBorders, SccalingMethod sccalingMethod = SccalingMethod.Cut)
        {
            int[] bitmapPreScaleFirst = new int[directBitmapPre.Width * directBitmapPre.Height];
            int[] bitmapPreScaleSecond = new int[directBitmapPre.Width * directBitmapPre.Height];
            int[] resultImage = new int[directBitmapPre.Width * directBitmapPre.Height];

            int xBond = kernelMethod == KernelMethod.NoBorders ? 1 : 0;
            List<int> maskSumFirst;
            List<int> maskSumSecond;

            int calculatedX;
            int calculatedY;

            int[] maskFirst = ImageProcessingEngine.RobertsFirst;
            int[] maskSecond = ImageProcessingEngine.RobertsSecond; 



            for (int x = 0; x < directBitmapPre.Width - xBond; ++x)
            {
                for(int y = 0; y < directBitmapPre.Height - xBond; ++y)
                {
                    maskSumFirst = new List<int>();
                    maskSumSecond = new List<int>();

                    for(int xi = 0; xi < 2; xi++)
                    {
                        for(int yi = 0; yi < 2; yi++)
                        {
                            calculatedX = x + xi;
                            calculatedY = y + yi;

                            if (kernelMethod == KernelMethod.UseExisting)
                            {
                                if (calculatedX >= directBitmapPre.Width || calculatedY >= directBitmapPre.Height)
                                {
                                    continue;
                                }
                            }
                            else if(kernelMethod == KernelMethod.CloneBorder)
                            {
                                if(calculatedX >= directBitmapPre.Width || calculatedY >= directBitmapPre.Height)
                                {
                                    //mask[xi + (yi * maskSize)]
                                    maskSumFirst.Add(maskFirst[xi + (yi * 2)] * directBitmapPre.GetPixel(x, y).R);
                                    maskSumSecond.Add(maskSecond[xi + (yi * 2)] * directBitmapPre.GetPixel(x, y).R);
                                    continue;
                                }
                            }
                            maskSumFirst.Add(maskFirst[xi + (yi * 2)] * directBitmapPre.GetPixel(x+xi, y+yi).R);
                            maskSumSecond.Add(maskSecond[xi + (yi * 2)] * directBitmapPre.GetPixel(x+xi, y+yi).R);

                        }
                    }

                    bitmapPreScaleFirst[x +(y*directBitmapPre.Width)] = Math.Abs(maskSumFirst.Sum());
                    bitmapPreScaleSecond[x + (y * directBitmapPre.Width)] = Math.Abs(maskSumSecond.Sum());

                    
                }
            }

            int finalPixel = 0;
            for(int x = 0; x < directBitmapPre.Height; x++)
            {
                for(int y = 0; y < directBitmapPre.Width; y++)
                {
                    finalPixel = bitmapPreScaleFirst[x + (y * directBitmapPre.Width)] + bitmapPreScaleSecond[x + (y * directBitmapPre.Width)];
                    resultImage[x + (y * directBitmapPre.Width)] = finalPixel;
                }
            }
            int min = resultImage.Min();
            int max = resultImage.Max();
            // resultImage.Select(x => this.ScalePixelValue( x, sccalingMethod, min, max));
            for (int i = 0; i < directBitmapPre.Height * directBitmapPost.Width; i++)
            {
                resultImage[i] = this.ScalePixelValue(resultImage[i], sccalingMethod, min, max);
            }

            directBitmapPost.load(resultImage);
   
            return resultImage; 
        }

        public int[] sobelPrewwitOperation(KernelMethod kernelMethod = KernelMethod.NoBorders, SccalingMethod sccalingMethod = SccalingMethod.Cut, DirectionEdgeMask directionEdgeMask =  DirectionEdgeMask.Prewwit)
        {
            //TODO: Merge all neighborhood operations in little framework, big code redundancy, but its prototype so cool
            int[] bitmapPreScaleFirst = new int[directBitmapPre.Width * directBitmapPre.Height];
            int[] bitmapPreScaleSecond = new int[directBitmapPre.Width * directBitmapPre.Height];
            int[] resultImage = new int[directBitmapPre.Width * directBitmapPre.Height];

            int xBond = kernelMethod == KernelMethod.NoBorders ? 1 : 0;
            List<int> maskSumFirst;
            List<int> maskSumSecond;

            int calculatedX;
            int calculatedY;

            int[] maskFirst = null;
            int[] maskSecond = null;

            switch(directionEdgeMask)
            {
                case DirectionEdgeMask.Prewwit:
                    maskFirst = ImageProcessingEngine.PrewwitFirst;
                    maskSecond = ImageProcessingEngine.PrewwitSecond;
                    break;
                case DirectionEdgeMask.Sobel:
                    maskFirst = ImageProcessingEngine.SobelFirst;
                    maskSecond = ImageProcessingEngine.SobelSecond;
                    break;
            }

            const int maskSize = 3;

            int overlap = 0; // = maskSize / 2;
            int maskOverlap = maskSize / 2;

            switch (kernelMethod)
            {
                case KernelMethod.NoBorders:
                    overlap = maskSize / 2;
                    break;
                case KernelMethod.CloneBorder:
                    overlap = 0;
                    break;
                case KernelMethod.UseExisting:
                    overlap = 0;
                    break;

            }

            for (int x = overlap; x < directBitmapPre.Width - overlap; ++x)
            {
                for (int y = overlap; y < directBitmapPre.Height - overlap; ++y)
                {
                    maskSumFirst = new List<int>();
                    maskSumSecond = new List<int>();

                    for (int xi = 0; xi < 3; xi++)
                    {
                        for (int yi = 0; yi < 3; yi++)
                        {
                            calculatedX = x + xi - maskOverlap;
                            calculatedY = y + yi - maskOverlap;

                            if (kernelMethod == KernelMethod.UseExisting)
                            {
                                if (calculatedX >= directBitmapPre.Width || calculatedY >= directBitmapPre.Height || calculatedX < 0 || calculatedY < 0)
                                {
                                    continue;
                                }
                            }
                            else if (kernelMethod == KernelMethod.CloneBorder)
                            {
                                if (calculatedX >= directBitmapPre.Width || calculatedY >= directBitmapPre.Height || calculatedX < 0 || calculatedY < 0)
                                {
                                    //mask[xi + (yi * maskSize)]
                                    maskSumFirst.Add(maskFirst[xi + (yi * 3)] * directBitmapPre.GetPixel(x, y).R);
                                    maskSumSecond.Add(maskSecond[xi + (yi * 3)] * directBitmapPre.GetPixel(x, y).R);
                                    continue;
                                }
                            }
                            maskSumFirst.Add(maskFirst[xi + (yi * 3)] * directBitmapPre.GetPixel(calculatedX, calculatedY).R);
                            maskSumSecond.Add(maskSecond[xi + (yi * 3)] * directBitmapPre.GetPixel(calculatedX, calculatedY).R);

                        }
                    }

                    bitmapPreScaleFirst[x + (y * directBitmapPre.Width)] = Math.Abs(maskSumFirst.Sum());
                    bitmapPreScaleSecond[x + (y * directBitmapPre.Width)] = Math.Abs(maskSumSecond.Sum());


                }
            }

            int finalPixel = 0;
            for (int x = 0; x < directBitmapPre.Width; x++)
            {
                for (int y = 0; y < directBitmapPre.Height; y++)
                {
                    finalPixel = bitmapPreScaleFirst[x + (y * directBitmapPre.Width)] + bitmapPreScaleSecond[x + (y * directBitmapPre.Width)];
                    resultImage[x + (y * directBitmapPre.Width)] = finalPixel;
                }
            }
            int min = resultImage.Min();
            int max = resultImage.Max();
            // resultImage.Select(x => this.ScalePixelValue( x, sccalingMethod, min, max));
            for (int i = 0; i < directBitmapPre.Height * directBitmapPost.Width; i++)
            {
                resultImage[i] = this.ScalePixelValue(resultImage[i], sccalingMethod, min, max);
            }

            directBitmapPost.load(resultImage);

            return resultImage;
        }

    }
}

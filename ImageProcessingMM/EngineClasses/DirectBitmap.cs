using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ImageProcessingMM.EngineClasses
{
    public class HSVBit
    {
        public float HUE { get; set; } //[0-360]
        public float saturation { get; set; } //[0-1]
        public float value { get; set; } //[0-1]

        private static float maxValue(params float[] values)
        {
            return values.Max();
        }

        private static float minValue(params float[] values)
        {
            return values.Min();
        }

        public Color getColor()
        {
            float c = this.value * this.saturation;
            float x = c * (1 - Math.Abs((this.HUE / 60) % 2 - 1));
            float m = this.value - c;

            float rPrim = 0;
            float gPrim = 0;
            float bPrim = 0;

            //R' G' B'
            if (0 <= this.HUE && 60 >= this.HUE)
            {
                //C X 0
                rPrim = c;
                gPrim = x;
                bPrim = 0;
            }
            else if(60 <= this.HUE && 120 >= this.HUE)
            {
                //X C 0
                rPrim = x;
                gPrim = c;
                bPrim = 0;
            }
            else if(120 <= this.HUE && 180 >= this.HUE)
            {
                //0 C X
                rPrim = 0;
                gPrim = c;
                bPrim = x;
            }
            else if(180 <= this.HUE && 240 >= this.HUE)
            {
                //0 X C
                rPrim = 0;
                gPrim = x;
                bPrim = c;
            }
            else if(240 <= this.HUE && 300 >= this.HUE)
            {
                //X 0 C
                rPrim = x;
                gPrim = 0;
                bPrim = c;
            }
            else if(300 <= this.HUE && 360 >= this.HUE)
            {
                //C 0 X
                rPrim = c;
                gPrim = 0;
                bPrim = x;
            }

            //(R, G, B) = ((R'+m)×255, (G' + m)×255, (B'+m)×255)
            return Color.FromArgb((int)((rPrim + m) * 255), (int)((gPrim + m) * 255), (int)((bPrim + m) * 255));


        }

        static public HSVBit getFromColor(Color color)
        {
            HSVBit hsvBit = new HSVBit();

            float redPrim = (float)color.R / 255;
            float greenPrim = (float)color.G / 255;
            float bluePrim = (float)color.B / 255;

            float cMax = HSVBit.maxValue(redPrim, greenPrim, bluePrim);
            float cMin = HSVBit.minValue(redPrim, greenPrim, bluePrim);

            float delta = cMax - cMin;

            if(delta == 0)
            {
                hsvBit.HUE = 0;
            }
            else
            {
               if(cMax == redPrim)
                {
                    hsvBit.HUE = 60 * (((greenPrim - redPrim) / delta) % 6);
                }
               else if(cMax == greenPrim)
                {
                    hsvBit.HUE = 60 * ((bluePrim - redPrim) / delta + 2);
                }
               else if(cMax == bluePrim)
                {
                    hsvBit.HUE = 60 * ((redPrim - greenPrim) / delta + 4);
                }
            }

            if(cMax == 0)
            {
                hsvBit.saturation = 0;
            }
            else
            {
                hsvBit.saturation = delta / cMax;
            }

            hsvBit.value = cMax;

            return hsvBit;
        }
    }






    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

        public HSVBit[] HSVBits { get; private set; }

        
    

        protected GCHandle BitsHandle { get; private set; }

        public DirectBitmap(DirectBitmap directBitmap)
        {
            Width = directBitmap.Width;
            Height = directBitmap.Height;
            Bits = (Int32[])directBitmap.Bits.Clone();
            Bitmap = (Bitmap)directBitmap.Bitmap.Clone();

        }



        public DirectBitmap(int width, int height)
        {
            Width = width;
            Height = height;
            Bits = new Int32[width * height];
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(width, height, width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());
        }

        public DirectBitmap(Bitmap bitmap)
        {
            Bitmap innerBitmap = (Bitmap)bitmap.Clone(); //to unlock
            Width = bitmap.Width;
            Height = bitmap.Height;
            Bits = new Int32[Width * Height];

            BitmapData imageData = bitmap.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadWrite, PixelFormat.Format32bppPArgb);
       
            Marshal.Copy(imageData.Scan0, Bits, 0, Height * Width);

            //Bits = this.generateBitsFromBitmap(bitmap);
            BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap = new Bitmap(Width, Height, Width * 4, PixelFormat.Format32bppPArgb, BitsHandle.AddrOfPinnedObject());



        }


        public void SetPixel(int x, int y, Color colour)
        {
            int index = x + (y * Width);
            int col = colour.ToArgb();

            Bits[index] = col;
        }

        public Color GetPixel(int x, int y)
        {
            int index = x + (y * Width);
            int col = Bits[index];
            Color result = Color.FromArgb(col);

            return result;
        }

        
        private Int32[] generateBitsFromBitmap(Bitmap bitmap)
        {
            Int32[] innerBits = new Int32[bitmap.Width * bitmap.Height];
            Color helpPixel;
            int index;
            int col; 

            for (int x = 0; x < bitmap.Width; ++x)
            {
                for (int y = 0; y < bitmap.Height; ++y)
                {
                    helpPixel = bitmap.GetPixel(x, y);
                    index = x + (y * bitmap.Width);
                    innerBits[index] = helpPixel.ToArgb();
                }
            }

            return innerBits;
        }


        public void Dispose()
        {
            if (Disposed) return;
            Disposed = true;
            Bitmap.Dispose();
            BitsHandle.Free();
        }

        public void load(int[] intTable)
        {
            int helpValue = 0;
            for(int x = 0; x < Width; x++)
            {
                for(int y = 0; y < Height; y++)
                {
                    helpValue = intTable[x + (y * Width)];
                    this.SetPixel(x, y, Color.FromArgb(helpValue, helpValue, helpValue));
                }
            }

        }


        public Int32 getMin()
        {
            return Bits.Min(x => Color.FromArgb(x).R);
        }

        public Int32 getMax()
        {
            return Bits.Max(x => Color.FromArgb(x).R);
        }


        //Method to generate Array of HSV values
        public void generateHSVBits()
        {
            HSVBits = new HSVBit[Width * Height];
            int length = Bits.Length;

            for(int i = 0; i < length; i++)
            {
                HSVBits[i] = HSVBit.getFromColor(Color.FromArgb(Bits[i]));
            }
        }
    }
}

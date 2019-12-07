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
    public class DirectBitmap : IDisposable
    {
        public Bitmap Bitmap { get; private set; }
        public Int32[] Bits { get; private set; }
        public bool Disposed { get; private set; }
        public int Height { get; private set; }
        public int Width { get; private set; }

    

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
                    helpValue = intTable[x + (y * Height)];
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
    }
}

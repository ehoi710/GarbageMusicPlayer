using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace GarbageMusicPlayerClassLibrary
{
    public class ImageController
    {
        private static int Max(int a, int b)
        {
            return a > b ? a : b;
        }
        private static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        private static byte GetBitsPerPixel(PixelFormat pixelFormat)
        {
            switch(pixelFormat)
            {
                case PixelFormat.Format32bppArgb: return 32;
            }

            throw new Exception();
        }

        // Bluring function
        private static Bitmap BoxBlurH(Bitmap originBitmap, int blurringRadius)
        {
            Bitmap blurredBitmap = new Bitmap(originBitmap.Width, originBitmap.Height);

            BitmapData originData = originBitmap.LockBits(
                new Rectangle(0, 0, originBitmap.Width, originBitmap.Height),
                ImageLockMode.ReadWrite,
                originBitmap.PixelFormat
            );
            BitmapData blurredData = blurredBitmap.LockBits(
                new Rectangle(0, 0, blurredBitmap.Width, blurredBitmap.Height),
                ImageLockMode.ReadWrite,
                blurredBitmap.PixelFormat
            );

            byte bitsPerPixel = GetBitsPerPixel(originBitmap.PixelFormat);
            int size = originData.Stride * originData.Height;

            byte[] origin = new byte[size];
            byte[] blurred = new byte[size];
            int blurringDiameter = 2 * blurringRadius + 1;

            System.Runtime.InteropServices.Marshal.Copy(originData.Scan0, origin, 0, size);
            for (int y = 0; y < blurredBitmap.Height; y++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int xx = -blurringRadius; xx <= blurringRadius; xx++)
                {
                    int ix = Min(blurredBitmap.Width - 1, Max(0, xx));
                    int pos = y * originData.Stride + ix * bitsPerPixel / 8;

                    B += origin[pos + 0];
                    G += origin[pos + 1];
                    R += origin[pos + 2];
                }

                for (int x = 1; x < blurredBitmap.Width; x++)
                {
                    int minx = Min(blurredBitmap.Width - 1, Max(0, x - blurringRadius - 1));
                    int maxx = Min(blurredBitmap.Width - 1, Max(0, x + blurringRadius));

                    int blurredpos = y * blurredData.Stride + x * bitsPerPixel / 8;
                    int minpos = y * originData.Stride + minx * bitsPerPixel / 8;
                    int maxpos = y * originData.Stride + maxx * bitsPerPixel / 8;

                    B = B - origin[minpos + 0] + origin[maxpos + 0];
                    G = G - origin[minpos + 1] + origin[maxpos + 1];
                    R = R - origin[minpos + 2] + origin[maxpos + 2];

                    blurred[blurredpos + 0] = (byte)(B / blurringDiameter);
                    blurred[blurredpos + 1] = (byte)(G / blurringDiameter);
                    blurred[blurredpos + 2] = (byte)(R / blurringDiameter);
                    blurred[blurredpos + 3] = 0xFF;
                }
            }

            originBitmap.UnlockBits(originData);
            System.Runtime.InteropServices.Marshal.Copy(blurred, 0, blurredData.Scan0, size);
            blurredBitmap.UnlockBits(blurredData);

            return blurredBitmap;
        }
        private static Bitmap BoxBlurT(Bitmap originBitmap, int blurringRadius)
        {
            Bitmap blurredBitmap = new Bitmap(originBitmap.Width, originBitmap.Height);

            BitmapData originData = originBitmap.LockBits(
                new Rectangle(0, 0, originBitmap.Width, originBitmap.Height),
                ImageLockMode.ReadWrite,
                originBitmap.PixelFormat
            );
            BitmapData blurredData = blurredBitmap.LockBits(
                new Rectangle(0, 0, blurredBitmap.Width, blurredBitmap.Height),
                ImageLockMode.ReadWrite,
                blurredBitmap.PixelFormat
            );

            byte bitsPerPixel = GetBitsPerPixel(originBitmap.PixelFormat);
            int size = originData.Stride * originData.Height;

            byte[] origin = new byte[size];
            byte[] blurred = new byte[size];
            int blurringDiameter = 2 * blurringRadius + 1;

            System.Runtime.InteropServices.Marshal.Copy(originData.Scan0, origin, 0, size);
            for (int x = 0; x < blurredBitmap.Width; x++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int yy = -blurringRadius; yy <= blurringRadius; yy++)
                {
                    int iy = Min(blurredBitmap.Width - 1, Max(0, yy));
                    int pos = iy * originData.Stride + x * bitsPerPixel / 8;

                    B += origin[pos + 0];
                    G += origin[pos + 1];
                    R += origin[pos + 2];
                }

                for (int y = 1; y < blurredBitmap.Height; y++)
                {
                    int miny = Min(blurredBitmap.Height - 1, Max(0, y - blurringRadius - 1));
                    int maxy = Min(blurredBitmap.Height - 1, Max(0, y + blurringRadius));

                    int blurredpos = y * blurredData.Stride + x * bitsPerPixel / 8;
                    int minpos = miny * originData.Stride + x * bitsPerPixel / 8;
                    int maxpos = maxy * originData.Stride + x * bitsPerPixel / 8;

                    B = B - origin[minpos + 0] + origin[maxpos + 0];
                    G = G - origin[minpos + 1] + origin[maxpos + 1];
                    R = R - origin[minpos + 2] + origin[maxpos + 2];

                    blurred[blurredpos + 0] = (byte)(B / blurringDiameter);
                    blurred[blurredpos + 1] = (byte)(G / blurringDiameter);
                    blurred[blurredpos + 2] = (byte)(R / blurringDiameter);
                    blurred[blurredpos + 3] = 0xFF;
                }
            }

            originBitmap.UnlockBits(originData);
            System.Runtime.InteropServices.Marshal.Copy(blurred, 0, blurredData.Scan0, size);
            blurredBitmap.UnlockBits(blurredData);

            return blurredBitmap;
        }
        public static Bitmap BoxBlur(Bitmap image, int blurringRadius)
        {
            Bitmap blurred = BoxBlurH(image, blurringRadius);
            blurred = BoxBlurT(blurred, blurringRadius);
            return blurred;
        }

        // Resize function
        public static Bitmap ResizeBitmap(Bitmap bitmap, float amount)
        {
            Size size = new Size((int)(bitmap.Width * amount), (int)(bitmap.Height * amount));
            return new Bitmap(bitmap, size);
        }
        public static Bitmap ResizeBitmapFitBigger(Bitmap bitmap, Size size)
        {
            float HeightRatio = (float)size.Height / bitmap.Height;
            float WidthRatio = (float)size.Width / bitmap.Width;

            if(WidthRatio < HeightRatio)
            {
                return ResizeBitmap(bitmap, WidthRatio);
            }
            else
            {
                return ResizeBitmap(bitmap, HeightRatio);
            }
        }
        public static Bitmap ResizeBitmapFitSmaller(Bitmap bitmap, Size size)
        {
            float HeightRatio = (float)size.Height / bitmap.Height;
            float WidthRatio = (float)size.Width / bitmap.Width;

            if(WidthRatio > HeightRatio)
            {
                return ResizeBitmap(bitmap, WidthRatio);
            }
            else
            {
                return ResizeBitmap(bitmap, HeightRatio);
            }
        }

        // Crop function
        public static Bitmap CropBitmap(Bitmap bitmap, Size size)
        {
            Point location = new Point((bitmap.Width - size.Width) / 2, (bitmap.Height - size.Height) / 2);
            Rectangle cropArea = new Rectangle(location, size);
            Bitmap CroppedBitmap = new Bitmap(size.Width, size.Height);

            using (Graphics g = Graphics.FromImage(CroppedBitmap))
            {
                g.DrawImage(bitmap, -cropArea.X, -cropArea.Y);
                return CroppedBitmap;
            }
        }

        // Grayscale
        public static Bitmap Grayscale(Bitmap bitmap)
        {
            Bitmap res = bitmap;
            for(int y = 0; y < res.Height; y++)
            {
                for(int x = 0; x < res.Width; x++)
                {
                    Color color = res.GetPixel(x, y);
                    if (color.R == 0x00) continue;

                    int Gray = 0;
                    Gray += color.R;
                    Gray += color.G;
                    Gray += color.B;

                    res.SetPixel(
                        x, y,
                        Color.FromArgb(
                            Gray / 3,
                            Gray / 3,
                            Gray / 3
                        )
                    );
                }
            }
            return res;
        }

        // Shadow
        public static Bitmap DrawShadow(Bitmap bitmap, Rectangle rect)
        {
            Bitmap res = bitmap;

            Graphics graphics = Graphics.FromImage(bitmap);
            graphics.DrawRectangle(new Pen(Color.FromArgb(0x7F / 3, 0, 0, 0), 6.0f), rect);
            graphics.DrawRectangle(new Pen(Color.FromArgb((0x7F * 2) / 3, 0, 0, 0), 4.0f), rect);
            graphics.DrawRectangle(new Pen(Color.FromArgb(0x7F, 0, 0, 0), 2.0f), rect);

            return res;
        }

        // Make round Edge Image
        public static Bitmap CropImageRoundEdge(Bitmap bitmap)
        {
            Bitmap res = new Bitmap(bitmap.Width, bitmap.Height);

            Graphics graphics = Graphics.FromImage(res);
            graphics.SmoothingMode = SmoothingMode.AntiAlias;

            GraphicsPath path = new GraphicsPath();

            int arcX = bitmap.Width / 10;
            int arcY = bitmap.Height / 10;

            path.AddArc(0, 0, arcX, arcY, 180, 90);
            path.AddArc(bitmap.Width - arcX, 0, arcX, arcY, 270, 90);
            path.AddArc(bitmap.Width - arcX, bitmap.Height - arcY, arcX, arcY, 0, 90);
            path.AddArc(0, bitmap.Height - arcY, arcX, arcY, 90, 90);

            graphics.FillPath(new TextureBrush(bitmap), path);

            return res;
        }
    }
}

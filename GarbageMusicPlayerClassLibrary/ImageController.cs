using System.Drawing;

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

        // Bluring function
        private static Bitmap BoxBlurH(Bitmap image, int blurringRadius)
        {
            Bitmap blurredBitmap = new Bitmap(image);
            int blurringDiameter = 2 * blurringRadius + 1;

            for (int y = 0; y < blurredBitmap.Height; y++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int xx = -blurringRadius; xx <= blurringRadius; xx++)
                {
                    int ix = Min(blurredBitmap.Width - 1, Max(0, xx));
                    Color pixel = image.GetPixel(ix, y);

                    R += pixel.R;
                    G += pixel.G;
                    B += pixel.B;
                }

                for (int x = 1; x < blurredBitmap.Width; x++)
                {
                    int minx = Min(blurredBitmap.Width - 1, Max(0, x - blurringRadius - 1));
                    int maxx = Min(blurredBitmap.Width - 1, Max(0, x + blurringRadius));

                    Color leavePixel = image.GetPixel(minx, y);
                    Color arrivePixel = image.GetPixel(maxx, y);

                    R = R - leavePixel.R + arrivePixel.R;
                    G = G - leavePixel.G + arrivePixel.G;
                    B = B - leavePixel.B + arrivePixel.B;

                    blurredBitmap.SetPixel(x, y, Color.FromArgb(R / blurringDiameter, G / blurringDiameter, B / blurringDiameter));
                }
            }

            return blurredBitmap;
        }
        private static Bitmap BoxBlurT(Bitmap image, int blurringRadius)
        {
            Bitmap blurredBitmap = new Bitmap(image);
            int blurringDiameter = 2 * blurringRadius + 1;

            for (int x = 0; x < blurredBitmap.Width; x++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int yy = -blurringRadius; yy <= blurringRadius; yy++)
                {
                    int iy = Min(blurredBitmap.Width - 1, Max(0, yy));
                    Color pixel = image.GetPixel(x, iy);

                    R += pixel.R;
                    G += pixel.G;
                    B += pixel.B;
                }

                for (int y = 1; y < blurredBitmap.Height; y++)
                {
                    int miny = Min(blurredBitmap.Height - 1, Max(0, y - blurringRadius - 1));
                    int maxy = Min(blurredBitmap.Height - 1, Max(0, y + blurringRadius));

                    Color leavePixel = image.GetPixel(x, miny);
                    Color arrivePixel = image.GetPixel(x, maxy);

                    R = R - leavePixel.R + arrivePixel.R;
                    G = G - leavePixel.G + arrivePixel.G;
                    B = B - leavePixel.B + arrivePixel.B;

                    blurredBitmap.SetPixel(x, y, Color.FromArgb(R / blurringDiameter, G / blurringDiameter, B / blurringDiameter));
                }
            }

            return blurredBitmap;
        }
        public static Bitmap BoxBlur(Bitmap image, int blurringRadius)
        {
            Bitmap blurred = BoxBlurT(BoxBlurH(image, blurringRadius), blurringRadius);
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
        public static Bitmap CropBitmap(Bitmap bitmap, Point location, Size size)
        {
            Rectangle cropArea = new Rectangle(location, size);
            Bitmap CroppedBitmap = new Bitmap(size.Width, size.Height);

            using (Graphics g = Graphics.FromImage(CroppedBitmap))
            {
                g.DrawImage(bitmap, -cropArea.X, -cropArea.Y);
                return CroppedBitmap;
            }
        }

        // Value decrese
        public static Bitmap DecreseValue(Bitmap bitmap, Point location, Size size)
        {
            Bitmap res = bitmap;
            for (int y = 0; y < size.Height; y++)
            {
                for (int x = 0; x < size.Width; x++)
                {
                    Color color = res.GetPixel(x + location.X, y + location.Y);
                    res.SetPixel(
                        x + location.X, y + location.Y,
                        Color.FromArgb(
                            Max(color.R - 30, 0),
                            Max(color.G - 30, 0),
                            Max(color.B - 30, 0)
                        ));
                }
            }

            return res;
        }
    }
}

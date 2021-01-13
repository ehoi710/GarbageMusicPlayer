using System.Drawing;

namespace GarbageMusicPlayerClassLibrary
{
    public class ImageController
    {
        public enum ImageResizeMode
        {
            Bigger,
            Smaller
        };

        private static int Max(int a, int b)
        {
            return a > b ? a : b;
        }

        private static int Min(int a, int b)
        {
            return a < b ? a : b;
        }

        private static Bitmap BoxBlurH(Bitmap image, int blurringRadius)
        {
            Bitmap blurred = new Bitmap(image);
            int blurringDiameter = 2 * blurringRadius + 1;

            for (int y = 0; y < blurred.Height; y++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int xx = -blurringRadius; xx <= blurringRadius; xx++)
                {
                    int ix = Min(blurred.Width - 1, Max(0, xx));
                    Color pix = image.GetPixel(ix, y);

                    R += pix.R;
                    G += pix.G;
                    B += pix.B;
                }

                for (int x = 1; x < blurred.Width; x++)
                {
                    int minx = Min(blurred.Width - 1, Max(0, x - blurringRadius - 1));
                    int maxx = Min(blurred.Width - 1, Max(0, x + blurringRadius));

                    Color d = image.GetPixel(minx, y);
                    Color a = image.GetPixel(maxx, y);

                    R = R - d.R + a.R;
                    G = G - d.G + a.G;
                    B = B - d.B + a.B;

                    blurred.SetPixel(x, y, Color.FromArgb(R / blurringDiameter, G / blurringDiameter, B / blurringDiameter));
                }
            }

            return blurred;
        }

        private static Bitmap BoxBlurT(Bitmap image, int blurringRadius)
        {
            Bitmap blurred = new Bitmap(image);
            int blurringDiameter = 2 * blurringRadius + 1;

            for (int x = 0; x < blurred.Width; x++)
            {
                int R = 0;
                int G = 0;
                int B = 0;

                for (int yy = -blurringRadius; yy <= blurringRadius; yy++)
                {
                    int iy = Min(blurred.Width - 1, Max(0, yy));
                    Color pix = image.GetPixel(x, iy);

                    R += pix.R;
                    G += pix.G;
                    B += pix.B;
                }

                for (int y = 1; y < blurred.Height; y++)
                {
                    int miny = Min(blurred.Height - 1, Max(0, y - blurringRadius - 1));
                    int maxy = Min(blurred.Height - 1, Max(0, y + blurringRadius));

                    Color d = image.GetPixel(x, miny);
                    Color a = image.GetPixel(x, maxy);

                    R = R - d.R + a.R;
                    G = G - d.G + a.G;
                    B = B - d.B + a.B;

                    blurred.SetPixel(x, y, Color.FromArgb(R / blurringDiameter, G / blurringDiameter, B / blurringDiameter));
                }
            }

            return blurred;
        }

        public static Bitmap BoxBlur(Bitmap image, int blurringRadius)
        {
            Bitmap blurred = BoxBlurT(BoxBlurH(image, blurringRadius), blurringRadius);
            return blurred;
        }

        public static Bitmap ResizeBitmap(Bitmap bitmap, int width, int height, ImageResizeMode mode)
        {
            float HeightRatio = (float)height / bitmap.Height;
            float WidthRatio = (float)width / bitmap.Width;

            float amount = 0.0f;
            if (
                ((mode == ImageResizeMode.Smaller) && (WidthRatio > HeightRatio)) || 
                ((mode == ImageResizeMode.Bigger) && (WidthRatio < HeightRatio))
               )
            {
                amount = WidthRatio;
            }
            else
            {
                amount = HeightRatio;
            }

            return ResizeBitmap(bitmap, amount);
        }

        public static Bitmap ResizeBitmap(Bitmap bitmap, float amount)
        {
            Size size = new Size((int)(bitmap.Width * amount), (int)(bitmap.Height * amount));
            return new Bitmap(bitmap, size);
        }

        public static Bitmap CropBitmap(Bitmap bitmap, int width, int height)
        {
            Rectangle cropArea = new Rectangle((bitmap.Width - width) / 2, (bitmap.Height - height) / 2, width, height);
            Bitmap CroppedBitmap = new Bitmap(width, height);

            using (Graphics g = Graphics.FromImage(CroppedBitmap))
            {
                g.DrawImage(bitmap, -cropArea.X, -cropArea.Y);
                return CroppedBitmap;
            }
        }
    }
}

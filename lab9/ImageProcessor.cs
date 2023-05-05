using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;


namespace lab9
{
    public class ImageProcessor
    {

        /* -------------------------------------------------------------------------- */
        /*                                 Grey scale                                 */
        /* -------------------------------------------------------------------------- */
        public void toGreyscale(string path)
        {
            System.Console.WriteLine("Converting to greyscale...");
            using Image<Rgb24> image = Image.Load<Rgb24>(path);

            Image<Rgb24> clone = image.Clone();

            // Using (R + G + B) / 3
            for (int a = 0; a < image.Width; a++)
                for (int b = 0; b < image.Height; b++)
                {
                    // Get the pixel color
                    byte R = image[a, b].R;
                    byte G = image[a, b].G;
                    byte B = image[a, b].B;

                    clone[a, b] = new Rgb24((byte)((R + G + B) / 3), (byte)((R + G + B) / 3), (byte)((R + G + B) / 3));
                }
            clone.Save("output/greyscale.png");

        }

        /* -------------------------------------------------------------------------- */
        /*                            Median, Min, Max, Avg                           */
        /* -------------------------------------------------------------------------- */
        public void Transform(string path, int frame_width, int frame_hight)
        {
            System.Console.WriteLine("Transforming...");
            if (frame_width % 2 == 0 || frame_hight % 2 == 0)
                throw new ArgumentException("Width and height must be odd numbers.");

            // Load the image as Image<Rgba32>
            using (var image = Image.Load<Rgba32>(path))
            {
                using var resultMedian = new Image<Rgba32>(image.Width, image.Height);
                using var resultMax = new Image<Rgba32>(image.Width, image.Height);
                using var resultMin = new Image<Rgba32>(image.Width, image.Height);
                using var resultAvg = new Image<Rgba32>(image.Width, image.Height);

                // Parallel for loop
                Parallel.For(0, image.Width, x =>
                {
                    Parallel.For(0, image.Height, y =>
                    {
                        resultMedian[x, y] = medianPixel(image, x, y, frame_width, frame_hight);
                        resultMax[x, y] = maxPixel(image, x, y, frame_width, frame_hight);
                        resultMin[x, y] = minPixel(image, x, y, frame_width, frame_hight);
                        resultAvg[x, y] = avgPixel(image, x, y, frame_width, frame_hight);
                    });
                });
                resultAvg.Save("output/avg.png");
                resultMin.Save("output/min.png");
                resultMax.Save("output/max.png");
                resultMedian.Save("output/median.png");



            }
        }

        /* ------------------------------- Median filter ------------------------------- */
        private Rgba32 medianPixel(Image<Rgba32> image, int x, int y, int frame_width, int frame_hight)
        {
            List<byte> R = new List<byte>();
            List<byte> G = new List<byte>();
            List<byte> B = new List<byte>();

            // Get the pixels in the frame
            for (int a = x - frame_width / 2; a <= x + frame_width / 2; a++)
                for (int b = y - frame_hight / 2; b <= y + frame_hight / 2; b++)
                {
                    // Check if the pixel is in the image
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }

            // Sort the lists
            R.Sort();
            G.Sort();
            B.Sort();

            return new Rgba32(R[R.Count / 2], G[G.Count / 2], B[B.Count / 2]);
        }

        /* ------------------------------- Min filter ------------------------------- */
        private Rgba32 minPixel(Image<Rgba32> image, int x, int y, int frame_width, int frame_hight)
        {
            List<byte> R = new List<byte>();
            List<byte> G = new List<byte>();
            List<byte> B = new List<byte>();

            // Get the pixels in the frame
            for (int a = x - frame_width / 2; a <= x + frame_width / 2; a++)
                for (int b = y - frame_hight / 2; b <= y + frame_hight / 2; b++)
                {
                    // Check if the pixel is in the image
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }

            // Sort the lists
            R.Sort();
            G.Sort();
            B.Sort();


            return new Rgba32(R[0], G[0], B[0]);
        }

        /* ------------------------------- Max filter ------------------------------- */
        private Rgba32 maxPixel(Image<Rgba32> image, int x, int y, int frame_width, int frame_hight)
        {
            List<byte> R = new List<byte>();
            List<byte> G = new List<byte>();
            List<byte> B = new List<byte>();

            // Get the pixels in the frame
            for (int a = x - frame_width / 2; a <= x + frame_width / 2; a++)
                for (int b = y - frame_hight / 2; b <= y + frame_hight / 2; b++)
                {
                    // Check if the pixel is in the image
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }

            // Sort the lists
            R.Sort();
            G.Sort();
            B.Sort();

            return new Rgba32(R[R.Count - 1], G[G.Count - 1], B[B.Count - 1]);

        }

        /* ------------------------------- Avg Filter ------------------------------- */
        private Rgba32 avgPixel(Image<Rgba32> image, int x, int y, int frame_width, int frame_hight)
        {
            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            // Get the pixels in the frame
            for (int a = x - frame_width / 2; a <= x + frame_width / 2; a++)
                for (int b = y - frame_hight / 2; b <= y + frame_hight / 2; b++)
                {
                    // Check if the pixel is in the image
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }


            // Save to variables
            return new Rgba32((byte)(R.Average()), (byte)(G.Average()), (byte)(B.Average()));
        }



        /* -------------------------------------------------------------------------- */
        /*                             Convolution filter                             */
        /* -------------------------------------------------------------------------- */
        public void convolutionTransform(string path, int[,] kernel)
        {
            System.Console.WriteLine("Convolution transform");
            // Load the image
            using var image = Image.Load<Rgba32>(path);

            // Create a new image
            using (var result = new Image<Rgba32>(image.Width, image.Height))
            {
                for (int x = 0; x < image.Width; x++)
                {
                    for (int y = 0; y < image.Height; y++)
                    {
                        result[x, y] = convolutionPixel(image, x, y, kernel);
                    }
                }
                result.Save("output/convolution.png");
            }

        }
        private Rgba32 convolutionPixel(Image<Rgba32> image, int x, int y, int[,] kernel)
        {
            int R = 0;
            int G = 0;
            int B = 0;

            // Get the pixels in the frame
            for (int a = x - kernel.GetLength(0) / 2; a <= x + kernel.GetLength(0) / 2; a++)
                for (int b = y - kernel.GetLength(1) / 2; b <= y + kernel.GetLength(1) / 2; b++)
                {
                    // Matrix operations
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R += image[a, b].R * kernel[a - x + kernel.GetLength(0) / 2, b - y + kernel.GetLength(1) / 2];
                        G += image[a, b].G * kernel[a - x + kernel.GetLength(0) / 2, b - y + kernel.GetLength(1) / 2];
                        B += image[a, b].B * kernel[a - x + kernel.GetLength(0) / 2, b - y + kernel.GetLength(1) / 2];
                    }
                }

            // Check if the value is in the range
            if (R < 0) R = 0;
            if (R > 255) R = 255;
            if (G < 0) G = 0;
            if (G > 255) G = 255;
            if (B < 0) B = 0;
            if (B > 255) B = 255;

            return new Rgba32((byte)(R), (byte)(G), (byte)(B));
        }


    }
}

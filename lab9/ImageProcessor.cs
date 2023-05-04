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

        /* ------------------------------- Grey scale ------------------------------- */
        public void toGreyscale(string path)
        {
            using (Image<Rgb24> image = Image.Load<Rgb24>(path))
            {
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
                clone.Save("greyscale.png");
            }
        }


        /* --------------------------------- Filters -------------------------------- */
        public void medianFilter(string path, int frame_width, int frame_hight)
        {
            if (frame_width % 2 == 0 || frame_hight % 2 == 0)
                throw new ArgumentException("Width and height must be odd numbers.");

            // Load the image as Image<Rgba32>
            using (var image = Image.Load<Rgba32>(path))
            {
                using (var result = new Image<Rgba32>(image.Width, image.Height))
                {
                    for (int a = 0; a < image.Width; a++)
                    {
                        for (int b = 0; b < image.Height; b++)
                        {
                            List<Rgba32> colors_of_neighbouring_pixels = getColorsOfNeighbouringPixels(image, a, b, frame_width, frame_hight);


                            // Sort the list by brightness
                            colors_of_neighbouring_pixels.Sort((a, b) => ((a.R + a.G + a.B) / 3.0).CompareTo((b.R + b.G + b.B) / 3.0));
                            // Median
                            var median = colors_of_neighbouring_pixels[colors_of_neighbouring_pixels.Count / 2];
                            result[a, b] = median;
                        }
                    }
                    // Save the result image to a new file
                    result.Save("median-filtered.png");
                }
            }
        }

        public void maxMinAvgFilter(string path, int frame_width, int frame_hight)
        {
            if (frame_width % 2 == 0 || frame_hight % 2 == 0)
                throw new ArgumentException("Width and height must be odd numbers.");

            using (var image = Image.Load<Rgba32>(path))
            {
                // save 3 pictures, one for max, one for min, one for avg
                using (var result_max = new Image<Rgba32>(image.Width, image.Height))
                {
                    using (var result_min = new Image<Rgba32>(image.Width, image.Height))
                    {
                        using (var result_avg = new Image<Rgba32>(image.Width, image.Height))
                        {
                            for (int a = 0; a < image.Width; a++)
                            {
                                for (int b = 0; b < image.Height; b++)
                                {
                                    List<Rgba32> colors_of_neighbouring_pixels = getColorsOfNeighbouringPixels(image, a, b, frame_width, frame_hight);

                                    // Sort the list by brightness
                                    colors_of_neighbouring_pixels.Sort((a, b) => ((a.R + a.G + a.B) / 3.0).CompareTo((b.R + b.G + b.B) / 3.0));

                                    // Max
                                    var max = colors_of_neighbouring_pixels[colors_of_neighbouring_pixels.Count - 1];
                                    result_max[a, b] = max;

                                    // Min
                                    var min = colors_of_neighbouring_pixels[0];
                                    result_min[a, b] = min;

                                    // Avg
                                    int sumR = 0;
                                    int sumG = 0;
                                    int sumB = 0;
                                    foreach (var color in colors_of_neighbouring_pixels)
                                    {
                                        sumR += color.R;
                                        sumG += color.G;
                                        sumB += color.B;
                                    }
                                    result_avg[a, b] = new Rgba32((byte)(sumR / colors_of_neighbouring_pixels.Count), (byte)(sumG / colors_of_neighbouring_pixels.Count), (byte)(sumB / colors_of_neighbouring_pixels.Count));
                                }
                            }
                            // Save the result image to a new file
                            result_max.Save("max-filtered.png");
                            result_min.Save("min-filtered.png");
                            result_avg.Save("avg-filtered.png");
                        }
                    }
                }
            }
        }
        private List<Rgba32> getColorsOfNeighbouringPixels(Image<Rgba32> image, int x, int y, int frame_width, int frame_hight)
        {
            var colors_of_neighbouring_pixels = new List<Rgba32>();

            // Loop through the frame around the current pixel
            for (int i = -frame_width / 2; i <= frame_width / 2; i++)
            {
                for (int j = -frame_hight / 2; j <= frame_hight / 2; j++)
                {
                    // Coordinates of the neighboring pixel
                    int nx = x + i;
                    int ny = y + j;

                    // Coordinates within the image bounds?
                    if (nx >= 0 && nx < image.Width && ny >= 0 && ny < image.Height)
                    {
                        // Color of the neighboring pixel
                        var neighbor = image[nx, ny];
                        colors_of_neighbouring_pixels.Add(neighbor);
                    }
                }
            }
            return colors_of_neighbouring_pixels;
        }


        /* --------------------------- Convolution filter --------------------------- */
        public void convolutionFilter(string path, int[,] kernel)
        {
            // Load the image as Image<Rgba32>
            using (var image = Image.Load<Rgba32>(path))
            {
                using (var result = new Image<Rgba32>(image.Width, image.Height))
                {
                    for (int a = 0; a < image.Width; a++)
                    {
                        for (int b = 0; b < image.Height; b++)
                        {
                            List<Rgba32> colors_of_neighbouring_pixels = getColorsOfNeighbouringPixels(image, a, b, kernel.GetLength(0), kernel.GetLength(1));

                            // Apply the kernel
                            int sumR = 0;
                            int sumG = 0;
                            int sumB = 0;
                            for (int i = 0; i < kernel.GetLength(0); i++)
                            {
                                for (int j = 0; j < kernel.GetLength(1); j++)
                                {
                                    sumR += colors_of_neighbouring_pixels[i * kernel.GetLength(0) + j].R * kernel[i, j];
                                    sumG += colors_of_neighbouring_pixels[i * kernel.GetLength(0) + j].G * kernel[i, j];
                                    sumB += colors_of_neighbouring_pixels[i * kernel.GetLength(0) + j].B * kernel[i, j];
                                }
                            }
                            result[a, b] = new Rgba32((byte)(sumR / kernel.GetLength(0) / kernel.GetLength(1)), (byte)(sumG / kernel.GetLength(0) / kernel.GetLength(1)), (byte)(sumB / kernel.GetLength(0) / kernel.GetLength(1)));
                        }
                    }
                    // Save the result image to a new file
                    result.Save("convolution-filtered.png");
                }
            }
        }
    }
}
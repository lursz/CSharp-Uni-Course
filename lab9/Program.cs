using lab9;
class Program
{
    private static void Main(string[] args)
    {
        ImageProcessor processor = new ImageProcessor();
        processor.toGreyscale("test.png");
        processor.Transform("test.png", 11, 11);
        processor.convolutionTransform("test.png", new int[,] { { -1, 0, 1 }, { -1, 0, 1 }, { -1, 0, 1 } });
        // processor.convolutionTransform("test.png", new int[,] { { 1, 1, 1 }, { 0, 0, 0 }, { -1, -1, -1 } });


    }

}
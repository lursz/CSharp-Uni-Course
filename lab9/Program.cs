using lab9;
class Program
{
    private static void Main(string[] args)
    {
        ImageProcessor processor = new ImageProcessor();
        processor.toGreyscale("test.png");
        processor.Transform("test.png", 9, 9);
        processor.convolutionTransform("test.png", new int[,] { { 1, 1, 1 }, { 1, 1, 1 }, { 1, 1, 1 } });

    }

}
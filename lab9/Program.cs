using lab9;
class Program
{
    private static void Main(string[] args)
    {
        ImageProcessor processor = new ImageProcessor();
        // processor.toGreyscale("test.png");
        processor.medianFilter("test.png", 3, 3);
    }

}
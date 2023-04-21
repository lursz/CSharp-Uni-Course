using SHA512orMD5;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 3)
        {
            Console.WriteLine("Error. Too few arguments.");
            return;
        }

        Verifier verifier = new Verifier();
        // Does file B exist?
        if (!File.Exists(args[1]))
        {
            string hash = verifier.ComputeHash(args[0], args[2]);
            File.WriteAllText(args[1], hash);
            System.Console.WriteLine("Hash file created.");

        }
        else
        {
            if (verifier.verifyFile(args[0], args[1], args[2]))
            {
                Console.WriteLine("File is valid.");
            }
            else
            {
                Console.WriteLine("File is invalid.");
            }
        }

    }
}
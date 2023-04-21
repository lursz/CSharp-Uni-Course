using lab7;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Error. Too few arguments.");
            return;
        }
        AsymmetricKey cipher = new AsymmetricKey();
        switch (int.Parse(args[0]))
        {
            case 0:
                cipher.generateKeysToFile();
                break;
            case 1:
                cipher.cipherFile(args[1], args[2]);
                break;
            case 2:
                cipher.decipherFile(args[1], args[2]);
                break;
            case 4:
                if (!File.Exists(args[2]))
                {
                    cipher.generateSignature(args[1], args[2]);
                }
                else
                {
                    cipher.verifySignaturre(args[1], args[2]);
                }
                break;
            default:
                Console.WriteLine("Wrong command");
                break;
        }


    }
}
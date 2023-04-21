using AssymetricPassword;
internal class Program
{
    private static void Main(string[] args)
    {
        if (args.Length < 4)
        {
            Console.WriteLine("Error. Too few arguments.");
            return;
        }

        Password password = new Password();

        if (args[3] == "0")
        {
            password.Encrypt(args[1], args[2], args[3]);
        }
        else if (args[3] == "1")
        {
            password.Decrypt(args[1], args[2], args[3]);
        }
        else
        {
            Console.WriteLine("Error. Invalid arguments.");
        }

    

    }
}
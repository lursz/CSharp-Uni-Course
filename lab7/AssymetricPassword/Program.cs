using System.ComponentModel;
using System.Text;
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
            string file = File.ReadAllText(args[0]);
            byte[] input_data = Encoding.UTF8.GetBytes(file);
            byte[] encrypted = password.Encrypt(input_data, args[2]);
            File.WriteAllBytes(args[1], encrypted);
            
        }
        else if (args[3] == "1")
        {
            byte [] file = File.ReadAllBytes(args[0]);
            byte[] decrypted = password.Decrypt(file, args[2]);
            File.WriteAllText(args[1], Encoding.UTF8.GetString(decrypted));

        }
        else
        {
            Console.WriteLine("Error. Invalid arguments.");
        }

    

    }
}
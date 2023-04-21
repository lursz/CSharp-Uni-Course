using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace lab7
{
    public class AsymmetricKey
    {
        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
        string filename_public;
        string filename_private;
        
        public AsymmetricKey()
        {
        }
        /* ----------------------------------- ex1 ---------------------------------- */
        public void generateKeysToFile(string filename_public = "publicKey.xml", string filename_private = "privateKey.xml")
        {
            this.filename_public = filename_public;
            this.filename_private = filename_private;
            Console.WriteLine("Generating keys...");
            var publicKey = rsa.ToXmlString(false);
            var privateKey = rsa.ToXmlString(true);
            System.IO.File.WriteAllText(filename_public, publicKey);
            System.IO.File.WriteAllText(filename_private, privateKey);
        }

        public void cipherFile(string filename_a, string filename_b)
        {
            Console.WriteLine("Crypting...");
            var publicKey = File.ReadAllText(filename_public);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);

            byte[] inputBytes = File.ReadAllBytes(filename_a);
            byte[] encryptedBytes = rsa.Encrypt(inputBytes, false);

            File.WriteAllBytes(filename_b, encryptedBytes);
        }

        public void decipherFile(string filename_a, string filename_b)
        {
            Console.WriteLine("Decrypting...");
            var privateKey = File.ReadAllText(filename_private);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            byte[] inputBytes = File.ReadAllBytes(filename_b);
            byte[] decryptedBytes = rsa.Decrypt(inputBytes, false);

            File.WriteAllBytes(filename_a, decryptedBytes);
        }
        /* ----------------------------------- ex3 ---------------------------------- */
        public bool verifySignaturre(string filename_a, string filename_b)
        {
            Console.WriteLine("Verifying...");
            var publicKey = File.ReadAllText(filename_public);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(publicKey);

            byte[] inputBytes = File.ReadAllBytes(filename_a);
            byte[] signatureBytes = File.ReadAllBytes(filename_b);

            bool verified = rsa.VerifyData(inputBytes, new SHA1CryptoServiceProvider(), signatureBytes);
            Console.WriteLine("Verified: " + verified);
            return verified;
        }

        public void generateSignature(string filename_a, string filename_b)
        {
            Console.WriteLine("Generating signature...");
            var privateKey = File.ReadAllText(filename_private);
            var rsa = new RSACryptoServiceProvider();
            rsa.FromXmlString(privateKey);

            byte[] inputBytes = File.ReadAllBytes(filename_a);
            byte[] signatureBytes = rsa.SignData(inputBytes, new SHA1CryptoServiceProvider());

            File.WriteAllBytes(filename_b, signatureBytes);
        }

    }
}
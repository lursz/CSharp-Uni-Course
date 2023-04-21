using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Text;

namespace AssymetricPassword
{
    public class Password
    {

        // create salt form string
        byte[] salt = Encoding.UTF8.GetBytes("CwuuJx/7");
        byte[] initVector = Encoding.UTF8.GetBytes("uyZG5sl561Wo2ZTE");
        int iterations = 5;


        public byte[] Encrypt(byte[] input_data, string password)
        {
            System.Console.WriteLine(salt.Length);
            System.Console.WriteLine(initVector.Length); 
            Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            Aes encAlg = Aes.Create();
            encAlg.IV = initVector;
            encAlg.Key = key.GetBytes(16);
            MemoryStream encryptionStream = new MemoryStream();
            CryptoStream encrypt = new CryptoStream(encryptionStream,
                encAlg.CreateEncryptor(), CryptoStreamMode.Write);
            encrypt.Write(input_data, 0, input_data.Length);
            encrypt.FlushFinalBlock();
            encrypt.Close();
            key.Reset();
            return encryptionStream.ToArray();
        }

        public byte[] Decrypt(byte[] input_data, string password)
        {   

            Rfc2898DeriveBytes k1 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            Aes decAlg = Aes.Create();
            decAlg.Key = k1.GetBytes(16);
            decAlg.IV = initVector;
            MemoryStream decryptionStreamBacking = new MemoryStream();
            CryptoStream decrypt = new CryptoStream(
                decryptionStreamBacking, decAlg.CreateDecryptor(), CryptoStreamMode.Write);
            decrypt.Write(input_data, 0, input_data.Length);
            decrypt.Flush();
            decrypt.Close();
            k1.Reset();
            return decryptionStreamBacking.ToArray();
        }
    }
}

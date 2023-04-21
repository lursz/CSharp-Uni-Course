using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AssymetricPassword
{
    public class Password
    {

        public void Encrypt(string filename_a, string filename_b, string password)
        {
            try
            {
                using (FileStream fileStreamIn = new FileStream(filename_a, FileMode.Open))
                {
                    byte[] key ={0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16,0x17, 0x18, 0x19, 0x20, 0x21, 0x21, 0x22, 0x23,0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x30, 0x31};
                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = key;
                        using (FileStream fileStreamOut = new FileStream(filename_b, FileMode.Create))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(fileStreamOut, aes.CreateEncryptor(), CryptoStreamMode.Write))
                            {
                                fileStreamIn.CopyTo(cryptoStream);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }


        public void Decrypt(string filename_a, string filename_b, string password)
        {
            try
            {
                using (FileStream fileStreamIn = new FileStream(filename_a, FileMode.Open))
                {
                    byte[] key ={0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08,0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16,0x17, 0x18, 0x19, 0x20, 0x21, 0x21, 0x22, 0x23,0x24, 0x25, 0x26, 0x27, 0x28, 0x29, 0x30, 0x31};
                    using (Aes aes = Aes.Create())
                    {
                        aes.Key = key;
                        using (FileStream fileStreamOut = new FileStream(filename_b, FileMode.Create))
                        {
                            using (CryptoStream cryptoStream = new CryptoStream(fileStreamOut, aes.CreateDecryptor(), CryptoStreamMode.Write))
                            {
                                fileStreamIn.CopyTo(cryptoStream);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}
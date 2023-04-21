using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace SHA512orMD5
{
    public class Verifier
    {
        public string ComputeHash( string filePath, string algorithm)
        {
            string [] available_algorithm = new string[] {"MD5", "SHA256", "SHA512"}; 
            if (!available_algorithm.Contains(algorithm.ToUpper()))
                throw new ArgumentException("Invalid algorithm specified");
        

            using (var hashAlgorithm = HashAlgorithm.Create(algorithm.ToUpper()))
            {
                using (var stream = System.IO.File.OpenRead(filePath))
                {
                    var hash = hashAlgorithm.ComputeHash(stream);
                    return Convert.ToBase64String(hash);
                }
            }
           
        }
        public bool verifyFile (string file_path,  string hash_file, string algorithm)
        {
            string [] available_algorithm = new string[] {"MD5", "SHA256", "SHA512"}; 
            if (!available_algorithm.Contains(algorithm.ToUpper()))
                throw new ArgumentException("Invalid algorithm specified");
            if (!File.Exists(hash_file))
                throw new ArgumentException("Hash file does not exist");

            string hash = File.ReadAllText(hash_file);
            string file_hash = ComputeHash(file_path, algorithm);
            return hash == file_hash;

        }
        
        
    }
}
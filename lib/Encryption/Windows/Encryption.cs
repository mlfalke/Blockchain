using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

using System.Security.Permissions;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain
{
    public class Encryption
    {
        

        public string DataEncrypt(string data, X509Certificate2 cert)
        {

            

            using (RSA rsa = cert.GetRSAPublicKey()) {

               UnicodeEncoding ByteConverter = new UnicodeEncoding();
               byte[] Bdata = ByteConverter.GetBytes(data); 
               byte[] encryptedData = rsa.Encrypt(Bdata, RSAEncryptionPadding.OaepSHA256);
               string encryptedDataS = Convert.ToBase64String(encryptedData);
               return encryptedDataS;

            }
            
            
        }

        public string DataDecrypt(string data, X509Certificate2 cert)
        {
            
            using (RSA rsa = cert.GetRSAPrivateKey())
            {
               UnicodeEncoding ByteConverter = new UnicodeEncoding();
               byte[] Bdata = ByteConverter.GetBytes(data); 
               byte[] decryptedData = rsa.Decrypt(Bdata, RSAEncryptionPadding.OaepSHA256);
               string decryptedDataS = Convert.ToBase64String(decryptedData);
               return decryptedDataS;
            }
            
        }
        
        
        
    
        


    }
}
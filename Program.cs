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
    class Program
    {

        

        static void Main(string[] args)
        {
            string Private = @"private_key.pem";
            string Public = @"public_key.pem";
            Key.GenerateRsaKeyPair(Private, Public);

            //;
            //Encryption.AddToMyStore(Encryption.Create("watdan", DateTime.Now, DateTime.MaxValue, "Hetwachtwoord"));
            Blockchain GovernmentChain = new Blockchain();
            //FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);
             // The path to the certificate.
            //string Certificate =  @"0AE76E4CB07854E7CE19866BB97CE73246DFAA27.cer";
            
			//Create X509Certificate2 object from .cer file.
            //X509Certificate2 x509 = new X509Certificate2(File.ReadAllBytes(Certificate));
			//byte[] rawData = Encryption.ReadFile(Certificate);
            

			
    	    // Console.WriteLine("{0}Public Key Format: {1}{0}",Environment.NewLine,x509.PublicKey.Key.ToString());
            // Console.WriteLine("{0}Certificate to XML String: {1}{0}",Environment.NewLine,x509.PublicKey.ToString());
            //Load the certificate into an X509Certificate object.
            // X509Certificate cert = X509Certificate.CreateFromCertFile(Certificate);
    	    //Encryption.PrivateKeyFromPemFile(Certificate);
            // // //Get the value.
            // byte[] results = cert.GetPublicKey();
            // Encryption.IsInMyStore();
            // // Display the value to the console.
            // foreach(byte b in results)
            // {
            //     Console.Write(b);
            // }
            //Reads a file.
            
            GovernmentChain.AddBlock(new Block(DateTime.Now,null,"{sender:deze}"));

            Console.WriteLine(JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented));
            //string fileName =  @"397B767444CA06357BE84C8F0D914C3C9A64C4EF.cer";
           
			
            // using(StreamWriter writer = new StreamWriter(filestreamCreate))
            // {
                
            //     writer.Write(JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented));
                
            // };
            
            
            Console.ReadKey();
        }
    }

}


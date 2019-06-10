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
            //FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);
             // The path to the certificate.
            //string Certificate =  @"0AE76E4CB07854E7CE19866BB97CE73246DFAA27.cer";
            
            Blockchain GovernmentChain = new Blockchain();
            //FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);
            
            List<Company> companies = new List<Company>();
            using (StreamReader r = new StreamReader("companies.json"))
            {
                string companiesJson = r.ReadToEnd();
                companies = JsonConvert.DeserializeObject<List<Company>>(companiesJson);
            }
            
            //Change to loadServerJson() (server output)
            Server server;
            using (StreamReader r = new StreamReader("Server.json"))
                {
                    string serverJson = r.ReadToEnd();
                    server = JsonConvert.DeserializeObject<Server>(serverJson);
                }
            //Search in lists of companies for host name to get hostCompany
            Company hostCompany = new Company();
            foreach( Company c in companies){
                if(c.name == server.Name){
                    hostCompany = c;
                    break;
                }
            }

            //Hardcoded shit as information input:
            Person subject = new Person("Sparrow", "746385746", DateTime.Now);
            string newDataType = "antecedenten";
            string newDataValue = "1";
            Data newData = new Data(newDataType, newDataValue);

            GovernmentChain.AddBlock(new Block(DateTime.Now, newData, subject, companies, hostCompany));
            Console.WriteLine(JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented));


            // using(StreamWriter writer = new StreamWriter(filestreamCreate))
            // {                
            //     writer.Write(JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented)); 
            // };
            
            Console.ReadKey();
        }
    }

}


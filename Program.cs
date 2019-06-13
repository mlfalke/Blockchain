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

            Blockchain GovernmentChain = new Blockchain();
            
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

            // Opzet voor tests.
            string blocktest = "";
            string hashtest = "";

            Block testblock;
            testblock = new Block(DateTime.Now, newData, subject, companies, hostCompany);
            // #####################

            // Unit test blocks
            blocktest = JsonConvert.SerializeObject(GovernmentChain.GetLatestBlock(), Formatting.Indented);
            Console.WriteLine("Block test:" + Environment.NewLine + blocktest + Environment.NewLine);
            // #####################

            // Integration test adding blocks to the chain
            Console.WriteLine("Before adding block:" + Environment.NewLine + JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented) + Environment.NewLine);
            GovernmentChain.AddBlock(testblock);
            Console.WriteLine("After adding block:" + Environment.NewLine + JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented) + Environment.NewLine);

            // Test op validiteit
            Console.WriteLine("Validiteit test:" + Environment.NewLine + GovernmentChain.IsValid() + Environment.NewLine);
            // #####################

            // Unit test encryption
            hashtest = testblock.CalculateHash();
            Console.WriteLine("Hash test:" + Environment.NewLine + hashtest + Environment.NewLine);
            // #####################

            // Test op validiteit
            Console.WriteLine("Validiteit test:" + Environment.NewLine + GovernmentChain.IsValid() + Environment.NewLine);
            // #####################
        }
    }

}


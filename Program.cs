using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace Blockchain
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Blockchain GovernmentChain = new Blockchain();
            FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);

            GovernmentChain.AddBlock(new Block(DateTime.Now,null,  "{sender:deze}"));

            Console.WriteLine(JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented));

            

            using(StreamWriter writer = new StreamWriter(filestreamCreate))
            {
                
                writer.Write(JsonConvert.SerializeObject(GovernmentChain, Formatting.Indented));
                
            };
            
            
            Console.ReadKey();
        }
    }

}


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
            
            Blockchain mafcoin = new Blockchain();
            FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);

            mafcoin.AddBlock(new Block(DateTime.Now,null,  "{sender:deze}"));

            Console.WriteLine(JsonConvert.SerializeObject(mafcoin, Formatting.Indented));

            

            using(StreamWriter writer = new StreamWriter(filestreamCreate))
            {
                
                writer.Write(JsonConvert.SerializeObject(mafcoin, Formatting.Indented));
                
            };
            
            
            Console.ReadKey();
        }
    }

}


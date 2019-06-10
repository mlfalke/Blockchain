using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Blockchain
{
    public class Data {
        public string type;
        public string value;

        public Data(string type, string value){
            this.type = type;
            this.value = value;
        }
    }
}
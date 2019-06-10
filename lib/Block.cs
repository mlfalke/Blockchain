using System;
using System.Security.Cryptography;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace Blockchain
{
    public class Block  
{  
    public int Index { get; set; }  
    public DateTime TimeStamp { get; set; }  
    public string PreviousHash { get; set; }  
    public string Hash { get; set; }  
    public string Data { get; set; }  
  
    public Block(DateTime timeStamp){
        this.Index = 0;
        this.TimeStamp = timeStamp;
        this.PreviousHash = "";
        this.Data = "{}";
        this.Hash = CalculateHash();
    }

    public Block(DateTime timeStamp, Data newData, Person person)  
    {  
        // //assymetric encryption method
        // //name of keycontainer
        string KeyContainerName = "MyKeyContainer";
        Key.RSAPersistKeyInCSP(KeyContainerName);
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        // //encrypt string
        // byte[] Bdata = Encryption.RSAEncrypt(ByteConverter.GetBytes(data),KeyContainerName,false);
        // //decrypt bytes
        // byte[] Rdata = Encryption.RSADecrypt(Bdata,KeyContainerName,false);

        // // string Sdata = RSACryptography.CryptographyHelper.Encrypt(data);
        // // string Ddata= RSACryptography.CryptographyHelper.Decrypt(Sdata);

        //Get companyName by reading Server.json, which contains info over the running server(s)
        string companyName = "";
        using (StreamReader r = new StreamReader("../Server.json"))
                {
                    string serverJson = r.ReadToEnd();
                    Server server = JsonConvert.DeserializeObject<Server>(serverJson);
                    companyName = server.Name;
                }

        //Define 'data' variable that will be ecnrypted later
        string data = "{'value': "+newData.value+", 'person': {'surname': '"+person.surname+"', 'bsn': '"+person.bsn+"', 'birthDate': '"+person.birthDate.ToString()+"'}}";
        //Change data to byte[] in order to encrypt it later
        byte[] Bdata = ByteConverter.GetBytes(data);
        string blockData = "{'type': '"+newData.type+"', 'sender': '"+companyName+"', 'value': [";

        //Get companies from companies.json, including their permissions
        List<Company> companies = new List<Company>();
        using (StreamReader r = new StreamReader("../companies.json"))
                {
                    string companiesJson = r.ReadToEnd();
                    companies = JsonConvert.DeserializeObject<List<Company>>(companiesJson);
                }
        //Check if companies have permission to read the data. If so, encrypt the data with their keys and add it to JSON-formatted string blockData
        foreach(Company c in companies){
            foreach(Permission p in c.GetTruePermissions()){
                if(p.name == newData.type){
                    //TO DO: Add gathering of specific public key of the company (c) with "c.publicKey" and encrypt variable "data" with that key.
                    blockData = blockData + "{'targetCompany': '"+c.name+"', 'Data': '"+Encryption.Encrypt(data)+"'},";
                }
            }
        }
        //Close and finish the blockData string so it is fully closed off
        blockData = blockData.Substring(0,blockData.Length-1);
        blockData = blockData + "]}";

        this.Index = 0;  
        this.TimeStamp = timeStamp;  
        this.PreviousHash = "";  
        this.Data = blockData;
        this.Hash = CalculateHash();  
    }

    public Block(DateTime timeStamp, Data newData, Person person, List<Company> companies, Company hostCompany)  
    {  
        // //assymetric encryption method
        // //name of keycontainer
        string KeyContainerName = "MyKeyContainer";
        Key.RSAPersistKeyInCSP(KeyContainerName);
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        // //encrypt string
        // byte[] Bdata = Encryption.RSAEncrypt(ByteConverter.GetBytes(data),KeyContainerName,false);
        // //decrypt bytes
        // byte[] Rdata = Encryption.RSADecrypt(Bdata,KeyContainerName,false);

        // // string Sdata = RSACryptography.CryptographyHelper.Encrypt(data);
        // // string Ddata= RSACryptography.CryptographyHelper.Decrypt(Sdata);

        //Define variable 'data' that will be encrypted later
        string data = "{'value': "+newData.value+", 'person': {'surname': '"+person.surname+"', 'bsn': '"+person.bsn+"', 'birthDate': '"+person.birthDate.ToString()+"'}}";
        //Change data to byte[] in order to encrypt it later
        byte[] Bdata = ByteConverter.GetBytes(data);
        string blockData = "{'type': '"+newData.type+"', 'sender': '"+hostCompany.name+"', 'value': [";

        //Check if companies have permission to read the data. If so, encrypt the data with their keys and add it to JSON-formatted string blockData
        foreach(Company c in companies){
            foreach(Permission p in c.GetTruePermissions()){
                if(p.name == newData.type){
                    //TO DO: Add gathering of specific public key of the company (c) with "c.publicKey" and encrypt variable "data" with that key.
                    blockData = blockData + "{'targetCompany': '"+c.name+"', 'Data': '"+Encryption.Encrypt(data)+"'},";
                }
            }
        }
        //Close and finish the blockData string so it is fully closed off
        blockData = blockData.Substring(0,blockData.Length-1);
        blockData = blockData + "]}";

        this.Index = 0;  
        this.TimeStamp = timeStamp;  
        this.PreviousHash = "";  
        this.Data = blockData;
        this.Hash = CalculateHash();  
    }  

    
  
    public string CalculateHash()  
    {  
        SHA256 sha256 = SHA256.Create();  
  
        byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}");  
        byte[] outputBytes = sha256.ComputeHash(inputBytes);  
  
        return Convert.ToBase64String(outputBytes);  
    }  
}
}
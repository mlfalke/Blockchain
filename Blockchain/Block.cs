using System;
using System.Security.Cryptography;
using System.Text;

namespace Blockchain
{
    public class Block  
{  
    public int Index { get; set; }  
    public DateTime TimeStamp { get; set; }  
    public string PreviousHash { get; set; }  
    public string Hash { get; set; }  
    public string Data { get; set; }  
    public string Ddata { get; set; }
  
    public Block(DateTime timeStamp, string previousHash, string data)  
    {  
        //assymetric encryption method
        //name of keycontainer
        string KeyContainerName = "MyKeyContainer";
        Key.RSAPersistKeyInCSP(KeyContainerName);
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        //encrypt string
        byte[] Bdata = Encryption.RSAEncrypt(ByteConverter.GetBytes(data),KeyContainerName,false);
        //decrypt bytes
        byte[] Rdata = Encryption.RSADecrypt(Bdata,KeyContainerName,false);

        Index = 0;  
        TimeStamp = timeStamp;  
        PreviousHash = previousHash;  
        Data = ByteConverter.GetString(Rdata);
        Hash = CalculateHash();  
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
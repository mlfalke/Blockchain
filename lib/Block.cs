using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
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
    

  
    public Block(DateTime timeStamp, string previousHash, string data)  
    {  
        // assymetric encryption method
        // name of keycontainer
        //FUCKDEZECODE(data);
        // decrypt bytes
        //byte[] Rdata = Encryption.RSADecrypt(Bdata,KeyContainerName,false);
        
        
        //string Sdata = RSACryptography.CryptographyHelper.Encrypt(data);
        //string Ddata= RSACryptography.CryptographyHelper.Decrypt(Sdata); 
        

        Index = 0;  
        TimeStamp = timeStamp;  
        PreviousHash = previousHash;  
        Data = FUCKDEZECODE(data);
        Hash = CalculateHash();  
    }  

    
  
    public string CalculateHash()  
    {  
        SHA256 sha256 = SHA256.Create();  
  
        byte[] inputBytes = Encoding.ASCII.GetBytes($"{TimeStamp}-{PreviousHash ?? ""}-{Data}");  
        byte[] outputBytes = sha256.ComputeHash(inputBytes);  
  
        return Convert.ToBase64String(outputBytes);  
    } 
    public string FUCKDEZECODE(string data)
    {

        string Private = "private_key.pem";
        string Public = "public_key.pem";
        
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        var ndata = ByteConverter.GetBytes(data);
        string output = string.Empty;
        X509Certificate2 cert = Encryption.Create("watdan", DateTime.Now, DateTime.MaxValue, "Hetwachtwoord");
        using (RSA rsa = cert.GetRSAPublicKey())
        {
            return Convert.ToBase64String(rsa.Encrypt(ndata, RSAEncryptionPadding.OaepSHA1));
        }

       


        // var Sdata = Encryption.RSAEncrypt(ByteConverter.GetBytes(data),"key",false);
        // var RData = Encryption.RSADecrypt(Sdata,"key",false);

        
    }

}

}
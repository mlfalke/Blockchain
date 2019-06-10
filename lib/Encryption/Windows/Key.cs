using System;
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;

namespace Blockchain
{
    public class Key
    {
        //windows manier van opslaan RSA keys
        public static void RSAPersistKeyInCSP(string ContainerName)
    {
        try
        {
            // Create a new instance of CspParameters.  Pass
            // 13 to specify a DSA container or 1 to specify
            // an RSA container.  The default is 1.
            CspParameters cspParams = new CspParameters();

            // Specify the container name using the passed variable.
            cspParams.KeyContainerName = ContainerName;

            FileStream filestreamCreate = new FileStream("Blockchain.txt", FileMode.OpenOrCreate);

            //Create a new instance of RSACryptoServiceProvider to generate
            //a new key pair.  Pass the CspParameters class to persist the 
            //key in the container.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);
            //Indicate that the key was persisted.
            Console.WriteLine("The RSA key was persisted in the container, \"{0}\".", ContainerName);
        }
        catch(CryptographicException e)
        {
            Console.WriteLine(e.Message);

        }
    }

    //windows manier van RSA key verwijderen
    public static void RSADeleteKeyInCSP(string ContainerName)
    {
        try
        {
            // Create a new instance of CspParameters.  Pass
            // 13 to specify a DSA container or 1 to specify
            // an RSA container.  The default is 1.
            CspParameters cspParams = new CspParameters();

            // Specify the container name using the passed variable.
            cspParams.KeyContainerName = ContainerName;

            //Create a new instance of RSACryptoServiceProvider. 
            //Pass the CspParameters class to use the 
            //key in the container.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

            //Delete the key entry in the container.
            RSAalg.PersistKeyInCsp = false;

            //Call Clear to release resources and delete the key from the container.
            RSAalg.Clear();

            //Indicate that the key was persisted.
            Console.WriteLine("The RSA key was deleted from the container, \"{0}\".", ContainerName);
        }
        catch(CryptographicException e)
        {
            Console.WriteLine(e.Message);

        }
    }

    //maakt een public of private key aan
    public static void GenerateRsaKeyPair(String privateKeyFilePath, String publicKeyFilePath)  
{  
    X509Name name = new X509Name("CN=Client Cert, C=NL");
    RsaKeyPairGenerator rsaGenerator = new RsaKeyPairGenerator();  
    rsaGenerator.Init(new KeyGenerationParameters(new SecureRandom(), 2048));  
    AsymmetricCipherKeyPair keyPair = rsaGenerator.GenerateKeyPair();  
  
    Pkcs10CertificationRequest csr = new Pkcs10CertificationRequest("SHA1WITHRSA", name, keyPair.Public, null, keyPair.Private);
  
    Chilkat.Rsa rsa = new Chilkat.Rsa();

    bool succes = rsa.GenerateKey(1024);
    if (succes != true) {
        Debug.WriteLine(rsa.LastErrorText);
        return;
    }

    string publicKeyXml = rsa.ExportPublicKey();
    Debug.WriteLine(publicKeyXml);

    string privateKeyXml = rsa.ExportPrivateKey();
    Debug.WriteLine(privateKeyXml);

    Chilkat.PrivateKey privKey = new Chilkat.PrivateKey();
    succes = privKey.LoadXml(privateKeyXml);
    succes = privKey.SaveRsaPemFile("privateKey.pem");

    Chilkat.PublicKey pubKey = new Chilkat.PublicKey();
    succes = pubKey.LoadXml(publicKeyXml);
    succes = pubKey.SaveOpenSslPemFile("publicKey.pem");
    
    
    using (TextWriter privateKeyTextWriter = new StringWriter())  
    {  
        //RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(1);
        PemWriter pemWriter = new PemWriter(privateKeyTextWriter);  
        pemWriter.WriteObject(csr);  
        pemWriter.Writer.Flush();  
        File.WriteAllText(privateKeyFilePath, privateKeyTextWriter.ToString());  
    }  
  
  
    using (TextWriter publicKeyTextWriter = new StringWriter())  
    {  
        //RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(1);
        PemWriter pemWriter = new PemWriter(publicKeyTextWriter);  
        pemWriter.WriteObject(csr);  
        pemWriter.Writer.Flush();  
  
        File.WriteAllText(publicKeyFilePath, publicKeyTextWriter.ToString());  
  

    }
}
    }}


    

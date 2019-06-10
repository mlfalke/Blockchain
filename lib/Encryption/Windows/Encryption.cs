using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Org.BouncyCastle.Asn1.Pkcs;
using Org.BouncyCastle.Asn1.X509;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Math;
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Pkcs;
using Org.BouncyCastle.Security;
using Org.BouncyCastle.X509;
using X509Certificate = Org.BouncyCastle.X509.X509Certificate;




using Org.BouncyCastle.Crypto.Prng;
using Org.BouncyCastle.Crypto.Operators;

namespace Blockchain
{
    public class Encryption
    {

        private static string KEY = "kPSwFT2IILOOzUOB6NpGgxwDyrBWjklO"; // pick some other 32 chars
        private static byte[] KEY_BYTES = Encoding.UTF8.GetBytes(KEY);

        static public byte[] RSAEncrypt(byte[] DataToEncrypt, string ContainerName, bool DoOAEPPadding)
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
            //Pass the CspParameters class to use the key 
            //from the key in the container.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);
            //Console.WriteLine(RSAalg.ToXmlString(true));
            
            UnicodeEncoding ByteConverter = new UnicodeEncoding();

            RSAalg.Encrypt(DataToEncrypt, DoOAEPPadding);
            
            //Encrypt the passed byte array and specify OAEP paddi   ng.  
            //OAEP padding is only available on Microsoft Windows XP or
            //later.  
            return RSAalg.Encrypt(DataToEncrypt, DoOAEPPadding);
        }
            //Catch and display a CryptographicException  
            //to the console.
        catch(CryptographicException e)
        {
            Console.WriteLine(e.Message);

            return null;
        }

    }

         static public byte[] RSADecrypt(byte[] DataToDecrypt, string ContainerName, bool DoOAEPPadding)
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
            //Pass the CspParameters class to use the key 
            //from the key in the container.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider(cspParams);

            //Decrypt the passed byte array and specify OAEP padding.  
            //OAEP padding is only available on Microsoft Windows XP or
            //later.  
            return RSAalg.Decrypt(DataToDecrypt, DoOAEPPadding);
        }
            //Catch and display a CryptographicException  
            //to the console.
        catch(CryptographicException e)
        {
            Console.WriteLine(e.ToString());

            return null;
        }


    }


    internal static byte[] ReadFile (string fileName)
	{
		FileStream f = new FileStream(fileName, FileMode.Open, FileAccess.Read);
		int size = (int)f.Length;
		byte[] data = new byte[size];
		size = f.Read(data, 0, size);
		f.Close();
		return data;
	}



    public static string Decrypt(string cipherText) {
         // Check arguments.
         if (cipherText == null || cipherText.Length <= 0)
             throw new ArgumentNullException("cipherText");
 
         // Declare the string used to hold
         // the decrypted text.
         string plaintext = null;
 
         // Create an AesManaged object
         // with the specified key and IV.
         using (Rijndael algorithm = Rijndael.Create()) {
             algorithm.Key = KEY_BYTES;
 
             // Get bytes from input string
             byte[] cipherBytes = Convert.FromBase64String(cipherText);
 
             // Create the streams used for decryption.
             using (MemoryStream msDecrypt = new MemoryStream(cipherBytes)) {
                 // Read IV first
                 byte[] IV = new byte[16];
                 msDecrypt.Read(IV, 0, IV.Length);
 
                 // Assign IV to an algorithm
                 algorithm.IV = IV;
 
                 // Create a decrytor to perform the stream transform.
                 var decryptor = algorithm.CreateDecryptor(algorithm.Key, algorithm.IV);
 
                 using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read)) {
                     using (var srDecrypt = new StreamReader(csDecrypt)) {
                         // Read the decrypted bytes from the decrypting stream
                         // and place them in a string.
                         plaintext = srDecrypt.ReadToEnd();
                     }
                 }
             }
         }
         return plaintext;
     }

//      public static RSACryptoServiceProvider PrivateKeyFromPemFile(String filePath)  
// {  
//     using (TextReader privateKeyTextReader = new StringReader(File.ReadAllText(filePath)))  
//     {  
//         X509Certificate2 readKeyPair = (X509Certificate2)new PemReader(privateKeyTextReader).ReadObject();  
  
  
//         RsaPrivateCrtKeyParameters privateKeyParams = ((RsaPrivateCrtKeyParameters)readKeyPair);  
//         RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();  
//         RSAParameters parms = new RSAParameters();  
  
//         parms.Modulus = privateKeyParams.Modulus.ToByteArrayUnsigned();  
//         parms.P = privateKeyParams.P.ToByteArrayUnsigned();  
//         parms.Q = privateKeyParams.Q.ToByteArrayUnsigned();  
//         parms.DP = privateKeyParams.DP.ToByteArrayUnsigned();  
//         parms.DQ = privateKeyParams.DQ.ToByteArrayUnsigned();  
//         parms.InverseQ = privateKeyParams.QInv.ToByteArrayUnsigned();  
//         parms.D = privateKeyParams.Exponent.ToByteArrayUnsigned();  
//         parms.Exponent = privateKeyParams.PublicExponent.ToByteArrayUnsigned();  
  
//         cryptoServiceProvider.ImportParameters(parms);  
  
//         return cryptoServiceProvider;  
//     }  
// }  
  
public static RSACryptoServiceProvider PublicKeyFromPemFile(String filePath)  
{  
    using (TextReader publicKeyTextReader = new StringReader(File.ReadAllText(filePath)))  
    {  
        RsaKeyParameters publicKeyParam = (RsaKeyParameters)new PemReader(publicKeyTextReader).ReadObject();  
  
        RSACryptoServiceProvider cryptoServiceProvider = new RSACryptoServiceProvider();  
        RSAParameters parms = new RSAParameters();  
  
  
        
        parms.Modulus = publicKeyParam.Modulus.ToByteArrayUnsigned();  
        parms.Exponent = publicKeyParam.Exponent.ToByteArrayUnsigned();  
  
  
        cryptoServiceProvider.ImportParameters(parms);  
  
       return cryptoServiceProvider;  
    }  
}


     public static string Encrypt(string plainText) {
         // Check arguments.
         if (plainText == null || plainText.Length <= 0)
             throw new ArgumentNullException("plainText");
 
         byte[] encrypted;
         // Create an AesManaged object
         // with the specified key and IV.
         using (Rijndael algorithm = Rijndael.Create()) {
             algorithm.Key = KEY_BYTES;
 
             // Create a decrytor to perform the stream transform.
             var encryptor = algorithm.CreateEncryptor(algorithm.Key, algorithm.IV);
 
             // Create the streams used for encryption.
             using (var msEncrypt = new MemoryStream()) {
                 using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write)) {
                     using (var swEncrypt = new StreamWriter(csEncrypt)) {
                         // Write IV first
                         msEncrypt.Write(algorithm.IV, 0, algorithm.IV.Length);
                         //Write all data to the stream.
                         swEncrypt.Write(plainText);
                     }
                     encrypted = msEncrypt.ToArray();
                 }
             }
         }
 
         // Return the encrypted bytes from the memory stream.
         return Convert.ToBase64String(encrypted);
     }




     #region Methods

        /// <summary>
        /// Adds a certificate to My store for the LocalMachine
        /// </summary>
        public static void AddToMyStore(X509Certificate2 certificate2)
        {
            X509Store store = ReturnStore();
            store.Add(certificate2);
            store.Close();
        }

        /// <summary>
        /// Static method used to create a certificate and return as a .net object
        /// </summary>
        
        public static X509Certificate2 Create(string name, DateTime start, DateTime end, string userPassword, bool addtoStore = true, string exportDirectory = @"")
        {
            // generate a key pair using RSA
            var generator = new RsaKeyPairGenerator();
            // keys have to be a minimum of 2048 bits for Azure
            generator.Init(new KeyGenerationParameters(new SecureRandom(new CryptoApiRandomGenerator()), 2048));
            AsymmetricCipherKeyPair cerKp = generator.GenerateKeyPair();
            // get a copy of the private key
            AsymmetricKeyParameter privateKey = cerKp.Private;


            // create the CN using the name passed in and create a unique serial number for the cert
            var certName = new X509Name("CN=" + name);
            BigInteger serialNo = BigInteger.ProbablePrime(120, new Random());

            // start the generator and set CN/DN and serial number and valid period
            var x509Generator = new X509V3CertificateGenerator();
            x509Generator.SetSerialNumber(serialNo);
            x509Generator.SetSubjectDN(certName);
            x509Generator.SetIssuerDN(certName);
            x509Generator.SetNotBefore(start);
            x509Generator.SetNotAfter(end);
            // add the server authentication key usage
            var keyUsage = new KeyUsage(KeyUsage.KeyEncipherment);
            x509Generator.AddExtension(X509Extensions.KeyUsage, false, keyUsage.ToAsn1Object());
            var extendedKeyUsage = new ExtendedKeyUsage(new[] {KeyPurposeID.IdKPServerAuth});
            x509Generator.AddExtension(X509Extensions.ExtendedKeyUsage, true, extendedKeyUsage.ToAsn1Object());
            // algorithm can only be SHA1 ??


            ISignatureFactory signatureFactory = new Asn1SignatureFactory("SHA256WITHRSA", cerKp.Private);
            // Set the key pair
            x509Generator.SetPublicKey(cerKp.Public);
            X509Certificate certificate = x509Generator.Generate(signatureFactory);
            

            // export the certificate bytes
            byte[] certStream = DotNetUtilities.ToX509Certificate(certificate).Export(X509ContentType.Pkcs12, userPassword);

            // build the key parameter and the certificate entry
            var keyEntry = new AsymmetricKeyEntry(privateKey);
            var entry = new X509CertificateEntry(certificate);
            // build the PKCS#12 store to encapsulate the certificate
            var builder = new Pkcs12StoreBuilder();
            builder.SetUseDerEncoding(true);
            builder.SetCertAlgorithm(PkcsObjectIdentifiers.Sha256WithRsaEncryption);
            builder.SetKeyAlgorithm(PkcsObjectIdentifiers.Sha256WithRsaEncryption);
            builder.Build();
            // create a memorystream to hold the output
            var stream = new MemoryStream(10000);
            // create the individual store and set two entries for cert and key
            var store = new Pkcs12Store();
            store.SetCertificateEntry("Created by Blockchain", entry);
            store.SetKeyEntry("Created by Blockchain", keyEntry, new[] { entry });
            store.Save(stream, userPassword.ToCharArray(), new SecureRandom());

             // Create the equivalent C# representation
            var cert = new X509Certificate2(stream.GetBuffer(), userPassword, X509KeyStorageFlags.Exportable);
            // set up the PEM writer too
            if (exportDirectory != null)
            {
                var textWriter = new StringWriter();
                var pemWriter = new PemWriter(textWriter);
                pemWriter.WriteObject(cerKp.Private, "DESEDE", userPassword.ToCharArray(), new SecureRandom());
                
                pemWriter.Writer.Flush();
                string privateKeyPem = textWriter.ToString();
                using (var writer = new StreamWriter(Path.Combine(exportDirectory, cert.Thumbprint + ".pem")))
                    {
                        writer.WriteLine(privateKeyPem);
                    }
                // also export the certs - first the .pfx
                byte[] privateKeyBytes = cert.Export(X509ContentType.Pfx, userPassword);
                using (var writer = new FileStream(Path.Combine(exportDirectory, cert.Thumbprint + ".pfx"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    writer.Write(privateKeyBytes, 0, privateKeyBytes.Length);
                }
                // also export the certs - then the .cer
                byte[] publicKeyBytes = cert.Export(X509ContentType.Cert);
                using (var writer = new FileStream(Path.Combine(exportDirectory, cert.Thumbprint + ".cer"), FileMode.OpenOrCreate, FileAccess.Write))
                {
                    writer.Write(publicKeyBytes, 0, publicKeyBytes.Length);
                }

            }

            // if specified then this add this certificate to the store
            if (addtoStore)
                AddToMyStore(cert);

            return cert;
        }

        /// <summary>
        /// Checks to see whether the certificate is in the appropriate store
        /// </summary>
        public static bool IsInMyStore(X509Certificate2 certificate2)
        {
            X509Store store = ReturnStore();
            X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindByThumbprint,
                                                                                certificate2.Thumbprint, false);
            return certCollection.Count > 0;
        }

        /// <summary>
        /// Removes a certificate from My LocalMachine Store
        /// </summary>
        public static void RemoveFromMyStore(X509Certificate2 certificate2)
        {
            X509Store store = ReturnStore();
            store.Remove(certificate2);
            store.Close();
        }

        /// <summary>
        /// Returns the My LocalMachine store
        /// </summary>
        /// <returns></returns>
        private static X509Store ReturnStore()
        {
            var store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.OpenExistingOnly | OpenFlags.ReadWrite);
            return store;
        }

        #endregion Methods
    }

     


    }

    




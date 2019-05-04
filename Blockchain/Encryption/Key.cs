using System;
using System.Security.Cryptography;

namespace Blockchain
{
    public class Key
    {
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
    }
}
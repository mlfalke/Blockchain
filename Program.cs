using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;
using System.Net.NetworkInformation;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Timers;
using System.Threading;

namespace Blockchain
{
    class Program
    {
        private static System.Timers.Timer aTimer;
        public static int Port = 0;
        public static string name = "Unknown";
        public static string IpV4Address;
        public static P2PServer Server = null;
        public static P2PClient Client = new P2PClient();
        public static Server Serverslist;
        public static Blockchain GovernmentChain = new Blockchain();
        private static Boolean tryConnect = false;
        static ManualResetEvent _quitEvent = new ManualResetEvent(false);

        static void Main(string[] args)
        {
            loadServerJson();
            if (Port > 0)
            {
                Server = new P2PServer();
                Server.Start();
                ConnectServers();
                Console.CancelKeyPress += (sender, eArgs) =>
                {
                    _quitEvent.Set();
                    eArgs.Cancel = true;
                };

                // kick off asynchronous stuff 

                _quitEvent.WaitOne();

                // cleanup/shutdown and quit


            }


        }
        static void loadServerJson()
        {
            using (StreamReader r = new StreamReader("Server.json"))
            {
                string json = r.ReadToEnd();
                Serverslist = JsonConvert.DeserializeObject<Server>(json);
                Program.IpV4Address = Serverslist.Ip;
                Program.Port = Serverslist.Port;
            }
        }
        static void ConnectServers()
        {
            foreach (var item in Serverslist.Nodes)
            {
                try
                {
                    if (Client.Connect($"ws://{item.Ip}:{item.Port}/Blockchain")==false)
                    {
                        tryConnect = true;
                    };
                }
                catch (System.Exception Error)
                {
                    Console.WriteLine(Error);
                }
            }

            if (tryConnect)
            {
                tryConnect = false;
                // Create a timer with a two second interval.
                aTimer = new System.Timers.Timer();
                // Hook up the Elapsed event for the timer. 
                aTimer.Elapsed += OnTimedEvent;
                aTimer.AutoReset = false;
                aTimer.Interval = 1000;
                aTimer.Enabled = true;
            }
        }
        private static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            Console.WriteLine("The Elapsed event was raised at {0:HH:mm:ss.fff}",
                            e.SignalTime);
            ConnectServers();
        }
    }

    public class Server
    {
        public int Port;
        public string Ip;
        public string Name;
        public Server[] Nodes;
    }
}


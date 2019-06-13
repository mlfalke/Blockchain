using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using WebSocketSharp;
using WebSocketSharp.Server;


namespace Blockchain
{
    public class P2PServer : WebSocketBehavior
    {
        bool chainSynched = false;
        WebSocketServer wss = null;
        WebSocketServer ws = null;

        public void Start()
        {
            wss = new WebSocketServer($"ws://{Program.IpV4Address}:{Program.Port}");
            // ws = new WebSocketServer(Program.Port);
            wss.AddWebSocketService<P2PServer>("/Blockchain");
            wss.Start();
            Console.WriteLine($"Started server at ws://{Program.IpV4Address}:{Program.Port}");
        }
        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == "Hi Server")
            {
                Console.WriteLine(e.Data);
                Send("Hi Client");
            }
            else
            {
                Blockchain newChain = JsonConvert.DeserializeObject<Blockchain>(e.Data);

                if (newChain.IsValid() && newChain.Chain.Count > Program.GovernmentChain.Chain.Count)
                {
                    List<Transaction> newTransactions = new List<Transaction>();
                    // newTransactions.AddRange(newChain.PendingTransactions);
                    // newTransactions.AddRange(Program.GovernmentChain.PendingTransactions);

                    // newChain.PendingTransactions = newTransactions;
                    Program.GovernmentChain = newChain;
                }

                if (!chainSynched)
                {
                    Send(JsonConvert.SerializeObject(Program.GovernmentChain));
                    chainSynched = true;
                }
            }
        }



    }
}

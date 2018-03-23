using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace Websocket_Sharp_Client
{
    class Program
    {
        static void Main(string[] args)
        {

           

            while (true)
            {
                Thread.Sleep(750);
                try
                {
                    Console.WriteLine(DateTime.Now.ToString("hh:MM:ss.ff")+" - Attempting to connect...");
                    using (var ws = new WebSocket("wss://localhost:5000"))
                    {
                        ws.SslConfiguration.ServerCertificateValidationCallback = (sender, certificate, chain, sslPolicyErrors) =>
                        {
                            Console.Write("Certificate validation: ");
                            // Do something to validate the server certificate.
                            if (sslPolicyErrors == SslPolicyErrors.None)
                            {
                                Console.WriteLine(" SUCCESS --> SslPolicyErrors.None ");
                            }
                            else
                            {
                                Console.WriteLine("ERROR --> "+sslPolicyErrors.ToString());
                            }
                            return false; // If the server certificate is valid.
                        };
                        ws.Connect();

                        ws.OnMessage += (sender, e) =>
                        {
                            Console.WriteLine("Server responded with: " + e.Data);
                        };
                        ws.Send("Test message");
                        ws.WaitTime = TimeSpan.FromSeconds(1);
                        Thread.Sleep(750);
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Connection error: " + ex.Message);
                }
            }
        }

    }
}

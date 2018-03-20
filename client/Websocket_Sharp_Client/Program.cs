using System;
using System.Collections.Generic;
using System.Linq;
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
                Thread.Sleep(250);
                try
                {
                    Console.WriteLine(DateTime.Now.ToString("hh:MM:ss.ff")+" - Attempting to connect...");
                    using (var ws = new WebSocket("ws://localhost:5000"))
                    {
                        ws.Connect();
                        ws.OnMessage += (sender, e) => {
                            Console.WriteLine("Server responded with: " + e.Data);
                        };
                        ws.Send("Test message");
                        ws.WaitTime = TimeSpan.FromSeconds(1);
                        Thread.Sleep(250);
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

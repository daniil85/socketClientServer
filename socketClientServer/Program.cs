using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Xml.Serialization;

using socketClientServer.Models;

namespace socketClientServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Server server = new Server();
            Client client = new Client();

            // Start the server in a separate thread
            var serverThread = new System.Threading.Thread(server.Start);
            serverThread.Start();

            // Simulate client requests
            client.RequestAllCars();
            client.RequestCarByIndex(0);

            // Wait for server thread to finish
            serverThread.Join();
        }
    }
}

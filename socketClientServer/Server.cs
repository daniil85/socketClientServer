using socketClientServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace socketClientServer
{
    public class Server
    {
        private List<Car> carList;

        public Server()
        {
            carList = new List<Car>
            {
                new Car { Mark = "Nissan", Year = 2008, EngineVolume = 1.6f, NumberOfDoors = 4 }
            };
        }

        public void Start()
        {
            TcpListener listener = new TcpListener(IPAddress.Any, 1234);
            listener.Start();
            Console.WriteLine("Server started. Waiting for client connection...");

            while (true)
            {
                using (TcpClient client = listener.AcceptTcpClient())
                using (NetworkStream stream = client.GetStream())
                {
                    byte[] buffer = new byte[1024];
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                    string response = ProcessRequest(request);
                    byte[] responseData = Encoding.ASCII.GetBytes(response);

                    stream.Write(responseData, 0, responseData.Length);
                    stream.Flush();

                    Console.WriteLine("Response sent to client: " + response);
                }
            }
        }

        private string ProcessRequest(string request)
        {
            string[] requestParts = request.Split(':');
            string command = requestParts[0];

            if (command == "getall")
            {
                return GetCarListXml();
            }
            else if (command == "getbyindex" && requestParts.Length > 1)
            {
                int index = int.Parse(requestParts[1]);
                if (index >= 0 && index < carList.Count)
                {
                    return GetCarXml(carList[index]);
                }
                else
                {
                    return "Invalid index";
                }
            }

            return "Invalid command";
        }

        private string GetCarListXml()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Car>));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, carList);
                return writer.ToString();
            }
        }

        private string GetCarXml(Car car)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Car));
            using (StringWriter writer = new StringWriter())
            {
                serializer.Serialize(writer, car);
                return writer.ToString();
            }
        }
    }
}

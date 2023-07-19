using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace socketClientServer
{
    public class Client
    {
        public void RequestAllCars()
        {
            using (TcpClient client = new TcpClient("localhost", 1234))
            using (NetworkStream stream = client.GetStream())
            {
                string request = "getall";
                byte[] requestData = Encoding.ASCII.GetBytes(request);

                stream.Write(requestData, 0, requestData.Length);
                stream.Flush();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Car list received from server:");
                Console.WriteLine(response);
                SaveXmlToFile(response, "car_list.xml");
            }
        }

        public void RequestCarByIndex(int index)
        {
            using (TcpClient client = new TcpClient("localhost", 1234))
            using (NetworkStream stream = client.GetStream())
            {
                string request = "getbyindex:" + index;
                byte[] requestData = Encoding.ASCII.GetBytes(request);

                stream.Write(requestData, 0, requestData.Length);
                stream.Flush();

                byte[] buffer = new byte[1024];
                int bytesRead = stream.Read(buffer, 0, buffer.Length);
                string response = Encoding.ASCII.GetString(buffer, 0, bytesRead);

                Console.WriteLine("Car received from server:");
                Console.WriteLine(response);
                SaveXmlToFile(response, "car.xml");
            }
        }

        private void SaveXmlToFile(string xml, string fileName)
        {
            File.WriteAllText(fileName, xml);
            Console.WriteLine("XML saved to file: " + fileName);
        }
    }
}

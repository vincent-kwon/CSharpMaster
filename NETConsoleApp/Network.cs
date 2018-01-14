using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace NETConsoleApp
{

    class Server
    {
        string bindIp = "10.11.111.22";
        const int bindPort = 5425;
        TcpListener server = null;
        public void TestNetwork()
        {
            try
            {
                IPEndPoint localAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);

                server = new TcpListener(localAddress);

                Console.WriteLine("Start server...");

                while (true)
                {
                    TcpClient tcpClient = server.AcceptTcpClient();
                    //@20180113-vincent: get client info as below
                    Console.WriteLine("client: {0} ", ((IPEndPoint)tcpClient.Client.RemoteEndPoint).ToString());

                    NetworkStream stream = tcpClient.GetStream();

                    int length;
                    string data = null;
                    byte[] bytes = new byte[256];
                    //@20180113-vincent: add offset and length when read into byte array
                    while ((length = stream.Read(bytes, 0, bytes.Length)) != 0) 
                    {
                        data = Encoding.Default.GetString(bytes, 0, length); //@20180115-vincent: how to get string from bytes
                        Console.WriteLine(String.Format("receive: {0}", data));
                        byte[] msg = Encoding.Default.GetBytes(data);
                        stream.Write(msg, 0, data.Length);
                        Console.WriteLine(String.Format("send: {0}", data));
                    }

                    stream.Close();
                    tcpClient.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                server.Stop();
            }
        }
    }

    class Client
    {
        string bindIp = "0.0.0.0";
        string serverIp = "serverip";
        int bindPort = 5028;
        int serverPort = 1232;
        string message = "";

        public Client()
        {
            try
            {
                IPEndPoint clientAddress = new IPEndPoint(IPAddress.Parse(bindIp), bindPort);
                IPEndPoint serverAddress = new IPEndPoint(IPAddress.Parse(serverIp), serverPort);

                TcpClient client = new TcpClient(clientAddress);

                client.Connect(serverAddress);

                byte[] data = System.Text.Encoding.Default.GetBytes(message);

                NetworkStream stream = client.GetStream();

                stream.Write(data, 0, data.Length);

                Console.WriteLine("receive: {0}", message);

                data = new byte[256];

                string responseData = "";

                int bytes = stream.Read(data, 0, data.Length);

                responseData = Encoding.Default.GetString(data, 0, bytes);

                Console.WriteLine("receive: {0}", responseData);

                stream.Close();
                stream.Close();
            }
            catch (SocketException e)
            {
                Console.WriteLine(e);
            }
            Console.WriteLine("end of client");
        }
    }
}

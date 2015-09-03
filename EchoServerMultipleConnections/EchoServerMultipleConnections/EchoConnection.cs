using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServerMultipleConnections
{

    // this class is used as a single connection to a tcp client
    class EchoConnection
    {
        private Socket socket;
        private StreamReader reader;
        private StreamWriter writer;
        private EchoServer echoServer;

        public EchoConnection(Socket socket, EchoServer echoServer)
        {
            this.echoServer = echoServer;
            this.socket = socket;
          
            NetworkStream netStr = new NetworkStream(socket);
            reader = new StreamReader(netStr);
            writer = new StreamWriter(netStr);

            Console.WriteLine("Connected");
        }

        public void Listen()
        {
            while (true)
            {
                string receivedString = reader.ReadLine();
                echoServer.incomingMessage(receivedString);
            }            
        }

        public void sendMessage(String message)
        {
            writer.WriteLine(message);
            writer.Flush();
        }

        public void disposeSocket()
        {
            writer.Close();
            reader.Close();
            socket.Close();
        }

    }
}

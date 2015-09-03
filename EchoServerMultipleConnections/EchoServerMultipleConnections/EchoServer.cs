using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EchoServerMultipleConnections
{
    // this class spawns waits for a tcp connection and spawn a echocennection for each tcp connection
    // server supports multiple connections and echo to everyone connected
    // each connection is sotred in a list
    // todo: closing connections gracefully

    class EchoServer
    {
        // set port number and ip
        int portNumber = 8000;
        IPAddress ip = IPAddress.Parse("127.0.0.1");


        private List<EchoConnection> connections = new List<EchoConnection>();

        public EchoServer()
        {
            TcpListener listener = new TcpListener(ip, portNumber);
            listener.Start();

            while (true)
            {
                Console.WriteLine("Waiting for a conncetion - number of connections: {0}", connections.Count);
                Socket socket = listener.AcceptSocket();
                EchoConnection conn = new EchoConnection(socket, this);
                connections.Add(conn);

                Task.Factory.StartNew(() => conn.Listen());
                Console.WriteLine("Connected a client");          
            }
        }

        public void incomingMessage(string receivedString)
        {
            Console.WriteLine("Incoming message: {0}", receivedString);
           foreach (EchoConnection con in connections)
            {
                con.sendMessage(receivedString);
            }
        }
    }
}
﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace EchoServerMultipleConnections
{
    class Program
    {
       
        static void Main(string[] args)
        {

            new EchoServer();

        }
    }
}
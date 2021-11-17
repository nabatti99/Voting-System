using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Voting_System_Server
{
    class Program
    {
        private const int PORT = 5000;
        private const string addressString = "127.0.0.1";

        private static UnicodeEncoding encoding = new UnicodeEncoding();
        private List<Task<Socket>> sockets = new List<Task<Socket>>();

        static void Main(string[] args)
        {
            IPAddress address = IPAddress.Parse(addressString);
            TcpListener listener = new TcpListener(address, PORT);
            listener.Start();

            Console.WriteLine("Waiting for connection...");

            while (true)
            {
                Socket socket = listener.AcceptSocket();
                Console.WriteLine("A connection will be accept...");
                Task task = new Task(() =>
                {
                    new Client(socket);
                });

                task.Start();
            }
        }
    }
}

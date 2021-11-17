using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Voting_System.Utilities
{
    public class AppConnector
    {
        private TcpClient tcpClient;
        private const string HOST_NAME = "localhost";
        private const int PORT = 5000;
        private const int BUFFER_SIZE = 1024;
        private const int TIME_OUT = 30000;

        // States
        public const string ERROR = "ERROR";
        public const string SUCCESS = "SUCCESS";
        public const string DONE = "DONE";

        private static AppConnector connector;
        private static NetworkStream stream;

        private AppConnector()
        {
            try
            {
                Console.WriteLine($"Connecting to {HOST_NAME}:{PORT} ...");
                tcpClient = new TcpClient(HOST_NAME, PORT);
                stream = tcpClient.GetStream();
            } catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
            }
        }

        public static AppConnector GetInstance()
        {
            if (connector == null || !connector.tcpClient.Connected)
            {
                connector = new AppConnector();
                Console.WriteLine($"Connected to {connector.tcpClient.Client.RemoteEndPoint}");
            }

            return connector;
        }

        public static Task<Dictionary<string, string>> Request(Dictionary<string, string> content)
        {
            GetInstance();

            return Task.Run(() =>
            {
                Dictionary<string, string> response;

                try
                {
                    byte[] contentJSON = JsonSerializer.SerializeToUtf8Bytes(content);
                    stream.Write(contentJSON, 0, contentJSON.Length);

                    List<byte> responseJSON = new List<byte>();
                    byte[] buffer = new byte[BUFFER_SIZE];

                    // Waiting for data from server
                    DateTime timestamp = DateTime.Now;
                    while (!stream.DataAvailable)
                    {
                        TimeSpan time = DateTime.Now - timestamp;
                        if (time.TotalMilliseconds - TIME_OUT > 0)
                            throw new Exception("Không nhận được tín hiệu từ server");
                    }

                    // Get all data
                    do
                    {
                        stream.Read(buffer, 0, BUFFER_SIZE);
                        responseJSON.AddRange(buffer);
                    } while (stream.DataAvailable);


                    Utf8JsonReader reader = new Utf8JsonReader(responseJSON.ToArray());
                    response = JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader);

                    Console.WriteLine($"Received at {DateTime.Now}:");
                    Console.WriteLine(JsonSerializer.Serialize(response));
                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);

                    response = new Dictionary<string, string>();
                    response.Add("status", ERROR);
                    response.Add("message", e.Message);
                }

                return response;
            });
        }

    }
}

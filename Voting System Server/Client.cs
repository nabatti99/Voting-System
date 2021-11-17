using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using System.IO;
using Voting_System_Server.Models;
using Voting_System_Server.Controllers;
using System.Text.Json;

namespace Voting_System_Server
{
    public class Client
    {
        private Socket socket;
        NetworkStream stream;

        public AppState state = new AppState();

        private LoginController loginController;
        private GetInfoController getInfoController;
        private UpdateInfoController updateInfoController;
        private AddNewController addNewController;
        private DeleteController deleteController;

        private const int BUFFER_SIZE = 1024;

        // States
        public const string ERROR = "ERROR";
        public const string SUCCESS = "SUCCESS";
        public const string DONE = "DONE";

        public Client(Socket socket)
        {
            this.socket = socket;
            Console.WriteLine($"Connection received from {socket.RemoteEndPoint}");

            stream = new NetworkStream(socket);

            // Initiale controllers
            loginController = new LoginController(this);
            getInfoController = new GetInfoController(this);
            updateInfoController = new UpdateInfoController(this);
            addNewController = new AddNewController(this);
            deleteController = new DeleteController(this);

            HandleAllEvents();
        }

        private void HandleAllEvents()
        {
            while (true)
            {
                try
                {
                    Dictionary<string, string> data = Receive();

                    Console.WriteLine($"Received at {DateTime.Now}:");
                    Console.WriteLine(JsonSerializer.Serialize(data));

                    loginController.Handle(data);
                    getInfoController.Handle(data);
                    updateInfoController.Handle(data);
                    addNewController.Handle(data);
                    deleteController.Handle(data);

                } catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine(e.StackTrace);

                    if (!socket.Connected)
                    {
                        Console.WriteLine($"Connection from {socket.RemoteEndPoint} is closed");
                        socket.Close();
                        break;
                    }
                }
            }
        }

        public void Send(Dictionary<string, string> content)
        {
            byte[] contentJSON = JsonSerializer.SerializeToUtf8Bytes(content);
            stream.Write(contentJSON, 0, contentJSON.Length);
        }

        public Dictionary<string, string> Receive()
        {
            List<byte> responseJSON = new List<byte>();
            byte[] buffer = new byte[BUFFER_SIZE];

            // Waiting for data
            while (!stream.DataAvailable) {}

            // Get all data
            do
            {
                stream.Read(buffer, 0, BUFFER_SIZE);
                responseJSON.AddRange(buffer);
            } while (stream.DataAvailable);

            Utf8JsonReader reader = new Utf8JsonReader(responseJSON.ToArray());
            return JsonSerializer.Deserialize<Dictionary<string, string>>(ref reader);
        }
    }
}

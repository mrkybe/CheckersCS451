using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Data;

namespace CheckersServer
{
    internal class ConnectedClient
    {
        private Socket mySocket;
        private string clientString = "";
        private CheckersGM gameBoard;

        internal ConnectedClient(Socket inClient, CheckersGM gameBoard, string clientString)
        {
            mySocket = inClient;
            this.gameBoard = gameBoard;
            this.clientString = clientString;
            Thread connection = new Thread(DoConnection);
            connection.Start();
        }

        private void DoConnection()
        {
            int requestCount = 0;
            byte[] bytesFrom = new byte[10025];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = null;
            string rCount = null;
            requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = new NetworkStream(mySocket);
                    networkStream.Read(bytesFrom, 0, (int)mySocket.ReceiveBufferSize);
                    dataFromClient = System.Text.Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> " + "From client-" + clientString + dataFromClient);

                    rCount = Convert.ToString(requestCount);
                    serverResponse = "Server to clinet(" + clientString + ") " + rCount;
                    sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }
}
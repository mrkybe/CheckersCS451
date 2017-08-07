using System;
using System.IO;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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
            byte[] bytesFrom = new byte[10000];
            string dataFromClient = null;
            Byte[] sendBytes = null;
            string serverResponse = "SUP";
            string rCount = null;
            requestCount = 0;

            while (true)
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = new NetworkStream(mySocket);
                    networkStream.Read(bytesFrom, 0, bytesFrom.Length);

                    Turn obj = Turn.FromBytes(bytesFrom);
                    Console.WriteLine(obj.ToString());

                    sendBytes = gameBoard.ToBytes();

                    uint messageLength = (uint)sendBytes.Length;
                    byte[] messageLengthBytes = BitConverter.GetBytes(messageLength);

                    networkStream.Write(messageLengthBytes, 0, messageLengthBytes.Length);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);

                    networkStream.Flush();
                }
                catch (IOException ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                    Console.WriteLine(" Killing this thread - " + clientString);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(" >> " + ex.ToString());
                }
            }
        }
    }
}
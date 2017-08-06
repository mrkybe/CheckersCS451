using System;
using System.Collections.Generic;
using System.Threading;
using System.Net.Sockets;
using System.Text;
using Data;
using System.Drawing;
using System.Linq;
using System.Net;

namespace CheckersServer
{
    class Program
    {
        private static List<ConnectedClient> clients;
        private static List<CheckersGM> games;
        static void Main(string[] args)
        {
            clients = new List<ConnectedClient>();
            games = new List<CheckersGM>();

            SetupSockets();
        }

        public static void SetupSockets()
        {
            int clientCount = 0;

            IPHostEntry ipHostInfo = Dns.GetHostEntry("");
            IPAddress ipAddress = ipHostInfo.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 1337);

            Socket listener = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                listener.Bind(localEndPoint);
                listener.Listen(10);

                // Start listening for connections.  
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");
                    // Program is suspended while waiting for an incoming connection.  
                    Socket handler = listener.Accept();
                    Console.WriteLine("  Connection recieved, spinning up ConnectClient thread");
                    CheckersGM gameBoard;
                    if (clientCount % 2 == 0)
                    {
                        gameBoard = new CheckersGM();
                        games.Add(gameBoard);
                        Console.WriteLine("  Even connected client count, creating new gameboard");
                    }
                    else
                    {
                        gameBoard = games.Last();
                        Console.WriteLine("  Odd connected client count, matching to last gameboard");
                    }
                    clientCount++;
                    ConnectedClient client = new ConnectedClient(handler, gameBoard,  clientCount.ToString());
                    clients.Add(client);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\nServer ending, press enter to close terminal.");
            Console.Read();
        }

        static void LocalTestMain(string[] args)
        {
            CheckersGM gm = new CheckersGM();
            while (true)
            {
                try
                {
                    gm.DebugPrintFullBoard();
                    Console.Write("ENTER MOVE>");
                    var res = ParseCommand(Console.ReadLine());
                    gm.DebugPrintQueryNode(res.Item1);
                    if (res.Item1 != res.Item2)
                    {
                        gm.MakeTurn(new Turn(res.Item1, res.Item2));
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        static Tuple<Point, Point> ParseCommand(string cmd)
        {
            var values = cmd.Split(',');
            if (values.Length == 4)
            {
                var x1 = int.Parse(values[0]);
                var y1 = int.Parse(values[1]);
                var x2 = int.Parse(values[2]);
                var y2 = int.Parse(values[3]);
                var result = new Tuple<Point, Point>(new Point(x1, y1), new Point(x2, y2));
                return result;
            }
            else
            {
                var x1 = int.Parse(values[0]);
                var y1 = int.Parse(values[1]);
                var result = new Tuple<Point, Point>(new Point(x1, y1), new Point(x1, y1));
                return result;
            }
        }
    }
}

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
		CheckersGM.Player playerColor = CheckersGM.Player.NULL;
		private readonly CheckersGM gameBoard;

		public ConnectedClient otherPlayer;

		public void Notify()
		{
			try
			{
				//NetworkStream networkStream = new NetworkStream(mySocket);
				//SendBoard(networkStream);
				SendBoard();
			}
			catch (Exception ex)
			{
				Console.WriteLine(" >> " + ex.ToString());
				Console.WriteLine(" Killing this thread - " + clientString);
			}
		}

		internal ConnectedClient(Socket inClient, CheckersGM gameBoard, CheckersGM.Player playerColor, string clientString)
		{
			mySocket = inClient;
			this.gameBoard = gameBoard;
			this.playerColor = playerColor;
			this.clientString = clientString;

			Thread connection = new Thread(DoConnection);
			connection.Start();
		}

		private Turn ReadTurnFromStream() //ReadTurnFromStream(NetworkStream stream)
		{
			uint messageLength = 0;
			byte[] messageLengthBytes = BitConverter.GetBytes(messageLength);
			mySocket.Receive(messageLengthBytes, 0, messageLengthBytes.Length, SocketFlags.None);
			//stream.Read(messageLengthBytes, 0, messageLengthBytes.Length);
			messageLength = BitConverter.ToUInt32(messageLengthBytes, 0);

			byte[] bytesFrom = new byte[messageLength];
			mySocket.Receive(bytesFrom, 0, bytesFrom.Length, SocketFlags.None);
			//stream.Read(bytesFrom, 0, bytesFrom.Length);
			return Turn.FromBytes(bytesFrom);
		}

		private void SendBoard() //SendBoard(NetworkStream stream)
		{
			byte[] sendInfo = gameBoard.ToBytes();
			byte[] sendSize = BitConverter.GetBytes((uint)sendInfo.Length);

			byte[] combined = new byte[sendInfo.Length + sendSize.Length];
			Array.Copy(sendSize, combined, sendSize.Length);
			Array.Copy(sendInfo, 0, combined, sendSize.Length, sendInfo.Length);

			mySocket.Send(combined);

			//stream.Write(messageLengthBytes, 0, messageLengthBytes.Length);
			//stream.Write(sendBytes, 0, sendBytes.Length);

			//stream.Flush();
		}

		private void SendPlayerColor() //SendPlayerColor(NetworkStream stream)
		{
			byte[] sendBytes;
			if (playerColor == CheckersGM.Player.PLAYER_BLACK)
			{
				sendBytes = new byte[1] { 1 };
			}
			else if (playerColor == CheckersGM.Player.PLAYER_RED)
			{
				sendBytes = new byte[1] { 2 };
			}
			else
			{
				sendBytes = new byte[1] { 0 };
			}

			mySocket.Send(sendBytes);

			//stream.Write(sendBytes, 0, sendBytes.Length);
			//stream.Flush();
		}

		private void DoConnection()
		{
			int requestCount = 0;
			while (true)
			{
				try
				{
					//NetworkStream networkStream = new NetworkStream(mySocket);
					if (requestCount == 0)
					{
						//SendPlayerColor(networkStream);
						//SendBoard(networkStream);
						SendPlayerColor();
						SendBoard();
						requestCount++;
					}
					else
					{
						//Turn obj = ReadTurnFromStream(networkStream);
						Turn obj = ReadTurnFromStream();
						Console.WriteLine(obj.ToString());
						lock (gameBoard)
						{
							if (playerColor == gameBoard.GetCurrentPlayer())
							{
								gameBoard.MakeTurn(obj);
							}
							else
							{
								Console.WriteLine("  " + clientString + " tried to move opponents piece");
							}
						}
						otherPlayer?.Notify();
						//SendBoard(networkStream);
						SendBoard();

						requestCount++;
					}
				}
				catch (IOException ex)
				{
					Console.WriteLine(" >> " + ex.ToString());
					Console.WriteLine(" Killing this thread - " + clientString);
					break;
				}
                catch (SocketException se) {
					if (se.Message.Trim().ToLower().Equals("an existing connection was forcibly closed by the remote host"))
					{
						Console.WriteLine(" Killing this thread - " + clientString);
						break;
                    }
                }
				catch (Exception ex)
				{
					Console.WriteLine(" >> " + ex.ToString());
				}
			}
		}
	}
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;
using System.Windows.Forms;
using Data;

namespace CheckersCS451
{

	public partial class Form1 : Form
	{

		private Bitmap image_pieceRed, image_pieceBlack;
		private Bitmap image_kingRed, image_kingBlack;

		private EventHandler _finalizeLoadEvent;

		private CheckersGM _game;
		private CheckersGM.Player _myColor = CheckersGM.Player.NULL;

		private Socket clientSocket;
		private Boolean _flip = false, _jumping = false;
		private List<Point> _highlighted;

		public Form1()
		{
			InitializeComponent();

			this.SuspendLayout();

			this.ClientSize = new Size(702, 746);
			this.Size = this.ClientSize;
			this.MinimumSize = this.Size;

			InitHook();

			#region DEBUG_CODE
			// TODO: Prevent resize instead of slapping users in the 
			// face with a snap-back for the window size
			this.Resize += (s, e) => this.Size = new Size(702, 746);

			#endregion

			this.ResumeLayout();

			this._finalizeLoadEvent = new EventHandler(this.FinalizeForm);

			this.Shown += this._finalizeLoadEvent;
			this.connectToolStripMenuItem.Click += Connect;
		}

		private void Connect(object s, EventArgs e)
		{

            if (clientSocket != null && clientSocket.Connected) {
                return;
            }

			try
			{
				clientSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);

                IPAddress ipAddress = null;

                if (box_serverIP.Text.Trim().Equals(String.Empty))
                {
                    IPHostEntry ipHostInfo = Dns.GetHostEntry(box_serverIP.Text);
                    ipAddress = ipHostInfo.AddressList[0];
                } else
                {
                    try
                    {
                        ipAddress = IPAddress.Parse(box_serverIP.Text);
                    } catch (Exception)
                    {
                        MessageBox.Show("Invalid IP Address entered!");
                        return;
                    }
                }
                
                if (ipAddress == null)
                {
                    MessageBox.Show("Invalid IP Address!");
                    return;
                }

				clientSocket.Connect(ipAddress, 1337);

				_myColor = GetPlayerColor();
				_flip = _myColor == CheckersGM.Player.PLAYER_BLACK;

				keepBoardUpdatedThread = new Thread(KeepBoardUpdated);
				keepBoardUpdatedThread.Start();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.ToString());
			}
		}

		private Thread keepBoardUpdatedThread;
		private Point _from;

		private void KeepBoardUpdated()
		{
			while (clientSocket != null && clientSocket.Connected)
			{
				try
				{
					_game = GetBoardState();
					Update(_game);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.ToString());
				}
			}
		}

		private CheckersGM.Player GetPlayerColor()
		{
			NetworkStream serverStream = new NetworkStream(clientSocket);
			byte[] inStream = new byte[1];

			serverStream.Read(inStream, 0, 1);
            Thread.Sleep(100);

			if (inStream[0] == 1)
			{
				return CheckersGM.Player.PLAYER_BLACK;
			}
			else if (inStream[0] == 2)
			{
				return CheckersGM.Player.PLAYER_RED;
			}
			else
			{
				return CheckersGM.Player.NULL;
			}
		}

		private void SendTurn(Turn turn)
		{
			NetworkStream serverStream = new NetworkStream(clientSocket);

			byte[] outStream = turn.ToBytes();
			uint messageLength = (uint)outStream.Length;
			byte[] messageLengthBytes = BitConverter.GetBytes(messageLength);

			serverStream.Write(messageLengthBytes, 0, messageLengthBytes.Length);
			serverStream.Write(outStream, 0, outStream.Length);
			serverStream.Flush();
		}

		private CheckersGM GetBoardState()
		{
			NetworkStream serverStream = new NetworkStream(clientSocket);
			byte[] messageLengthBytes = new byte[4];

			serverStream.Read(messageLengthBytes, 0, messageLengthBytes.Length);
			var messageLength = BitConverter.ToUInt32(messageLengthBytes, 0);

			var inStream = new byte[messageLength];
			serverStream.Read(inStream, 0, inStream.Length);

			var gmBoardState = CheckersGM.FromBytes(inStream);

			return gmBoardState;
		}

		private void InitHook()
		{
			try
			{
				this._from = Point.Empty;
				this._highlighted = new List<Point>();

				Assembly exAss = Assembly.GetExecutingAssembly();

				Stream bgImg = exAss.GetManifestResourceStream("CheckersCS451.Resources.board_v2.png");

				Stream pcRed = exAss.GetManifestResourceStream("CheckersCS451.Resources.piece_red.png");
				Stream pcBlk = exAss.GetManifestResourceStream("CheckersCS451.Resources.piece_black2.png");

				Stream kgRed = exAss.GetManifestResourceStream("CheckersCS451.Resources.king_red.png");
				Stream kgBlk = exAss.GetManifestResourceStream("CheckersCS451.Resources.king_black.png");

				this.board.BackgroundImage = new Bitmap(bgImg);

				this.image_pieceRed = new Bitmap(pcRed);
				this.image_pieceBlack = new Bitmap(pcBlk);

				this.image_kingRed = new Bitmap(kgRed);
				this.image_kingBlack = new Bitmap(kgBlk);
			}
			catch (Exception e)
			{
				Debug.WriteLine("Exception: " + e.Message);

				/* TODO: Handle issues with loading board or piece images? */
			}
		}

		private void FinalizeForm(object sender, EventArgs e)
		{
			// Remove finalizeLoadEvent to prevent it running more than once
			this.Shown -= this._finalizeLoadEvent;

			this.board.SuspendLayout();

			for (int row = 0; row < this.board.RowCount; row++)
			{
				for (int col = 0; col < this.board.ColumnCount; col++)
				{
					this.board.Controls.Add(new Label
					{
						Name = String.Format("label_r{0}c{1}", row, col),
						Text = "",
						Anchor = AnchorStyles.None,
						TextAlign = ContentAlignment.MiddleCenter,
						BackColor = Color.Transparent,
						Dock = DockStyle.Fill,
						BackgroundImageLayout = ImageLayout.Center,
						BackgroundImage = null
					}, col, row);

					this.board.GetControlFromPosition(col, row).MouseClick += ProcessClick;
				}
			}

			this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
			this.board.ResumeLayout();
		}

		private void SetupPieces()
		{
			for (int row = 0; row < this.board.RowCount; row++)
			{
				for (int col = 0; col < this.board.ColumnCount; col++)
				{
					Control cell = this.board.GetControlFromPosition(col, row);

					if (cell == null)
					{
						continue;
					}

					cell.BackColor = Color.Transparent;
					cell.Dock = DockStyle.Fill;

					Image img = null;

					if ((row % 2 == 0) ^ (col % 2 == 0))
					{
						if (row < 3)
						{
							if (_flip)
								img = this.image_pieceBlack;
							else
								img = this.image_pieceRed;
						}
						else if (row > 4)
						{
							if (_flip)
								img = this.image_pieceRed;
							else
								img = this.image_pieceBlack;
						}
						else
						{
							img = null;
						}
					}

					cell.BackgroundImage = img;
					cell.BackgroundImageLayout = ImageLayout.Center;

					cell.MouseClick += (s, e) => this.ProcessClick(s, e);
				}
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			var f = box_serverPT.Text.Split(',');
			int x1 = int.Parse(f[0]) - 1;
			int y1 = int.Parse(f[1]) - 1;
			int x2 = int.Parse(f[2]) - 1;
			int y2 = int.Parse(f[3]) - 1;
			Turn test = new Turn(new Point(x1, y1), new Point(x2, y2));
			SendTurn(test);
		}

		private void ProcessClick(object sender, MouseEventArgs e)
		{
			if (this._game == null || (this._game.Turn % 2 == 0) != this._flip)
			{
				return;
			}

			int row = 0, col = 0;
			String id = (sender as Control).Name;

			row = Int32.Parse(id.Substring(7, id.LastIndexOf('c') - 7));
			col = Int32.Parse(id.Substring(id.LastIndexOf('c') + 1));

			if (row < 0 || row > 7 || col < 0 || col > 7)
			{
				Console.WriteLine("Malformed cell ID\n");
				return;
			}

			Point click = new Point(col, row);

			// Flip black to top

			if (this._highlighted.Count != 0)
			{

				if (!this._highlighted.Contains(click))
				{
					this.RemoveHighlights();
					ProcessClick(sender, e);
					return;
				}

				this.RemoveHighlights();

				if (_flip)
				{
					click = new Point(7 - click.X, 7 - click.Y);
				}

				if (_jumping)
				{
					if (_flip)
					{
						click.Offset(_from.X < click.X ? -1 : 1, _from.Y < click.Y ? -1 : 1);
					}
					else
					{
						click.Offset(_from.X < click.X ? -1 : 1, _from.Y < click.Y ? -1 : 1);
					}
				}

				SendTurn(new Turn(this._game.ToSparseXY(_from), this._game.ToSparseXY(click)));
				this._from = Point.Empty;
				return;
			}
			else
			{
				if (_flip)
				{
					click = new Point(7 - click.X, 7 - click.Y);
				}

				Point sparseClick = _game.ToSparseXY(click);
				State stateClick = _game.SparseArray[sparseClick.X, sparseClick.Y].GetState();

				if (stateClick == State.EMPTY)
				{
					return;
				}

				if (this._myColor == CheckersGM.Player.PLAYER_RED)
				{
					if (stateClick == State.BLACK || stateClick == State.KING_BLACK)
					{
						return;
					}
				}
				else if (this._myColor == CheckersGM.Player.PLAYER_BLACK)
				{
					if (stateClick == State.RED || stateClick == State.KING_RED)
					{
						return;
					}
				}

				List<Node> moves = _game.GetAvailableEmptyPositionsFor(sparseClick);
				List<Node> jumps = _game.GetAvailableJumps(sparseClick);

				if (moves.Count <= 0 && jumps.Count <= 0)
				{
					return;
				}

				this._from = click;
				if (jumps.Count == 0)
				{
					_jumping = false;
					foreach (Node opt in moves)
					{
						Point loc = opt.GetNormalPosition();
						if (_flip)
						{
							loc = new Point(7 - loc.X, 7 - loc.Y);
						}

						Control cell = this.board.GetControlFromPosition(loc.X, loc.Y);

						cell.BackColor = Color.Cyan;
						this._highlighted.Add(loc);
					}
				}
				else
				{
					foreach (Node opt in jumps)
					{
						_jumping = true;
						Point loc = opt.GetNormalPosition();
						if (_flip)
						{
							loc = new Point(7 - loc.X, 7 - loc.Y);
						}

						if (_flip)
						{
							click = new Point(7 - click.X, 7 - click.Y);
							loc.Offset(loc.X < click.X ? -1 : 1, loc.Y < click.Y ? -1 : 1);
							click = new Point(7 - click.X, 7 - click.Y);
						}
						else
						{
							loc.Offset(loc.X < click.X ? -1 : 1, loc.Y < click.Y ? -1 : 1);
						}

						Control cell = this.board.GetControlFromPosition(loc.X, loc.Y);

						cell.BackColor = Color.Cyan;
						this._highlighted.Add(loc);
					}
				}

			}
		}

		private void RemoveHighlights()
		{
			foreach (Point loc in this._highlighted)
			{
				this.board.GetControlFromPosition(loc.X, loc.Y).BackColor = Color.Transparent;
			}

			this._highlighted.Clear();
		}

		public void UpdateText()
		{
			string player = _game.GetCurrentPlayer().ToString();
			string color = _myColor.ToString();
			string gameState = _game.GetCurrentGameState().ToString();
			string turnNumber = _game.Turn.ToString();

			this.Invoke((MethodInvoker)(() => { Text = "Playing as: " + color + " | " + "Waiting on: " + player + " | " + "Turn #: " + turnNumber + " | " + "Game state: " + gameState; }));

			// this.Text = "Playing as: " + color + " | " + "Waiting on: " + player + " | " + "Turn #: " + turnNumber + " | " + "Game state: " + gameState;
		}

		public void Update(CheckersGM newState)
		{
			this._game = newState;
			UpdateText();
			Node[,] sparseArray = this._game.SparseArray;

			if (sparseArray == null)
			{

				// TODO: Handle this in a better manner

				String _err = "Malformed sparse array sent to Form1.update";
				MessageBox.Show(String.Format("Error: {0}", _err));
				// throw new ArgumentException(_err);
			}

			if (_flip)
			{
				//Debug.WriteLine(DebugPrintSparseArrayLOCAL(sparseArray));

				int rs = sparseArray.GetLength(0) - 1, cs = sparseArray.GetLength(1) - 1;
				Node[,] temp = new Node[rs + 1, cs + 1];

				for (Int32 row = 0; row <= rs; row++)
				{
					for (Int32 col = 0; col <= cs; col++)
					{
						temp[row, col] = sparseArray[rs - row, cs - col];
					}
				}

				sparseArray = temp;

				//Debug.Write("\n--------------------------\n\n");
				//Debug.WriteLine(DebugPrintSparseArrayLOCAL(sparseArray));
			}

			for (int row = 0; row < this.board.RowCount; row++)
			{

				int sparseCol = 0;
				for (int col = 0; col < this.board.ColumnCount; col++)
				{
					if ((col - (row % 2 == 0 ? 1 : 0)) % 2 != 0)
					{
						continue;
					}

					Control cell = this.board.GetControlFromPosition(col, row);

					if (cell == null)
					{
						continue;
						// Wut?
					}

					State cellState = (sparseArray[sparseCol, row]).GetState();

					Image background = null;

					switch (cellState)
					{
						case State.RED:
							background = image_pieceRed;
							break;
						case State.KING_RED:
							background = image_kingRed;
							break;
						case State.BLACK:
							background = image_pieceBlack;
							break;
						case State.KING_BLACK:
							background = image_kingBlack;
							break;
						default: // Implied: State.EMPTY
							background = null;
							break;
					}

					cell.BackgroundImage = background;
					//cell.Invoke((MethodInvoker)(() => { BackgroundImage = background; }));

					sparseCol++;
				}
			}
		}



		public string DebugPrintSparseArrayLOCAL(Node[,] sparseArray)
		{
			var display = "";
			var space = "   ";
			var connectedSpaceR = " \\ ";
			var connectedSpaceL = " / ";
			var connectedSpaceX = " X ";
			var linkedSpace = "---";
			for (var y = -1; y < 8; y++)
			{
				var connectionMapVertical = " " + space;
				for (var x = -1; x < 4; x++)
				{
					var connectionMapHorizontal = space;
					if (x != -1 && y != -1)
					{
						var s = " ";
						s = sparseArray[x, y].GetStateString();
						if (x != 3)
						{
							var linked = sparseArray[x, y].IsLinkedTo(sparseArray[x + 1, y]);
							if (linked)
							{
								connectionMapHorizontal = linkedSpace;
							}
						}
						var RightLink = false;
						var LeftLink = false;
						var DownLink = false;
						if (y != 7 && x != 3)
						{
							RightLink = sparseArray[x, y].IsLinkedTo(sparseArray[x + 1, y + 1]);
						}
						if (y != 7)
						{
							DownLink = sparseArray[x, y].IsLinkedTo(sparseArray[x, y + 1]);
						}
						if (y != 7 && x != 0)
						{
							LeftLink = sparseArray[x, y].IsLinkedTo(sparseArray[x - 1, y + 1]);
						}
						if (LeftLink)
						{
							var previous = connectionMapVertical[connectionMapVertical.Length - 2];
							connectionMapVertical = connectionMapVertical.Substring(0, connectionMapVertical.Length - space.Length);
							if (previous == '\\')
							{
								connectionMapVertical += connectedSpaceX;
							}
							else
							{
								connectionMapVertical += connectedSpaceL;
							}
						}
						if (DownLink)
						{
							connectionMapVertical += "|";
						}
						else
						{
							connectionMapVertical += " ";
						}
						if (RightLink)
						{
							connectionMapVertical += connectedSpaceR;
						}
						else
						{
							connectionMapVertical += space;
						}
						display += s + connectionMapHorizontal;
					}
					else if (y != -1)
					{
						display += y + space;
					}
					else if (x == -1)
					{
						display += " " + space;
					}
					else if (x != -1)
					{
						display += x + space;
					}
				}
				if (y >= 0 && y < 7)
				{
					display += "\n" + connectionMapVertical + "\n";
					//display += "\n" + space + " | \\" + " | \\" + " | \\" + " |" + "\n";
				}
				else
				{
					display += "\n\n";
				}
			}
			return display;
		}
	}
}

using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Data;

namespace CheckersCS451
{

    public partial class Form1 : Form
    {

		private Bitmap image_pieceRed, image_pieceBlack;
		private Bitmap image_kingRed , image_kingBlack;

        private EventHandler _finalizeLoadEvent;

        private CheckersGM _game;
        private Boolean _first;

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

            //this.text_gameInfo.Text = (
                //String.Concat((this.Width + " x " + this.Height),
                              //"  |  ",
                              //(this.board.Width + " x " + this.board.Height),
                              //"  |  ",
                              //("CELL SZ: (" +
                               // (this.board.Width / this.board.ColumnCount) +
                               // " x " +
                               // (this.board.Height / this.board.RowCount) +
                               //")")));

            this.connectToolStripMenuItem.Click += (s, e) => Connect(s, e);
            #endregion

            this.ResumeLayout();

            this._finalizeLoadEvent = new EventHandler(this.FinalizeForm);
            this.Shown += this._finalizeLoadEvent;
        }

        private void Connect(object s, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private void InitHook()
        {
            try
            {
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

            for (int row = 0; row < this.board.RowCount; row++) {
                for (int col = 0; col < this.board.ColumnCount; col++)
                {
                    this.board.Controls.Add(new Label 
                    { 
                        Name = String.Format("label_r{0}c{1}", row, col),
                        Text = "", 
                        Anchor = AnchorStyles.None, 
                        TextAlign = ContentAlignment.MiddleCenter
                    }, col, row);
                }
            }

            this.board.ResumeLayout();

            this.SetupPieces();
        }

        private void SetupPieces()
        {
            for (int row = 0; row < this.board.RowCount; row++) {
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
                    Boolean flip = _game != null && (_game.Turn % 2 == 0) != this._first;

					if ((row % 2 == 0) ^ (col % 2 == 0))
					{
                        if (row < 3) {
                            if (!flip)
                                img = this.image_pieceBlack;
                            else
                                img = this.image_pieceRed;
						}
						else if (row > 4)
						{
							if (!flip)
							    img = this.image_pieceRed;
							else
								img = this.image_pieceBlack;
                        } else {
                            img = null;
                        }
					}

                    cell.BackgroundImage = img;
                    cell.BackgroundImageLayout = ImageLayout.Center;

                    cell.MouseClick += (s, e) => this.ProcessClick(s, e);
				}
            }
        }

        private void ProcessClick(object sender, MouseEventArgs e)
        {
            if (this._game == null)
            {
                return;
            }

            // If turn % 2 == 0, we're playing as the first player.
            // If we're not the first player, we don't play.
            // I.e. turn % 2 == this._first ^ 1
			if ((this._game.Turn % 2 == 0) != this._first)
            {
                return;
            }

            Point click = e.Location;
            click.Offset(new Point(-this.board.Location.X, -this.board.Location.Y));

            // this.board.GetControlFromPosition((click.X - click.X % this.board.Width / this.board.ColumnCount) / (this.board.Width * this.board.ColumnCount));
			// Control cell = this.board.GetControlFromPosition(col, row);
        }

        public void Update(CheckersGM newState)
        {
            this._game = newState;
            Node[,] sparseArray = this._game.SparseArray;

            if (sparseArray == null || sparseArray.Length != 4)
            {

                // TODO: Handle this in a better manner

                String _err = "Malformed sparse array sent to Form1.update";
                Debug.WriteLine("Error: " + _err);
                // throw new ArgumentException(_err);
            }

            for (int row = 0; row < this.board.RowCount; row++) {
                
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

                    switch (cellState)
					{
						case State.RED:
                            cell.BackgroundImage = image_pieceRed;
							break;
						case State.KING_RED:
							cell.BackgroundImage = image_kingRed;
							break;
						case State.BLACK:
							cell.BackgroundImage = image_pieceBlack;
							break;
						case State.KING_BLACK:
							cell.BackgroundImage = image_kingBlack;
							break;
                        default: // Implied: State.EMPTY
                            cell.BackgroundImage = null;
                            break;
                    }

                    sparseCol++;
                }
            }
        }
    }
}

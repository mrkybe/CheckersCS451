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
        // private 

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

            this.text_gameInfo.Text = (
                String.Concat((this.Width + " x " + this.Height),
                              "  |  ",
                              (this.board.Width + " x " + this.board.Height),
                              "  |  ",
                              ("CELL SZ: (" +
                                (this.board.Width / this.board.ColumnCount) +
                                " x " +
                                (this.board.Height / this.board.RowCount) +
                               ")")));

            this.newGameToolStripMenuItem.Click += (s, e) => DispDebugCellInfo();
            #endregion

            this.ResumeLayout();

            this._finalizeLoadEvent = new EventHandler(this.FinalizeForm);
            this.Shown += this._finalizeLoadEvent;
        }

        private void InitHook()
        {
            try
            {
                Assembly exAss = Assembly.GetExecutingAssembly();

                Stream bgImg = exAss.GetManifestResourceStream("CheckersCS451.Resources.board_v2.png");

                Stream pcRed = exAss.GetManifestResourceStream("CheckersCS451.Resources.piece_red.png");
                Stream pcBlk = exAss.GetManifestResourceStream("CheckersCS451.Resources.piece_black.png");

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


    //Me.TableLayoutPanel1.SuspendLayout()
    //For column = 0 To Me.TableLayoutPanel1.RowCount - 1
    //  Me.TableLayoutPanel1.ColumnStyles.Add(New RowStyle(SizeType.AutoSize, 100))
    //  For row = 0 To Me.TableLayoutPanel1.RowCount - 1
    //    Me.TableLayoutPanel1.RowStyles.Add(New RowStyle(SizeType.AutoSize, 100))
    //    Dim lblLabel As New Label()
    //    lblLabel.Text = "Label: " + row.ToString() + column.ToString
    //    Me.TableLayoutPanel1.Controls.Add(lblLabel, column, row)
    //  Next row
    //Next column
    //Me.TableLayoutPanel1.ResumeLayout()

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


            this.DispDebugCellInfo();
        }

        private void DispDebugCellInfo()
        {
            //int cellSzX = this.board.Width / this.board.ColumnCount;
            //int cellSzY = this.board.Height / this.board.RowCount;

            //int baseX = this.board.Location.X;
            //int baseY = this.board.Location.Y;

            //for (int row = (cellSzY / 2); row < this.board.Height; row += cellSzY)
            //{
            //    for (int col = (cellSzX / 2); col < this.board.Height; col += cellSzX)
            //    {
            //        Control cell = this.board.GetControlFromPosition(col + baseX, row + baseY);

            //        int DEBUG_ROW = (row - (cellSzY / 2)) / cellSzY;
            //        int DEBUG_COL = (col - (cellSzX / 2)) / cellSzX;

            //        //	cell.Text = String.Format("r{0}c{1}", DEBUG_ROW, DEBUG_COL);
            //    }
            //}

            for (int row = 0; row < this.board.RowCount; row++) {
                for (int col = 0; col < this.board.ColumnCount; col++)
                {
                    Control cell = this.board.GetControlFromPosition(col, row);

                    Debug.WriteLineIf(cell == null, String.Format("No cell at r{0}c{1}", row, col) as object);
					cell.Text = String.Format("r{0}c{1}", row, col);
				}
            }
        }

        public void Update(Node[,] sparseArray)
        {
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

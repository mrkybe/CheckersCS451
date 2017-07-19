using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CheckersCS451
{

	public partial class Form1 : Form
	{

		private Bitmap image_pieceRed, image_pieceBlack;

		public Form1()
		{
			InitializeComponent();
			initHook();
		}

		private void initHook()
		{
			try
			{
				Assembly exAss = Assembly.GetExecutingAssembly();

				Stream bgImg = exAss.GetManifestResourceStream("CheckersCS451.Resources.board.png");
				Stream pcRed = exAss.GetManifestResourceStream("CheckersCS451.Resources.piece_red.png");
				Stream pcBlk = exAss.GetManifestResourceStream("CheckersCS451.Resources.piece_black.png");


				// TODO: Fix background image (8x8 does not stretch as expected)
				this.board.BackgroundImage = new Bitmap(bgImg);
				this.image_pieceRed = new Bitmap(pcRed);
				this.image_pieceBlack = new Bitmap(pcBlk);
			}
			catch
			{
				/* TODO: Handle issues with loading board or piece images? */
			}
		}
	}
}

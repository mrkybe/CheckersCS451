using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace CheckersCS451
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.menuStrip1 = new MenuStrip();
			this.newGameToolStripMenuItem = new ToolStripMenuItem();
			this.text_gameInfo = new ToolStripTextBox();
			this.panel_game = new Panel();
			this.table_rhs_split = new TableLayoutPanel();
			this.board = new TableLayoutPanel();
			this.table_top = new TableLayoutPanel();
			this.table_letters = new TableLayoutPanel();
			this.label_colH = new Label();
			this.label_colG = new Label();
			this.label_colF = new Label();
			this.label_colE = new Label();
			this.label_colD = new Label();
			this.label_colC = new Label();
			this.label_colA = new Label();
			this.label_colB = new Label();
			this.table_numbers = new TableLayoutPanel();
			this.label_row1 = new Label();
			this.label_row2 = new Label();
			this.label_row3 = new Label();
			this.label_row4 = new Label();
			this.label_row5 = new Label();
			this.label_row6 = new Label();
			this.label_row7 = new Label();
			this.label_row8 = new Label();
			this.menuStrip1.SuspendLayout();
			this.panel_game.SuspendLayout();
			this.table_rhs_split.SuspendLayout();
			this.board.SuspendLayout();
			this.table_top.SuspendLayout();
			this.table_letters.SuspendLayout();
			this.table_numbers.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new ToolStripItem[] {
			this.newGameToolStripMenuItem,
			this.text_gameInfo});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(904, 43);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// newGameToolStripMenuItem
			// 
			this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
			this.newGameToolStripMenuItem.Size = new System.Drawing.Size(149, 39);
			this.newGameToolStripMenuItem.Text = "New Game";
			// 
			// text_gameInfo
			// 
			this.text_gameInfo.Alignment = ToolStripItemAlignment.Right;
			this.text_gameInfo.BorderStyle = BorderStyle.None;
			this.text_gameInfo.Name = "text_gameInfo";
			this.text_gameInfo.Overflow = ToolStripItemOverflow.Never;
			this.text_gameInfo.ReadOnly = true;
			this.text_gameInfo.RightToLeft = RightToLeft.Yes;
			this.text_gameInfo.ShortcutsEnabled = false;
			this.text_gameInfo.Size = new System.Drawing.Size(633, 39);
			this.text_gameInfo.Text = "{{Game.Information}}";
			// 
			// panel_game
			// 
			this.panel_game.BorderStyle = BorderStyle.Fixed3D;
			this.panel_game.Controls.Add(this.table_rhs_split);
			this.panel_game.Dock = DockStyle.Fill;
			this.panel_game.Location = new System.Drawing.Point(0, 43);
			this.panel_game.Margin = new Padding(4);
			this.panel_game.Name = "panel_game";
			this.panel_game.Size = new System.Drawing.Size(904, 762);
			this.panel_game.TabIndex = 2;
			// 
			// table_rhs_split
			// 
			this.table_rhs_split.ColumnCount = 2;
			this.table_rhs_split.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 36F));
			this.table_rhs_split.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 520F));
			this.table_rhs_split.Controls.Add(this.board, 1, 1);
			this.table_rhs_split.Controls.Add(this.table_top, 1, 0);
			this.table_rhs_split.Controls.Add(this.table_numbers, 0, 1);
			this.table_rhs_split.Dock = DockStyle.Fill;
			this.table_rhs_split.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
			this.table_rhs_split.Location = new System.Drawing.Point(0, 0);
			this.table_rhs_split.Margin = new Padding(0);
			this.table_rhs_split.Name = "table_rhs_split";
			this.table_rhs_split.RowCount = 2;
			this.table_rhs_split.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
			this.table_rhs_split.RowStyles.Add(new RowStyle(SizeType.Absolute, 488F));
			this.table_rhs_split.Size = new System.Drawing.Size(900, 758);
			this.table_rhs_split.TabIndex = 1;
			// 
			// board
			// 
			this.board.AllowDrop = true;
			this.board.BackgroundImageLayout = ImageLayout.Stretch;
			this.board.ColumnCount = 8;
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.board.Dock = DockStyle.Fill;
			this.board.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;
			this.board.Location = new System.Drawing.Point(36, 36);
			this.board.Margin = new Padding(0);
			this.board.Name = "board";
			this.board.RowCount = 8;
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.board.Size = new System.Drawing.Size(864, 722);
			this.board.TabIndex = 0;
			// 
			// table_top
			// 
			this.table_top.ColumnCount = 1;
			this.table_top.ColumnStyles.Add(new ColumnStyle());
			this.table_top.Controls.Add(this.table_letters, 0, 0);
			this.table_top.Dock = DockStyle.Fill;
			this.table_top.Location = new System.Drawing.Point(36, 0);
			this.table_top.Margin = new Padding(0);
			this.table_top.Name = "table_top";
			this.table_top.RowCount = 1;
			this.table_top.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
			this.table_top.RowStyles.Add(new RowStyle(SizeType.Absolute, 36F));
			this.table_top.Size = new System.Drawing.Size(864, 36);
			this.table_top.TabIndex = 1;
			// 
			// table_letters
			// 
			this.table_letters.ColumnCount = 8;
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 12.5F));
			this.table_letters.Controls.Add(this.label_colH, 7, 0);
			this.table_letters.Controls.Add(this.label_colG, 6, 0);
			this.table_letters.Controls.Add(this.label_colF, 5, 0);
			this.table_letters.Controls.Add(this.label_colE, 4, 0);
			this.table_letters.Controls.Add(this.label_colD, 3, 0);
			this.table_letters.Controls.Add(this.label_colC, 2, 0);
			this.table_letters.Controls.Add(this.label_colA, 0, 0);
			this.table_letters.Controls.Add(this.label_colB, 1, 0);
			this.table_letters.Dock = DockStyle.Fill;
			this.table_letters.Location = new System.Drawing.Point(0, 0);
			this.table_letters.Margin = new Padding(0);
			this.table_letters.Name = "table_letters";
			this.table_letters.RowCount = 1;
			this.table_letters.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
			this.table_letters.Size = new System.Drawing.Size(864, 36);
			this.table_letters.TabIndex = 0;
			// 
			// label_colH
			// 
			this.label_colH.Anchor = AnchorStyles.None;
			this.label_colH.AutoSize = true;
			this.label_colH.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colH.Location = new System.Drawing.Point(795, 2);
			this.label_colH.Margin = new Padding(0);
			this.label_colH.Name = "label_colH";
			this.label_colH.Size = new System.Drawing.Size(30, 32);
			this.label_colH.TabIndex = 7;
			this.label_colH.Text = "H";
			this.label_colH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colG
			// 
			this.label_colG.Anchor = AnchorStyles.None;
			this.label_colG.AutoSize = true;
			this.label_colG.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colG.Location = new System.Drawing.Point(687, 2);
			this.label_colG.Margin = new Padding(0);
			this.label_colG.Name = "label_colG";
			this.label_colG.Size = new System.Drawing.Size(30, 32);
			this.label_colG.TabIndex = 6;
			this.label_colG.Text = "G";
			this.label_colG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colF
			// 
			this.label_colF.Anchor = AnchorStyles.None;
			this.label_colF.AutoSize = true;
			this.label_colF.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colF.Location = new System.Drawing.Point(579, 2);
			this.label_colF.Margin = new Padding(0);
			this.label_colF.Name = "label_colF";
			this.label_colF.Size = new System.Drawing.Size(30, 32);
			this.label_colF.TabIndex = 5;
			this.label_colF.Text = "F";
			this.label_colF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colE
			// 
			this.label_colE.Anchor = AnchorStyles.None;
			this.label_colE.AutoSize = true;
			this.label_colE.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colE.Location = new System.Drawing.Point(471, 2);
			this.label_colE.Margin = new Padding(0);
			this.label_colE.Name = "label_colE";
			this.label_colE.Size = new System.Drawing.Size(30, 32);
			this.label_colE.TabIndex = 4;
			this.label_colE.Text = "E";
			this.label_colE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colD
			// 
			this.label_colD.Anchor = AnchorStyles.None;
			this.label_colD.AutoSize = true;
			this.label_colD.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colD.Location = new System.Drawing.Point(363, 2);
			this.label_colD.Margin = new Padding(0);
			this.label_colD.Name = "label_colD";
			this.label_colD.Size = new System.Drawing.Size(30, 32);
			this.label_colD.TabIndex = 3;
			this.label_colD.Text = "D";
			this.label_colD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colC
			// 
			this.label_colC.Anchor = AnchorStyles.None;
			this.label_colC.AutoSize = true;
			this.label_colC.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colC.Location = new System.Drawing.Point(255, 2);
			this.label_colC.Margin = new Padding(0);
			this.label_colC.Name = "label_colC";
			this.label_colC.Size = new System.Drawing.Size(30, 32);
			this.label_colC.TabIndex = 2;
			this.label_colC.Text = "C";
			this.label_colC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colA
			// 
			this.label_colA.Anchor = AnchorStyles.None;
			this.label_colA.AutoSize = true;
			this.label_colA.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colA.Location = new System.Drawing.Point(39, 2);
			this.label_colA.Margin = new Padding(0);
			this.label_colA.Name = "label_colA";
			this.label_colA.Size = new System.Drawing.Size(30, 32);
			this.label_colA.TabIndex = 0;
			this.label_colA.Text = "A";
			this.label_colA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colB
			// 
			this.label_colB.Anchor = AnchorStyles.None;
			this.label_colB.AutoSize = true;
			this.label_colB.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colB.Location = new System.Drawing.Point(147, 2);
			this.label_colB.Margin = new Padding(0);
			this.label_colB.Name = "label_colB";
			this.label_colB.Size = new System.Drawing.Size(30, 32);
			this.label_colB.TabIndex = 1;
			this.label_colB.Text = "B";
			this.label_colB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// table_numbers
			// 
			this.table_numbers.ColumnCount = 1;
			this.table_numbers.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
			this.table_numbers.Controls.Add(this.label_row1, 0, 7);
			this.table_numbers.Controls.Add(this.label_row2, 0, 6);
			this.table_numbers.Controls.Add(this.label_row3, 0, 5);
			this.table_numbers.Controls.Add(this.label_row4, 0, 4);
			this.table_numbers.Controls.Add(this.label_row5, 0, 3);
			this.table_numbers.Controls.Add(this.label_row6, 0, 2);
			this.table_numbers.Controls.Add(this.label_row7, 0, 1);
			this.table_numbers.Controls.Add(this.label_row8, 0, 0);
			this.table_numbers.Dock = DockStyle.Fill;
			this.table_numbers.Location = new System.Drawing.Point(0, 36);
			this.table_numbers.Margin = new Padding(0);
			this.table_numbers.Name = "table_numbers";
			this.table_numbers.RowCount = 8;
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
			this.table_numbers.Size = new System.Drawing.Size(36, 722);
			this.table_numbers.TabIndex = 2;
			// 
			// label_row1
			// 
			this.label_row1.Anchor = AnchorStyles.None;
			this.label_row1.AutoSize = true;
			this.label_row1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row1.Location = new System.Drawing.Point(3, 660);
			this.label_row1.Margin = new Padding(0);
			this.label_row1.Name = "label_row1";
			this.label_row1.Size = new System.Drawing.Size(30, 32);
			this.label_row1.TabIndex = 7;
			this.label_row1.Text = "1";
			this.label_row1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row2
			// 
			this.label_row2.Anchor = AnchorStyles.None;
			this.label_row2.AutoSize = true;
			this.label_row2.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row2.Location = new System.Drawing.Point(3, 569);
			this.label_row2.Margin = new Padding(0);
			this.label_row2.Name = "label_row2";
			this.label_row2.Size = new System.Drawing.Size(30, 32);
			this.label_row2.TabIndex = 6;
			this.label_row2.Text = "2";
			this.label_row2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row3
			// 
			this.label_row3.Anchor = AnchorStyles.None;
			this.label_row3.AutoSize = true;
			this.label_row3.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row3.Location = new System.Drawing.Point(3, 479);
			this.label_row3.Margin = new Padding(0);
			this.label_row3.Name = "label_row3";
			this.label_row3.Size = new System.Drawing.Size(30, 32);
			this.label_row3.TabIndex = 5;
			this.label_row3.Text = "3";
			this.label_row3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row4
			// 
			this.label_row4.Anchor = AnchorStyles.None;
			this.label_row4.AutoSize = true;
			this.label_row4.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row4.Location = new System.Drawing.Point(3, 389);
			this.label_row4.Margin = new Padding(0);
			this.label_row4.Name = "label_row4";
			this.label_row4.Size = new System.Drawing.Size(30, 32);
			this.label_row4.TabIndex = 4;
			this.label_row4.Text = "4";
			this.label_row4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row5
			// 
			this.label_row5.Anchor = AnchorStyles.None;
			this.label_row5.AutoSize = true;
			this.label_row5.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row5.Location = new System.Drawing.Point(3, 299);
			this.label_row5.Margin = new Padding(0);
			this.label_row5.Name = "label_row5";
			this.label_row5.Size = new System.Drawing.Size(30, 32);
			this.label_row5.TabIndex = 3;
			this.label_row5.Text = "5";
			this.label_row5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row6
			// 
			this.label_row6.Anchor = AnchorStyles.None;
			this.label_row6.AutoSize = true;
			this.label_row6.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row6.Location = new System.Drawing.Point(3, 209);
			this.label_row6.Margin = new Padding(0);
			this.label_row6.Name = "label_row6";
			this.label_row6.Size = new System.Drawing.Size(30, 32);
			this.label_row6.TabIndex = 2;
			this.label_row6.Text = "6";
			this.label_row6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row7
			// 
			this.label_row7.Anchor = AnchorStyles.None;
			this.label_row7.AutoSize = true;
			this.label_row7.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row7.Location = new System.Drawing.Point(3, 119);
			this.label_row7.Margin = new Padding(0);
			this.label_row7.Name = "label_row7";
			this.label_row7.Size = new System.Drawing.Size(30, 32);
			this.label_row7.TabIndex = 1;
			this.label_row7.Text = "7";
			this.label_row7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row8
			// 
			this.label_row8.Anchor = AnchorStyles.None;
			this.label_row8.AutoSize = true;
			this.label_row8.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_row8.Location = new System.Drawing.Point(3, 29);
			this.label_row8.Margin = new Padding(0);
			this.label_row8.Name = "label_row8";
			this.label_row8.Size = new System.Drawing.Size(30, 32);
			this.label_row8.TabIndex = 0;
			this.label_row8.Text = "8";
			this.label_row8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(904, 805);
			this.Controls.Add(this.panel_game);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new Padding(4);
			this.Name = "Main";
			this.Text = "Main";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panel_game.ResumeLayout(false);
			this.table_rhs_split.ResumeLayout(false);
			this.board.ResumeLayout(false);
			this.table_top.ResumeLayout(false);
			this.table_letters.ResumeLayout(false);
			this.table_letters.PerformLayout();
			this.table_numbers.ResumeLayout(false);
			this.table_numbers.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private TableLayoutPanel board;

		private MenuStrip menuStrip1;
		private ToolStripMenuItem newGameToolStripMenuItem;
		private Panel panel_game;
		private ToolStripTextBox text_gameInfo;

		private TableLayoutPanel table_rhs_split;
		private TableLayoutPanel table_top;
		private TableLayoutPanel table_letters;
		private TableLayoutPanel table_numbers;

		private Label label_colA, label_colB, label_colC, label_colD
		, label_colE, label_colF, label_colG, label_colH;

		private Label label_row1, label_row2, label_row3, label_row4
		, label_row5, label_row6, label_row7, label_row8;
	}
}


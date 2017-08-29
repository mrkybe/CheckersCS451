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
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.box_serverIP = new System.Windows.Forms.ToolStripTextBox();
			this.box_serverPT = new System.Windows.Forms.ToolStripTextBox();
			this.panel_game = new System.Windows.Forms.Panel();
			this.table_rhs_split = new System.Windows.Forms.TableLayoutPanel();
			this.board = new System.Windows.Forms.TableLayoutPanel();
			this.table_top = new System.Windows.Forms.TableLayoutPanel();
			this.table_letters = new System.Windows.Forms.TableLayoutPanel();
			this.label_colH = new System.Windows.Forms.Label();
			this.label_colG = new System.Windows.Forms.Label();
			this.label_colF = new System.Windows.Forms.Label();
			this.label_colE = new System.Windows.Forms.Label();
			this.label_colD = new System.Windows.Forms.Label();
			this.label_colC = new System.Windows.Forms.Label();
			this.label_colA = new System.Windows.Forms.Label();
			this.label_colB = new System.Windows.Forms.Label();
			this.table_numbers = new System.Windows.Forms.TableLayoutPanel();
			this.label_row1 = new System.Windows.Forms.Label();
			this.label_row2 = new System.Windows.Forms.Label();
			this.label_row3 = new System.Windows.Forms.Label();
			this.label_row4 = new System.Windows.Forms.Label();
			this.label_row5 = new System.Windows.Forms.Label();
			this.label_row6 = new System.Windows.Forms.Label();
			this.label_row7 = new System.Windows.Forms.Label();
			this.label_row8 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.menuStrip1.SuspendLayout();
			this.panel_game.SuspendLayout();
			this.table_rhs_split.SuspendLayout();
			this.table_top.SuspendLayout();
			this.table_letters.SuspendLayout();
			this.table_numbers.SuspendLayout();
			this.SuspendLayout();
			// 
			// menuStrip1
			// 
			this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
			this.connectToolStripMenuItem,
			this.box_serverIP,
			this.box_serverPT});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Padding = new System.Windows.Forms.Padding(8, 2, 0, 2);
			this.menuStrip1.Size = new System.Drawing.Size(904, 43);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// connectToolStripMenuItem
			// 
			this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
			this.connectToolStripMenuItem.Size = new System.Drawing.Size(118, 39);
			this.connectToolStripMenuItem.Text = "Connect";
			// 
			// box_serverIP
			// 
			this.box_serverIP.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.box_serverIP.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.box_serverIP.Name = "box_serverIP";
			this.box_serverIP.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.box_serverIP.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.box_serverIP.Size = new System.Drawing.Size(314, 39);
			this.box_serverIP.ShortcutsEnabled = true;
			this.box_serverIP.Text = "Server IP";
			// 
			// box_serverPT
			// 
			this.box_serverPT.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
			this.box_serverPT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.box_serverPT.Name = "box_serverPT";
			this.box_serverPT.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
			this.box_serverPT.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.box_serverPT.ShortcutsEnabled = false;
			this.box_serverPT.Size = new System.Drawing.Size(314, 39);
			this.box_serverPT.Text = "Server Port";
			this.box_serverPT.Visible = false;
			// 
			// panel_game
			// 
			this.panel_game.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.panel_game.Controls.Add(this.table_rhs_split);
			this.panel_game.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel_game.Location = new System.Drawing.Point(0, 43);
			this.panel_game.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.panel_game.Name = "panel_game";
			this.panel_game.Size = new System.Drawing.Size(904, 861);
			this.panel_game.TabIndex = 2;
			// 
			// table_rhs_split
			// 
			this.table_rhs_split.ColumnCount = 2;
			this.table_rhs_split.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.table_rhs_split.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 860F));
			this.table_rhs_split.Controls.Add(this.board, 1, 1);
			this.table_rhs_split.Controls.Add(this.table_top, 1, 0);
			this.table_rhs_split.Controls.Add(this.table_numbers, 0, 1);
			this.table_rhs_split.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table_rhs_split.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.table_rhs_split.Location = new System.Drawing.Point(0, 0);
			this.table_rhs_split.Margin = new System.Windows.Forms.Padding(0);
			this.table_rhs_split.Name = "table_rhs_split";
			this.table_rhs_split.RowCount = 2;
			this.table_rhs_split.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.table_rhs_split.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 488F));
			this.table_rhs_split.Size = new System.Drawing.Size(900, 857);
			this.table_rhs_split.TabIndex = 1;
			// 
			// board
			// 
			this.board.AllowDrop = true;
			this.board.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
			this.board.ColumnCount = 8;
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.Dock = System.Windows.Forms.DockStyle.Fill;
			this.board.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
			this.board.Location = new System.Drawing.Point(36, 37);
			this.board.Margin = new System.Windows.Forms.Padding(0);
			this.board.Name = "board";
			this.board.RowCount = 8;
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.board.Size = new System.Drawing.Size(864, 820);
			this.board.TabIndex = 0;
			// 
			// table_top
			// 
			this.table_top.ColumnCount = 1;
			this.table_top.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.table_top.Controls.Add(this.table_letters, 0, 0);
			this.table_top.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table_top.Location = new System.Drawing.Point(36, 0);
			this.table_top.Margin = new System.Windows.Forms.Padding(0);
			this.table_top.Name = "table_top";
			this.table_top.RowCount = 1;
			this.table_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.table_top.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 37F));
			this.table_top.Size = new System.Drawing.Size(864, 37);
			this.table_top.TabIndex = 1;
			// 
			// table_letters
			// 
			this.table_letters.ColumnCount = 8;
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_letters.Controls.Add(this.label_colH, 7, 0);
			this.table_letters.Controls.Add(this.label_colG, 6, 0);
			this.table_letters.Controls.Add(this.label_colF, 5, 0);
			this.table_letters.Controls.Add(this.label_colE, 4, 0);
			this.table_letters.Controls.Add(this.label_colD, 3, 0);
			this.table_letters.Controls.Add(this.label_colC, 2, 0);
			this.table_letters.Controls.Add(this.label_colA, 0, 0);
			this.table_letters.Controls.Add(this.label_colB, 1, 0);
			this.table_letters.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table_letters.Location = new System.Drawing.Point(0, 0);
			this.table_letters.Margin = new System.Windows.Forms.Padding(0);
			this.table_letters.Name = "table_letters";
			this.table_letters.RowCount = 1;
			this.table_letters.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table_letters.Size = new System.Drawing.Size(864, 37);
			this.table_letters.TabIndex = 0;
			// 
			// label_colH
			// 
			this.label_colH.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colH.AutoSize = true;
			this.label_colH.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colH.Location = new System.Drawing.Point(795, 2);
			this.label_colH.Margin = new System.Windows.Forms.Padding(0);
			this.label_colH.Name = "label_colH";
			this.label_colH.Size = new System.Drawing.Size(30, 32);
			this.label_colH.TabIndex = 7;
			this.label_colH.Text = "H";
			this.label_colH.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colG
			// 
			this.label_colG.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colG.AutoSize = true;
			this.label_colG.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colG.Location = new System.Drawing.Point(687, 2);
			this.label_colG.Margin = new System.Windows.Forms.Padding(0);
			this.label_colG.Name = "label_colG";
			this.label_colG.Size = new System.Drawing.Size(30, 32);
			this.label_colG.TabIndex = 6;
			this.label_colG.Text = "G";
			this.label_colG.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colF
			// 
			this.label_colF.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colF.AutoSize = true;
			this.label_colF.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colF.Location = new System.Drawing.Point(579, 2);
			this.label_colF.Margin = new System.Windows.Forms.Padding(0);
			this.label_colF.Name = "label_colF";
			this.label_colF.Size = new System.Drawing.Size(30, 32);
			this.label_colF.TabIndex = 5;
			this.label_colF.Text = "F";
			this.label_colF.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colE
			// 
			this.label_colE.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colE.AutoSize = true;
			this.label_colE.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colE.Location = new System.Drawing.Point(471, 2);
			this.label_colE.Margin = new System.Windows.Forms.Padding(0);
			this.label_colE.Name = "label_colE";
			this.label_colE.Size = new System.Drawing.Size(30, 32);
			this.label_colE.TabIndex = 4;
			this.label_colE.Text = "E";
			this.label_colE.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colD
			// 
			this.label_colD.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colD.AutoSize = true;
			this.label_colD.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colD.Location = new System.Drawing.Point(363, 2);
			this.label_colD.Margin = new System.Windows.Forms.Padding(0);
			this.label_colD.Name = "label_colD";
			this.label_colD.Size = new System.Drawing.Size(30, 32);
			this.label_colD.TabIndex = 3;
			this.label_colD.Text = "D";
			this.label_colD.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colC
			// 
			this.label_colC.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colC.AutoSize = true;
			this.label_colC.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colC.Location = new System.Drawing.Point(255, 2);
			this.label_colC.Margin = new System.Windows.Forms.Padding(0);
			this.label_colC.Name = "label_colC";
			this.label_colC.Size = new System.Drawing.Size(30, 32);
			this.label_colC.TabIndex = 2;
			this.label_colC.Text = "C";
			this.label_colC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colA
			// 
			this.label_colA.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colA.AutoSize = true;
			this.label_colA.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colA.Location = new System.Drawing.Point(39, 2);
			this.label_colA.Margin = new System.Windows.Forms.Padding(0);
			this.label_colA.Name = "label_colA";
			this.label_colA.Size = new System.Drawing.Size(30, 32);
			this.label_colA.TabIndex = 0;
			this.label_colA.Text = "A";
			this.label_colA.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_colB
			// 
			this.label_colB.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_colB.AutoSize = true;
			this.label_colB.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_colB.Location = new System.Drawing.Point(147, 2);
			this.label_colB.Margin = new System.Windows.Forms.Padding(0);
			this.label_colB.Name = "label_colB";
			this.label_colB.Size = new System.Drawing.Size(30, 32);
			this.label_colB.TabIndex = 1;
			this.label_colB.Text = "B";
			this.label_colB.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// table_numbers
			// 
			this.table_numbers.ColumnCount = 1;
			this.table_numbers.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.table_numbers.Controls.Add(this.label_row1, 0, 7);
			this.table_numbers.Controls.Add(this.label_row2, 0, 6);
			this.table_numbers.Controls.Add(this.label_row3, 0, 5);
			this.table_numbers.Controls.Add(this.label_row4, 0, 4);
			this.table_numbers.Controls.Add(this.label_row5, 0, 3);
			this.table_numbers.Controls.Add(this.label_row6, 0, 2);
			this.table_numbers.Controls.Add(this.label_row7, 0, 1);
			this.table_numbers.Controls.Add(this.label_row8, 0, 0);
			this.table_numbers.Dock = System.Windows.Forms.DockStyle.Fill;
			this.table_numbers.Location = new System.Drawing.Point(0, 37);
			this.table_numbers.Margin = new System.Windows.Forms.Padding(0);
			this.table_numbers.Name = "table_numbers";
			this.table_numbers.RowCount = 8;
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 12.5F));
			this.table_numbers.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 25F));
			this.table_numbers.Size = new System.Drawing.Size(36, 820);
			this.table_numbers.TabIndex = 2;
			// 
			// label_row1
			// 
			this.label_row1.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row1.AutoSize = true;
			this.label_row1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row1.Location = new System.Drawing.Point(3, 751);
			this.label_row1.Margin = new System.Windows.Forms.Padding(0);
			this.label_row1.Name = "label_row1";
			this.label_row1.Size = new System.Drawing.Size(30, 32);
			this.label_row1.TabIndex = 7;
			this.label_row1.Text = "1";
			this.label_row1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row2
			// 
			this.label_row2.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row2.AutoSize = true;
			this.label_row2.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row2.Location = new System.Drawing.Point(3, 647);
			this.label_row2.Margin = new System.Windows.Forms.Padding(0);
			this.label_row2.Name = "label_row2";
			this.label_row2.Size = new System.Drawing.Size(30, 32);
			this.label_row2.TabIndex = 6;
			this.label_row2.Text = "2";
			this.label_row2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row3
			// 
			this.label_row3.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row3.AutoSize = true;
			this.label_row3.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row3.Location = new System.Drawing.Point(3, 545);
			this.label_row3.Margin = new System.Windows.Forms.Padding(0);
			this.label_row3.Name = "label_row3";
			this.label_row3.Size = new System.Drawing.Size(30, 32);
			this.label_row3.TabIndex = 5;
			this.label_row3.Text = "3";
			this.label_row3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row4
			// 
			this.label_row4.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row4.AutoSize = true;
			this.label_row4.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row4.Location = new System.Drawing.Point(3, 443);
			this.label_row4.Margin = new System.Windows.Forms.Padding(0);
			this.label_row4.Name = "label_row4";
			this.label_row4.Size = new System.Drawing.Size(30, 32);
			this.label_row4.TabIndex = 4;
			this.label_row4.Text = "4";
			this.label_row4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row5
			// 
			this.label_row5.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row5.AutoSize = true;
			this.label_row5.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row5.Location = new System.Drawing.Point(3, 341);
			this.label_row5.Margin = new System.Windows.Forms.Padding(0);
			this.label_row5.Name = "label_row5";
			this.label_row5.Size = new System.Drawing.Size(30, 32);
			this.label_row5.TabIndex = 3;
			this.label_row5.Text = "5";
			this.label_row5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row6
			// 
			this.label_row6.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row6.AutoSize = true;
			this.label_row6.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row6.Location = new System.Drawing.Point(3, 239);
			this.label_row6.Margin = new System.Windows.Forms.Padding(0);
			this.label_row6.Name = "label_row6";
			this.label_row6.Size = new System.Drawing.Size(30, 32);
			this.label_row6.TabIndex = 2;
			this.label_row6.Text = "6";
			this.label_row6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row7
			// 
			this.label_row7.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row7.AutoSize = true;
			this.label_row7.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
			this.label_row7.Location = new System.Drawing.Point(3, 137);
			this.label_row7.Margin = new System.Windows.Forms.Padding(0);
			this.label_row7.Name = "label_row7";
			this.label_row7.Size = new System.Drawing.Size(30, 32);
			this.label_row7.TabIndex = 1;
			this.label_row7.Text = "7";
			this.label_row7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label_row8
			// 
			this.label_row8.Anchor = System.Windows.Forms.AnchorStyles.None;
			this.label_row8.AutoSize = true;
			this.label_row8.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label_row8.Location = new System.Drawing.Point(3, 35);
			this.label_row8.Margin = new System.Windows.Forms.Padding(0);
			this.label_row8.Name = "label_row8";
			this.label_row8.Size = new System.Drawing.Size(30, 32);
			this.label_row8.TabIndex = 0;
			this.label_row8.Text = "8";
			this.label_row8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// button1
			// 
			this.button1.Enabled = false;
			this.button1.Location = new System.Drawing.Point(152, 2);
			this.button1.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(94, 44);
			this.button1.TabIndex = 3;
			this.button1.Text = "button1";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Visible = false;
			// this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(904, 904);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel_game);
			this.Controls.Add(this.menuStrip1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MainMenuStrip = this.menuStrip1;
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "Form1";
			this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
			this.Text = "Main";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.panel_game.ResumeLayout(false);
			this.table_rhs_split.ResumeLayout(false);
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
		private ToolStripMenuItem connectToolStripMenuItem;
		private Panel panel_game;
		private ToolStripTextBox box_serverIP, box_serverPT;

		private TableLayoutPanel table_rhs_split;
		private TableLayoutPanel table_top;
		private TableLayoutPanel table_letters;
		private TableLayoutPanel table_numbers;

		private Label label_colA, label_colB, label_colC, label_colD
		, label_colE, label_colF, label_colG, label_colH;

		private Label label_row1, label_row2, label_row3, label_row4
		, label_row5, label_row6, label_row7, label_row8;
		private Button button1;
	}
}


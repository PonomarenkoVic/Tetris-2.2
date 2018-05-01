namespace TetrisForm
{
    partial class TetrisForm
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
            this.GBoard = new System.Windows.Forms.PictureBox();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.StripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuStartItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuStopItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuPauseItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuExitItem = new System.Windows.Forms.ToolStripMenuItem();
            this.StripMenuInformationItem = new System.Windows.Forms.ToolStripMenuItem();
            this.NFigureBoard = new System.Windows.Forms.PictureBox();
            this.ScoreText = new System.Windows.Forms.Label();
            this.Score = new System.Windows.Forms.Label();
            this.LineText = new System.Windows.Forms.Label();
            this.BurnedLine = new System.Windows.Forms.Label();
            this.Level = new System.Windows.Forms.Label();
            this.LevelText = new System.Windows.Forms.Label();
            this.GameOverLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.GBoard)).BeginInit();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NFigureBoard)).BeginInit();
            this.SuspendLayout();
            // 
            // GBoard
            // 
            this.GBoard.AccessibleName = "PictureBox1";
            this.GBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(192)))), ((int)(((byte)(235)))));
            this.GBoard.ErrorImage = null;
            this.GBoard.InitialImage = null;
            this.GBoard.Location = new System.Drawing.Point(22, 38);
            this.GBoard.Margin = new System.Windows.Forms.Padding(2);
            this.GBoard.Name = "GBoard";
            this.GBoard.Size = new System.Drawing.Size(250, 500);
            this.GBoard.TabIndex = 0;
            this.GBoard.TabStop = false;
            // 
            // MainMenu
            // 
            this.MainMenu.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenuItem,
            this.StripMenuInformationItem});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(432, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // StripMenuItem
            // 
            this.StripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StripMenuStartItem,
            this.StripMenuStopItem,
            this.StripMenuPauseItem,
            this.StripMenuExitItem});
            this.StripMenuItem.Name = "StripMenuItem";
            this.StripMenuItem.Size = new System.Drawing.Size(46, 20);
            this.StripMenuItem.Text = "Игра";
            // 
            // StripMenuStartItem
            // 
            this.StripMenuStartItem.AccessibleName = "StartButton";
            this.StripMenuStartItem.Name = "StripMenuStartItem";
            this.StripMenuStartItem.Size = new System.Drawing.Size(108, 22);
            this.StripMenuStartItem.Text = "Старт";
            this.StripMenuStartItem.Click += new System.EventHandler(this.StripMenuStartItem_Click);
            // 
            // StripMenuStopItem
            // 
            this.StripMenuStopItem.Name = "StripMenuStopItem";
            this.StripMenuStopItem.Size = new System.Drawing.Size(108, 22);
            this.StripMenuStopItem.Text = "Стоп";
            this.StripMenuStopItem.Click += new System.EventHandler(this.StripMenuStopItem_Click);
            // 
            // StripMenuPauseItem
            // 
            this.StripMenuPauseItem.Name = "StripMenuPauseItem";
            this.StripMenuPauseItem.Size = new System.Drawing.Size(108, 22);
            this.StripMenuPauseItem.Text = "Пауза";
            this.StripMenuPauseItem.Click += new System.EventHandler(this.StripMenuPauseItem_Click);
            // 
            // StripMenuExitItem
            // 
            this.StripMenuExitItem.Name = "StripMenuExitItem";
            this.StripMenuExitItem.Size = new System.Drawing.Size(108, 22);
            this.StripMenuExitItem.Text = "Выход";
            this.StripMenuExitItem.Click += new System.EventHandler(this.StripMenuExitItem_Click);
            // 
            // StripMenuInformationItem
            // 
            this.StripMenuInformationItem.Name = "StripMenuInformationItem";
            this.StripMenuInformationItem.Size = new System.Drawing.Size(93, 20);
            this.StripMenuInformationItem.Text = "Информация";
            this.StripMenuInformationItem.Click += new System.EventHandler(this.StripMenuInformationItem_Click);
            // 
            // NFigureBoard
            // 
            this.NFigureBoard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(182)))), ((int)(((byte)(192)))), ((int)(((byte)(235)))));
            this.NFigureBoard.Location = new System.Drawing.Point(302, 38);
            this.NFigureBoard.Margin = new System.Windows.Forms.Padding(2);
            this.NFigureBoard.Name = "NFigureBoard";
            this.NFigureBoard.Size = new System.Drawing.Size(100, 100);
            this.NFigureBoard.TabIndex = 2;
            this.NFigureBoard.TabStop = false;
            // 
            // ScoreText
            // 
            this.ScoreText.AutoSize = true;
            this.ScoreText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.ScoreText.Location = new System.Drawing.Point(299, 186);
            this.ScoreText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.ScoreText.Name = "ScoreText";
            this.ScoreText.Size = new System.Drawing.Size(42, 17);
            this.ScoreText.TabIndex = 3;
            this.ScoreText.Text = "Очки";
            // 
            // Score
            // 
            this.Score.AccessibleDescription = "";
            this.Score.AccessibleName = "";
            this.Score.AutoSize = true;
            this.Score.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Score.Location = new System.Drawing.Point(386, 186);
            this.Score.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Score.Name = "Score";
            this.Score.Size = new System.Drawing.Size(16, 17);
            this.Score.TabIndex = 4;
            this.Score.Text = "0";
            // 
            // LineText
            // 
            this.LineText.AutoSize = true;
            this.LineText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LineText.Location = new System.Drawing.Point(299, 240);
            this.LineText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LineText.Name = "LineText";
            this.LineText.Size = new System.Drawing.Size(50, 17);
            this.LineText.TabIndex = 5;
            this.LineText.Text = "Линий";
            // 
            // BurnedLine
            // 
            this.BurnedLine.AutoSize = true;
            this.BurnedLine.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.BurnedLine.Location = new System.Drawing.Point(386, 240);
            this.BurnedLine.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.BurnedLine.Name = "BurnedLine";
            this.BurnedLine.Size = new System.Drawing.Size(16, 17);
            this.BurnedLine.TabIndex = 6;
            this.BurnedLine.Text = "0";
            // 
            // Level
            // 
            this.Level.AutoSize = true;
            this.Level.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.Level.Location = new System.Drawing.Point(386, 286);
            this.Level.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.Level.Name = "Level";
            this.Level.Size = new System.Drawing.Size(16, 17);
            this.Level.TabIndex = 8;
            this.Level.Text = "0";
            // 
            // LevelText
            // 
            this.LevelText.AutoSize = true;
            this.LevelText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.LevelText.Location = new System.Drawing.Point(299, 286);
            this.LevelText.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.LevelText.Name = "LevelText";
            this.LevelText.Size = new System.Drawing.Size(63, 17);
            this.LevelText.TabIndex = 7;
            this.LevelText.Text = "Уровень";
            // 
            // GameOverLabel
            // 
            this.GameOverLabel.AutoSize = true;
            this.GameOverLabel.BackColor = System.Drawing.Color.LightBlue;
            this.GameOverLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.GameOverLabel.Location = new System.Drawing.Point(69, 260);
            this.GameOverLabel.Name = "GameOverLabel";
            this.GameOverLabel.Size = new System.Drawing.Size(159, 31);
            this.GameOverLabel.TabIndex = 9;
            this.GameOverLabel.Text = "Конец игры";
            this.GameOverLabel.Visible = false;
            // 
            // TetrisForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gainsboro;
            this.ClientSize = new System.Drawing.Size(432, 559);
            this.Controls.Add(this.GameOverLabel);
            this.Controls.Add(this.Level);
            this.Controls.Add(this.LevelText);
            this.Controls.Add(this.BurnedLine);
            this.Controls.Add(this.LineText);
            this.Controls.Add(this.Score);
            this.Controls.Add(this.ScoreText);
            this.Controls.Add(this.NFigureBoard);
            this.Controls.Add(this.GBoard);
            this.Controls.Add(this.MainMenu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MinimizeBox = false;
            this.Name = "TetrisForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Тетрис 2.1";
            this.Load += new System.EventHandler(this.TetrisForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TetrisForm_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.GBoard)).EndInit();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.NFigureBoard)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox GBoard;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem StripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem StripMenuStartItem;
        private System.Windows.Forms.ToolStripMenuItem StripMenuPauseItem;
        private System.Windows.Forms.ToolStripMenuItem StripMenuExitItem;
        private System.Windows.Forms.ToolStripMenuItem StripMenuInformationItem;
        private System.Windows.Forms.PictureBox NFigureBoard;
        private System.Windows.Forms.Label ScoreText;
        private System.Windows.Forms.Label Score;
        private System.Windows.Forms.Label LineText;
        private System.Windows.Forms.Label BurnedLine;
        private System.Windows.Forms.Label Level;
        private System.Windows.Forms.Label LevelText;
        private System.Windows.Forms.ToolStripMenuItem StripMenuStopItem;
        private System.Windows.Forms.Label GameOverLabel;
    }
}

namespace System
{
   
}


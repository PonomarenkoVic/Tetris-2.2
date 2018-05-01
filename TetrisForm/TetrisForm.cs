﻿using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Media;
using TetrisForm.Properties;
using TetrisLogic;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using TetrisInterfaces;
using TetrisInterfaces.Enum;
using TetrisLogic.Figures;
using LinearGradientBrush = System.Drawing.Drawing2D.LinearGradientBrush;

namespace TetrisForm
{
    public partial class TetrisForm : Form
    {
       
        public TetrisForm()
        {
            InitializeComponent();
            //GBoard.BackColor = Color.LightBlue;
            //NFigureBoard.BackColor = Color.LightBlue;
 
            _gameBoard = new GameBoard(10, 20);
            _gameBoard.UpdateEvent += Updating;
            _gameBoard.GameOverEvent += GameOver;
            _gameBoard.SoundEvent += Sound;          
        }

        const byte SizePoint = 25;
        const byte WidthHeightNFigureBoard = 4;


        private void TetrisForm_Load(object sender, EventArgs e)
        {
            StripMenuStopItem.Enabled = false;
            StripMenuPauseItem.Enabled = false;
            _graphicGameBoard = GBoard.CreateGraphics();
            _graphicNextFigure = NFigureBoard.CreateGraphics();
            //LinearGradientBrush grBrush = new LinearGradientBrush(GBoard.DisplayRectangle, Color.Cyan, Color.Beige, 0.5f);
            ////grBrush.GradientStops.Add(new GradientStop(){Color = System.Windows.Media.Color.FromArgb(250, 250, 250, 250), Offset = 0});
            ////grBrush.GradientStops.Add(new GradientStop() { Color = System.Windows.Media.Color.FromArgb(178, 23, 31, 110), Offset = 0.5 });
            ////grBrush.GradientStops.Add(new GradientStop() { Color = System.Windows.Media.Color.FromArgb(0, 0, 0, 0), Offset = 1});
            //_graphicGameBoard.FillRectangle(grBrush, GBoard.DisplayRectangle);
        }



        #region Methods by subscription


        private void Sound(object sender, SoundEventArg arg)
        {
            global::TetrisForm.Sound.Play(arg.Sound);
        }

        private void GameOver()
        {
            GameOverLabel.Visible = true;
        }

        private void Updating(object sender, ShowEventArg arg)
        {
            Level.Text = (arg.Level + 1).ToString();
            Score.Text = arg.Score.ToString();
            BurnedLine.Text = arg.BurnedLine.ToString();
            ShowGameBoard(arg);
            ShowNextFigure(arg);
        }
      

        #endregion



        #region Control



        private void TetrisForm_KeyDown(object sender, KeyEventArgs e)
        {
            Direction dir = Direction.Empty;
            switch (e.KeyCode)
            {
                case Keys.Left:
                    dir = Direction.Left;   
                    break;
                case Keys.Right:
                    dir = Direction.Right;
                   
                    break;
                case Keys.Down:
                    dir = Direction.Down;
              
                    break;
                case Keys.Space:
                    _gameBoard.Turn();
                    break;
                case Keys.P:
                    _gameBoard.Pause();
                    break;               
            }

            if (dir != Direction.Empty)
            {
                _gameBoard.Move(dir);
            }
            
        }


        private void StripMenuStartItem_Click(object sender, EventArgs e)
        {
            GameOverLabel.Visible = false;           
            _gameBoard.Start();
            StripMenuStartItem.Enabled = false;
            StripMenuExitItem.Enabled = false;
            StripMenuInformationItem.Enabled = false;
            StripMenuPauseItem.Enabled = true;
            StripMenuStopItem.Enabled = true;
        }


        private void StripMenuPauseItem_Click(object sender, EventArgs e)
        {
            _gameBoard.Pause();
        }

        private void StripMenuExitItem_Click(object sender, EventArgs e)
        {
            if (ActiveForm != null) ActiveForm.Close();
        }

        private void StripMenuStopItem_Click(object sender, EventArgs e)
        {
            _gameBoard.Stop();
            StripMenuStartItem.Enabled = true;
            StripMenuExitItem.Enabled = true;
            StripMenuInformationItem.Enabled = true;
            StripMenuPauseItem.Enabled = false;
            StripMenuStopItem.Enabled = false;
        }

        private void StripMenuInformationItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.TetrisForm_StripMenuInformationItem_Click_, "Control");
        }

        #endregion



        #region Showing

        private void ShowNextFigure(ShowEventArg arg)
        {
            for (byte i = 0; i < arg.NextFigure.GetLength(1); i++)
            {
                for (byte j = 0; j < arg.NextFigure.GetLength(0); j++)
                {
                    if (arg.NextFigure[j, i] != null)
                    {
                        Brush colorN = View.GetColor(arg.NextFigure[j, i].Col);
                        _graphicNextFigure.FillEllipse(colorN,
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                    else
                    {
                        _graphicNextFigure.FillEllipse(new SolidBrush(Color.FromArgb(182,192,235)), 
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                }
            }
        }

        private void ShowGameBoard(ShowEventArg arg)
        {
            int boardWidth = arg.Board.GetLength(0);
            int boardHight = arg.Board.GetLength(1);
            for (byte i = 0; i < boardHight; i++)
            {
                for (byte j = 0; j < boardWidth; j++)
                {
                    if (arg.Board[j, i] != null)
                    {
                        _graphicGameBoard.FillEllipse(View.GetColor(arg.Board[j, i].Col),
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                    else
                    {
                        _graphicGameBoard.FillEllipse(new SolidBrush(Color.FromArgb(182, 192, 235)),
                            new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                }
            }
        }

        #endregion


        Graphics _graphicGameBoard;
        Graphics _graphicNextFigure;
        private readonly GameBoard _gameBoard;

       
    }
}

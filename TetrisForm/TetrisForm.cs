using System;
using System.Drawing;
using System.Windows.Forms;
using TetrisForm.Properties;
using TetrisLogic;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using TetrisInterfaces;
namespace TetrisForm
{
    public partial class TetrisForm : Form
    {
       
        public TetrisForm()
        {
            InitializeComponent();
            GBoard.BackColor = Color.LightBlue;
            NFigureBoard.BackColor = Color.LightBlue;
 
            _gameBoard = new GameBoard(10, 20);
            _gameBoard.UpdateEvent += Updating;
            _gameBoard.GameOverEvent += GameOver;
            _gameBoard.SoundBurnLineEvent += SoundBurningLine;
            _gameBoard.SoundStepEvent += SoundStep;
            _gameBoard.SoundTurningEvent += SoundTurning;

        }


        const byte SizePoint = 25;
        const byte WidthHeightNFigureBoard = 4;



        private void TetrisForm_Load(object sender, EventArgs e)
        {
            StripMenuStopItem.Enabled = false;
            StripMenuPauseItem.Enabled = false;
        }




        #region Methods by subscription

        private void SoundTurning()
        {
            Sound.PlayTurningSound();
        }

        private void SoundStep()
        {
            Sound.PlayStepSound();
        }

        private void SoundBurningLine()
        {
            Sound.PlayBurningSound();
        }

        private void GameOver()
        {
            GameOverLabel.Visible = true;
        }

        private void Updating()
        {
            PaintGameBoard(_gameBoard.GetData);
        }

        #endregion






        #region Control



        private void TetrisForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    _gameBoard.StepLeft();
                    break;
                case Keys.Right:
                    _gameBoard.StepRight();
                    break;
                case Keys.Down:
                    _gameBoard.NextStep();
                    break;
                case Keys.Space:
                    _gameBoard.Turn();
                    break;
                case Keys.P:
                    _gameBoard.Pause();
                    break;
            }
        }




        private void StripMenuStartItem_Click(object sender, EventArgs e)
        {
            GameOverLabel.Visible = false;
            _graphicGameBoard = GBoard.CreateGraphics();
            _graphicNextFigure = NFigureBoard.CreateGraphics();
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
        #endregion
      



        #region Showing


        void PaintGameBoard(IShowable board)
        {
            Level.Text = (board.Level + 1).ToString();
            Score.Text = board.Score.ToString();
            BurnedLine.Text = board.BurnedLines.ToString();
            int boardWidth = board.Field[0].GetLength(0);
            int boardHight = board.Field[0].GetLength(1);

            for (byte i = 0; i < boardHight; i++)
            {
                for (byte j = 0; j < boardWidth; j++)
                {
                    if (board.Field[0][j, i])
                    {
                       _graphicGameBoard.FillEllipse(View.GetColor(board.ColorField[j, i]), new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                    else
                    {
                        _graphicGameBoard.FillEllipse(Brushes.LightBlue, new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                }
            }


            Brush colorN = View.GetColor(board.ColorNextFigure);
            for (byte i = 0; i < WidthHeightNFigureBoard; i++)
            {
                for (byte j = 0; j < WidthHeightNFigureBoard; j++)
                {
                    if (board.Field[1][j, i])
                    {
                        _graphicNextFigure.FillEllipse(colorN, new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                    else
                    {
                        _graphicNextFigure.FillEllipse(Brushes.LightBlue, new Rectangle(j * SizePoint, i * SizePoint, SizePoint, SizePoint));
                    }
                }
            }
        }


        #endregion


        Graphics _graphicGameBoard;
        Graphics _graphicNextFigure;
        private readonly GameBoard _gameBoard;

        private void StripMenuInformationItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.TetrisForm_StripMenuInformationItem_Click_, "Control");
        }
    }
}

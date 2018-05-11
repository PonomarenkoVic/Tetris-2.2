using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Threading;
using TetrisForm.Properties;
using TetrisLogic;
using Brush = System.Drawing.Brush;
using Brushes = System.Drawing.Brushes;
using Color = System.Drawing.Color;
using KeyEventArgs = System.Windows.Forms.KeyEventArgs;
using TetrisInterfaces;
using TetrisInterfaces.Enum;
using TetrisLogic.Classes;
using TetrisLogic.Figures;
using LinearGradientBrush = System.Drawing.Drawing2D.LinearGradientBrush;

namespace TetrisForm
{
    public partial class TetrisForm : Form
    {
       
        public TetrisForm()
        {
            InitializeComponent();          
        }


        const byte SizePoint = 25;
        const byte WidthHeightNFigureBoard = 4;


        private void SubscribeToEvents()
        {
            _gameBoard.UpdateEvent += Updating;
            _gameBoard.GameOverEvent += GameOver;
            _gameBoard.SoundEvent += Sound;
            _gameBoard.VelocityChangeEvent += VelocityChanged;
            _timer.Tick += Step;
        }

        private void TetrisForm_Load(object sender, EventArgs e)
        {
            StripMenuStopItem.Enabled = false;
            StripMenuPauseItem.Enabled = false;
            StripMenuSaveGameItem.Enabled = false;
        }



        #region Methods by subscription


        private void Sound(object sender, SoundEventArg arg)
        {
            global::TetrisForm.Sound.Play(arg.Sound);
        }

        private void GameOver()
        {
            MessageLabel.Text = "Конец игры";
            MessageLabel.Visible = true;
            _timer.Stop();   
        }

        private void Updating(object sender, ShowEventArg arg)
        {
            Level.Text = (arg.Level + 1).ToString();
            Score.Text = arg.Score.ToString();
            BurnedLine.Text = arg.BurnedLine.ToString();
            ShowGameBoard(arg);
            ShowNextFigure(arg);
        }

        private void Step(object sender, EventArgs eventArgs)
        {
            _gameBoard.Step();
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
                    if (_timer.IsEnabled)
                    {
                        _gameBoard.Turn();
                    }
                    
                    break;
                case Keys.P:
                    StripMenuPauseItem_Click(null, null);
                    break;               
            }

            if (dir != Direction.Empty && _timer.IsEnabled)
            {
                _gameBoard.Move(dir);
            }
            
        }

        private void StripMenuStartItem_Click(object sender, EventArgs e)
        {
            InitializeGame();
            MessageLabel.Visible = false;           
            _gameBoard.Start();
            SetMenuStatus(true);
            _timer.Start();
        }

        private void StripMenuPauseItem_Click(object sender, EventArgs e)
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
                MessageLabel.Text = "     Пауза";
                MessageLabel.Visible = true;
                StripMenuSaveOptions.Enabled = false;
                StripMenuOpenGameItem.Enabled = true;
                StripMenuSaveGameItem.Enabled = true;
            }
            else
            {
                StripMenuOpenGameItem.Enabled = false;
                StripMenuSaveGameItem.Enabled = false;
                MessageLabel.Visible = false;
                _timer.Start();
            }            
        }

        private void StripMenuExitItem_Click(object sender, EventArgs e)
        {
            ActiveForm?.Close();
        }

        private void StripMenuStopItem_Click(object sender, EventArgs e)
        {
           _timer.Stop();
            GameOver();
            SetMenuStatus(false);
        }

       

        private void StripMenuInformationItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(Resources.TetrisForm_StripMenuInformationItem_Click_, "Control");
        }



        private void MenuSaveOptionsItem_Click(object sender, EventArgs e)
        {
            SaveOptionsForm form = new SaveOptionsForm();
            form.ConnBuiltEvent += (object obj, ConnEventArg args) => _connString = args.ConnString;
            form.ShowDialog();

        }

        private void StripMenuSaveGameItem_Click(object sender, EventArgs e)
        {
            _gameBoard.Save();
        }

        private void StripMenuOpenGameItem_Click(object sender, EventArgs e)
        {
            if (_gameBoard == null)
            {
                InitializeGame();
            }
            DataTable saveTable = _gameBoard.GetSavePoints();
            if (saveTable != null)
            {

                OpenGameForm form = new OpenGameForm(saveTable);
                form.PointChose = (obj, args) => _gameBoard.Open(args.Id, args.Level, args.BurnedLine, args.Score, args.IdField);
                form.ShowDialog();
                StripMenuStartItem.Enabled = true;
                StripMenuPauseItem.Enabled = false;
            }
            else
            {
                MessageBox.Show("Ошибка чтения сохраненных данных");
            }


        }

        private void SetMenuStatus(bool status)
        {
            StripMenuStartItem.Enabled = !status;
            StripMenuExitItem.Enabled = !status;
            StripMenuInformationItem.Enabled = !status;
            StripMenuPauseItem.Enabled = status;
            StripMenuStopItem.Enabled = status;
            StripMenuSaveOptions.Enabled = !status;

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


        private void VelocityChanged(object obj, VelocChangedEventArg arg)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / arg.Vel));
        }


        private void InitializeGame()
        {
            if (_timer == null)
            {
                _timer = new DispatcherTimer();
            }
            if (_connString == null)
            {
                MenuSaveOptionsItem_Click(null, null);
            }

            InitializeGameBoard();
           
        }

        private void InitializeGameBoard()
        {
            if (_gameBoard == null)
            {
                _gameBoard = new TetrisGameBoard(10, 20, _connString);
                SubscribeToEvents();
                _graphicGameBoard = GBoard.CreateGraphics();
                _graphicNextFigure = NFigureBoard.CreateGraphics();
            }
        }


        Graphics _graphicGameBoard;
        Graphics _graphicNextFigure;
        private ITetrisLogic _gameBoard;
        private DispatcherTimer _timer;
        private SqlConnectionStringBuilder _connString;
    }
}

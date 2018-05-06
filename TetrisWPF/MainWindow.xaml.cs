using System;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using TetrisWPF.ViewModel;
using TetrisInterfaces;
using TetrisInterfaces.Enum;


namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {


        private float Velosity
        {
            set => _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / value));
        }


        public MainWindow()
        {
            InitializeComponent();
            _timer = new DispatcherTimer();
            _viewModel = new global::ViewModel.ViewModel(_width, _height);
            DataContext = _viewModel;
            SubscribeToEvents();
            //    var binding = new Binding("Velocity") { Mode = BindingMode.OneWay };
            //    this.SetBinding(Velosity, binding);
        }

        private const int _width = 10;
        private const int _height = 20;


        //public readonly DependencyProperty ViewModelFloatProperty =
        //    DependencyProperty.Register(
        //        nameof(Velosity),
        //        typeof(float),
        //        typeof(MainWindow),
        //        new PropertyMetadata(OnVelocityChanged)
        //    );

        private  void OnVelocityChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / (float)e.NewValue));
        }



        private void SubscribeToEvents()
        {
            _viewModel.UpdateBoard += Update;
            _viewModel.GameOverEvent += GameOver;
            _viewModel.SoundEvent += Sound;
            _viewModel.VelChangeEvent += VelocityChanged;
            _timer.Tick += Step;
        }

        #region Methods by subscription

        private void Sound(object sender, SoundEventArg arg)
        {
            TetrisWPF.Sound.Play(arg.Sound);
        }

        private void GameOver()
        {        
            _timer.Stop();
            PauseAnimation.Content = "Game Over";
            RectPause.Visibility = Visibility.Visible;
            if (!GameBoard.Children.Contains(RectPause))
            {
                GameBoard.Children.Remove(RectPause);
                GameBoard.Children.Add(RectPause);
            }
            SetMenuItemStatus(false);
        }

        private void Update(object sender, ShowEventArg arg)
        {
            if (arg != null)
            {
                DrawGameBoard(arg);
                DrawNextFigure(arg);
            }
        }

        private void VelocityChanged(object obj, VelocChangedEventArg arg)
        {
            _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / arg.Vel));
        }

        private void Step(object sender, EventArgs eventArgs)
        {
            //_viewModel.Step();
        }

        #endregion



        #region  Control

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            Direction dir = Direction.Empty;
            switch (e.Key)
            {
                case Key.Down:
                    dir = Direction.Down;

                    break;
                case Key.Left:
                    dir = Direction.Left;
                    break;
                case Key.Right:
                    dir = Direction.Right;
                    break;
                case Key.Space:
                    if (_timer.IsEnabled == true)
                    {
                        _viewModel.Turn();
                    }
                    break;
                case Key.P:
                    PauseGame_Click(null, null);
                    break;
            }
            if (dir != Direction.Empty && _timer.IsEnabled == true)
            {
                _viewModel.Move(dir);
            }           
        }

        private void StopGame_Click(object sender, RoutedEventArgs e)
        {                 
            GameOver();
        }

        private void PauseGame_Click(object sender, RoutedEventArgs e)
        {         
            if (_timer.IsEnabled == true)
            {
                PauseAnimation.Content = "Pause";
                RectPause.Visibility = Visibility.Visible;
                if (!GameBoard.Children.Contains(RectPause))
                {
                    GameBoard.Children.Remove(RectPause);
                    GameBoard.Children.Add(RectPause);
                }
                _timer.Stop();
            }
            else
            {
                RectPause.Visibility = Visibility.Hidden;
                _timer.Start();
            }
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Left - <\nRight - >\nDown - v\nTurn - Space\nPause - P", "Control");
        }

        private void StartGame_OnClick(object sender, RoutedEventArgs e)
        {                    
            SetMenuItemStatus(true);
            RectPause.Visibility = Visibility.Hidden;
            _timer.Start();
        }

        private void SetMenuItemStatus(bool status)
        {
            StopGame.IsEnabled = status;
            StartGame.IsEnabled = !status;
            PauseGame.IsEnabled = status;
            Information.IsEnabled = !status;
        }

        #endregion



        #region Showing


        private void DrawNextFigure(ShowEventArg arg)
        {
            if (arg.NextFigure != null)
            {
                NextFigureBoard.Children.Clear();
                for (byte i = 0; i < arg.NextFigure.GetLength(1); i++)
                {
                    for (byte j = 0; j < arg.NextFigure.GetLength(0); j++)
                    {
                        if (arg.NextFigure[j, i] != null)
                        {
                            SolidColorBrush color = View.GetColor(arg.NextFigure[j, i].Col);
                            NextFigureBoard.Children.Add(View.CreateRectangle(color, j, i));
                        }
                    }
                }
            }
        }

        private void DrawGameBoard(ShowEventArg arg)
        {
            if (arg.Board != null)
            {              
                GameBoard.Children.Clear();
                for (byte i = 0; i < _height; i++)
                {
                    for (byte j = 0; j < _width; j++)
                    {
                        if (arg.Board[j, i] != null)
                        {
                            SolidColorBrush color = View.GetColor(arg.Board[j, i].Col);
                            GameBoard.Children.Add(View.CreateRectangle(color, j, i));
                        }
                    }
                }
            }
        }


        #endregion

       

           
        private readonly global::ViewModel.ViewModel _viewModel;
        private readonly DispatcherTimer _timer;

        private void OpenGame_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void SaveGame_OnClick(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }


    
}

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using TetrisWPF.ViewModel;
using TetrisInterfaces;



namespace TetrisWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
             _viewModel = new global::ViewModel.ViewModel(10, 20);
            _viewModel.UpdateBoard += Update;
            _viewModel.GameOverEvent += GameOver;
            _viewModel.SoundBurnLineEvent += SoundBurningLine;
            _viewModel.SoundStepEvent += SoundStep;
            _viewModel.SoundTurningEvent += SoundTurning;
            DataContext = _viewModel.GetDataContext();
        }


        const byte StrokeThickness = 1;
        const byte SizeRectangle = 25;
        const byte WidthHeightNFigureBoard = 4;
        const float Opac = 1f;


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

            PauseAnimation.Content = "Game Over";
            RectPause.Visibility = Visibility.Visible;
            if (!GameBoard.Children.Contains(RectPause))
            {
                GameBoard.Children.Add(RectPause);
            }
        }

        private void Update()
        {
            PaintGameBoard(_viewModel.GetData());
        }

        #endregion
       
      

        #region  Control
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Down:
                    _viewModel.DownClick();
                    break;
                case Key.Left:
                    _viewModel.LeftClick();
                    break;
                case Key.Right:
                    _viewModel.RightClick();
                    break;
                case Key.Space:
                    _viewModel.SpaceClick();
                    break;
                case Key.P:
                    PauseGame_Click(null, null);
                    break;
            }
        }


        private void StopGame_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.StopClick();
            StopGame.IsEnabled = false;
            StartGame.IsEnabled = true;
            PauseGame.IsEnabled = false;
            Information.IsEnabled = true;
        }

        private void PauseGame_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.PClick();
            if (RectPause.Visibility == Visibility.Visible)
            {
                RectPause.Visibility = Visibility.Hidden;
            }
            else
            {
                PauseAnimation.Content = "Pause";
                RectPause.Visibility = Visibility.Visible;
                if (!GameBoard.Children.Contains(RectPause))
                {
                    GameBoard.Children.Add(RectPause);
                }
            }
        }

        private void Information_Click(object sender, RoutedEventArgs e)
        {

            MessageBox.Show("Left - <\nRight - >\nDown - v\nTurn - Space\nPause - P", "Control");
        }


        private void StartGame_OnClick(object sender, RoutedEventArgs e)
        {
            _viewModel.Start();
            StopGame.IsEnabled = true;
            StartGame.IsEnabled = false;
            PauseGame.IsEnabled = true;
            Information.IsEnabled = false;

            RectPause.Visibility = Visibility.Hidden;
        }

        #endregion



        #region Showing


        void PaintGameBoard(IShowable board)
        {

            int boardWidth = board.Field[0].GetLength(0);
            int boardHight = board.Field[0].GetLength(1);
            GameBoard.Children.Clear();
            for (byte i = 0; i < boardHight; i++)
            {
                for (byte j = 0; j < boardWidth; j++)
                {
                    if (board.Field[0][j, i])
                    {
                        SolidColorBrush color = View.GetColor(board.ColorField[j, i]);
                        GameBoard.Children.Add(CreateRectangle(color, j, i));

                    }
                }
            }

            NextFigureBoard.Children.Clear();
            for (byte i = 0; i < WidthHeightNFigureBoard; i++)
            {
                for (byte j = 0; j < WidthHeightNFigureBoard; j++)
                {
                    if (board.Field[1][j, i])
                    {
                        SolidColorBrush color = View.GetColor(board.ColorNextFigure);
                        NextFigureBoard.Children.Add(CreateRectangle(color, j, i));
                    }
                }
            }
        }

        private Rectangle CreateRectangle(SolidColorBrush color, byte j, byte i)
        {
            Rectangle rectangle = new Rectangle()
            {
                StrokeThickness = StrokeThickness,
                Stroke = BorderColor,
                Width = SizeRectangle,
                Height = SizeRectangle,
                Opacity = Opac
            };

            rectangle.Fill = color;
            rectangle.SetValue(Canvas.LeftProperty, j * (double)SizeRectangle);
            rectangle.SetValue(Canvas.TopProperty, i * (double)SizeRectangle);
            return rectangle;
        }

        #endregion



        private static readonly SolidColorBrush BorderColor = new SolidColorBrush(Colors.Black);     
        private readonly global::ViewModel.ViewModel _viewModel;
    }
}

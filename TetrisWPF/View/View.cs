using System;
using System.Windows.Media;

namespace TetrisWPF.ViewModel
{
    internal static class View
    {
        public static SolidColorBrush GetColor(byte source)
        {
            SolidColorBrush color;
            switch (source)
            {
                case 0:
                    color = new SolidColorBrush(Colors.Brown);
                    break;
                case 1:
                    color = new SolidColorBrush(Colors.Red);
                    break;
                case 2:
                    color = new SolidColorBrush(Colors.Blue);
                    break;
                case 3:
                    color = new SolidColorBrush(Colors.BlueViolet);
                    break;
                case 4:
                    color = new SolidColorBrush(Colors.Green);
                    break;
                case 5:
                    color = new SolidColorBrush(Colors.DarkMagenta);
                    break;
                case 6:
                    color = new SolidColorBrush(Colors.LightSeaGreen);
                    break;
                case 7:
                    color = new SolidColorBrush(Colors.Lime);
                    break;
                case 8:
                    color = new SolidColorBrush(Colors.Crimson);
                    break;
                case 9:
                    color = new SolidColorBrush(Colors.SpringGreen);
                    break;
              default:
                  color = new SolidColorBrush(Colors.Black);
                  break;
            }
            return color;
        }
    }
}

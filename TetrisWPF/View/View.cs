using System;
using System.Windows.Media;
using TetrisInterfaces;

namespace TetrisWPF.ViewModel
{
    internal static class View
    {
        public static SolidColorBrush GetColor(TColor source)
        {
            SolidColorBrush color;
            switch (source)
            {
                case TColor.BlueViolet:
                    color = new SolidColorBrush(Colors.BlueViolet);
                    break;
                case TColor.Brown:
                    color = new SolidColorBrush(Colors.Brown);
                    break;
                case TColor.Green:
                    color = new SolidColorBrush(Colors.Green);
                    break;
                case TColor.Orange:
                    color = new SolidColorBrush(Colors.DarkOrange);
                    break;
                case TColor.Pink:
                    color = new SolidColorBrush(Colors.DeepPink);
                    break;
                case TColor.Red:
                    color = new SolidColorBrush(Colors.Red);
                    break;               
              default:
                  color = new SolidColorBrush(Colors.Black);
                  break;
            }
            return color;
        }
    }
}

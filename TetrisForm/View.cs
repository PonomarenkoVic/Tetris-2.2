
using System.Drawing;


namespace TetrisForm
{
    public class View
    {
        public static Brush GetColor(byte source)
        {
            Brush color;
            switch (source)
            {
                case 0:
                    color = Brushes.Brown;
                    break;
                case 1:
                    color = Brushes.Red;
                    break;
                case 2:
                    color =  Brushes.Blue;
                    break;
                case 3:
                    color =  Brushes.BlueViolet;
                    break;
                case 4:
                    color = Brushes.Green;
                    break;
                case 5:
                    color = Brushes.DarkMagenta;
                    break;
                case 6:
                    color =  Brushes.LightSeaGreen;
                    break;
                case 7:
                    color = Brushes.Lime;
                    break;
                case 8:
                    color = Brushes.Crimson;
                    break;
                case 9:
                    color =  Brushes.SpringGreen;
                    break;
                default:
                    color = Brushes.Black;
                    break;
            }
            return color;
        }
    }
}

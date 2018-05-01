
using System.Drawing;
using TetrisInterfaces;


namespace TetrisForm
{
    public class View
    {
        public static Brush GetColor(TColor source)
        {
            Brush color;
            switch (source)
            {
                case TColor.Brown:
                    color = Brushes.Brown;
                    break;
                case TColor.Red:
                    color = Brushes.Red;
                    break;
                case TColor.BlueViolet:
                    color =  Brushes.BlueViolet;
                    break;
                case TColor.Green:
                    color = Brushes.Green;
                    break;
                case TColor.Orange:
                    color = Brushes.DarkOrange;
                    break;
                case TColor.Pink:
                    color =  Brushes.DeepPink;
                    break;               
                default:
                    color = Brushes.Black;
                    break;
            }
            return color;
        }
    }
}

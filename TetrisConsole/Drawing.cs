using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TetrisInterfaces;

namespace TetrisConsole
{
    public static class Drawing
    {

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("kernel32.dll")]
        public static extern IntPtr GetConsoleWindow();

        [DllImport("user32.dll")]
        public static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDc);

        [DllImport("gdi32.dll")]
        static extern IntPtr DeleteDC(IntPtr hDc);

        static IntPtr _hWnd = IntPtr.Zero;
        static IntPtr _hDc = IntPtr.Zero;
        public static void Draw(object sender, ShowEventArg args)
        {
            _hWnd = GetConsoleWindow();
            if (_hWnd != IntPtr.Zero)
            {
                _hDc = GetDC(_hWnd);
                if (_hDc != IntPtr.Zero)
                {
                    using (Graphics consoleGraphics = Graphics.FromHdc(_hDc))
                    {

                        //Pen whitePen = new Pen(Color.White);
                        //Pen redPen = new Pen(Color.Red, 2);
                        Font font = new Font("verdana", 13);

                        ////consoleGraphics.DrawLine(whitePen, new Point(0, 30), new Point(300, 30));
                        ////consoleGraphics.DrawLine(whitePen, new Point(30, 0), new Point(30, 300));
                        consoleGraphics.DrawString("Tetris 2.2", font, Brushes.White, 10, 10);
                        consoleGraphics.DrawString("S - Start, Q - Stop, P -Pause", font, Brushes.White, 120, 650);
                        consoleGraphics.DrawString("v - Down, < - Left, > - Right, Space - Turn", font, Brushes.White, 50, 670);

                        //int y = 0;
                        //int x = 100;
                        //for (; x < 250; x++)
                        //{
                        //    y = x + 2;
                        //    consoleGraphics.DrawRectangle(redPen, x, y, 2, 2);
                        //}
                        int size = 25;
                        int horizIndention = 20;
                        int VertIndention = 40;
                        for (int i = 0; i < args.Board.GetLength(1); i++)
                        {
                            for (int j = 0; j < args.Board.GetLength(0); j++)
                            {
                                if (args.Board[j, i] != null)
                                {
                                    consoleGraphics.FillEllipse(GetColor(args.Board[j, i].Col), new Rectangle(j * size + horizIndention, i * size + VertIndention, size, size));
                                }
                                else
                                {
                                    consoleGraphics.FillEllipse(Brushes.Black, new Rectangle(j * size + horizIndention, i * size + VertIndention, size, size));
                                }
                                
                            }
                        }

                        font.Dispose();
                        //whitePen.Dispose();
                        //redPen.Dispose();
                    }

                }
                ReleaseDC(_hWnd, _hDc);
                DeleteDC(_hDc);

            }

        }



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
                    color = Brushes.BlueViolet;
                    break;
                case TColor.Green:
                    color = Brushes.Green;
                    break;
                case TColor.Orange:
                    color = Brushes.DarkOrange;
                    break;
                case TColor.Pink:
                    color = Brushes.DeepPink;
                    break;
                default:
                    color = Brushes.Black;
                    break;
            }
            return color;
        }

    }
}

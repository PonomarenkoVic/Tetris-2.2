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
                    int size = 25;
                    Graphics consoleGraphics = Graphics.FromHdc(_hDc);
                    DrawGameData(args, consoleGraphics);
                    DrawGameBoard(args, consoleGraphics, size);
                    DrawNextFigure(args, consoleGraphics, size);
                    //font.Dispose();
                    //whitePen.Dispose();
                    //redPen.Dispose();
                }
                //ReleaseDC(_hWnd, _hDc);
                //DeleteDC(_hDc);
            }
        }

        private static void DrawGameData(ShowEventArg args, Graphics consoleGraphics)
        {
            int horizIndention = 315;
            int vertIndention = 200;
            try
            {
                var font = new Font("verdana", 13);
                consoleGraphics.FillRectangle(Brushes.RoyalBlue, new Rectangle(300, 190, 160,
                    160));
                consoleGraphics.DrawString($"Level: {args.Level + 1}", font, Brushes.White,
                    horizIndention, 200);
                consoleGraphics.DrawString($"Line:   {args.BurnedLine}", font, Brushes.White,
                    horizIndention, 40 + vertIndention);
                consoleGraphics.DrawString($"Score: {args.Score}", font, Brushes.White,
                    horizIndention, 80 + vertIndention);
            }
            catch (System.Runtime.InteropServices.ExternalException e)
            {
                //Console.WriteLine(e);
                //throw;
            }
        }

        private static void DrawGameBoard(ShowEventArg args, Graphics consoleGraphics, int size)
        {
            int horizIndention = 30;
            int vertIndention = 50;
            for (int i = 0; i < args.Board.GetLength(1); i++)
            {
                for (int j = 0; j < args.Board.GetLength(0); j++)
                {
                    try
                    {
                        if (args.Board[j, i] != null)
                        {
                            consoleGraphics.FillEllipse(GetColor(args.Board[j, i].Col), new Rectangle(
                                j * size + horizIndention, i * size + vertIndention, size,
                                size));
                        }
                        else
                        {
                            consoleGraphics.FillEllipse(Brushes.CornflowerBlue,
                                new Rectangle(j * size + horizIndention, i * size + vertIndention, size,
                                    size));
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException e)
                    {
                        //Console.WriteLine(e);
                    }
                }
            }
        }

        private static void DrawNextFigure(ShowEventArg args, Graphics consoleGraphics, int size)
        {           
            int horizIndention = 325;
            int vertIndention = 50;

            for (int i = 0; i < args.NextFigure.GetLength(1); i++)
            {
                for (int j = 0; j < args.NextFigure.GetLength(0); j++)
                {
                    try
                    {
                        if (args.NextFigure[j, i] != null)
                        {
                            consoleGraphics.FillEllipse(GetColor(args.NextFigure[j, i].Col),
                                new Rectangle(j * size + horizIndention, i * size + vertIndention, size,
                                    size));
                        }
                        else
                        {
                            consoleGraphics.FillEllipse(Brushes.CornflowerBlue,
                                new Rectangle(j * size + horizIndention, i * size + vertIndention, size,
                                    size));
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException e)
                    {
                        //Console.WriteLine(e);
                    }
                }
            }
        }

        public static void Draw(string str)
        {
            _hWnd = GetConsoleWindow();
            if (_hWnd != IntPtr.Zero)
            {
                _hDc = GetDC(_hWnd);
                if (_hDc != IntPtr.Zero)
                {
                    using (Graphics consoleGraphics = Graphics.FromHdc(_hDc))
                    {
                        Font font = new Font("verdana", 16);
                        consoleGraphics.DrawString(str, font, Brushes.White, 50, 300);
                        font.Dispose();
                    }
                }

                ReleaseDC(_hWnd, _hDc);
                DeleteDC(_hDc);
            }
        }

        public static void DrawInitialData()
        {
            _hWnd = GetConsoleWindow();

            if (_hWnd != IntPtr.Zero)
            {
                _hDc = GetDC(_hWnd);
                if (_hDc != IntPtr.Zero)
                {
                    Graphics consoleGraphics = Graphics.FromHdc(_hDc);
                    Font font;
                    try
                    {
                        font = new Font("verdana", 13);
                        consoleGraphics.FillRectangle(Brushes.RoyalBlue, new Rectangle(0, 0, 500,
                            800));
                        consoleGraphics.DrawString("S - Start, Q - Stop, P -Pause", font, Brushes.White, 100, 560);
                        consoleGraphics.DrawString("v - Down, < - Left, > - Right, Space - Turn", font, Brushes.White,
                            35, 580);
                        consoleGraphics.DrawString("Tetris 2.2", font, Brushes.White, 10, 10);
                        for (int i = 0; i < 5; i++)
                        {
                            consoleGraphics.DrawLine(new Pen(Color.White), 30 - i, 50, 30 - i, 550);
                            consoleGraphics.DrawLine(new Pen(Color.White), 280 + i, 50, 280 + i, 550);
                            consoleGraphics.DrawLine(new Pen(Color.White), 26, 50 - i, 284, 50 - i);
                            consoleGraphics.DrawLine(new Pen(Color.White), 26, 550 + i, 284, 550 + i);
                            consoleGraphics.FillRectangle(Brushes.CornflowerBlue, new Rectangle(30, 50, 250, 500));
                            consoleGraphics.FillRectangle(Brushes.CornflowerBlue, new Rectangle(325, 50, 100, 100));
                            consoleGraphics.DrawLine(new Pen(Color.White), 325 - i, 50, 325 - i, 150);
                            consoleGraphics.DrawLine(new Pen(Color.White), 425 + i, 50, 425 + i, 150);
                            consoleGraphics.DrawLine(new Pen(Color.White), 321, 50 - i, 429, 50 - i);
                            consoleGraphics.DrawLine(new Pen(Color.White), 321, 150 + i, 429, 150 + i);
                        }
                    }
                    catch (System.Runtime.InteropServices.ExternalException e)
                    {
                        //Console.WriteLine(e);
                        //throw;
                    }
                }
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

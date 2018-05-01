using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TetrisInterfaces;
using TetrisInterfaces.Enum;
using TetrisLogic.Figures;
using System.Timers;
using Timer = System.Timers.Timer;
using System.Windows.Forms;

namespace TetrisConsole
{
    class TetrisControl
    {
       
        static void Main(string[] args)
        {

            _board = new GameBoard(10,20);
            _board.GameOverEvent += GameOver;
            _board.SoundEvent += PlaySound;
            _board.UpdateEvent += Update;
            Timer.Elapsed += Step;
            _board.VelocityChangeEvent += VelocityChanged;
            bool statusGame = false;
            bool statusPause = false;
            Console.CursorVisible = false;
            Console.WindowWidth = 60;
            Console.WindowHeight = 45;
            Console.BackgroundColor = ConsoleColor.Black;

            Form f = new Form();
            f.BackColor = Color.White;
            //f.FormBorderStyle = FormBorderStyle.None;
            f.Bounds = Screen.PrimaryScreen.Bounds;
            f.TopMost = true;
            Application.EnableVisualStyles();
            Application.Run(f);


            while (true)
            {

                //Thread.Sleep(20);
                if (Console.KeyAvailable == true)
                {
                    var key = Console.ReadKey(true);
                    Direction dir = Direction.Empty;
                    switch (key.Key)
                    {

                        case ConsoleKey.DownArrow:
                            dir = Direction.Down;
                            break;
                        case ConsoleKey.LeftArrow:
                            dir = Direction.Left;
                            break;
                        case ConsoleKey.RightArrow:
                            dir = Direction.Right;
                            break;
                        case ConsoleKey.Spacebar:
                            _board.Turn();
                            break;
                        case ConsoleKey.P:
                            if (!statusPause)
                            {
                                statusPause = true;
                                Timer.Stop();
                                Drawing.Draw("Пауза");
                            }
                            else
                            {
                                statusPause = false;
                                Timer.Start();
                            }
                            break;
                        case ConsoleKey.S:
                            if (!statusGame)
                            {
                                statusGame = true;                               
                                _board.Start();
                                Timer.Start();
                            }
                            break;
                        case ConsoleKey.Q:
                            statusGame = false;
                            statusPause = false;
                            Timer.Stop();
                            GameOver();
                            break;
                        default:
                            dir = Direction.Empty;
                            break;
                    }
                    if (dir != Direction.Empty && statusPause != true && statusGame == true)
                    {
                        _board.Move(dir);
                    }
                }
                
            }

        }

        private static void Step(object sender, ElapsedEventArgs e)
        {
            _board.Step();
        }

        private static void Update(object obj, ShowEventArg arg)
        {
            Drawing.Draw(obj, arg);
        }

        private static void PlaySound(object obj, SoundEventArg arg)
        {
            Sound.Play(arg.Sound);
        }

        private static void GameOver()
        {
            Drawing.Draw("Конец игры");
        }



        private static void VelocityChanged(object obj, VelocChangedEventArg arg)
        {
            Timer.Interval = 600 / arg.Vel;
        }


        private static readonly Timer Timer = new Timer();
        private static GameBoard _board;
    }
}

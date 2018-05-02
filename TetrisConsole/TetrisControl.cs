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
            
            Console.CursorVisible = false;
            Console.WindowWidth = 56;
            Console.WindowHeight = 38;
            Drawing.DrawInitialData();      
            StartKeyControl();
        }

        private static void StartKeyControl()
        {
            bool statusGame = false;
            bool statusPause = false;
            while (true)
            {
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
                            Pause(ref statusPause);
                            break;
                        case ConsoleKey.S:
                            Start(ref statusGame);
                            break;
                        case ConsoleKey.Q:
                            Stop(ref statusGame, ref statusPause);
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

        private static void Stop(ref bool statusGame, ref bool statusPause)
        {
            statusGame = false;
            statusPause = false;
            Timer.Stop();
            GameOver();
 
        }

        private static void Start(ref bool statusGame)
        {
            if (!statusGame)
            {
                statusGame = true;
                _board.Start();
                Timer.Start();
            }
        }

        private static void Pause(ref bool statusPause)
        {
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
            Drawing.Draw("     Game over");
        }

        private static void VelocityChanged(object obj, VelocChangedEventArg arg)
        {
            Timer.Interval = 600 / arg.Vel;
        }

        private static readonly Timer Timer = new Timer();
        private static GameBoard _board;
    }
}

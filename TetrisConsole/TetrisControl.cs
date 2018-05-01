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

namespace TetrisConsole
{
    class TetrisControl
    {
        static void Main(string[] args)
        {

            GameBoard board = new GameBoard(10,20);
            board.GameOverEvent += GameOver;
            board.SoundEvent += PlaySound;
            board.UpdateEvent += Update;
            bool statusGame = false;
            bool statusPause = false;
            Console.CursorVisible = false;
            Console.WindowWidth = 60;
            Console.WindowHeight = 45;
            Console.BackgroundColor = ConsoleColor.Black;

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
                            board.Turn();
                            break;
                        case ConsoleKey.P:
                            if (!statusPause)
                            {
                                statusPause = true;
                                board.Pause();
                            }
                            else
                            {
                                statusPause = false;
                                board.Pause();
                            }
                            break;
                        case ConsoleKey.S:
                            if (!statusGame)
                            {
                                statusGame = true;
                                //Thread a = new Thread(board.Start);
                                //a.Start();
                                board.Start();
                            }
                            break;
                        case ConsoleKey.Q:
                            statusGame = false;
                            statusPause = false;
                            board.Stop();
                            break;
                        default:
                            dir = Direction.Empty;
                            break;
                    }

                    if (dir != Direction.Empty && statusPause != true && statusGame == true)
                    {
                        board.Move(dir);
                    }
                }
                
            }

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
            throw new NotImplementedException();
        }

    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using TetrisInterfaces.Enum;

namespace TetrisConsole
{
    public static class Sound
    {
        public static void Play(TSound sound)
        {
            string path = null;
            switch (sound)
            {
                case TSound.Burning:
                    path = "burn.wav";
                    break;
                case TSound.Stepping:
                    path = "move.wav";
                    break;
                case TSound.Turning:
                    path = "turn.wav";
                    break;
            }

            if (File.Exists(path))
            {
                Player.SoundLocation = path;
                Player.Play();
            }           
        }
        private static readonly SoundPlayer Player = new SoundPlayer();
    }
}

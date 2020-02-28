
using System.IO;
using System.Media;
using TetrisInterfaces.Enum;


namespace TetrisForm
{
    internal class Sound
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
            if (!File.Exists(path))
                return;

            Player.SoundLocation = path;
            Player.Play();
        }
        private static readonly SoundPlayer Player = new SoundPlayer();

    }
}

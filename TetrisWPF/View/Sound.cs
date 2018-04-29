using System;
using System.Media;


namespace TetrisWPF
{
    internal static class Sound
    {
        
        public static void PlayStepSound()
        {
            Player.SoundLocation = "move.wav";
            Player.Play();
        }

        public static void PlayTurningSound()
        {
            Player.SoundLocation = "turn.wav";
            Player.Play();
        }

        public static void PlayBurningSound()
        {
            Player.SoundLocation = "burn.wav";
            Player.Play();
        }

  
        private static readonly SoundPlayer Player = new SoundPlayer();
    }
}

using System;
using TetrisInterfaces;


namespace TetrisLogic.Classes
{
    class ShowBoards:IShowable
    {
        public ShowBoards(bool[][,] field, byte[,] color, byte colorNextFigure, float velocity, int level, int burnedLines, int score)
        {
            NumberOfColors = Initializer.NumberOfColors;
            ColorNextFigure = colorNextFigure;
            ColorField = (byte[,])color.Clone();
            Field = (bool[][,])field.Clone();
            Velocity = velocity;
            Level = level;
            BurnedLines = burnedLines;
            Score = score;
        }
        public byte[,] ColorField { get; private set; }
        public byte ColorNextFigure { get; private set; }
        public bool[][,] Field { get; private set; }
        public byte NumberOfColors { get; private set; }
        public float Velocity { get; private set; }
        public int Level { get; private set; }
        public int BurnedLines { get; private set; }
        public int Score { get; private set; }
    }
}

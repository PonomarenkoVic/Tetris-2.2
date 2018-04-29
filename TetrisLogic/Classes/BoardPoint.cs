using System;


namespace TetrisLogic.Classes
{
    internal class BoardPoint
    {
        public BoardPoint(byte color )
        {
            ColPoint = color;
        }
        public byte ColPoint { get; set; } // point color (from 0  to Figure.NumberOfColors)
    }
}

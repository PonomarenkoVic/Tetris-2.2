using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TetrisInterfaces;
using TetrisLogic.Interfaces;

namespace TetrisLogic.Figures
{
    internal sealed class LeftG : Figure, IRotatable
    {

        public LeftG(TColor color, int[,] body, GameBoard board) : base(color, body, board)
        {
        }

        public override string ToString()
        {
            return "LeftG";
        }

        public bool Turn()
        {
            return base.TurnFigure();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisInterfaces
{
    public interface IShowable
    {
        byte[,] ColorField { get; }
        byte ColorNextFigure { get; }
        bool[][,] Field { get; }
        byte NumberOfColors { get; }
        float Velocity { get; }
        int Level { get; }
        int BurnedLines { get; }
        int Score { get; }
    }
}

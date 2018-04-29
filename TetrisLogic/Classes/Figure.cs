using System;


namespace TetrisLogic
{
    internal  class Figure:ICloneable
    {
        public Figure(string name, bool rotatable, byte color, byte[,] body)
        {
            Name = name;
            Rotatable = rotatable;
            FColor = color;
            Body = body;
            _correction = 0;
            NumberOfColors = Initializer.NumberOfColors;            
        }

        static Figure()
        {
            NumberFigurePoint = Initializer.NumberOfFigurePoint;
        }

        private static readonly byte NumberFigurePoint;

        public readonly byte NumberOfColors;
        public string Name { get; private set; }
        public bool Rotatable { get; private set; }
        public byte FColor { get; private set; }
        public byte[,] Body
        {
            get
            {
                return _figureBody;
            }
            set
            {
                _figureBody = value;
            }
        }

        
        public void StepLeft()
        {
            for (int i = 0; i < 4; i++)
            {
                _figureBody[i, 0]--;
            }
        }

        public void StepRight()
        {
            for (int i = 0; i < 4; i++)
            {
                _figureBody[i, 0]++;
            }
        }

        public void StepDown()
        {
            for (int i = 0; i < 4; i++)
            {
                _figureBody[i, 1]++;
            }
        }


        public byte[,] GetCoordTurnedFigure()
        {         
               
            byte xMin,
                 xMax,
                 yMin,
                 yMax;

            // determining of square of the current figure
            DeterminCoordSquareFig(_figureBody, out xMin, out xMax, out yMin, out yMax);

            // determining of of the rotation center
            byte x0 = (byte)((byte)((xMin + xMax) / 2) + _correction),
                 y0 = (byte)((yMin + yMax) / 2);

            // _corr - correction factor for the figure does not move when turning left or right
            _correction = (byte)(_correction == 0 ? 1 : 0);

            byte [,] rotatedFig = new byte[NumberFigurePoint, 2];
            for (int i = 0; i < NumberFigurePoint; i++)
            {
                //The reduction of the rotation center to the 0 point of coordinates
                byte x =(byte)(_figureBody[i, 0] - x0);
                byte y = (byte)(_figureBody[i, 1] - y0);

                //point rotation  around 0 by 90 degree. x1= x*cos(90) - y*sin(90), y1= x*sin(90) + y*cos(90); x1 = -y, y1 = x 

                //shift the center of rotation backwards
                byte x1 = (byte)(-y + x0);
                byte y1 = (byte)(x + y0);
                rotatedFig[i, 0] = x1;
                rotatedFig[i, 1] = y1;
            }

            return rotatedFig;
        }


        private static void DeterminCoordSquareFig(byte[,] fig, out byte xMin, out byte xMax, out byte yMin,
            out byte yMax)
        {
            // array of coordinates of the current figure
            // determining of square of the current figure
            xMin = byte.MaxValue;
            yMin = byte.MaxValue;
            xMax = byte.MinValue;
            yMax = byte.MinValue;

            for (int i = 0; i < 4; i++)
            {
                byte x = fig[i, 0];
                byte y = fig[i, 1];

                if (x < xMin)
                {
                    xMin = x;
                }
                if (x > xMax)
                {
                    xMax = x;
                }
                if (y < yMin)
                {
                    yMin = y;
                }
                if (y > yMax)
                {
                    yMax = y;
                }
            }

        }

        public object Clone()
        {
            Figure clone = (Figure)MemberwiseClone();
            clone._figureBody =(byte[,])_figureBody.Clone();
            return clone;
        }

       
        private byte _correction;
        private byte[,] _figureBody;
    }
}

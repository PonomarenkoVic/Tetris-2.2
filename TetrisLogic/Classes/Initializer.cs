using System;




namespace TetrisLogic
{
    internal static class Initializer
    {

        static Initializer()
        {
            if (RotatabilityOfFigures.Length == FigureNames.Length && FigureNames.Length == BodyFigures.Length)
            {
                NumberOfFigures = (byte)RotatabilityOfFigures.Length;
            }
            else
            {
                throw new Exception("Error in the initialization data of the figures");
            }

            if (FillingOfLevels.Length == LevelVelocity.Length)
            {
                NumberOfLev = LevelVelocity.Length;
            }
            else
            {
                throw new Exception("Error in the initialization data of the levels");
            }

        }

        public const byte NumberOfColors = 6;
        public const int LimitScore = 2000;
        public const byte NumberOfFigurePoint = 4;


        public static Figure GetNewFigure()
        {
            Figure figure = null;
            if (NumberOfFigures > 0)
            {
                int choseFigure = Rnd.Next(0, NumberOfFigures);
                byte color = (byte)Rnd.Next(0, NumberOfColors);
                figure = new Figure(FigureNames[choseFigure], RotatabilityOfFigures[choseFigure], color, (byte[,])BodyFigures[choseFigure].Clone());
            }
            return figure;
        }

        public static int NumberOfLevel
        {
            get
            {
                return NumberOfLev;
            }
        }

        public static float GetVelocity(int level)
        {
            return LevelVelocity[level];
        }

        public static byte[,] GetLevelFilling(int level)
        {
            return (byte[,])FillingOfLevels[level].Clone();
        }


        #region Data for game levels

        //velocity of moving of the figure on each of ten level
        static readonly float[] LevelVelocity = { 1, 1, 1.2f, 1.2f, 1.5f, 1.71f, 2, 2.4f, 3, 6 };


        static readonly byte[][,] FillingOfLevels ={
            null,                                 //Level 1
            null,                                 //Level 2
            null,                                 //Level 3
            new byte[,] {{1,1,0,1,1,0,0,1,1,1}},  //Level 4
 
            new byte[,] {
                {1,1,0,1,1,0,0,1,1,1},   //Level 5
                {0,1,1,1,0,1,1,1,0,1}},

            new byte[,] {
                {1,1,1,1,1,0,0,1,1,1},   //Level 6
                {0,1,0,1,0,1,1,1,1,1},
                {0,1,0,1,1,1,0,0,1,0}},
 
            new byte[,] {
                {1,1,0,1,1,0,0,1,1,1},   //Level 7
                {0,1,0,1,0,1,0,0,0,1},
                {0,1,0,1,0,1,1,1,1,1}},

            new byte[,] {
                {1,1,0,1,1,0,0,1,1,1},   //Level 8
                {1,1,1,0,1,0,1,1,0,1},
                {1,1,0,0,1,0,1,0,1,1},
                {1,0,1,1,0,0,0,1,1,1}},
 
            new byte[,] {
                {1,1,0,1,1,0,1,1,1,1},   //Level 9
                {0,1,1,1,1,0,1,0,1,0},
                {0,1,0,0,1,0,1,0,1,1},
                {1,1,1,1,1,0,1,0,0,1}},
 
            new byte[,] {
                {1,0,0,1,1,0,1,1,1,1},   //Level 10
                {0,0,1,1,1,0,1,0,1,0},
                {0,0,1,0,1,0,1,0,1,1},
                {1,0,1,1,1,0,1,0,0,1}} 
        };




        #endregion

        #region Data for initializing of figures

        private static readonly bool[] RotatabilityOfFigures =
        {
            false,  //square
            true,   //stick 
            true,   //left zigzag
            true,   //right zigzag
            true,   //right Г 
            true,   //left  Г
            true    // Т
        };

        private static readonly string[] FigureNames =
        {
            "Square",
            "Stick",
            "Left zigzag",
            "Right zigzag",
            "Right Г",
            "Left Г",
            "T figure"

        };

        private static readonly byte[][,] BodyFigures =
        {
            new byte[,] {{1, 1}, {2, 1}, {1, 2}, {2, 2}}, //square
            new byte[,] {{2, 0}, {2, 1}, {2, 2}, {2, 3}}, //stick 
            new byte[,] {{1, 0}, {1, 1}, {2, 1}, {2, 2}}, //left zigzag
            new byte[,] {{2, 0}, {1, 1}, {2, 1}, {1, 2}}, //right zigzag
            new byte[,] {{2, 0}, {2, 1}, {1, 2}, {2, 2}}, //right Г 
            new byte[,] {{1, 0}, {2, 0}, {2, 1}, {2, 2}}, //left  Г
            new byte[,] {{1, 1}, {2, 1}, {3, 1}, {2, 2}}, // Т
        };

        #endregion

        private static readonly int NumberOfLev;
        private static readonly byte NumberOfFigures;
        private static readonly Random Rnd = new Random();

    }
}

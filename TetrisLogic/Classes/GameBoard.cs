using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Threading;
using TetrisInterfaces;
using TetrisLogic.Classes;



namespace TetrisLogic
{
   
    

    public class GameBoard : ITetrisLogic
    {


        
        public GameBoard(byte width, byte height)
        {
            _width = width;
            _height = height;
            _fieldGameBoard = new BoardPoint[_width, _height];
            _velocity = Initializer.GetVelocity(0);
            _limitScore = Initializer.LimitScore;
            _level = 0;
            _burnedLines = 0;
            _score = 0;
            _timer = new DispatcherTimer {Interval = new TimeSpan(0, 0, 0, 0, (int) (600 / _velocity))};
            _timer.Tick += Step;
        }


        #region Properties

        public IShowable GetData
        {
            get
            {
                return GetJoinedData();
            }
        }

        public event Action GameOverEvent
        {
            add
            {
                _gameOverEvent += value;
            }
            // ReSharper disable once ValueParameterNotUsed
            remove
            {
                _gameOverEvent = null;
            }
        }

        public event Action UpdateEvent
        {
            add
            {
                _updateEvent = value;
            }
            // ReSharper disable once ValueParameterNotUsed
            remove
            {
                _updateEvent = null;
            }
        }

        public event Action SoundBurnLineEvent
        {
            add
            {
                _soundBurnLine = value;
            }
            // ReSharper disable once ValueParameterNotUsed
            remove
            {
                _soundBurnLine = null;
            }
        }

        public event Action SoundStepEvent
        {
            add
            {
                _soundStepEvent = value;
            }
            // ReSharper disable once ValueParameterNotUsed
            remove
            {
                _soundStepEvent = null;
            }
        }

        public event Action SoundTurningEvent
        {
            add
            {
                _soundTurningEvent = value;
            }
            // ReSharper disable once ValueParameterNotUsed
            remove
            {
                _soundTurningEvent = null;
            }
        }

        #endregion

            

        public void Step(object sender, EventArgs arg)
        {

            if (_timer.IsEnabled)
            {
                if (CheckPermissionMoveDown())
                {
                    _currentFigure.StepDown();
                    MoveFigureDown();
                   
                }
                else
                {
                    CheckAndBurnLine();
                    if (CheckScoreLevelUp())
                    {
                        LevelUp();
                    }                  
                    ExchangeFigures();
                     if (!CopyCurrentFigureToBoard())
                    {
                        GameOver();
                    }
                    
                }

                if (_updateEvent != null)
                {
                    _updateEvent();
                }        
            }           
        }

       
        #region Methods of operations with a figure


        private void ExchangeFigures()
        {
            _currentFigure = (Figure)_nextFigure.Clone();
            _nextFigure = Initializer.GetNewFigure();
        }


        private int GetNumberEmptyTopLineOfCurrentFigure()
        {
            int minRow = 3;
            for (int i = 0; i < WidthHeighFigureField; i++)
            {
                if (_currentFigure.Body[i, 1] < minRow)
                {
                    minRow = _currentFigure.Body[i, 1];
                }
            }
            return minRow;
        }


        private bool CopyCurrentFigureToBoard()
        {
            bool result = true;
            int k = GetNumberEmptyTopLineOfCurrentFigure();
            for (int i = 0; i < WidthHeighFigureField; i++)
            {
                byte x = (byte)(_currentFigure.Body[i, 0] + 3);
                byte y = (byte)(_currentFigure.Body[i, 1] - k);
                if (x < _width && y < _height)
                {
                    _currentFigure.Body[i, 0] = x;
                    _currentFigure.Body[i, 1] = y;

                    if (_fieldGameBoard[x, y] == null)
                    {
                        _fieldGameBoard[x, y] = new BoardPoint(_currentFigure.FColor);
                    }
                    else
                    {
                        result = false;
                        break;
                    }
                }
                else
                {
                    result = false;
                }
            }
            return result;
        }

        #endregion


        #region Control Methods

        public void Start()
        {
            Reset();
            _currentFigure = Initializer.GetNewFigure();
            _nextFigure = Initializer.GetNewFigure();
            CopyCurrentFigureToBoard();
            _timer.Start();
            if (_updateEvent != null)
            {
                _updateEvent();
            }
        }

        public void Stop()
        {
            _timer.Stop();
            GameOver();
        }

        public void Pause()
        {
            if (_timer.IsEnabled)
            {
                _timer.Stop();
            }
            else
            {
                _timer.Start();
            }
        }

        public void StepLeft()
        {
            if (_timer.IsEnabled)
            {
                if (CheckPermissionMoveLeft())
                {
                    _currentFigure.StepLeft();
                    MoveFigureLeft();
                }               
                if (_updateEvent != null)
                {
                    _updateEvent();
                }
            }           
        }

        public void StepRight()
        {
            if (_timer.IsEnabled)
            {
                if (CheckPermissionMoveRight())
                {
                    _currentFigure.StepRight();
                    MoveFigureRight();
                }
                if (_updateEvent != null)
                {
                    _updateEvent();
                }
            }           
        }
    
        public void NextStep()
        {
            Step(null, null);
        }

        public void Turn()
        {
            if (_currentFigure.Rotatable)
            {
                byte[,] turnedFigure = GetCoordinTurnedFigure();
                if (turnedFigure != null)
                {
                    Turning(turnedFigure);
                    _currentFigure.Body = (byte[,])turnedFigure.Clone();

                }
                if (_updateEvent != null)
                {
                    _updateEvent();
                }
            }           
        }


        #endregion


        #region MovingOfFigure


        private void MoveFigureDown()
        {
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                int x = _currentFigure.Body[i, 0];
                int y = _currentFigure.Body[i, 1];
                _fieldGameBoard[x, y - 1] = null;

            }
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                int x = _currentFigure.Body[i, 0];
                int y = _currentFigure.Body[i, 1];
                _fieldGameBoard[x, y] = new BoardPoint(_currentFigure.FColor);
            }       
        }


        private void MoveFigureLeft()
        {
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                int x = _currentFigure.Body[i, 0];
                int y = _currentFigure.Body[i, 1];
                _fieldGameBoard[x + 1, y] = null;

            }
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                int x = _currentFigure.Body[i, 0];
                int y = _currentFigure.Body[i, 1];
                _fieldGameBoard[x, y] = new BoardPoint(_currentFigure.FColor);
            }

            if (_soundStepEvent != null)
            {
                _soundStepEvent();
            }
        }


        private void MoveFigureRight()
        {
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                int x = _currentFigure.Body[i, 0];
                int y = _currentFigure.Body[i, 1];
                _fieldGameBoard[x - 1, y] = null;

            }
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                int x = _currentFigure.Body[i, 0];
                int y = _currentFigure.Body[i, 1];
                _fieldGameBoard[x, y] = new BoardPoint(_currentFigure.FColor);
            }

            if (_soundStepEvent != null)
            {
                _soundStepEvent();
            }
            
        }




        private void Turning(byte[,] turnedFigure)
        {
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                byte x = _currentFigure.Body[i, 0];
                byte y = _currentFigure.Body[i, 1];
                _fieldGameBoard[x, y] = null;
            }
            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                byte x = turnedFigure[i, 0];
                byte y = turnedFigure[i, 1];
                _fieldGameBoard[x, y] = new BoardPoint(_currentFigure.FColor);
            }

            if (_soundTurningEvent != null)
            {
                _soundTurningEvent();
            }
          
        }

        #endregion


        #region FindMaxMinPoints

        private List<Point> GetLowestFigurePoints()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < WidthHeighFigureField; i++)
            {
                byte x = _currentFigure.Body[i, 0];
                byte y = _currentFigure.Body[i, 1];
                if (points.Count == 0)
                {
                    points.Add(new Point(x, y));
                }
                else
                {
                    bool findFitColumn = false;
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (x == points[j].X)
                        {
                            findFitColumn = true;
                            if (y > points[j].Y)
                            {
                                points[j] = (new Point(x, y));
                            }
                        }
                    }

                    if (!findFitColumn)
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }

            return points;
        }



        private List<Point> GetRightMostPoints()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < WidthHeighFigureField; i++)
            {
                byte x = _currentFigure.Body[i, 0];
                byte y = _currentFigure.Body[i, 1];
                if (points.Count == 0)
                {
                    points.Add(new Point(x, y));
                }
                else
                {
                    bool findFitRows = false;
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (y == points[j].Y)
                        {
                            findFitRows = true;
                            if (x > points[j].X)
                            {
                                points[j] = (new Point(x, y));
                            }
                        }
                    }

                    if (!findFitRows)
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }

            return points;
        }


        private List<Point> GetLeftMostPoints()
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < WidthHeighFigureField; i++)
            {
                byte x = _currentFigure.Body[i, 0];
                byte y = _currentFigure.Body[i, 1];
                if (points.Count == 0)
                {
                    points.Add(new Point(x, y));
                }
                else
                {
                    bool findFitRows = false;
                    for (int j = 0; j < points.Count; j++)
                    {
                        if (y == points[j].Y)
                        {
                            findFitRows = true;
                            if (x < points[j].X)
                            {
                                points[j] = (new Point(x, y));
                            }
                        }
                    }

                    if (!findFitRows)
                    {
                        points.Add(new Point(x, y));
                    }
                }
            }

            return points;
        }

        #endregion


        #region CheckPermissionForMoving

        private bool CheckPermissionMoveDown()
        {
            bool result = true;
            List<Point> points = GetLowestFigurePoints();
            foreach (var point in points)
            {
                byte x = (byte)point.X;
                byte y = (byte)point.Y;

                if (y >= 19 || _fieldGameBoard[x, y + 1] != null)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }


        private bool CheckPermissionMoveLeft()
        {
            bool result = true;
            List<Point> points = GetLeftMostPoints();
            foreach (var point in points)
            {
                byte x = (byte)point.X;
                byte y = (byte)point.Y;

                if (x <= 0 || _fieldGameBoard[x - 1, y] != null)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private bool CheckPermissionMoveRight()
        {
            bool result = true;
            List<Point> points = GetRightMostPoints();
            foreach (var point in points)
            {
                byte x = (byte)point.X;
                byte y = (byte)point.Y;

                if (x >= 9 || _fieldGameBoard[x + 1, y] != null)
                {
                    result = false;
                    break;
                }
            }
            return result;
        }

        private byte[,] GetCoordinTurnedFigure()
        {
            bool permition = true;
            byte[,] result = null;
            byte[,] turnedFigure = _currentFigure.GetCoordTurnedFigure();

            for (int i = 0; i < numberOfFigurePoints; i++)
            {
                byte x = turnedFigure[i, 0];
                byte y = turnedFigure[i, 1];
                if ( x > 9 || y > 19)
                {
                    permition = false;
                    break;
                }
            }

            if (permition)
            {
                List<Point> points = new List<Point>();
                for (int i = 0; i < numberOfFigurePoints; i++)
                {
                    bool fitPoint = false;
                    byte xTurnedFig = turnedFigure[i, 0];
                    byte yTurnedFig = turnedFigure[i, 1];
                    for (int j = 0; j < numberOfFigurePoints; j++)
                    {
                        byte xCurrentFig = _currentFigure.Body[j, 0];
                        byte yCurrentFig = _currentFigure.Body[j, 1];
                        if (xCurrentFig == xTurnedFig && yCurrentFig == yTurnedFig)
                        {
                            fitPoint = true;
                            break;
                        }
                    }

                    if (!fitPoint)
                    {
                        points.Add(new Point(xTurnedFig, yTurnedFig));
                    }
                }
            }

            

            

            if (permition)
            {
                result = (byte[,])turnedFigure.Clone();
            }
            return result;
        }

        #endregion


        #region Prepairing showable information

        public IShowable GetJoinedData()
        {
            bool[][,] field =
            {
                new bool[_fieldGameBoard.GetLength(0), _fieldGameBoard.GetLength(1)],
                new bool[WidthHeighFigureField, WidthHeighFigureField]
            };
            byte[,] color = new byte[_fieldGameBoard.GetLength(0), _fieldGameBoard.GetLength(1)];


            FillShowField(field, color);

            return new ShowBoards(field, color, _nextFigure.FColor, _velocity, _level, _burnedLines, _score);
        }


        private void FillShowField(bool[][,] field, byte[,] color)
        {
            for (int i = 0; i < field[0].GetLength(1); i++)
            {
                for (int j = 0; j < field[0].GetLength(0); j++)
                {
                    if (_fieldGameBoard[j, i] != null)
                    {
                        field[0][j, i] = true;
                        color[j, i] = _fieldGameBoard[j, i].ColPoint;
                    }
                    else
                    {
                        field[0][j, i] = false;
                    }
                }
            }


            for (int i = 0; i < WidthHeighFigureField; i++)
            {
                for (int j = 0; j < WidthHeighFigureField; j++)
                {
                    field[1][j, i] = false;
                }
            }

            for (int i = 0; i < WidthHeighFigureField; i++)
            {
                if (_nextFigure != null)
                {
                    byte x = _nextFigure.Body[i, 0];
                    byte y = _nextFigure.Body[i, 1];
                    field[1][x, y] = true;
                }
            }
        }


        #endregion



        private void GameOver()
        {
            _timer.Stop();
            if (_gameOverEvent != null)
            {
                _gameOverEvent();
            }
              
        }

        private  void CheckAndBurnLine()
        {

            for (int i = 0; i < _height; i++)
            {
                bool lineBurn = true;
                for (int j = 0; j < _width; j++)
                {
                    if (_fieldGameBoard[j, i] == null)
                    {
                        lineBurn = false;
                        break;
                    }
                }
                if (!lineBurn) continue;
                BurnLine(i);
            }
        }

        private  void BurnLine(int line)
        {
            for (int i = line; i > 0; i--)
            {
                for (int j = 0; j < _width; j++)
                {
                    _fieldGameBoard[j, i] = _fieldGameBoard[j, i - 1];
                }
            }
            _score += 100;
            _burnedLines += 1;
            if (_soundBurnLine != null)
            {
                _soundBurnLine();
            }
        }

        private void LevelUp()
        {
           _level++;
           _velocity = Initializer.GetVelocity(_level);
           _timer.Interval = new TimeSpan(0, 0, 0, 0, (int)(600 / _velocity));
           ClearGameBoard();
            if (_level <= 9 && _level > 2) //filling of game board when game level is from 4 to 9
            {
                FillBoardByLevel();
            }

        }

        private void FillBoardByLevel()
        {
            Random rnd = new Random();
            byte[,] fillLevel = Initializer.GetLevelFilling(_level);
            // index of array Levels number 0 fit Level 4 of the game 
            for (int i = fillLevel.GetLength(0) - 1; i >= 0; i--)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (fillLevel[i, j] == 1)
                    {
                        //entering the points of the figure into the Field of the structure board
                        _fieldGameBoard[j, 19 - i] = new BoardPoint((byte)(rnd.Next(0, Initializer.NumberOfColors)));
                    }
                }
            }
        }

        private  bool CheckScoreLevelUp()
        {
            return (_score - _level * _limitScore >= _limitScore + _level * _limitScore) && _level < 9;
        }

        private void Reset()
        {
            _velocity = 0;
            ClearGameBoard();
            _currentFigure = null;
            _nextFigure = null;
            _level = 0;
            _burnedLines = 0;
            _score = 0;
        }

        private void ClearGameBoard()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _fieldGameBoard[j, i] = null;
                }
            }
        }


        private Figure _currentFigure;
        private Figure _nextFigure;
        private readonly BoardPoint[,] _fieldGameBoard;
        private readonly DispatcherTimer _timer;
        private float _velocity;
        private int _level;
        private int _burnedLines;
        private int _score;

        private Action _gameOverEvent;
        private Action _updateEvent;
        private Action _soundBurnLine;
        private Action _soundStepEvent;
        private Action _soundTurningEvent;

        private readonly byte _width;
        private readonly byte _height;
        private readonly int _limitScore;
        private const byte WidthHeighFigureField = 4;
        private readonly byte numberOfFigurePoints = 4;
    }
}

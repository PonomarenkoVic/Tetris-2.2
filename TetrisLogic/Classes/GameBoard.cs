using System;
using TetrisInterfaces;
using TetrisInterfaces.Enum;
using TetrisLogic.Figures;
using TetrisLogic.Interfaces;

namespace TetrisLogic.Classes
{
    

    public class GameBoard : ITetrisLogic
    {

        public GameBoard(int width, int height)
        {
            Width = width;
            Height = height;
            _field = new BoardPoint[_width, _height];      
            _level = 0;
            _burnedLines = 0;
            _score = 0;
        }

        


        #region Properties

        public BoardPoint[,] Field
        {
            get
            {
                return (BoardPoint[,])_field.Clone();
            }
            private set
            {
                _field = (BoardPoint[,])value.Clone();
            }
        }

        public int Width
        {
            get => _width;
            private set
            {
                if (value > 5 && value < 20)
                {
                    _width = value;
                }
                else
                {
                    throw new Exception("Not allowed range of gameboard size. Allowed size is (width = 5 - 20, height = 10 - 30)");
                }
            }
        }

        public int Height
        {
            get => _height;
            private set
            {
                if (value > 10 && value < 30)
                {
                    _height = value;
                }
                else
                {
                    throw new Exception("Not allowed range of gameboard size. Allowed size is (width = 5 - 20, height = 10 - 30)");
                }
            }
        }

        public event SoundT SoundEvent;
        public event ShowT UpdateEvent;
        public event Action GameOverEvent;
        public event VelocChange VelocityChangeEvent;

       

        #endregion





        #region Control Methods

        public void Start()
        {
            Reset();
            SetVelocity();
            _currentFigure = Initializer.GetFigure(this);
            _currentFigure.DeleteTopFreeLineAndCenter();
            _nextFigure = Initializer.GetFigure(this);
            Update();
        }     
        
        public void Move(Direction dir)
        {          
                if (dir == Direction.Down)
                { 
                    if ( _currentFigure.Move(dir))
                    {
                        Update();
                    }
                    else
                    {
                        CopyCurrentFigureToField();
                                            
                        CheckAndBurnLine();
                        if (CheckScoreLevelUp())
                        {
                            LevelUp();
                        }
                        ExchangeFigures();
                        if (!Logic.CheckFreeArea(_currentFigure, Field))
                        {
                            _currentFigure = null;
                            GameOverEvent?.Invoke();
                        }
                        else
                        {
                            Update();
                        }                      
                    }
                }
                else
                {
                    if (_currentFigure.Move(dir))
                    {
                        SoundEvent?.Invoke(this, new SoundEventArg(TSound.Stepping));
                        Update();
                    }
                }
        }   

        public void Turn()
        {
            if (_currentFigure is IRotatable fig)
            {               
                if (fig.Turn())
                {
                    SoundEvent?.Invoke(this, new SoundEventArg(TSound.Turning));
                }
                Update();
            }
        }

        public void Step()
        {         
            Move((Direction.Down)); 
        }


        #endregion


        private bool CopyCurrentFigureToField()
        {
            bool result = true;
            int[,] body = _currentFigure.Body;
            for (int i = 0; i < body.GetLength(0); i++)
            {
                int x = body[i, 0];
                int y = body[i, 1];
                if (_field[x, y] == null)
                {
                    _field[x, y] = new BoardPoint(_currentFigure.Color);
                }
                else
                {
                    result = false;
                    break;
                }              
            }
            return result;
        }

        private void ExchangeFigures()
        {
            _currentFigure = (Figure)_nextFigure.Clone();
            _currentFigure.DeleteTopFreeLineAndCenter();
            _nextFigure = Initializer.GetFigure(this);
        }

        private void Update()
        {
            if (UpdateEvent != null)
            {
                BoardPoint[,] nextFigure = new BoardPoint[Initializer.NumberOfFigurePoint, Initializer.NumberOfFigurePoint];
                for (int i = 0; i < Initializer.NumberOfFigurePoint; i++)
                {
                    nextFigure[_nextFigure.Body[i, 0], _nextFigure.Body[i, 1]] = new BoardPoint(_nextFigure.Color);
                }

                BoardPoint[,] board = (BoardPoint[,]) _field.Clone();
                if (_currentFigure != null)
                {
                    for (int i = 0; i < _currentFigure.Body.GetLength(0); i++)
                    {
                        int x = _currentFigure.Body[i, 0];
                        int y = _currentFigure.Body[i, 1];

                        board[x, y] = new BoardPoint(_currentFigure.Color);
                    }
                }
                

                UpdateEvent(this, new ShowEventArg(board, nextFigure, _level, _burnedLines, _score));
            }
        }

        private void SetVelocity()
        {
            VelocityChangeEvent?.Invoke(this, new VelocChangedEventArg(Initializer.GetVelocityByLevel(_level)));
        }

        private void Reset()
        {

            ClearGameBoard();
            _currentFigure = null;
            _nextFigure = null;
            _level = 0;
            _burnedLines = 0;
            _score = 0;
            SetVelocity();
        }

        private void ClearGameBoard()
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    _field[j, i] = null;
                }
            }
        }

        private bool CheckScoreLevelUp()
        {
            return (_score - _level * Initializer.LimitScore >= Initializer.LimitScore + _level * Initializer.LimitScore) && _level < 9;
        }

        private void LevelUp()
        {
            _level++;
            SetVelocity();
            ClearGameBoard();  
            Initializer.FillBoardFieldByLevel(_field, _level);           
        }

        private void CheckAndBurnLine()
        {

            for (int i = 0; i < _height; i++)
            {
                bool lineBurn = true;
                for (int j = 0; j < _width; j++)
                {
                    if (_field[j, i] == null)
                    {
                        lineBurn = false;
                        break;
                    }
                }
                if (!lineBurn) continue;
                BurnLine(i);
            }
        }

        private void BurnLine(int line)
        {
            for (int i = line; i > 0; i--)
            {
                for (int j = 0; j < _width; j++)
                {
                    _field[j, i] = _field[j, i - 1];
                }
            }
            _score += 100;
            _burnedLines += 1;
            SoundEvent?.Invoke(this, new SoundEventArg(TSound.Burning));
        }

        public bool SaveGame()
        {
            return Logic.SaveGame(Field, (int[,])_currentFigure.Body.Clone(), (int[,])_nextFigure.Body.Clone(), _level, _burnedLines, _score);
        }



 
        private Figure _currentFigure;
        private Figure _nextFigure;
        private int _width;
        private int _height;
        private BoardPoint[,] _field;        
        private int _level;
        private int _burnedLines;
        private int _score;    
    }
}

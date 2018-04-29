using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace ViewModel
{
    public class Property:INotifyPropertyChanged
    {
        


            public event PropertyChangedEventHandler PropertyChanged;

            public int Level
            {
                get
                {
                    return _level;
                }
                set
                {
                    _level = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Level"));
                    }
                }
            }

            public int BurnedLine
            {
                get
                {
                    return _burnedLine;
                }
                set
                {
                    _burnedLine = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("BurnedLine"));
                    }
                }
            }

            public int Score
            {
                get
                {
                    return _score;
                }
                set
                {
                    _score = value;
                    if (PropertyChanged != null)
                    {
                        PropertyChanged(this, new PropertyChangedEventArgs("Score"));
                    }
                }
            }

            //public Shape GameBoard
            //{
            //    get
            //    {
            //        return _gameBoard;
            //    }
            //    set
            //    {
            //        _gameBoard = value;
            //        if (PropertyChanged != null)
            //        {
            //            PropertyChanged(this, new PropertyChangedEventArgs("GameBoard"));
            //        }
            //    }
            //}

     
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                var handler = PropertyChanged;
                if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
            }


            private int _level;
            private int _burnedLine;
            private int _score;
            //private Shape _gameBoard;
        }

}

using System;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TetrisInterfaces;
using TetrisInterfaces.Enum;
using TetrisLogic.Classes;
using TetrisLogic.Figures;

namespace ViewModel
{
    public class ViewModel : ViewModelBase
    {



        public ViewModel(byte width, byte height, SqlConnectionStringBuilder connS)
        {
            _connString = connS;
            _model = new TetrisGameBoard(width, height, _connString );         
            _model.UpdateEvent += Update;
            _model.GameOverEvent += GameOver;
            _model.SoundEvent += Sound;
            _model.VelocityChangeEvent += VelocityChanged;

            _save = new Command(OnSave,parameter => true);
            _open = new Command(OnOpen, parameter => true);
            _start = new Command(OnStart, parameter => true);
            _keyDown = new Command(OnKeyDown, parameter => true);
            
        }

        public event ShowT UpdateBoard;
        public event SoundT SoundEvent;
        public event Action GameOverEvent;
        public event VelocChange VelChangeEvent;

        #region Properties


        public int Level
        {
            get => _level;
            set => SetProperty(ref _level, value, nameof(Level));
        }

        public int BurnedLine
        {
            get => _burnedLine;
            set => SetProperty(ref _burnedLine, value, nameof(BurnedLine));
        }


        public int Score
        {
            get => _score;
            set => SetProperty(ref _score, value, nameof(Score));
        }

        public float Velocity
        {
            get => _velocity;
            set => SetProperty(ref _velocity, value, nameof(Velocity));
        }

        public SoundT Sound
        {
            get => _sound;
            set => SetProperty(ref _sound, value, nameof(Sound));
        }

        public KeyEventArgs Key
        {
            get => _key;
            set => SetProperty(ref _key, value, nameof(Key));
        }

        public ICommand Save
        {
            get => _save;
            set => SetProperty(ref _save, value, nameof(Save));
        }

        public ICommand Open
        {
            get => _open;
            set => SetProperty(ref _open, value, nameof(Open));
        }


        public ICommand Start
        {
            get => _start;
            set => SetProperty(ref _start, value, nameof(Start));
        }


        public ICommand KeyDown
        {
            get => _keyDown;
            set => SetProperty(ref _keyDown, value, nameof(KeyDown));
        }

        #endregion




        #region Methods

        private void OnSave(object parameter)
        {
            throw new NotImplementedException();
        }

        private void OnKeyDown(object parameter)
        {
            switch (((string)parameter))
            {
                case "Down":
                    _model.Move(Direction.Down);
                    break;
                case "Left":
                    _model.Move(Direction.Left);
                    break;
                case "Right":
                    _model.Move(Direction.Right);
                    break;
                case "Space":
                    _model.Turn();
                    break;
            }
        }

        private void OnStart(object parameter)
        {
            _model.Start();
        }

        private void OnOpen(object parameter)
        {
            throw new NotImplementedException();
        }
        #endregion






        private void VelocityChanged(object obj, VelocChangedEventArg arg)
        {
            VelChangeEvent?.Invoke(obj, arg);
        }


       



        private void Sound1(object sender, SoundEventArg arg)
        {
            SoundEvent?.Invoke(sender, arg);
        }

        private void GameOver()
        {
            GameOverEvent?.Invoke();
        }


        public void Start1()
        {
            _model.Start();
        }
      
        private void Update(object sender, ShowEventArg arg)
        {         
            Level = arg.Level + 1;
            BurnedLine = arg.BurnedLine;
            Score = arg.Score;
            UpdateBoard?.Invoke(sender, arg);
        }   

        public void Move(Direction dir)
        {
            _model.Move(dir);
        }


        public void Turn()
        {
            _model.Turn();
        }

        public void Step()
        {
            _model.Step();
        }






        private readonly ITetrisLogic _model;
  

        private int _level;
        private int _burnedLine;
        private int _score;



        private ICommand _save;
        private ICommand _open;
        private ICommand _start;
        private ICommand _keyDown;
        private float _velocity;
        private SoundT _sound;
        private KeyEventArgs _key;
        private SqlConnectionStringBuilder _connString;

        public DataTable GetSavePoints()
        {
            return _model.GetSavePoints();
        }

        public void OpenSavePoint(int id, int level, int burnedLine, int score, int idField)
        {
           _model.Open(id, level, burnedLine, score, idField );
        }

        public void SaveGame()
        {
            _model.Save();
        }
    }
}

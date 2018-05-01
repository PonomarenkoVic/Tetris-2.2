using System;
using TetrisInterfaces;
using TetrisInterfaces.Enum;
using TetrisLogic.Figures;

namespace ViewModel
{
    public class ViewModel
    {
        public ViewModel(byte width, byte height)
        {
            _model = new GameBoard(width, height);
            _property = new Property();
            InitializeCommands();
            _model.UpdateEvent += Update;
            _model.GameOverEvent += GameOver;
            _model.SoundEvent += Sound;            
        }


        public event ShowT UpdateBoard;
        public event SoundT SoundEvent;
        public event Action GameOverEvent;

        public Property GetDataContext()
        {
            return _property;
        }


        private void Sound(object sender, SoundEventArg arg)
        {
            if (SoundEvent != null)
            {
                SoundEvent(sender, arg);
            }
        }

        private void GameOver()
        {
            if (GameOverEvent != null)
            {
                GameOverEvent();
            }
        }

        private void InitializeCommands()
        {
           
        }

        private bool CanStart()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _model.Start();
        }
      
        private void Update(object sender, ShowEventArg arg)
        {           
            _property.Level = arg.Level + 1;
            _property.BurnedLine = arg.BurnedLine;
            _property.Score = arg.Score;
            if (UpdateBoard != null)
            {
                UpdateBoard(sender, arg); 
            }
           
        }   

        public void Move(Direction dir)
        {
            _model.Move(dir);
        }


        public void Turn()
        {
            _model.Turn();
        }

        public void Pause()
        {
            _model.Pause();
        }

        public void Stop()
        {
            _model.Stop();
        }


        private readonly ITetrisLogic _model;
        private readonly Property _property;
        private readonly Command _command;
    }
}

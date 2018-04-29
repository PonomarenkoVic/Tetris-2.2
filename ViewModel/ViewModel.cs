using System;
using TetrisInterfaces;
using TetrisLogic;

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
            _model.SoundBurnLineEvent += SoundBurningLine;
            _model.SoundStepEvent += SoundStep;
            _model.SoundTurningEvent += SoundTurning;
        }

        private void SoundTurning()
        {
            if (SoundTurningEvent != null)
            {
                SoundTurningEvent();
            }
        }

        private void SoundStep()
        {
            if (SoundStepEvent != null)
            {
                SoundStepEvent();
            }
        }

        private void SoundBurningLine()
        {
            if (SoundBurnLineEvent != null)
            {
                SoundBurnLineEvent();
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

        public event Action UpdateBoard;
        public event Action GameOverEvent;
        public event Action SoundBurnLineEvent;
        public event Action SoundStepEvent;
        public event Action SoundTurningEvent;

        public IShowable GetData()
        {
            return _model.GetJoinedData();
        }
        public Property GetDataContext()
        {
            return _property;
        }

        private void Update()
        {
            IShowable data = _model.GetJoinedData();
            _property.Level = data.Level + 1;
            _property.BurnedLine = data.BurnedLines;
            _property.Score = data.Score;
            if (UpdateBoard != null)
            {
                UpdateBoard(); 
            }
           
        }



        private readonly ITetrisLogic _model;
        private readonly Property _property;
        private readonly Command _command;

        public void DownClick()
        {
            _model.NextStep();
        }

        public void LeftClick()
        {
           _model.StepLeft();
        }

        public void RightClick()
        {
            _model.StepRight();
        }

        public void SpaceClick()
        {
            _model.Turn();
        }

        public void PClick()
        {
            _model.Pause();
        }

        public void StopClick()
        {
            _model.Stop();
        }
    }
}

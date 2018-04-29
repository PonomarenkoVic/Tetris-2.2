using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TetrisInterfaces
{
    public interface ITetrisLogic
    {

        event Action GameOverEvent;
        event Action UpdateEvent;
        event Action SoundBurnLineEvent;
        event Action SoundStepEvent;
        event Action SoundTurningEvent;
        void Start();
        void Stop();
        void Pause();
        void StepLeft();
        void StepRight();
        void NextStep();
        void Turn();
        IShowable GetJoinedData();
    }
}

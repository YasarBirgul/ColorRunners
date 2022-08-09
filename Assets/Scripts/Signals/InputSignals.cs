using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class InputSignals : MonoSingleton<InputSignals>
    {
        public UnityAction onEnableInput = delegate {  };
        public UnityAction onDisableInput = delegate {  };
        public UnityAction onFirstTimeTouchTaken = delegate { };
        public UnityAction onInputTaken = delegate { };
        public UnityAction<RunnerGameInputParams> onRunnerInputDragged = delegate { };
        public UnityAction<IdleGameInputParams> onIdleInputDragged = delegate {  };
        public UnityAction onInputReleased = delegate { };
    }
}
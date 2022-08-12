using Datas.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Commands.Input
{ 
    public class RunnerGameInputUpdateCommand
    {
        public void RunnerInputUpdate(FloatingJoystick _joystick,InputData _data)
        {
            if (_joystick.Horizontal != 0)
            {
                InputSignals.Instance.onRunnerInputDragged?.Invoke(new RunnerGameInputParams()
                {
                    XValue = _joystick.Horizontal,
                    ClampValues = new Vector2(_data.ClampSides.x,_data.ClampSides.y),
                });
                InputSignals.Instance.onInputTaken?.Invoke();
            }
            else
            {
                InputSignals.Instance.onInputReleased?.Invoke();
            }
        }
    }
}
using Datas.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Commands.Input
{ 
    public class RunnerGameInputUpdateCommand
    {
        public void RunnerInputUpdate(FloatingJoystick joystick,InputData data)
        {
            if (joystick.Horizontal != 0)
            {
                InputSignals.Instance.onRunnerInputDragged?.Invoke(new RunnerGameInputParams()
                {
                    XValue = joystick.Horizontal,
                    ClampValues = new Vector2(data.ClampSides.x,data.ClampSides.y),
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
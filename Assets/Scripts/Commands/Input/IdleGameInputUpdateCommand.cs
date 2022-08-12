using Keys;
using Signals;

namespace Commands.Input
{
    public class IdleGameInputUpdateCommand
    {
        public void IdleInputUpdate(FloatingJoystick _joystick)
        {
            if (_joystick.Horizontal != 0 || _joystick.Vertical != 0)
            {
                InputSignals.Instance.onIdleInputDragged?.Invoke(new IdleGameInputParams()
                {
                    XValue = _joystick.Horizontal,
                    ZValue = _joystick.Vertical,
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
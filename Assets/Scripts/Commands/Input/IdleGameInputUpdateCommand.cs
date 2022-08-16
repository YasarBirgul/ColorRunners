using Keys;
using Signals;

namespace Commands.Input
{
    public class IdleGameInputUpdateCommand
    {
        public void IdleInputUpdate(FloatingJoystick joystick)
        {
            if (joystick.Horizontal != 0 || joystick.Vertical != 0)
            {
                InputSignals.Instance.onIdleInputDragged?.Invoke(new IdleGameInputParams()
                {
                    XValue = joystick.Horizontal,
                    ZValue = joystick.Vertical,
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
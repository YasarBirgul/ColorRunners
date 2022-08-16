using UnityEngine;
using UnityEngine.UI;

namespace Commands.UI
{
    public class JoyStickStateCommand
    {
        public void JoystickUIStateChanger(GameStates state,GameObject joystickOuterCircle, GameObject joystickInnerCircle)
        {
            if (state == GameStates.Idle)
            {
                joystickInnerCircle.GetComponent<Image>().enabled = true;
                joystickOuterCircle.GetComponent<Image>().enabled = true;
            }
            else
            {
                joystickInnerCircle.GetComponent<Image>().enabled = false;
                joystickOuterCircle.GetComponent<Image>().enabled = false;
            }
        }
    }
}
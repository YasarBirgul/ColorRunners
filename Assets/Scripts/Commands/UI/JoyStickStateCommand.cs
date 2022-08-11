using UnityEngine;
using UnityEngine.UI;

namespace Commands.UI
{
    public class JoyStickStateCommand
    {
        public void JoystickUIStateChanger(GameStates state,GameObject JoystickOuterCircle, GameObject JoystickInnerCircle)
        {
            if (state == GameStates.Idle)
            {
                JoystickInnerCircle.GetComponent<Image>().enabled = true;
                JoystickOuterCircle.GetComponent<Image>().enabled = true;
            }
            else
            {
                JoystickInnerCircle.GetComponent<Image>().enabled = true;
                JoystickOuterCircle.GetComponent<Image>().enabled = true;
            }
        }
    }
}
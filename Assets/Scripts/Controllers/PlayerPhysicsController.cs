using Enums;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{ 
    public class PlayerPhysicsController : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private PlayerManager playerManager;
        
        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("IdleGameInvoker"))
            {
                playerManager.ChangeState(GameStates.Idle);
                CameraSignals.Instance.onSetCameraState(CameraStatesType.Idle);
            }
            if (other.CompareTag("ColorArea"))
            {
                StackSignals.Instance.onTurrentGroundControll?.Invoke(other.gameObject);
            }
            if (other.CompareTag("Gate"))
            {
                playerManager.SendGateColorData(other.GetComponent<GateManager>().Color);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DroneArea"))
            {
                playerManager.StopVerticalMovement();
                CameraSignals.Instance.onEnterMiniGame?.Invoke();
            }
        }
    }
}
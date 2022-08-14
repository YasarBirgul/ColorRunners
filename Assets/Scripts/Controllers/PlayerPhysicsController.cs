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
          //  if (other.CompareTag("Ground"))
          //  {
          //      CollectableSignals.Instance.onEnterGroundCheck?.Invoke(gameObject);
          //  }
            if (other.CompareTag("Changer"))
            {
                playerManager.SendGateColorData(other.GetComponent<GateCommand>().color);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Ground"))
            {
                CollectableSignals.Instance.onExitGroundCheck?.Invoke(gameObject);
            }
        }
    }
}
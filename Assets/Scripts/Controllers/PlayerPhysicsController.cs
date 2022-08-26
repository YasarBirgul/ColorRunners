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
            if (other.CompareTag("Collected"))
            {
                playerManager.OnIncreaseScale();
            }
            if (other.CompareTag("IdleGameInvoker"))
            {
                playerManager.ChangeState(GameStates.Roullette);
                playerManager.ActivateAllMovement(false);
            }
            if (other.CompareTag("ColorArea"))
            {
                StackSignals.Instance.onTurrentGroundControll?.Invoke(other.gameObject);
            }
            if (other.CompareTag("Gate"))
            {
                playerManager.SendGateColorData(other.GetComponent<GateManager>().Color);
            }
            if (other.CompareTag("AfterGround"))
            {
                playerManager.OnEnableFinalCollider(other.gameObject);
            }
            if (other.CompareTag("DroneColorArea"))
            {
                if (playerManager.PlayerColorType == other.GetComponent<DroneAreaColorController>().ColorType)
                {
                    other.GetComponent<DroneAreaColorController>().DroneAreaMatchType = CollectableMatchType.Match;
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("DroneArea"))
            {
                playerManager.StopVerticalMovement();
            }
        }
    }
}
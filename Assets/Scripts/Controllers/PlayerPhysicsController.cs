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
        
        #region Private Variables
        
        #endregion
        
        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("IdleGameInvoker"))
            {
                playerManager.ChangeState(GameStates.Roullette);
                playerManager.ActivateAllMovement(false);
            }
            else if (other.CompareTag("ColorArea"))
            {
                StackSignals.Instance.onTurrentGroundControll?.Invoke(other.gameObject);
            }
            else if (other.CompareTag("Gate"))
            {
                playerManager.SendGateColorData(other.GetComponent<GateManager>().Color);
            }
            else if (other.CompareTag("AfterGround"))
            {
                playerManager.OnEnableFinalCollider(other.gameObject);
            }
            else if (other.CompareTag("DroneColorArea"))
            {
                if (playerManager.PlayerColorType == other.GetComponent<DroneAreaColorController>().ColorType)
                {
                    other.GetComponent<DroneAreaColorController>().DroneAreaMatchType = CollectableMatchType.Match;
                }
            }
            else if (other.CompareTag("Collectable"))
            {
                if (playerManager.CurrentGameState == GameStates.Idle)
                {
                    var Collectable = other.transform.parent;
                    Collectable.gameObject.SetActive(false);
                    ScoreSignals.Instance.onIncreaseScore?.Invoke();
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
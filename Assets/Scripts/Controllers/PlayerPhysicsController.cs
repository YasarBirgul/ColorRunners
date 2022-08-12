using System;
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
            if(other.CompareTag("Collectable"))
            {
                StackSignals.Instance.onIncreaseStack?.Invoke(other.GetComponentInParent<CollectableManager>().gameObject);
            }
            
            if (other.CompareTag("IdleGameInvoker"))
            {
                CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            }
            if (other.CompareTag("ColorArea"))
            {
                StackSignals.Instance.onTurrentGroundControll?.Invoke(other.gameObject); //ground

            }//collectablefizikkontrollerde olsa sürekli çalısacagı ıcın playere aldım. her player yenı groundcolorchecke gırdıgınde calısacak.

            if (other.CompareTag("Ground"))
            {
                CollectableSignals.Instance.onEnterGroundCheck?.Invoke(gameObject);
            }

            if (other.CompareTag("DroneArea"))
            {
                playerManager.Invoke("EnteredDroneArea",.5f);
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
using System;
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
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Collectable"))
            {
                StackSignals.Instance.onIncreaseStack?.Invoke(other.gameObject);
            }
            
            if (other.CompareTag("IdleGameInvoker"))
            {
                CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
            }
            if (other.CompareTag("GroundColorCheck"))
            {
                StackSignals.Instance.onTurrentGroundControll?.Invoke(other.gameObject); //ground

            }//collectablefizikkontrollerde olsa sürekli çalısacagı ıcın playere aldım. her player yenı groundcolorchecke gırdıgınde calısacak.

            if (other.CompareTag("Ground"))
            {
                CollectableSignals.Instance.onEnterGroundCheck?.Invoke(gameObject);
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
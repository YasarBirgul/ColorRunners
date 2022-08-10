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
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("Collectable"))
            {
                CollectableSignals.Instance.onMansCollection?.Invoke(other.gameObject);
            }
        }

    }
}
using Signals;
using UnityEngine;

namespace Controllers
{
    public class DroneAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Seriazible Variables

        #endregion

        #region Private Variables
        
        #endregion

        #endregion
        
        private void OnEnable()
        {
            SubscribeEvents();
        } 
        #region Event Subscription
        private void SubscribeEvents()
        {
            DroneAreaSignals.Instance.onColliderDisable += OnColliderDisable;
            DroneAreaSignals.Instance.onEnableFinalCollider += OnFinalAreaCollider;
        } 
        private void UnsubscribeEvents()
        {
            DroneAreaSignals.Instance.onColliderDisable -= OnColliderDisable;
            DroneAreaSignals.Instance.onEnableFinalCollider -= OnFinalAreaCollider;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnColliderDisable()
        {
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        } 
        private void OnFinalAreaCollider()
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}


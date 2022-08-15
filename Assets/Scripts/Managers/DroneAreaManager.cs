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

        [SerializeField] private GameObject droneAreaCollectableHolder;
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
            DroneAreaSignals.Instance.onDroneAreaEnter += OnSetDroneAreaHolder; 
            // DroneAreaSignals.Instance.onDroneAreaCollectablesDeath += OnDroneAreaCollectablesDeath;
        } 
        private void UnsubscribeEvents()
        {
            DroneAreaSignals.Instance.onDroneAreaEnter -= OnSetDroneAreaHolder;
          //  DroneAreaSignals.Instance.onDroneAreaCollectablesDeath -= OnDroneAreaCollectablesDeath;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnSetDroneAreaHolder(GameObject gameObject)
        {
            gameObject.transform.SetParent(droneAreaCollectableHolder.transform);
        } 
        private void OnDroneAreaCollectablesDeath()
        {
            
        }
    }
}


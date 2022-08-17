using System.Threading.Tasks;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class DroneAreaManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        
        #endregion

        #region Seriazible Variables

        [SerializeField] private GameObject dronePrefab;
        
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
        private async void OnColliderDisable()
        {
            dronePrefab.SetActive(true);
            await Task.Delay(2000);
            transform.GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(0).GetComponent<BoxCollider>().enabled = false;
            transform.GetChild(1).GetComponent<BoxCollider>().enabled = false;
        } 
        private async void OnFinalAreaCollider()
        {
            await Task.Delay(3000);
            transform.GetChild(2).gameObject.SetActive(true);
        }
    }
}


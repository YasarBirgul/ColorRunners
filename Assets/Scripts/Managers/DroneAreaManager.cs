using System.Threading.Tasks;
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

        [SerializeField] private GameObject dronePrefab;
        [SerializeField] private GameObject colorArea1st;
        [SerializeField] private GameObject colorArea2nd;
        [SerializeField] private GameObject ColliderOnExitAreaGeneral;
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
            colorArea1st.GetComponent<BoxCollider>().enabled = false;
            colorArea2nd.GetComponent<BoxCollider>().enabled = false;
        } 
        private async void OnFinalAreaCollider()
        {
            await Task.Delay(3000);
            ColliderOnExitAreaGeneral.SetActive(true);
            colorArea1st.GetComponent<DroneAreaColorController>().Scale();
            colorArea2nd.GetComponent<DroneAreaColorController>().Scale();
            await Task.Delay(2000);
            ColliderOnExitAreaGeneral.SetActive(false);
        }
    }
}


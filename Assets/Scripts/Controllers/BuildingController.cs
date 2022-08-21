using TMPro;
using UnityEngine;

namespace Controllers
{
    public class BuildingController
    {
        
        
        #region Self Variables
    
        #region Public Variables
         
        public int BuildingAdressId;
        public int PayedAmount;
        public int MarketPrice;
        public float Saturation;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro MarketPriceText;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion

        #region Event Subscription
        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
           
        }

        private void UnsubscribeEvents()
        {

        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
    }
}
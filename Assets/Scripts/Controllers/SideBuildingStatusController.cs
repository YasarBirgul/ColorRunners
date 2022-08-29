using Managers;
using TMPro;
using UnityEngine;

namespace Controllers
{
    public class SideBuildingStatusController : MonoBehaviour
    {
        #region SelfVariables
    
        #region Public Variables
          
        public int PayedAmount;
        public int MarketPrice;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro marketPriceText;
        [SerializeField] private BuildingManager _manager;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion

        private void SetRequiredAmountToText()
        {
            marketPriceText.text = $"{_manager.buildingsData.SideObject.PayedAmount}/{_manager.buildingsData.SideObject.MarketPrice}";
        }
        public void UpdatePayedAmountText(int payedAmount)
        {
            PayedAmount = payedAmount;
            SetRequiredAmountToText();
        }
    }
}
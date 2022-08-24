using TMPro;
using UnityEngine;

namespace Controllers
{
    public class BuildingMarketStatusController : MonoBehaviour
    { 
        #region SelfVariables
    
        #region Public Variables
          
        public int PayedAmount;
        public int MarketPrice;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private TextMeshPro marketPriceText;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion

        private void SetRequiredAmountToText()
        {
            marketPriceText.text = $"(PayedAmount)/(MarketPrice)";
        }

        public void UpdatePayedAmountText(int payedAmount)
        {
            PayedAmount = payedAmount;
            SetRequiredAmountToText();
        }
        
    }
}
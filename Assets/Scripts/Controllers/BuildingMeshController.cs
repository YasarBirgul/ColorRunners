using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingMeshController : MonoBehaviour
    {
        #region SelfVariables
    
        #region Public Variables
          
        public float Saturation;

        #endregion

        #region Serialized Variables

        [SerializeField] private BuildingManager manager;

        #endregion

        #region Private Variables

        #endregion

        #endregion

        public float CalculateSaturation()
        {
            Saturation = (manager.PayedAmount / manager.MarketPrice);
            return Saturation;
        }
        
        
    }
}
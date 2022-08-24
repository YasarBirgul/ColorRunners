using Controllers;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class BuildingManager : MonoBehaviour
    { 
        #region Self Variables

        #region Public Variables
        
        public int buildingAdressId;
        public IdleLevelStateType IdleLevelStateType;
        public int PayedAmount;
        public float Saturation;
        public int MarketPrice;
       
        #endregion

        #region Private Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;

        #endregion

        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BuildingSignals.Instance.onDataReadyToUse += OnSetDataToControllers;
        } 
        private void UnsubscribeEvents()
        {
            BuildingSignals.Instance.onDataReadyToUse -= OnSetDataToControllers;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnSetDataToControllers()
        {
          //  SetDataToBuildingsMeshController();
            SetDataToBuildingsMarketStatusController();
        }

        private void SetDataToBuildingsMeshController()
        {
            buildingMeshController.Saturation = Saturation;
        }
        private void SetDataToBuildingsMarketStatusController()
        {
           buildingMarketStatusController.MarketPrice = MarketPrice;
           buildingMarketStatusController.UpdatePayedAmountText(PayedAmount);
           buildingMarketStatusController.PayedAmount = PayedAmount;
         //  UpdateSaturation();
        }

        public void UpdatePayedAmount()
        {
            PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(PayedAmount);
          //  UpdateSaturation();
        }
      // private void UpdateSaturation()
      // {
      //     Saturation = buildingMeshController.CalculateSaturation();
      // }

        public void UpdateBuildingStatus(IdleLevelStateType idleLevelState)
        {
            IdleLevelStateType = idleLevelState;
            BuildingSignals.Instance.onBuildingsCompleted.Invoke(buildingAdressId);
        }
    }
}
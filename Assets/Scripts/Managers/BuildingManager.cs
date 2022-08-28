using System.Collections.Generic;
using System.Linq;
using Controllers;
using Datas.ValueObject;
using Enums;
using Extentions;
using Signals;
using UnityEngine;
using UnityEngine.UIElements;

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
        public SideObjectData SideObjectData;
        // public IdleLevelStateType SideIdleLevelStateType;
        public int SidePayedAmount;
       // public float SideSaturation;
       // public int SideMarketPrice;
       // 
        #endregion

        #region Private Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;
        [SerializeField] private SideBuildingStatusController SidebuildingMarketStatusController;
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
            SidebuildingMarketStatusController.MarketPrice = SideObjectData.MarketPrice;
            SidebuildingMarketStatusController.UpdatePayedAmountText(SidePayedAmount);
            SidebuildingMarketStatusController.PayedAmount = SideObjectData.PayedAmount;

        }
        
        public void UpdatePayedAmount()
        {
            PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(PayedAmount);
            //  UpdateSaturation();
        }

        public void UpdateSidePayedAmount()
        {
            SidePayedAmount++;
            SidebuildingMarketStatusController.UpdatePayedAmountText(SidePayedAmount);
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
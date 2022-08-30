using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;
using Abstract;

namespace Managers
{
    public class BuildingManager : MonoBehaviour,ISaveable
    { 
        #region Self Variables

        #region Public Variables
        
        public BuildingsData BuildingsData;
        public int BuildingAddressID;
        public int IdleLevelId;
        
        #endregion

        #region Private Variables

        private Material _material;
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private SideBuildingMeshController sideBuildingMeshController;
        [SerializeField] private SideBuildingStatusController sideBuildingStatusController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;
        [SerializeField] private GameObject sideObject;

        #endregion

        #endregion
        private BuildingsData GetBuildingsData()
        {
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[IdleLevelId].BuildingsDatas[BuildingAddressID];
        }
        private void Awake()
        {
            GetIdleLevelID();
            if (!ES3.FileExists($"IdleBuildingDataKey{BuildingAddressID}.es3"))
            {
                if (!ES3.KeyExists("IdleBuildingDataKey"))
                {
                    BuildingsData = GetBuildingsData();
                    Save(BuildingAddressID);
                }
            }
            Load(BuildingAddressID);
            CheckBuildingScoreStatus(BuildingsData.idleLevelState);
            if (BuildingsData.IsDepended && BuildingsData.idleLevelState == IdleLevelStateType.Completed)
            {
                CheckSideBuildingScoreStatus(BuildingsData.SideObject.IdleLevelStateType);
            }
            SetDataToControllers();
        }
        private void GetIdleLevelID()
        {
            IdleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicatiponQuit += OnSave;
            CoreGameSignals.Instance.onGamePause += OnSave;
            LevelSignals.Instance.onNextLevel += OnSave;
            LevelSignals.Instance.onLevelInitialize += OnLoad;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onApplicatiponQuit -= OnSave;
            CoreGameSignals.Instance.onGamePause -= OnSave;
            LevelSignals.Instance.onNextLevel -= OnSave;
            LevelSignals.Instance.onLevelInitialize -= OnLoad;;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnSave()
        {
            Save(BuildingAddressID);
            SetDataToControllers();
        }
        private void OnLoad()
        {
            Load(BuildingAddressID);
            SetDataToControllers();
        }
        public void UpdatePayedAmount()
        { 
            var payedAmount = BuildingsData.PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(BuildingsData.PayedAmount);
            UpdateSaturation();
        }

        public void UpdateSidePayedAmount()
        {
            var SidePayedAmount = BuildingsData.SideObject.PayedAmount++;
            sideBuildingStatusController.UpdatePayedAmountText(BuildingsData.SideObject.PayedAmount);
            UpdateSideBuildingSaturation();
        }

        private void UpdateSaturation()
        {   
            buildingMeshController.CalculateSaturation();
        } 
        private void UpdateSideBuildingSaturation()
        {
            sideBuildingMeshController.CalculateSaturation();
        }
        private void SetDataToControllers() 
        {
            buildingMarketStatusController.UpdatePayedAmountText(BuildingsData.PayedAmount);
            buildingMeshController.Saturation = BuildingsData.Saturation;
            UpdateSaturation();
            
            if (BuildingsData.IsDepended)
            {    
                sideBuildingStatusController.UpdatePayedAmountText(BuildingsData.SideObject.PayedAmount);
                sideBuildingMeshController.Saturation = BuildingsData.SideObject.Saturation;
                UpdateSideBuildingSaturation();
            }
        }
        public void CheckBuildingScoreStatus(IdleLevelStateType idleLevelStateType)
        {
            if (idleLevelStateType == IdleLevelStateType.Completed)
            {
                buildingMarketStatusController.gameObject.SetActive(false);
            }
            else
            {
                buildingMarketStatusController.gameObject.SetActive(true);
            }
        } 
        public void CheckSideBuildingScoreStatus(IdleLevelStateType idleLevelStateType)
        {
            if (idleLevelStateType == IdleLevelStateType.Completed)
            {
                sideBuildingStatusController.gameObject.SetActive(false);
            }
            else
            {
                sideBuildingStatusController.gameObject.SetActive(true);
            }
        }
        public void UpdateBuildingStatus(IdleLevelStateType idleLevelState)
        {
            BuildingsData.idleLevelState = idleLevelState;
            if (!BuildingsData.IsDepended)
            {
                BuildingSignals.Instance.onBuildingsCompleted.Invoke(BuildingsData.BuildingAdressId);
            }
        }

        public void UpdateSideBuildingStatus(IdleLevelStateType idleLevelState)
        {
            BuildingsData.SideObject.IdleLevelStateType = idleLevelState;
            BuildingSignals.Instance.onSideBuildingsCompleted.Invoke(BuildingsData.SideObject.BuildingAddressId);
        }
        public void OpenSideObject()
        { 
            sideObject.SetActive(true);
          //buildingMarketStatusController.gameObject.SetActive(false);
        } 
        public void Save(int uniqueId)
        {
            BuildingsData  = new BuildingsData(BuildingsData.IsDepended,
            BuildingsData.SideObject,
            BuildingAddressID,
            BuildingsData.BuildingMarketPrice,
            BuildingsData.PayedAmount,
            BuildingsData.Saturation,
            BuildingsData.idleLevelState);
            SaveSignals.Instance.onSaveBuildingsData.Invoke(BuildingsData,uniqueId);
        }
        public void Load(int uniqueId)
        { 
            BuildingsData _buildingsData = SaveSignals.Instance.onLoadBuildingsData.Invoke(BuildingsData.Key, uniqueId);
            BuildingsData.SideObject = _buildingsData.SideObject;
            BuildingsData.Saturation = _buildingsData.Saturation;
            BuildingsData.PayedAmount = _buildingsData.PayedAmount;
            BuildingsData.idleLevelState = _buildingsData.idleLevelState;
            BuildingsData.BuildingMarketPrice = _buildingsData.BuildingMarketPrice;
            BuildingsData.IsDepended = _buildingsData.IsDepended;
        }
    }
}
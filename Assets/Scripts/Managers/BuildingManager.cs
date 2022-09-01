using System;
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
        
        public BuildingsData buildingsData;
        public int BuildingAddressID;
        
        #endregion

        #region Private Variables
        private int _idleLevelId;
        private Material _material;
        private string _stringUniqueID;
        private int _uniqueID;
        private int _one = 1;
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
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId].BuildingsDatas[BuildingAddressID];
        }

        private void GetIdleLevelData()
        {
            _idleLevelId = LevelSignals.Instance.onGetIdleLevelID.Invoke();
        }
        private void Start()
        {
            GetIdleLevelData();
            buildingsData = GetBuildingsData();
            _stringUniqueID = _one.ToString() + _idleLevelId.ToString()+BuildingAddressID.ToString(); 
            int.TryParse(_stringUniqueID, out _uniqueID);
            if (!ES3.FileExists($"IdleBuildingDataKey{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("IdleBuildingDataKey"))
                {
                    buildingsData = GetBuildingsData();
                    Save(_uniqueID);
                }
            }
            Load(_uniqueID);
            CheckBuildingScoreStatus(buildingsData.idleLevelState);
            if (buildingsData.IsDepended && buildingsData.idleLevelState == IdleLevelStateType.Completed)
            {
                CheckSideBuildingScoreStatus(buildingsData.SideObject.IdleLevelStateType);
            }
            SetDataToControllers();
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
            Save(_uniqueID);
            SetDataToControllers();
        }
        private void OnLoad()
        {
            Load(_uniqueID);
            SetDataToControllers();
        }
        public void UpdatePayedAmount()
        { 
            var payedAmount = buildingsData.PayedAmount++;
            buildingMarketStatusController.UpdatePayedAmountText(buildingsData.PayedAmount);
            UpdateSaturation();
        }

        public void UpdateSidePayedAmount()
        {
            var SidePayedAmount = buildingsData.SideObject.PayedAmount++;
            sideBuildingStatusController.UpdatePayedAmountText(buildingsData.SideObject.PayedAmount);
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
            buildingMarketStatusController.UpdatePayedAmountText(buildingsData.PayedAmount);
            buildingMeshController.Saturation = buildingsData.Saturation;
            UpdateSaturation();
            
            if (buildingsData.IsDepended)
            {    
                sideBuildingStatusController.UpdatePayedAmountText(buildingsData.SideObject.PayedAmount);
                sideBuildingMeshController.Saturation = buildingsData.SideObject.Saturation;
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
            buildingsData.idleLevelState = idleLevelState;
            if (!buildingsData.IsDepended)
            {
                BuildingSignals.Instance.onBuildingsCompleted.Invoke(buildingsData.BuildingAdressId);
            }
        }

        public void UpdateSideBuildingStatus(IdleLevelStateType idleLevelState)
        {
            buildingsData.SideObject.IdleLevelStateType = idleLevelState;
            BuildingSignals.Instance.onSideBuildingsCompleted.Invoke(buildingsData.SideObject.BuildingAddressId);
        }
        public void OpenSideObject()
        { 
            sideObject.SetActive(true);
          //buildingMarketStatusController.gameObject.SetActive(false);
        } 
        public void Save(int uniqueId)
        {
            buildingsData  = new BuildingsData(buildingsData.IsDepended,
            buildingsData.SideObject,
            BuildingAddressID,
            buildingsData.BuildingMarketPrice,
            buildingsData.PayedAmount,
            buildingsData.Saturation,
            buildingsData.idleLevelState);
            SaveSignals.Instance.onSaveBuildingsData.Invoke(buildingsData,uniqueId);
        }
        public void Load(int uniqueId)
        { 
            BuildingsData _buildingsData = SaveSignals.Instance.onLoadBuildingsData.Invoke(buildingsData.Key, uniqueId);
            buildingsData.SideObject = _buildingsData.SideObject;
            buildingsData.Saturation = _buildingsData.Saturation;
            buildingsData.PayedAmount = _buildingsData.PayedAmount;
            buildingsData.idleLevelState = _buildingsData.idleLevelState;
            buildingsData.BuildingMarketPrice = _buildingsData.BuildingMarketPrice;
            buildingsData.IsDepended = _buildingsData.IsDepended;
        }
    }
}
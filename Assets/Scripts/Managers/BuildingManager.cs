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
        public int _idleLevelId;
        
        #endregion

        #region Private Variables

        private Material material;
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private BuildingMarketStatusController buildingMarketStatusController;
        [SerializeField] private BuildingMeshController buildingMeshController;
        [SerializeField] private SideBuildingMeshController sideBuildingMeshController;
        [SerializeField] private SideBuildingStatusController sideBuildingStatusController;
        [SerializeField] private BuildingPhysicsController buildingPhysicsController;
        [SerializeField] private BuildingScorePhysicsController buildingScorePhysicsController;
        [SerializeField] private GameObject SideObject;
        [SerializeField] private SideObjectData sideObjectData;
        #endregion

        #endregion
        private BuildingsData GetBuildingsData()
        {
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId].BuildingsDatas[BuildingAddressID];
        }
        private void Awake()
        {
            GetIdleLevelID();
            if (!ES3.FileExists($"IdleBuildingDataKey{BuildingAddressID}.es3"))
            {
                if (!ES3.KeyExists("IdleBuildingDataKey"))
                {   
                    Debug.Log("Key does not exist!");
                    buildingsData = GetBuildingsData();
                    Save(BuildingAddressID);
                }
            }
            Debug.Log("Key Exist!");
            Load(BuildingAddressID);
            SetDataToControllers();
        }
        private void GetIdleLevelID()
        {
            _idleLevelId = CoreGameSignals.Instance.onGetIdleLevelID.Invoke();
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
        public void UpdateBuildingStatus(IdleLevelStateType idleLevelState)
        {
            buildingsData.idleLevelState = idleLevelState;
            BuildingSignals.Instance.onBuildingsCompleted.Invoke(buildingsData.BuildingAdressId);
        }

        public void UpdateSideBuildingStatus(IdleLevelStateType idleLevelState)
        {
            buildingsData.SideObject.IdleLevelStateType = idleLevelState;
            BuildingSignals.Instance.onBuildingsCompleted.Invoke(buildingsData.SideObject.BuildingAddressId);
        }
        public void OpenSideObject()
        {
            SideObject.SetActive(true);
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
            SaveSignals.Instance.onSaveIdleData.Invoke(buildingsData,uniqueId);
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
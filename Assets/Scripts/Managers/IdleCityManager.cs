using System.Collections.Generic;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class IdleCityManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("BuildingsData")] public IdleLevelData IdleLevelData;

        public List<GameObject> Buildings = new List<GameObject>();

        public List<BuildingManager> BuildingManagers = new List<BuildingManager>();
        
        public List<Transform> BuildingsTransforms = new List<Transform>();

        #endregion

        #region Private Variables
        
        private int _idleLevelId;

        #endregion

        #region Serialized Variables
        
        #endregion

        #endregion
        private IdleLevelData OnGetCityData()=> 
            Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId];  
        private void GetIdleLevelData()
        {
            _idleLevelId = LevelSignals.Instance.onGetIdleLevelID.Invoke();
        } 
        private void Start()
        { 
            GetIdleLevelData();
            if (!ES3.FileExists("IdleLevelProgress/IdleLevelProcessData.es3"))
            {
                IdleLevelData = OnGetCityData();
                SaveCityData(IdleLevelData);
            } 
             LoadCityData(IdleLevelData);
              SetDataToBuildingManagers();
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGamePause += OnSaveCityData;
            CoreGameSignals.Instance.onApplicatiponQuit += OnSaveCityData;
            BuildingSignals.Instance.onBuildingsCompleted += OnSetBuildingStatus;
            LevelSignals.Instance.onLevelInitialize += OnGetBuildingsDataFromBuildingManager;
            CoreGameSignals.Instance.onApplicatiponQuit += OnGetBuildingsDataFromBuildingManager;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGamePause -= OnSaveCityData;
            CoreGameSignals.Instance.onApplicatiponQuit -= OnSaveCityData;
            BuildingSignals.Instance.onBuildingsCompleted -= OnSetBuildingStatus;
            LevelSignals.Instance.onLevelInitialize -= OnGetBuildingsDataFromBuildingManager;
            CoreGameSignals.Instance.onApplicatiponQuit -= OnGetBuildingsDataFromBuildingManager;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
      
        private void OnSaveCityData()
        {
            SaveCityData(IdleLevelData);
        }


        private void SetDataToBuildingManagers()
        {
            for (int i = 0; i < Buildings.Count; i++)
            {
                IdleLevelData.BuildingsDatas[i].AddressId = i;
                BuildingManagers[i].buildingAdressId = i;
                Buildings[i].transform.position = BuildingsTransforms[i].transform.position;
                BuildingManagers[i].MarketPrice = IdleLevelData.BuildingsDatas[i].buildingMarketPrice;
                BuildingManagers[i].PayedAmount = IdleLevelData.BuildingsDatas[i].PayedAmount;
                BuildingManagers[i].Saturation = IdleLevelData.BuildingsDatas[i].Saturation;

                if (!IdleLevelData.BuildingsDatas[i].isDepended || IdleLevelData.BuildingsDatas[i].IdleLevelStateType !=
                    IdleLevelStateType.Completed)
                {
                    IdleLevelData.BuildingsDatas[i].AddressId = i;
                    BuildingManagers[i].buildingAdressId = i;
                    BuildingManagers[i].MarketPrice = IdleLevelData.BuildingsDatas[i].buildingMarketPrice;
                    BuildingManagers[i].PayedAmount = IdleLevelData.BuildingsDatas[i].PayedAmount;
                    BuildingManagers[i].Saturation = IdleLevelData.BuildingsDatas[i].Saturation;
                    
                }
                
            }

            BuildingsDatasAreSynced();
        }
        void BuildingsDatasAreSynced()
        { 
            BuildingSignals.Instance.onDataReadyToUse?.Invoke();
        }
        private void SaveCityData(IdleLevelData idlelevelData)
        {
            SaveSignals.Instance.onSaveIdleLevelProgressData?.Invoke(SaveStates.idleLevelProgress,idlelevelData);
        }
        private IdleLevelData LoadCityData(IdleLevelData idleLevelData)
        {
            return SaveSignals.Instance.onLoadIdleLevelProgressData.Invoke(SaveStates.idleLevelProgress,idleLevelData);
        }
        private void OnGetBuildingsDataFromBuildingManager()
        {
            for (int i = 0; i < BuildingManagers.Count ; i++)
            {
                IdleLevelData.BuildingsDatas[i].IdleLevelStateType = BuildingManagers[i].IdleLevelStateType;
                IdleLevelData.BuildingsDatas[i].PayedAmount = BuildingManagers[i].PayedAmount;
                IdleLevelData.BuildingsDatas[i].Saturation = BuildingManagers[i].Saturation;

                if (!IdleLevelData.BuildingsDatas[i].isDepended || IdleLevelData.BuildingsDatas[i].IdleLevelStateType
                    != IdleLevelStateType.Completed)
                {
                    IdleLevelData.BuildingsDatas[i].IdleLevelStateType = BuildingManagers[i].IdleLevelStateType;
                    IdleLevelData.BuildingsDatas[i].SideObjectData.PayedAmount = BuildingManagers[i].PayedAmount;
                    IdleLevelData.BuildingsDatas[i].SideObjectData.Saturation = BuildingManagers[i].Saturation;
                }
            }
        } 
        private void OnSetBuildingStatus(int adressid)
        {
            IdleLevelData.BuildingsDatas[adressid].IdleLevelStateType = IdleLevelStateType.Completed;
        }
    }
}


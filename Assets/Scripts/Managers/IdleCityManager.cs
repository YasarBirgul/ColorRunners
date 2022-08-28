using System;
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
        
        public List<BuildingManager> BuildingManagers = new List<BuildingManager>();

        public IdleLevelStateType IdleLevelStateType;

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
        private void Awake()
        {
            GetIdleLevelData();
            IdleLevelData = OnGetCityData();
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingsCompleted += OnSetBuildingStatus;
        } 
        private void UnsubscribeEvents()
        { 
            BuildingSignals.Instance.onBuildingsCompleted -= OnSetBuildingStatus;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void OnSetBuildingStatus(int adressid)
        {
            IdleLevelData.BuildingsDatas[adressid].idleLevelState = IdleLevelStateType.Completed;
        }
    }
}


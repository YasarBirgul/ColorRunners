using System;
using System.Collections.Generic;
using Abstract;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class IdleCityManager : MonoBehaviour, ISaveable
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
            SetData();
        }

        private void SetData()
        {
            GetIdleLevelData();
            if (!ES3.FileExists($"IdleLevelDataKey{_idleLevelId}.es3"))
            {
                if (!ES3.KeyExists("IdleLevelDataKey"))
                {
                    IdleLevelData = OnGetCityData();
                    GetIdleLevelData();
                    Save(_idleLevelId);
                }
            }
            Load(_idleLevelId);
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
            LevelSignals.Instance.onLevelInitialize += OnLoad;
            BuildingSignals.Instance.onBuildingsCompleted += OnSetBuildingStatus;
            BuildingSignals.Instance.onSideBuildingsCompleted += OnSetBuildingStatus;
        } 
        private void UnsubscribeEvents()
        { 
            CoreGameSignals.Instance.onApplicatiponQuit -= OnSave;
            CoreGameSignals.Instance.onGamePause -= OnSave;
            LevelSignals.Instance.onLevelInitialize -= OnLoad;
            BuildingSignals.Instance.onBuildingsCompleted -= OnSetBuildingStatus;
            BuildingSignals.Instance.onSideBuildingsCompleted -= OnSetBuildingStatus;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnSave()
        {
            Save(_idleLevelId);
        }
        private void OnLoad()
        {
            Load(_idleLevelId);
        }
        private void OnSetBuildingStatus(int adressid)
        {
            IdleLevelData.CompletedCount++;
            CheckLevelStatus();
        }
        private void CheckLevelStatus()
        {
            Save(_idleLevelId);
            if (IdleLevelData.CompletedCount == BuildingManagers.Count)
            {
                IdleLevelData.IdleLevelStateType = IdleLevelStateType.Completed;
                Save(_idleLevelId);
                LevelSignals.Instance.onIdleLevelChange.Invoke();
            }
        }
        public void Save(int uniqueId)
        {
            IdleLevelData = new IdleLevelData(IdleLevelData.IdleLevelStateType, IdleLevelData.CompletedCount);
            SaveSignals.Instance.onSaveIdleLevelData.Invoke(IdleLevelData,uniqueId);
        }
        public void Load(int uniqueId)
        {
            IdleLevelData _idleLevelData =
                SaveSignals.Instance.onLoadIdleLevelData.Invoke(IdleLevelData.key, uniqueId);
            _idleLevelData.IdleLevelStateType = _idleLevelData.IdleLevelStateType;
            IdleLevelData.CompletedCount = _idleLevelData.CompletedCount;
        }
    }
}


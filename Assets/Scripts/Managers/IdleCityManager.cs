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

        private int count;
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
            
             if (!ES3.FileExists($"IdleLevelData{_idleLevelId}.es3"))
             {
                 if (!ES3.KeyExists("IdleLevelData"))
                 {   
                     Debug.Log("Key does not exist!");
                     IdleLevelData = OnGetCityData();
                     Save(_idleLevelId);
                 }
             }
             Debug.Log("Key Exist!");
             Load(_idleLevelId);
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            BuildingSignals.Instance.onBuildingsCompleted += OnSetBuildingStatus;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        } 
        private void UnsubscribeEvents()
        { 
            BuildingSignals.Instance.onBuildingsCompleted -= OnSetBuildingStatus;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
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

        private void OnNextLevel()
        {
            Save(_idleLevelId);
        }
        
        public void Save(int uniqueId)
        {
            count++;
            


        }

        public void Load(int uniqueId)
        { 
            
        }
    }
}


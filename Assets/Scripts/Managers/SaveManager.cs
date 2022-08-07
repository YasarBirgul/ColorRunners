using System;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class SaveManager: MonoBehaviour
    {
        #region Self Variables

        #region Public Variables
        [Header("Data")] public SaveData Data;
        
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables
        
        #endregion
        
        #endregion
        private void Awake()
        {
            Data = GetSaveData();
        } 
        private SaveData GetSaveData() => Resources.Load<CD_Save>("Data/CD_Save").SaveData;

        #region EventSubscription
        private void OnEnable()
        {
            InitializeSyncDatas(SaveTypes.BonusStickyMen);
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SaveSignals.Instance.onChangeSaveData += OnChangeSaveData;
            SaveSignals.Instance.onSaveDataToDatabase += OnSaveDataToDatabase;
        }
        private void UnsubscribeEvents()
        {
            
        } 
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void OnChangeSaveData(SaveTypes saveType,int savedAmount)
        {
            switch (saveType)
            {
                case SaveTypes.All:
                    Data.BonusStickyMen = savedAmount;
                    Data.CollectedStickyMen = savedAmount;
                    Data.CurrentLevel = savedAmount;
                    break;
                case SaveTypes.BonusStickyMen:
                    Data.BonusStickyMen = savedAmount;
                    break;
                case SaveTypes.CollectedStickymen:
                    Data.CollectedStickyMen = savedAmount;
                    break;
                case SaveTypes.CurrentLevel:
                    Data.CollectedStickyMen = savedAmount;
                    break;
            }
        } 
        private void InitializeSyncDatas(SaveTypes savetype)
        {
            switch (savetype)
            {
                case SaveTypes.All:
                    Data.BonusStickyMen=ES3.Load<int>("BonusStickyMen");
                    Data.CollectedStickyMen=ES3.Load<int>("CollectedStickymen");
                    Data.CurrentLevel=ES3.Load<int>("CurrentLevel");
                    break;
                case SaveTypes.BonusStickyMen:
                    Data.BonusStickyMen=ES3.Load<int>("BonusStickyMen");
                    break;
                case SaveTypes.CollectedStickymen:
                    Data.CollectedStickyMen=ES3.Load<int>("CollectedStickymen");
                    break;
                case SaveTypes.CurrentLevel:
                    Data.CurrentLevel=ES3.Load<int>("CurrentLevel");
                    break;
            
            }
            SaveSignals.Instance.onSendDataToManagers?.Invoke(Data);
        }
        private void OnSaveDataToDatabase(SaveTypes saveTypes)
        {
            switch (saveTypes)
            {
                case SaveTypes.All:
                    ES3.Save("BonusColorman",Data.BonusStickyMen);
                    ES3.Save("CollectedColorman",Data.CollectedStickyMen);
                    ES3.Save("CurrentLevel",Data.CurrentLevel);
                    break;
                case SaveTypes.BonusStickyMen:
                    ES3.Save("BonusStickyMen",Data.BonusStickyMen);
                    break;
                case SaveTypes.CollectedStickymen:
                    ES3.Save("BonusStickyMen",Data.CollectedStickyMen);
                    break;
                case SaveTypes.CurrentLevel:
                    ES3.Save("CurrentLevel",Data.CurrentLevel);
                    break;
            }
        }
    }
}
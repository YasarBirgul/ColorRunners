using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class IdleCityManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("BuildingsData")] public IdleLevelData IdleLevelData;

        public List<GameObject> Buildings = new List<GameObject>();

        public List<BuildingController> BuildingControllers = new List<BuildingController>();

        public List<GameObject> PublicPlaces = new List<GameObject>();

        public List<Transform> BuildingsTransforms = new List<Transform>();

        public List<Transform> PublicPlacesTransforms = new List<Transform>();

        public List<Transform> SideObjectTransforms = new List<Transform>();

        #endregion

        #region Private Variables
 
        [ShowInInspector] private Dictionary<BuildingController, GameObject> buildingDictionary =
            new Dictionary<BuildingController, GameObject>();
        
        private int index;
        
        private int _idleLevelId;

        #endregion

        #region Serialized Variables
        
        #endregion

        #endregion
        private void GetIdleLevelData()
        {
            _idleLevelId = LevelSignals.Instance.onGetIdleLevelID.Invoke();

        }
        private void Start()
        {
            GetIdleLevelData();
            IdleLevelData = OnGetCityData();
        } 
        private IdleLevelData OnGetCityData()=>
            Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[_idleLevelId];
        
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
           
        } 
        private void UnsubscribeEvents()
        {
           
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
    }
}


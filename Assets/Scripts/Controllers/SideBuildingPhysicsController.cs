﻿using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class SideBuildingPhysicsController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        #endregion

        #region Serialized Variables

        [SerializeField] private BuildingManager buildingManager;
        [SerializeField] private int objetType;
        #endregion

        #region Private Variables

        private float _timer = 0f;

        #endregion

        #endregion
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("ScorePhysics"))
            {
                _timer -= Time.fixedDeltaTime;
                
                if (_timer <= 0)
                {
                    _timer = 0.2f;
  
                    if (buildingManager.buildingsData.SideObject.MarketPrice > buildingManager.buildingsData.SideObject.PayedAmount)
                    {
                        buildingManager.UpdateSidePayedAmount();
                    }
                    else
                    {
                        if (buildingManager.buildingsData.SideObject.IdleLevelStateType == IdleLevelStateType.Uncompleted)
                        {
                            buildingManager.UpdateSideBuildingStatus(IdleLevelStateType.Completed);
                            buildingManager.CheckSideBuildingScoreStatus(IdleLevelStateType.Completed);
                        }
                    }
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ScorePhysics"))
            {
                _timer = 0f;
            }
        }
    }
}
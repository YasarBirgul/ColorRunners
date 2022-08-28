using System;
using Enums;
using Extentions;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingScorePhysicsController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        
        #endregion

        #region Serialized Variables

        [SerializeField] private BuildingManager buildingManager;
        [SerializeField] private int ObjetType;
        #endregion

        #region Private Variables

        private float _timer = 0f;

        #endregion

        #endregion

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                 _timer -= Time.fixedDeltaTime;
                
              if (_timer <= 0)
              {
                  _timer = 0.2f;
  
                  if (buildingManager.MarketPrice > buildingManager.PayedAmount)
                  {
                      if (gameObject.CompareTag("Main"))
                      {
                          buildingManager.UpdatePayedAmount();
                      }
                      else if (gameObject.CompareTag("Side"))
                      {
                          buildingManager.UpdateSidePayedAmount();
                      }
                  }
                  else
                  {
                      gameObject.SetActive(false);
                      if (buildingManager.IdleLevelStateType == IdleLevelStateType.Uncompleted)
                      {
                          buildingManager.UpdateBuildingStatus(IdleLevelStateType.Completed);
                      }
                  }
              }
            }
           
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                _timer = 0f;
            }
        }
    }
}
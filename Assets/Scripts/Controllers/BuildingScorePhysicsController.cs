using Enums;
using Managers;
using Signals;
using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class BuildingScorePhysicsController : MonoBehaviour
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
        private void OnTriggerEnter(Collider other)
        {
            if(other.CompareTag("ScorePhysics"))
            {
                ParticleSignals.Instance.onParticleBurst?.Invoke(transform.position);
                Debug.Log("MainLocal : " + transform.position);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("ScorePhysics"))
            {
                 _timer -= Time.fixedDeltaTime;
                
              if (_timer <= 0)
              {
                  _timer = 0.2f;
  
                  if (buildingManager.BuildingsData.BuildingMarketPrice > buildingManager.BuildingsData.PayedAmount)
                  {
                      buildingManager.UpdatePayedAmount();
                      
                  }
                  else
                  {
                      if (buildingManager.BuildingsData.idleLevelState == IdleLevelStateType.Uncompleted)
                      {
                          buildingManager.OpenSideObject();
                          buildingManager.UpdateBuildingStatus(IdleLevelStateType.Completed);
                          buildingManager.CheckBuildingScoreStatus(IdleLevelStateType.Completed);
                      }
                  }
              }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ScorePhysics"))
            {
                if (buildingManager.BuildingsData.BuildingMarketPrice == buildingManager.BuildingsData.PayedAmount)
                {
                    buildingManager.OpenSideObject();
                    buildingManager.UpdateBuildingStatus(IdleLevelStateType.Completed);
                    buildingManager.CheckBuildingScoreStatus(IdleLevelStateType.Completed);
                }
                _timer = 0f;
            }
        }
    }
}
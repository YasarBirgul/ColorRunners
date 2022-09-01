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
  
                  if (buildingManager.buildingsData.BuildingMarketPrice > buildingManager.buildingsData.PayedAmount)
                  {
                      buildingManager.UpdatePayedAmount();
                      ParticleSignals.Instance.onParticleBurst?.Invoke(transform.position);
                  }
                  else
                  {
                      if (buildingManager.buildingsData.idleLevelState == IdleLevelStateType.Uncompleted)
                      {
                          ParticleSignals.Instance.onParticleStop.Invoke();
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
                if (buildingManager.buildingsData.BuildingMarketPrice == buildingManager.buildingsData.PayedAmount)
                {
                    buildingManager.OpenSideObject();
                    buildingManager.UpdateBuildingStatus(IdleLevelStateType.Completed);
                    buildingManager.CheckBuildingScoreStatus(IdleLevelStateType.Completed);
                }
                _timer = 0f;
            }

            if (other.CompareTag("Player"))
            {
                ParticleSignals.Instance.onParticleStop.Invoke();
            }
        }
    }
}
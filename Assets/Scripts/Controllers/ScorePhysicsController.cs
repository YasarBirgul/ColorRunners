using System;
using ES3Types;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class ScorePhysicsController : MonoBehaviour
    {
        #region SelfVariables
    
        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private ScoreManager manager;
        [SerializeField] private Collider collider;
        #endregion

        #region Private Variables

        private float _timer;
        
        
        #endregion

        #endregion
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("BuildingArea"))
            {
                _timer -= Time.fixedDeltaTime;
                
                if (_timer <= 0)
                {
                    _timer = 0.2f;
                    
                    manager.OnDecreaseScore();
                }
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("BuildingArea"))
            {
                _timer = 0f;
            }
        }
        public void SetColliderActive(bool Active)
        {
            if (Active == true)
            {
                collider.enabled = true;
            }
            else
            {
                collider.enabled = false;
            }
        }
    }
    
}

using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingPhysicsController : MonoBehaviour
    {
        #region SelfVariables
    
        #region Serialized Variables
          
        [SerializeField] private BuildingManager manager;

        #endregion
        
        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                Destroy(other.gameObject);
            }
        }
    }
}
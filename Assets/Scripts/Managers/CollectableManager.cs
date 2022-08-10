using Controllers;
using Datas.UnityObject;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        #endregion
        #region Serialized Variables
        //   [SerializeField] private CollectablePhysicsController collectablePhysicsController;
        #endregion
        #region Private Variables
        #endregion
        #endregion
        #region Event Subscription

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onMansCollection += OnMansCollection;
        }
        private void UnsubscribeEvents()
        { 
            CollectableSignals.Instance.onMansCollection -= OnMansCollection;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnMansCollection(GameObject other)
        {
            AddOnStack(other);
        } 
        private static void AddOnStack(GameObject other)
        {
            if (other.CompareTag("Collectable"))
            {
                other.tag = "Collected";
                
            }
        }

    }
}
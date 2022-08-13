using System;
using System.Security.AccessControl;
using Enums;
using Keys;
using Managers;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class CollectablePhysicsController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private CollectableManager collectableManager;

        #endregion

        #region Private Variables

        [SerializeField] private bool IsCollected;
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable") && IsCollected)
            {
                if (other.GetComponent<CollectablePhysicsController>().IsCollected == false)
                {
                    StackSignals.Instance.onIncreaseStack?.Invoke(other.transform.parent.gameObject);
                    other.GetComponent<CollectablePhysicsController>().IsCollected = true;
                }
            }
            if (other.CompareTag("Obstacle"))
            {
                StackSignals.Instance.onDecreaseStack?.Invoke(new ObstacleCollisionGOParams()
                {
                    Collected = gameObject,
                    Obstacle = other.gameObject
                });
            }
            if (other.CompareTag("Changer"))
            {
                StackSignals.Instance.onColorChange?.Invoke(other.gameObject);
            }
        }
    }
}

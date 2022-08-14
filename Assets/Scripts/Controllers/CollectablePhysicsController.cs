using System;
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
        
        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (CompareTag("Collected") && other.CompareTag("Collectable"))
            {
                CollectableManager otherColManager = other.transform.parent.GetComponent<CollectableManager>();

                if (collectableManager.ColorType == otherColManager.ColorType)
                {
                    StackSignals.Instance.onIncreaseStack?.Invoke(other.transform.parent.gameObject);
                    otherColManager.SetAnim(CollectableAnimationStates.Running);
                    other.tag = "Collected";
                }
                else
                {
                    other.transform.parent.gameObject.SetActive(false);
                    StackSignals.Instance.onDecreaseStack?.Invoke(new ObstacleCollisionGOParams()
                    { 
                       Collected = gameObject,
                    });
                }
            }
            if (other.CompareTag("Obstacle"))
            {
                other.gameObject.SetActive(false);
                StackSignals.Instance.onDecreaseStack?.Invoke(new ObstacleCollisionGOParams()
                {
                    Collected = gameObject,
                });
            }
            if (other.CompareTag("ColorArea"))
            {
                collectableManager.SetAnim(CollectableAnimationStates.CrouchWalking);
                collectableManager.DelistFromStack();
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ColorArea"))
            {
                collectableManager.SetAnim(CollectableAnimationStates.Running);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            
        }
    }
}

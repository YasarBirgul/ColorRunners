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
            if (other.CompareTag("Collected") && !IsCollected)
            {
                StackSignals.Instance.onIncreaseStack?.Invoke(transform.parent.gameObject);
                collectableManager.SetAnim(CollectableAnimationStates.Running);
                IsCollected = true;
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

            if (other.CompareTag("ColorArea"))
            {
                collectableManager.SetAnim(CollectableAnimationStates.CrouchWalking);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ColorArea"))
            {
                collectableManager.SetAnim(CollectableAnimationStates.Running);
            }
        }
    }
}

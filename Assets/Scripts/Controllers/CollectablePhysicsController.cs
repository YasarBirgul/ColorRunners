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

        public CollectableManager CollectableManager;

        #endregion

        #region Serialized Variables

        #endregion

        #region Private Variables

        #endregion

        #endregion
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                StackSignals.Instance.onIncreaseStack?.Invoke(other.gameObject);
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

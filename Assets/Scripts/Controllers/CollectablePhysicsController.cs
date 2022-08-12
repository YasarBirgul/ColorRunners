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

        #endregion

        #endregion


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collectable"))
            {
                StackSignals.Instance.onIncreaseStack?.Invoke(other.GetComponentInParent<CollectableManager>()
                    .gameObject);
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

            if (other.CompareTag(("ColorArea")))
            {
                collectableManager.EnterTurretArea();
                Debug.Log("Carpti");
            }

              if (other.CompareTag("DroneArea"))
             {
                collectableManager.EnterDroneArea();
             }


        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("ColorArea"))
            {
                collectableManager.ExitTurretArea();
            }
        }
    }
}

using Controllers;
using Enums;
using JetBrains.Annotations;
using MK.TextureChannelPacker;
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
        //   [SerializeField] private CollectablePhysicsController collectablePhysicsController;,
        [SerializeField] private CollectableAnimationStates collectableAnimationStates;
        [SerializeField] private CollectableAnimationController collectableAnimationController;
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
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
        }
        private void UnsubscribeEvents()
        { 
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnIncreaseStack(GameObject other)
        {
            AddOnStack(other);
        } 
        private static void AddOnStack(GameObject other)
        {
            var Tag = other.GetComponentInChildren<CollectablePhysicsController>().gameObject;
            if (Tag.CompareTag("Collectable"))
            {
                Tag.tag = "Collected";
            }
        }

        public void EnterTurretArea()
        {
            collectableAnimationController.WhenEnterTurretArea();
            
        }

        public void ExitTurretArea()
        {
            collectableAnimationController.WhenExitTurretArea();
        }
        
         public void EnterDroneArea()
        {
           collectableAnimationController.Invoke("WhenEnterDroneArea",.5f);
       }
    }
}
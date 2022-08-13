using Controllers;
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
        //   [SerializeField] private CollectablePhysicsController collectablePhysicsController;,
        [SerializeField] private CollectableAnimationController animationController;
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
        }
        private void UnsubscribeEvents()
        {
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        public void SetAnim(CollectableAnimationStates AnimState)
        {
           animationController.ChangeCollectableAnimation(AnimState);
        }
    }
}
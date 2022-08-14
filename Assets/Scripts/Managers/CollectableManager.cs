using System;
using Controllers;
using Enums;
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
        public CollectableMeshController collectableMeshController;
        #endregion
        #region Private Variables
        public ColorType ColorType;
        #endregion
        #endregion
        #region Event Subscription
        #endregion
        private void Awake()
        {
            ColorOnInit();
        }
        public void SetAnim(CollectableAnimationStates AnimState)
        {
           animationController.ChangeCollectableAnimation(AnimState);
        }
        public void ColorOnInit()
        {
            collectableMeshController.GetColor(ColorType);
        } 
        public void ChangeColor(ColorType _colorType)
        {
            collectableMeshController.GetColor(_colorType);
        }
    }
}
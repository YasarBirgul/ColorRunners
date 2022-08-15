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
        [SerializeField] private CollectableAnimationController animationController;
        [SerializeField] private CollectableMovementController collectableMovementController;
        public CollectableMeshController collectableMeshController;
        #endregion
        #region Private Variables
        public ColorType ColorType;
        public CollectableMatchType MatchType;
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
            ColorType = _colorType;
            collectableMeshController.GetColor(ColorType);
            
        } 
        public void DelistFromStack()
        {
            StackSignals.Instance.OnDroneArea?.Invoke(transform.GetSiblingIndex());
        }

        public void SetCollectablePositionOnDroneArea(Transform DroneCheckColorArea)
        {
            collectableMovementController.MoveToColorArea(DroneCheckColorArea);
        }
    }
}
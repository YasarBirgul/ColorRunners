using System.Threading.Tasks;
using Controllers;
using DG.Tweening;
using Enums;
using Signals;
using UnityEngine;

namespace Managers
{
    public class CollectableManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        
        public ColorType ColorType;
        public CollectableMatchType MatchType;
        public CollectableMeshController CollectableMeshController;
        
        #endregion
        #region Serialized Variables
        [SerializeField] private CollectableAnimationController animationController;
        [SerializeField] private CollectableMovementController collectableMovementController;
       
        #endregion
        #region Private Variables
        
        #endregion
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
            CollectableMeshController.GetColor(ColorType);
        } 
        public void ChangeColor(ColorType _colorType)
        {
            ColorType = _colorType;
            CollectableMeshController.GetColor(ColorType);
        } 
        public void DelistFromStack()
        {
            StackSignals.Instance.onEnterDroneArea?.Invoke(transform.GetSiblingIndex());
        }
        public void SetCollectablePositionOnDroneArea(Transform DroneCheckColorArea)
        {
            collectableMovementController.MoveToColorArea(DroneCheckColorArea);
        }
        public async void IncreaseStackAfterDroneArea()
        {
            await Task.Delay(3000);
            DOVirtual.DelayedCall(0.2f, () => SetAnim(CollectableAnimationStates.Running));
            RemoveOutline(false);
            StackSignals.Instance.onIncreaseStack?.Invoke(gameObject);
            SetAnim(CollectableAnimationStates.Running);
        }
        public void RemoveOutline(bool OutlineOn)
        {
             CollectableMeshController.OutlineChanger(OutlineOn);
        }
        public void EnterTurretArea(GameObject otherGameObject)
        {
            CollectableMeshController.CompareColorType(otherGameObject,ColorType);
        }
        public void SendCollectableTransform()
        {
           ColoredAreaSignals.Instance.onTurretDetect.Invoke(gameObject);
        }

        public void exitTurretArea()
        {
            ColoredAreaSignals.Instance.onExitTurretArea.Invoke();
        }
        public async void SetActiveFalse()
        {
            await Task.Delay(3000);
            gameObject.SetActive(false);
        }
    }
}
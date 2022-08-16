using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Commands.Stack;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers 
{ 
    public class StackManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        
        #endregion
        #region Serialized Variables

        [SerializeField] private GameObject tempHolder;
        
        [SerializeField] private List<GameObject> collected = new List<GameObject>();
        //[SerializeField] private GameObject collectorMeshRenderer;
        private Transform _playerManager;
        #region Private Variables
        
        [ShowInInspector] private StackData _data;
        private StackManager _stackMan;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private CollectableScaleUpCommand _colScaleUpCommand;
        private CollectableAddOnStackCommand _colAddOnStackCommand;
        private CollectableRemoveOnStackCommand _colRemoveOnStackCommand;
        
        #endregion
        #endregion
        #endregion
        private void Awake()
        { 
            _data = GetStackData();
            Init();
        } 
        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_Stack").StackData; 
        private void Init()
        {
            _stackMan = GetComponent<StackManager>();
            _stackLerpMovementCommand = new StackLerpMovementCommand(ref collected,ref _data);
            _colScaleUpCommand = new CollectableScaleUpCommand(ref collected,ref _data);
            _colAddOnStackCommand = new CollectableAddOnStackCommand(ref _stackMan,ref collected);
            _colRemoveOnStackCommand = new CollectableRemoveOnStackCommand(ref collected);
            
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onGameOpen += OnGameOpen;
            CoreGameSignals.Instance.onPlay += OnPlay;
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            StackSignals.Instance.onColorChange += OnColorChange;
            StackSignals.Instance.OnDroneArea += OnDroneArea;
            StackSignals.Instance.onRebuildStack += OnRebuildStack;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGameOpen -= OnGameOpen;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
            StackSignals.Instance.onColorChange -= OnColorChange;
            StackSignals.Instance.OnDroneArea -= OnDroneArea;
            StackSignals.Instance.onRebuildStack -= OnRebuildStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void Update()
        {
            if (!_playerManager)
                return;
            {
                _stackLerpMovementCommand.Execute(_playerManager);  
            }
        } 
        private void FindPlayer()
        {
            if (!_playerManager)
            {
                _playerManager = FindObjectOfType<PlayerManager>().transform;
            }
        }
        private void OnIncreaseStack(GameObject other)
        {
            _colAddOnStackCommand.Execute(other);
            StartCoroutine(_colScaleUpCommand.Execute(other));
        }
        private void OnDecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            _colRemoveOnStackCommand.Execute(obstacleCollisionGOParams);
        }
        private async void OnColorChange(ColorType colorType)
        {
            for (int i = 0; i < collected.Count; i++)
            {
                await Task.Delay(50);
                collected[i].GetComponent<CollectableManager>().ChangeColor(colorType);
            }
        } 
        private void OnDroneArea(int index)
        {
            collected[index].transform.SetParent(tempHolder.transform);
            //DroneAreaSignals.Instance.onDroneAreaEnter?.Invoke(collected[index].gameObject);
            collected.RemoveAt(index);
            collected.TrimExcess();
            if (collected.Count == 0)
            {
                SendCollectablesBackToDeath();
            }
        } 
        private void OnGameOpen()
        {
            for (int i = 0; i < _data.InitializedStack.Count; i++)
            { 
                var StartPack 
                    = Instantiate(_data.InitializedStack[i],Vector3.zero*(i+1)*2,transform.rotation);
                _colAddOnStackCommand.Execute(StartPack);
                collected[i].GetComponent<CollectableManager>().SetAnim(CollectableAnimationStates.Crouching);
            }
        } 
        private void OnPlay()
        {
            FindPlayer();
            for (int i = 0; i < collected.Count; i++)
            {
                collected[i].GetComponent<CollectableManager>().SetAnim(CollectableAnimationStates.Running);
            }
        }
        private void OnReset()
        {
            
            
        }
        private void SendCollectablesBackToStack()
        {
            for (int i = tempHolder.transform.childCount-1; i >= 0; i--)
            {
                tempHolder.transform.GetChild(i).GetComponent<CollectableManager>().IncreaseStackAfterDroneArea(tempHolder.transform.GetChild(i).gameObject);
            }
        }
        private void OnRebuildStack(GameObject stack)
        {
            collected.Add(stack);
            
            CameraSignals.Instance.onExitMiniGame?.Invoke();
            
            _playerManager.position = collected[0].transform.position;

            _playerManager.GetComponent<PlayerManager>().OnStartVerticalMovement();
            stack.GetComponent<CollectableManager>().ChangeOutline(false);
            stack.transform.SetParent(transform);
            stack.transform.position = collected[collected.Count - 1].transform.position + Vector3.back;

        }
        
        private async void SendCollectablesBackToDeath()
        {
            for (int i = 0; i < tempHolder.transform.childCount; i++)
            {
                await Task.Delay(50);

                CollectableManager collectableManager =
                    tempHolder.transform.GetChild(i).GetComponent<CollectableManager>();

                if (collectableManager.MatchType != CollectableMatchType.Match)
                {
                    collectableManager.SetAnim(CollectableAnimationStates.Dead);
                    Destroy(tempHolder.transform.GetChild(i).gameObject,3f);
                }
            }
            await Task.Delay(3000);
            SendCollectablesBackToStack();
            DroneAreaSignals.Instance.onColliderDisable?.Invoke();
        }
    }
}    

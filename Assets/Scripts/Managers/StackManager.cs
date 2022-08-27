using System.Collections.Generic;
using System.Threading.Tasks;
using Commands.Stack;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

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
        
        #region Private Variables
        
        [ShowInInspector] private StackData _data;
        private StackManager _stackMan;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private CollectableScaleUpCommand _colScaleUpCommand;
        private CollectableAddOnStackCommand _colAddOnStackCommand;
        private CollectableRemoveOnStackCommand _colRemoveOnStackCommand;
        private Transform _playerManager;
        
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
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            CoreGameSignals.Instance.onGameOpen += OnGameOpen;
            CoreGameSignals.Instance.onPlay += OnPlay;
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            StackSignals.Instance.onColorChange += OnColorChange;
            StackSignals.Instance.onEnterDroneArea += OnEnterDroneArea;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            CoreGameSignals.Instance.onGameOpen -= OnGameOpen;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
            StackSignals.Instance.onColorChange -= OnColorChange;
            StackSignals.Instance.onEnterDroneArea -= OnEnterDroneArea;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
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
        private void FindPlayer()
        {
            if (!_playerManager)
            {
                _playerManager = FindObjectOfType<PlayerManager>().transform;
                _stackLerpMovementCommand = new StackLerpMovementCommand(ref collected,ref _data,ref _playerManager);
            }
        }
        private void Update()
        {
            if (!_playerManager)
                return;
            {
                _stackLerpMovementCommand.Execute();   
            }
        }
        private void OnIncreaseStack(GameObject other)
        {
            _colAddOnStackCommand.Execute(other);
            StartCoroutine(_colScaleUpCommand.Execute(other));
            ScoreSignals.Instance.onIncreaseScore?.Invoke();
            if (collected.Count == 1)
            {
                _playerManager.transform.position = collected[0].transform.position;
                ScoreSignals.Instance.onPlayerScoreSetActive?.Invoke(true);
               
            }
        }
        private void OnDecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            _colRemoveOnStackCommand.Execute(obstacleCollisionGOParams);
            ScoreSignals.Instance.onDecreaseScore?.Invoke();
            if (collected.Count == 0 && tempHolder.transform.childCount == 0)
            {
                UISignals.Instance.onOpenPanel?.Invoke(UIPanels.FailPanel);
            }
        } 
        private async void OnColorChange(ColorType colorType)
        {
            for (int i = 0; i < collected.Count; i++)
            {
                await Task.Delay(50);
                collected[i].GetComponent<CollectableManager>().ChangeColor(colorType);
            }
        } 
        private void OnEnterDroneArea(int index)
        {
            ScoreSignals.Instance.onPlayerScoreSetActive?.Invoke(false);
            ScoreSignals.Instance.onDecreaseScore?.Invoke();
            collected[index].transform.SetParent(tempHolder.transform);
            collected[index].GetComponent<CollectableManager>().RemoveOutline(true);
            collected.RemoveAt(index);
            collected.TrimExcess();
            if (collected.Count == 0)
            {
                DroneAreaSignals.Instance.onColliderDisable?.Invoke();
                DroneAreaSignals.Instance.onEnableFinalCollider?.Invoke();
            }
        }
        private void OnChangeGameState(GameStates currentState)
        {
            if (currentState == GameStates.Roullette)
            {
                StackDeList();
                collected.TrimExcess();
            }
        }
        private async void StackDeList()
        {
            int ListTotalCount = collected.Count;
            for (int i = 0; i < ListTotalCount; i++)
            {
                await Task.Delay(100);
                collected[0].SetActive(false);
                collected[0].transform.parent = tempHolder.transform;
                PlayerSignal.Instance.onIncreaseScale?.Invoke();
                collected.RemoveAt(0);
            }
        }
    }
}    

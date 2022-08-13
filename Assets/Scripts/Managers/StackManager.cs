using System.Collections.Generic;
using Commands.Stack;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{ 
    public class StackManager : MonoBehaviour
    {
        
        #region Self Variables
        
        #region Public Variables
        
        Tween _tween;
        public GameObject TempHolder;
        [FormerlySerializedAs("RemovedCollectableHolder")] public GameObject ReColHolder;
        #endregion
        #region Serialized Variables
        
        [SerializeField] private List<GameObject> collected = new List<GameObject>();
        //[SerializeField] private GameObject collectorMeshRenderer;
        [SerializeField] private Transform playerManager;
        [SerializeField] private CollectableManager collectableManager;
       
        #region Private Variables
        
        [ShowInInspector] private StackData _data;
        private StackManager _stackMan;
        private StackLerpMovementCommand _stackLerpMovementCommand;
        private CollectableScaleUpCommand _colScaleUpCommand;
        private CollectableAddOnStackCommand _colAddOnStackCommand;
        private CollectableRemoveOnStackCommand _colRemoveOnStackCommand;
        private StackColorChangerCommand _stackColorChangerCommand;
        
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
            _colRemoveOnStackCommand = new CollectableRemoveOnStackCommand(ref collected,ref _stackMan,ref ReColHolder);
            _stackColorChangerCommand = new StackColorChangerCommand(ref collected);
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
            StackSignals.Instance.onTurrentGroundControll += OnTurrentGroundControll;
            CollectableSignals.Instance.onExitGroundCheck += OnExitGroundCheck;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onGameOpen -= OnGameOpen;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
            StackSignals.Instance.onColorChange -= OnColorChange;
            StackSignals.Instance.onTurrentGroundControll -= OnTurrentGroundControll;
            CollectableSignals.Instance.onExitGroundCheck -= OnExitGroundCheck;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void Update()
        {
            _stackLerpMovementCommand.Execute(playerManager);
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
        private void OnColorChange(GameObject colorChangerWall)
        {
            _stackColorChangerCommand.Execute(colorChangerWall);
        }
        private void OnTurrentGroundControll(GameObject Ground)
        {
           // TurrentGroundControll(Ground);
        }
        private void OnExitGroundCheck(GameObject other)
        {
            _tween.Kill();
        }
        private void TurrentGroundControll(GameObject Ground )
        {
            if (collected[0].GetComponent<Renderer>().material.color ==
                Ground.GetComponent<Renderer>().material.color)
            {
                 _tween.Kill();
                Debug.Log("AYNI");
            }
            if (collected[0].GetComponent<Renderer>().material.color !=
                Ground.GetComponent<Renderer>().material.color)
            {
                   _tween = DOVirtual.DelayedCall(0.5f, TurretDestroyOneItem ).SetLoops(-1);
                Debug.Log("FARKLI");
            }
        } 
        private void TurretDestroyOneItem() // farklı renkte bir yere girince adam öldüren fonk.
        {
            int RandomIndex = UnityEngine.Random.Range(0, collected.Count);
            Debug.Log("RandomIndex"+RandomIndex);
            collected[RandomIndex].SetActive(false);
            collected[RandomIndex].transform.SetParent(TempHolder.transform);
            collected.RemoveAt(RandomIndex);
        }

        private void OnGameOpen()
        {
            for (int i = 0; i < _data.InitializedStack.Count; i++)
            {
                var StartPack = Instantiate(_data.InitializedStack[i], Vector3.zero * (i + 1) * 2, transform.rotation);
                _colAddOnStackCommand.Execute(StartPack);
                collected[i].GetComponent<CollectableManager>().SetAnim(CollectableAnimationStates.Crouching);
            }
        }
        private void OnPlay()
        {
            for (int i = 0; i < collected.Count; i++)
            {
                collected[i].GetComponent<CollectableManager>().SetAnim(CollectableAnimationStates.Running);
            }
        }
    }
}    

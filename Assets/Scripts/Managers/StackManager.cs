using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Commands.Stack;
using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
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
        
        public GameObject TempHolder;
        
        #endregion
        
        #region Serialized Variables
          
        [SerializeField] private List<GameObject> collected = new List<GameObject>();
        
        [SerializeField] private GameObject collectorMeshRenderer;
        
        [SerializeField] private Transform playerManager;

        #region Private Variables

        [ShowInInspector] private StackData Data;

        private StackLerpMovementCommand _stackLerpMovementCommand;

        private CollectableScaleUpCommand _collectableScaleUpCommand;
        
        #endregion
        
        #endregion
        
        #endregion
        
        private void Awake()
        { 
            Data = GetStackData();
            Init();
        } 
        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_Stack").StackData;

        private void Init()
        {
            _stackLerpMovementCommand = new StackLerpMovementCommand();
            _collectableScaleUpCommand = new CollectableScaleUpCommand();
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack += OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
        }
        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onIncreaseStack -= OnIncreaseStack;
            StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void Update()
        {
            StackLerpMovement();
        }
        private void StackLerpMovement()
        { 
            _stackLerpMovementCommand.StackLerpMove(ref collected, playerManager, Data.LerpSpeed, ref Data.StackDistanceZ);
        }
        private void OnIncreaseStack(GameObject other)
        {
           // Debug.Log(collected[0].GetComponent<CollectableManager>()._collectableMaterial);
            Debug.Log(other.GetComponent<CollectableManager>()._collectableMaterial);
            if (collected[0].GetComponent<CollectableManager>()._collectableMaterial == other.GetComponent<CollectableManager>()._collectableMaterial)
            {
                Debug.Log("+++");
                AddOnStack(other);
                CollectableScaleUp();
            }
        }
        private void OnDecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            DecreaseStack(obstacleCollisionGOParams);
        }
        private void DecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            int CollidedObjectIndex = obstacleCollisionGOParams.Collected.transform.GetSiblingIndex();
            collected[CollidedObjectIndex].SetActive(false);
            collected[CollidedObjectIndex].transform.SetParent(TempHolder.transform);
            collected.RemoveAt(CollidedObjectIndex);
            Destroy(obstacleCollisionGOParams.Collected);
            Destroy(obstacleCollisionGOParams.Obstacle);
            collected.TrimExcess();
        }
        private void AddOnStack(GameObject other)
        { 
            other.transform.parent = transform;
            collected.Add(other.gameObject);
        }
        private void CollectableScaleUp()
        {
            StartCoroutine(_collectableScaleUpCommand.CollectableScaleUp(collected, Data.ScaleFactor, Data.StackScaleUpDelay));
        }
    }
}    

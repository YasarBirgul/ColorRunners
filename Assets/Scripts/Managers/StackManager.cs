using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Datas.UnityObject;
using Datas.ValueObject;
using DG.Tweening;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        
        #region Self Variables
        
        #region Public Variables
        
        public GameObject TempHolder;
        
        #endregion
        
        #region Serialized Variables
          
        [SerializeField] private List<GameObject> Collected = new List<GameObject>();
        
        [SerializeField] private GameObject collectorMeshRenderer;
        
        [SerializeField] private Transform playerManager;

        #region Private Variables

        [ShowInInspector] private StackData Data;
        
        #endregion
        
        #endregion
        
        #endregion
        
        #region Event Subscription

        private void Awake()
        {
            Data = GetStackData();
        }
        private StackData GetStackData() => Resources.Load<CD_Stack>("Data/CD_Stack").StackData;
        
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
        #region OnFunctionCheck
        private void Update() 
        {
           StackLerpMove();
        }
        private void OnIncreaseStack(GameObject other)
        {
            AddOnStack(other);
        }
        #endregion
        #region LerpMove
        private void StackLerpMove()
                {
                    if (Collected.Count > 0 )
                    {
                        Collected[0].transform.localPosition= new Vector3(
                            Mathf.Lerp(Collected[0].transform.localPosition.x, playerManager.position.x,Data.LerpSpeedX),
                            Mathf.Lerp(Collected[0].transform.localPosition.y, playerManager.position.y, Data.LerpSpeedY),
                            Mathf.Lerp(Collected[0].transform.localPosition.z, playerManager.position.z -2f, Data.LerpSpeedZ));
                        
                        for (int i = 1; i < Collected.Count; i++)
                        {
                            Collected[i].transform.position = new Vector3(
                                Mathf.Lerp(Collected[i].transform.localPosition.x, Collected[i-1].transform.localPosition.x,Data.LerpSpeedX),
                                Mathf.Lerp(Collected[i].transform.localPosition.y,Collected[i-1].transform.localPosition.y, Data.LerpSpeedY),
                                Mathf.Lerp(Collected[i].transform.localPosition.z, Collected[i-1].transform.localPosition.z -Data.StackDistanceZ, Data.LerpSpeedZ));
                        }
                    }
                }
        public IEnumerator CollectableScaleUp()
        {
            for (int i = Collected.Count -1; i >= 0; i--)
            {
                int index = i;
                Vector3 scale = Vector3.one * Data.ScaleFactor;
                Collected[index].transform.DOScale(scale, 0.2f).SetEase(Ease.Flash);
                Collected[index].transform.DOScale(Vector3.one, 0.2f).SetDelay(0.2f).SetEase(Ease.Flash);
                yield return new WaitForSeconds(0.05f);
            }
        }
        #endregion
        private void AddOnStack(GameObject other)
        { 
            other.transform.parent = transform;
            Collected.Add(other.gameObject);
            StartCoroutine(CollectableScaleUp());
        }
    }
}    

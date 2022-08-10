using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Controllers;
using DG.Tweening;
using Enums;
using Extentions;
using Signals;
using Sirenix.Utilities;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Managers
{
    public class StackManager : MonoBehaviour
    {
        
        #region Self Variables
        #region Public Variables
        Tween _tween;
        int DecreaseScoreValue = 1;
        int IncreaseScoreValue = 1;
        [SerializeField] private List<GameObject> Collected = new List<GameObject>(); //HAREKET ETTİRİLİYORLAR, private olarak değiştirilebilir.
        public GameObject TempHolder;
        private TweenCallback tweenCallback;
     //   [SerializeField] ScoreManager _scoreManager;
        #endregion
        #region Serialized Variables
        [SerializeField] private GameObject collectorMeshRenderer;
        #endregion
        #endregion
        #region Event Subscription 
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CollectableSignals.Instance.onMansCollection += OnMansCollection;
        }
        private void UnsubscribeEvents()
        {
            CollectableSignals.Instance.onMansCollection -= OnMansCollection;
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
        private void OnMansCollection(GameObject other)
        {
            AddOnStack(other);
            StartCoroutine(CollectableScaleUp());
        }
        #endregion
        #region LerpMove
        private void StackLerpMove()
                {
                    if (Collected.Count > 1)
                    {
                        for (int i = 1; i < Collected.Count; i++)
                        {
                            var FirstBall = Collected.ElementAt(i - 1);
                            var SectBall = Collected.ElementAt(i);
                                SectBall.transform.position = new Vector3(
                                Mathf.Lerp(SectBall.transform.position.x, FirstBall.transform.position.x,10 * Time.deltaTime),
                                Mathf.Lerp(SectBall.transform.position.y,FirstBall.transform.position.y, 5 * Time.deltaTime),
                                Mathf.Lerp(SectBall.transform.position.z, FirstBall.transform.position.z - 1f, 15*Time.deltaTime));
                        }
                    }
                }
        public IEnumerator CollectableScaleUp()
        {
            for (int i = Collected.Count -1; i >= 0; i--)
            {
                int index = i;
                Vector3 scale = Vector3.one * 1.5f;
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
        }
    }
}    

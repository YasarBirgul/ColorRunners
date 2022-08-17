using System;
using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class DroneAreaColorController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables

        public CollectableMatchType DroneAreaMatchType;
        
        public ColorType ColorType;
       
        public bool login;
       
        #endregion
    
        #region Private Variables

        private MeshRenderer _meshRenderer;
        
        #endregion
    
        #endregion
        private void Awake()
        {
            DroneAreaMatchType = CollectableMatchType.UnMatch;
            GetReferences();
        }
        private void Start()
        {
            SetGateMaterial(ColorType);
        }
        private void GetReferences()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        public void SetGateMaterial(ColorType colorType)
        {
            _meshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
        }
        public void Scale()
        { 
            if (DroneAreaMatchType == CollectableMatchType.UnMatch)
            {
                transform.DOScaleZ(0, 1f).OnComplete(() =>
                {
                    transform.gameObject.SetActive(false);
                });
            }
        }
    }
}


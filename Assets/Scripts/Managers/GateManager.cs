using Enums;
using UnityEngine;

namespace Managers
{ 
    public class GateManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        
        public ColorType Color;
        
        #endregion

        #region Private
        
        private MeshRenderer _meshRenderer;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            GetReferences();
        }
        private void Start()
        {
            SetGateMaterial(Color);
        }
        private void GetReferences()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
        private void SetGateMaterial(ColorType colorType)
        {
            _meshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
        }
    }
}
    


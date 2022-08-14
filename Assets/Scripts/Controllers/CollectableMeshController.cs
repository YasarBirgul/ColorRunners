using Enums;
using UnityEngine;

namespace Controllers
{
    public class CollectableMeshController : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
    
        #endregion
    
        #region Serialized Variables
        
        [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    
        #endregion
    
        #endregion
        public void GetColor(ColorType colorType)
        {
            skinnedMeshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
        }
    }
}


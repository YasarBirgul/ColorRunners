using DG.Tweening;
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


        public void OutlineChanger(bool outlineOn)
        {
            var matColor = skinnedMeshRenderer.material;
            
            if (outlineOn)
            {
                matColor.DOFloat(0f, "_OutlineSize", 1f);
            }
            else
            {
                matColor.DOFloat(100f, "_OutlineSize", 1f);
            }
        }
        
    }
}


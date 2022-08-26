using Enums;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    #region Self Variables
    
    #region Public Variables
    
    #endregion
    
    #region Serialized Variables
        
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    #endregion
    
    #endregion
    public void SetColor(ColorType colorType)
    {
        skinnedMeshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
    }
    public void IncreaseSize()
    {
        var PlayerScale = transform.parent.localScale;
        
        PlayerScale += new Vector3(1f, 1f, 1f);
        
       // if (PlayerScale.x >= 2)
       // { 
       //     PlayerScale = Vector3.one*2;
       // }
    }
}

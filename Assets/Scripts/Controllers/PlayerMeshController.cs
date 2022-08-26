using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

public class PlayerMeshController : MonoBehaviour
{
    #region Self Variables
    
    #region Public Variables
    
    #endregion
    
    #region Serialized Variables
        
    [SerializeField] private SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] private PlayerManager playerManager;
    #endregion
    
    #endregion
    public void SetColor(ColorType colorType)
    {
        skinnedMeshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
    }
    public void IncreaseSize()
    {
        playerManager.transform.DOScale(playerManager.transform.localScale*2, 2.0f);
    }
}

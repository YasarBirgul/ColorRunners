using System.Collections;
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
    
    #region Serialized Variables
    
    #endregion
    
    #endregion
    public void SetColor(ColorType colorType)
    {
        skinnedMeshRenderer.material = Resources.Load<Material>($"Materials/{colorType}");
    }
    public void IncreaseSize()
    {
        var PlayerManagerScale = playerManager.transform.localScale;
        
        if (PlayerManagerScale.x < Vector3.one.x * 2f)
        {
            playerManager.transform.DOScale(Vector3.one*2f, 2.0f);
        }
    } 
    public void DecreaseSize()
    {
        var PlayerManagerScale = playerManager.transform.localScale;

        if (PlayerManagerScale.x > Vector3.one.x*1f)
        {
            playerManager.transform.DOScale(Vector3.one, 2f);
        }
    }
}

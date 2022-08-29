using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class SideBuildingMeshController : MonoBehaviour
    {
        #region SelfVariables
    
        #region Public Variables
          
        public float Saturation;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private BuildingManager manager;
        [SerializeField] private List<MeshRenderer> renderer;

        #endregion

        #region Private Variables

        #endregion

        #endregion
        
        public void CalculateSaturation()
        {
            Saturation =(float)manager.buildingsData.SideObject.PayedAmount/manager.buildingsData.SideObject.MarketPrice*3f;
            SaturationChange(Saturation);
        }
        public void SaturationChange(float saturation)
        {
            for (int i = 0; i < renderer.Count; i++)
            {
                var matSaturation = renderer[i].material;
                matSaturation.DOFloat(saturation, "_Saturation", 1f);
            }
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class BuildingMeshController : MonoBehaviour
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
            Saturation =(float)manager.buildingsData.PayedAmount/manager.buildingsData.BuildingMarketPrice*3f;
            SaturationChange(Saturation);
        }
        private void SaturationChange(float saturation)
        {
            foreach (var matSaturation in renderer.Select(t => t.material))
            {
                matSaturation.DOFloat(saturation, "_Saturation", 1f);
            }
        }
    }
}
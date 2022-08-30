using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class UIPanelController : MonoBehaviour
    {
        [SerializeField] private List<GameObject> uıPanelList = new List<GameObject>();
        public void OpenPanel(UIPanels panelParam)
        {
            uıPanelList[(int) panelParam].SetActive(true);
        }

        public void ClosePanel(UIPanels panelParam)
        {
            uıPanelList[(int) panelParam].SetActive(false);
        }
    }
}
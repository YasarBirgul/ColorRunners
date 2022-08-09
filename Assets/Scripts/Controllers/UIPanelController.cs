using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class UIPanelController
    {
        [SerializeField] private List<GameObject> UIPanelList = new List<GameObject>();
        public void OpenPanel(UIPanels panelParam)
        {
            UIPanelList[(int) panelParam].SetActive(true);
        }

        public void ClosePanel(UIPanels panelParam)
        {
            UIPanelList[(int) panelParam].SetActive(false);
        }
    }
}
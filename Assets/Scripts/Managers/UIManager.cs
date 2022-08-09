using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables
        #region public vars
        #endregion
        #region serializefield vars

        [SerializeField] private TextMeshProUGUI levelText;
        [SerializeField] private TextMeshProUGUI _stickyManText;

        #endregion
        #region private vars
       
        private UIPanelController _uiPanelController;
        private int _levelID;
        private int _StickyMan;

        #endregion
        #endregion
        
       // private void OnEnable()
       // {
       //     SubscribeEvents();
       // }
       // 
      // #region Event Subscriptions

      // private void SubscribeEvents()
      // {
      //     UISignals.Instance.onOpenPanel += OnOpenPanel;
      //     UISignals.Instance.onClosePanel += OnClosePanel;
      //     CoreGameSignals.Instance.onPlay += OnPlay;
      //     CoreGameSignals.Instance.onLevelFailed += OnLevelFailed;
      //     CoreGameSignals.Instance.onLevelSuccessful += OnLevelSuccessful;
      // }

      // private void UnsubscribeEvents()
      // {
      //     UISignals.Instance.onOpenPanel -= OnOpenPanel;
      //     UISignals.Instance.onClosePanel -= OnClosePanel;
      //     CoreGameSignals.Instance.onPlay -= OnPlay;
      //     CoreGameSignals.Instance.onLevelFailed -= OnLevelFailed;
      //     CoreGameSignals.Instance.onLevelSuccessful -= OnLevelSuccessful;
      // }

      // private void OnDisable()
      // {
      //     UnsubscribeEvents();
      // }
      // #endregion

      // private void OnOpenPanel(UIPanels panelParam)
      // {
      //     _uiPanelController.OpenPanel(panelParam);
      // }

    }
}


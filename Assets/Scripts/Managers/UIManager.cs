using Controllers;
using Enums;
using Signals;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace Managers
{
    public class UIManager : MonoBehaviour
    {
        #region Self Variables
        #region Public Vars
        #endregion
        #region Serializefield Variables
        #endregion
        #region Private Vars
       
        [SerializeField] private UIPanelController _uiPanelController;
        [SerializeField] private Image _joyStickUIPanel;
        [SerializeField] private Image _joyStickUIPanel2;

        #endregion
        #endregion
        
       private void OnEnable()
       {
           SubscribeEvents();
       }
       #region Event Subscriptions
      private void SubscribeEvents()
      {
          UISignals.Instance.onOpenPanel += OnOpenPanel;
          UISignals.Instance.onClosePanel += OnClosePanel;
          CoreGameSignals.Instance.onPlay += OnPlay;
          CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
      }

      private void UnsubscribeEvents()
      {
          UISignals.Instance.onOpenPanel -= OnOpenPanel;
          UISignals.Instance.onClosePanel -= OnClosePanel;
          CoreGameSignals.Instance.onPlay -= OnPlay;
          CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
      }

      private void OnDisable()
      {
          UnsubscribeEvents();
      }
      #endregion
      private void OnOpenPanel(UIPanels panel)
      {
          _uiPanelController.OpenPanel(panel);
      }
      private void OnClosePanel(UIPanels panel)
      {
          _uiPanelController.ClosePanel(panel);
      }
      private void OnPlay()
      {
          UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
      }
      public void PlayButton()
      {
          CoreGameSignals.Instance.onPlay?.Invoke();
      }

      void OnChangeGameState(GameStates Current)
      {
          
      }
    }
}


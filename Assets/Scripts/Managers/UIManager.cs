using Commands.UI;
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
        #region Public Vars
        #endregion
        #region Serializefield Variables
        
        [SerializeField] private UIPanelController uiPanelController;

        [SerializeField] private GameObject joystickInner;
        [SerializeField] private GameObject joystickOuter;
        [SerializeField] private UIRoulletteController uıRoulletteController;
        [SerializeField] private TextMeshProUGUI levelText;

        #endregion
        
        #region Private Vars

        private JoyStickStateCommand _joyStickStateCommand;
        private GameStates _currentGameState;

        #endregion
        #endregion

        private void Awake()
        {
            Init();
        }

        void Init()
        {
            _joyStickStateCommand = new JoyStickStateCommand();
        }
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
          UISignals.Instance.onSetLevelText += OnSetLevelText;
          LevelSignals.Instance.onLevelFailed += OnLevelFailed;
      }

      private void UnsubscribeEvents()
      {
          UISignals.Instance.onOpenPanel -= OnOpenPanel;
          UISignals.Instance.onClosePanel -= OnClosePanel;
          CoreGameSignals.Instance.onPlay -= OnPlay;
          CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
          UISignals.Instance.onSetLevelText -= OnSetLevelText;
          LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
      }

      private void OnDisable()
      {
          UnsubscribeEvents();
      }
      #endregion
      private void OnOpenPanel(UIPanels panel)
      {
          uiPanelController.OpenPanel(panel);
      }
      private void OnClosePanel(UIPanels panel)
      {
          uiPanelController.ClosePanel(panel);
      }
      private void OnPlay()
      {
          UISignals.Instance.onClosePanel?.Invoke(UIPanels.StartPanel);
      }
      public void PlayButton()
      {
          CoreGameSignals.Instance.onPlay?.Invoke();
      }
      public void ClaimButton()
      { 
          OnClosePanel(UIPanels.RoullettePanel);
          CoreGameSignals.Instance.onChangeGameState?.Invoke(GameStates.Idle);
          OnOpenPanel(UIPanels.IdlePanel);
      }
      void OnChangeGameState(GameStates Current)
      {
          ChangeUIState(Current);
      } 
      private void ChangeUIState(GameStates Current)
      {
          switch (Current)
          {
              case GameStates.Roullette:
                  OnOpenPanel(UIPanels.RoullettePanel);
                  break;
              case GameStates.Idle:
                  _joyStickStateCommand.JoystickUIStateChanger(Current,joystickOuter,joystickInner);
                  OnOpenPanel(UIPanels.IdlePanel);
                  break;
          }
      }
      private void OnSetLevelText(int levelID)
      {
          if (levelID == 0)
          {
              levelID = 1;
              levelText.text = "level " + levelID.ToString();
              OnOpenPanel(UIPanels.LevelPanel);
          }
      } 
      private void OnLevelFailed()
      {
          OnOpenPanel(UIPanels.FailPanel);
      }
    }
}


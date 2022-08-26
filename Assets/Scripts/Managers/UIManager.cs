using Commands.UI;
using Controllers;
using Enums;
using Signals;
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

      void OnChangeGameState(GameStates Current)
      {
          _currentGameState = Current;
          ChangeUIState(_currentGameState);
      } 
      private void ChangeUIState(GameStates Current)
      {
          _joyStickStateCommand.JoystickUIStateChanger(Current,joystickOuter,joystickInner);
      }
    }
}


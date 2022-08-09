using Signals;
using UnityEngine;

namespace Managers
{
    public class GameManager : MonoBehaviour
    {
        public GameStates CurrentState;
        private void Awake()
        {
            Application.targetFrameRate = 60;
            OnGameOpen();
        }
        #region Event Subsription

        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
            OnGameClose();
        }
        #endregion
        private void OnGameOpen()
        {
            CurrentState = GameStates.Runner;
            CoreGameSignals.Instance.onGameOpen?.Invoke();
        }
        private void OnGameClose()
        {
            CoreGameSignals.Instance.onGameClose?.Invoke();
        }
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                CoreGameSignals.Instance.onGamePause?.Invoke(true);
            }
        }
        private void OnApplicationPause(bool paused)
        { 
            if (paused)
            { 
                CoreGameSignals.Instance.onGamePause?.Invoke(true);
            }
        } 
        private void OnChangeGameState(GameStates NextState)
        {
            CurrentState = NextState;
        }
    }
}
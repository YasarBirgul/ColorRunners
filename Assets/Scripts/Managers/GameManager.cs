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
            CoreGameSingals.Instance.onChangeGameState += OnChangeGameState;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSingals.Instance.onChangeGameState -= OnChangeGameState;
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
            CoreGameSingals.Instance.onGameOpen?.Invoke();
        }
        private void OnGameClose()
        {
            CoreGameSingals.Instance.onGameClose?.Invoke();
        }
        private void OnApplicationFocus(bool hasFocus)
        {
            if (!hasFocus)
            {
                CoreGameSingals.Instance.onGamePause?.Invoke(true);
            }
        }
        private void OnApplicationPause(bool paused)
        { 
            if (paused)
            { 
                CoreGameSingals.Instance.onGamePause?.Invoke(true);
            }
        } 
        private void OnChangeGameState(GameStates NextState)
        {
            CurrentState = NextState;
        }
    }
}
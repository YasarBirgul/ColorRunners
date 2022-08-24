using System;
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
            GameOpen();
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
            GameClose();
        }
        #endregion
        private void GameOpen()
        {
            CurrentState = GameStates.Runner;
            CoreGameSignals.Instance.onGameOpen?.Invoke();
        }
        private void GameClose()
        {
            CoreGameSignals.Instance.onGameClose?.Invoke();
        }
        private void OnApplicationPause(bool paused)
        { 
            if (paused)
            { 
                CoreGameSignals.Instance.onGamePause?.Invoke();
            }
        } 
        private void OnChangeGameState(GameStates NewCurrentState)
        {
            CurrentState = NewCurrentState;
        }

        private void OnApplicationQuit()
        {
            
        }
    }
}
using System;
using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables

        [SerializeField] private GameObject miniGamePlayer;
        [Space][SerializeField] private PlayerMovementController movementController;
        [SerializeField] private PlayerAnimationController playerAnimationController;

        #region Private Variables

        #endregion

        #endregion

        
        #endregion


        private void Awake()
        {
            Data = GetPlayerData();
            SendPlayerDataToControllers();
        }
        private PlayerData GetPlayerData() => Resources.Load<CD_Player>("Data/CD_Player").PlayerData;
        private void SendPlayerDataToControllers()
        {
            movementController.SetMovementData(Data.PlayerMovementData);
        }
       
        #region EventSubscription,
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        { 
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged+= OnGetRunnerInputValues;
            InputSignals.Instance.onIdleInputDragged+= OnGetIdleInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
        } 
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged-= OnGetRunnerInputValues;
            InputSignals.Instance.onIdleInputDragged -= OnGetIdleInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
        }

        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnActivateMovement()
        {
            movementController.EnableMovement();
        }

        private void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
        } 
        private void OnGetRunnerInputValues(RunnerGameInputParams runnerGameInputParams)
        {
            movementController.UpdateRunnerInputValue(runnerGameInputParams);
        }
        private void OnGetIdleInputValues(IdleGameInputParams IdleGameInputParams)
        {
            movementController.UpdateIdleInputValue(IdleGameInputParams);
        }
        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        }
        private void OnReset()
        {
            movementController.OnReset();
        }
    }
}

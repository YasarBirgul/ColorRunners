using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
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

        public GameStates CurrentGameState;

        public ColorType PlayerColorType;
        
        #endregion

        #region Serialized Variables
        
        [Space][SerializeField] private PlayerMovementController movementController;

        [SerializeField] private PlayerPhysicsController physicsController;

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
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        { 
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged+= OnGetRunnerInputValues;
            InputSignals.Instance.onIdleInputDragged+= OnGetIdleInputValues;
            
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged -= OnGetRunnerInputValues;
            InputSignals.Instance.onIdleInputDragged -= OnGetIdleInputValues;
           ;
           
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
            movementController.SetRunnerMovementValues(0,0);
        } 
        private void OnGetRunnerInputValues(RunnerGameInputParams runnerGameInputParams)
        {
            movementController.UpdateRunnerInputValue(runnerGameInputParams);
        }
        private void OnGetIdleInputValues(IdleGameInputParams idleGameInputParams)
        {
            movementController.UpdateIdleInputValue(idleGameInputParams);
        }
        public void SendGateColorData(ColorType colorType)
        {
            StackSignals.Instance.onColorChange?.Invoke(colorType);
            PlayerColorType = colorType;
        }
        public void ChangeState(GameStates state)
        {
            CoreGameSignals.Instance.onChangeGameState?.Invoke(state);
        }
        public void StopVerticalMovement()
        {
            movementController.StopVerticalMovement();
        }
        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        } 
        private void OnReset()
        {
            movementController.Reset();
        }
        private void OnChangeGameState(GameStates CurrentState)
        {
            CurrentGameState = CurrentState;
        }
        public void OnEnableFinalCollider(GameObject other)
        {
            movementController.StartVerticalMovement(other.gameObject);
        }
    }
}

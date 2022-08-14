using Controllers;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Keys;
using Signals;
using UnityEngine;
using UnityEngine.XR;

namespace Managers
{ 
    public class PlayerManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public PlayerData Data;

        #endregion

        #region Serialized Variables
        
        [Space][SerializeField] private PlayerMovementController movementController; 
        // [SerializeField] private PlayerAnimationController playerAnimationController;

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
            InputSignals.Instance.onInputTaken += OnActivateMovement;
            InputSignals.Instance.onInputReleased += OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged+= OnGetRunnerInputValues;
            InputSignals.Instance.onIdleInputDragged+= OnGetIdleInputValues;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            CollectableSignals.Instance.onExitGroundCheck += OnExitGroundCheck;
        } 
        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onInputTaken -= OnActivateMovement;
            InputSignals.Instance.onInputReleased -= OnDeactiveMovement;
            InputSignals.Instance.onRunnerInputDragged -= OnGetRunnerInputValues;
            InputSignals.Instance.onIdleInputDragged -= OnGetIdleInputValues;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            CollectableSignals.Instance.onExitGroundCheck -= OnExitGroundCheck;
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
        }
        public void ChangeState(GameStates state)
        {
            CoreGameSignals.Instance.onChangeGameState?.Invoke(state);
        }

        public void OnExitGroundCheck(GameObject go)
        {
            
        }
        private void OnPlay()
        {
            movementController.IsReadyToPlay(true);
        } 
        private void OnReset()
        {
            movementController.OnReset();
        }
        private void OnChangeGameState(GameStates CurrentState)
        {
            movementController.CurrentState(CurrentState);
        }
        public void EnteredDroneArea()
        {
            movementController.DeactiveMovement();
        }
    }
}

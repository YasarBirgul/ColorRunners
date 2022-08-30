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

        [SerializeField] private PlayerMeshController playerMeshController;

        [SerializeField] private GameObject playerMeshGO;

        [SerializeField] private PlayerAnimationController animationController;

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
            PlayerSignal.Instance.onIncreaseScale += OnIncreaseScale; 
            PlayerSignal.Instance.onDecreaseScale += OnDecreaseScale;
            LevelSignals.Instance.onLevelFailed += OnLevelFailed;
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
            PlayerSignal.Instance.onIncreaseScale -= OnIncreaseScale;
            PlayerSignal.Instance.onDecreaseScale -= OnDecreaseScale;
            LevelSignals.Instance.onLevelFailed -= OnLevelFailed;
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
        public void OnDeactiveMovement()
        {
            movementController.DeactiveMovement();
            movementController.SetRunnerMovementValues(0,0);
            animationController.ChangeCollectableAnimation(PlayerAnimationStates.Idle);
        } 
        private void OnGetRunnerInputValues(RunnerGameInputParams runnerGameInputParams)
        {
            movementController.UpdateRunnerInputValue(runnerGameInputParams);
        }
        private void OnGetIdleInputValues(IdleGameInputParams idleGameInputParams)
        {
            movementController.UpdateIdleInputValue(idleGameInputParams);
            animationController.ChangeCollectableAnimation(PlayerAnimationStates.Running);
            Vector3 movementDirection = new Vector3(idleGameInputParams.XValue, 0, idleGameInputParams.ZValue);
            if (movementDirection != Vector3.zero)
            {
                Quaternion toRotation = Quaternion.LookRotation(movementDirection,Vector3.up);
                animationController.transform.rotation = Quaternion.RotateTowards( animationController.transform.rotation,toRotation,30);
            }
        }
        public void SendGateColorData(ColorType colorType)
        {
            StackSignals.Instance.onColorChange?.Invoke(colorType);
            PlayerColorType = colorType;
            playerMeshController.SetColor(colorType);
        }
        public void ChangeState(GameStates Currentstate)
        {
            CoreGameSignals.Instance.onChangeGameState?.Invoke(Currentstate);
            if (Currentstate == GameStates.Runner)
            {
                playerMeshGO.SetActive(false);
            }
            else
            {
                playerMeshGO.SetActive(true);
            }
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
            gameObject.SetActive(false);
        }
        private void OnChangeGameState(GameStates CurrentState)
        {
            CurrentGameState = CurrentState;
            if (CurrentState == GameStates.Idle)
            { 
                ActivateAllMovement(true);
            }
        }
        public void OnEnableFinalCollider(GameObject other)
        {
            movementController.StartVerticalMovement(other.gameObject);
        }
        public void ActivateAllMovement(bool activate)
        {
            movementController.IsReadyToPlay(activate);
        }
        private void OnIncreaseScale()
        { 
            if (CurrentGameState == GameStates.Roullette)
            {
                playerMeshController.IncreaseSize();
            }
        }
        private void OnDecreaseScale()
        { 
            if (CurrentGameState == GameStates.Idle)
            {
                playerMeshController.DecreaseSize();
            }
        }
        private void OnLevelFailed()
        {
            ActivateAllMovement(false);
        }
    }
}

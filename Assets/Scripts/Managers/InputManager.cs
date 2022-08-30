using Commands.Input;
using Datas.UnityObject;
using Datas.ValueObject;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Serialization;

namespace Managers
{ 
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables,
        
        [FormerlySerializedAs("joystickRunner")] [SerializeField] private FloatingJoystick joystickInput;
        
        #endregion

        #region Private Variables
        
        [ShowInInspector] private bool _isReadyForTouch, _isFirstTimeTouchTaken;
        
        private GameStates _currentState = GameStates.Runner;

        private IdleGameInputUpdateCommand _idleGameInputUpdateCommand;

        private RunnerGameInputUpdateCommand _runnerGameInputUpdateCommand;

        private bool _isTouching;

        private float _currentVelocity;
        
        private Vector2? _mousePosition; 
        
        private Vector3 _moveVector; 

        #endregion

        #endregion
        private void Awake()
        {
            Data = GetInputData();
            Init();
        }
        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;

        private void Init()
        {
            _idleGameInputUpdateCommand = new IdleGameInputUpdateCommand();
            _runnerGameInputUpdateCommand = new RunnerGameInputUpdateCommand();
        }
        
        #region Event Subscriptions
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            InputSignals.Instance.onEnableInput += OnEnableInput;
            InputSignals.Instance.onDisableInput += OnDisableInput;
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onReset += OnReset;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
        }

        private void UnsubscribeEvents()
        {
            InputSignals.Instance.onEnableInput -= OnEnableInput;
            InputSignals.Instance.onDisableInput -= OnDisableInput;
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onReset -= OnReset;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        private void Update()
        {
            if (_currentState == GameStates.Runner)
            {
                _runnerGameInputUpdateCommand.RunnerInputUpdate(joystickInput,Data);
            }
            if(_currentState == GameStates.Idle)
            {
                _idleGameInputUpdateCommand.IdleInputUpdate(joystickInput);
            }
        }
        private void OnEnableInput()
        {
            _isReadyForTouch = true;
        }
        private void OnDisableInput()
        {
            _isReadyForTouch = false;
        }
        
        private void OnPlay()
        {
            _isReadyForTouch = true;
        }
        void OnChangeGameState(GameStates currentState)
        {
            _currentState = currentState;
        }
        private void OnReset()
        {
            _isTouching = false;
            _isReadyForTouch = false;
            _isFirstTimeTouchTaken = false;
        }
    }
}


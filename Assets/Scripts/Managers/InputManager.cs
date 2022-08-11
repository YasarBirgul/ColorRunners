using System.Collections.Generic;
using Commands.Input;
using Datas.UnityObject;
using Datas.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;
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

        [ShowInInspector] private bool isReadyForTouch, isFirstTimeTouchTaken;

        
        #endregion

        #region Private Variables
        
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
            else
            {
                _idleGameInputUpdateCommand.IdleInputUpdate(joystickInput);
            }
        }
        private void OnEnableInput()
        {
            isReadyForTouch = true;
        }
        private void OnDisableInput()
        {
            isReadyForTouch = false;
        }
        
        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        }
        private void OnPlay()
        {
            isReadyForTouch = true;
        }
        void OnChangeGameState(GameStates CurrentState)
        {
            _currentState = CurrentState;
        }
        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }
    }
}


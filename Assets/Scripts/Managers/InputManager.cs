using System.Collections.Generic;
using Datas.UnityObject;
using Datas.ValueObject;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{ 
    public class InputManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        [Header("Data")] public InputData Data;

        #endregion

        #region Serialized Variables,
        
        [SerializeField] private FloatingJoystick joystickRunner;

        [ShowInInspector] private bool isReadyForTouch, isFirstTimeTouchTaken;

        private GameStates _currentState = GameStates.Runner;
        #endregion

        #region Private Variables

        
            
        private bool _isTouching;

        private float _currentVelocity;
        
        private Vector2? _mousePosition; 
        
        private Vector3 _moveVector; 

        #endregion

        #endregion
        
        private void Awake()
        {
            Data = GetInputData();
        }

        private InputData GetInputData() => Resources.Load<CD_Input>("Data/CD_Input").InputData;
        
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
                RunnerInput();
            }
            else
            {
                IdleInput();
            }
        }
        private void IdleInput()
        {
            if (joystickRunner.Horizontal != 0 || joystickRunner.Vertical != 0)
            {
                InputSignals.Instance.onIdleInputDragged?.Invoke(new IdleGameInputParams()
                {
                    XValue = joystickRunner.Horizontal,
                    ZValue = joystickRunner.Vertical,
                });
                InputSignals.Instance.onInputTaken?.Invoke();
            }

            if (joystickRunner.Horizontal == 0f)
            {
                InputSignals.Instance.onInputReleased?.Invoke();
            }
        }

        private void RunnerInput()
        {
            if (joystickRunner.Horizontal > 0.1f || joystickRunner.Horizontal < -0.1f)
            {
                InputSignals.Instance.onRunnerInputDragged?.Invoke(new RunnerGameInputParams()
                {
                    XValue = joystickRunner.Horizontal,
                    ClampValues = new Vector2(Data.ClampSides.x, Data.ClampSides.y),
                });
                InputSignals.Instance.onInputTaken?.Invoke();
            }

            if (joystickRunner.Horizontal == 0f)
            {
                InputSignals.Instance.onInputReleased?.Invoke();
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

        private void OnPlay()
        {
            isReadyForTouch = true;
        }
        void OnChangeGameState(GameStates CurrentState)
        {
            _currentState = CurrentState;
        }
        private bool IsPointerOverUIElement()
        {
            var eventData = new PointerEventData(EventSystem.current);
            eventData.position = Input.mousePosition;
            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(eventData, results);
            return results.Count > 0;
        } 
        private void OnReset()
        {
            _isTouching = false;
            isReadyForTouch = false;
            isFirstTimeTouchTaken = false;
        }
    }
}


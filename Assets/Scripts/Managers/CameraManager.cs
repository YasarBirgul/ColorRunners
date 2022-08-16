using Cinemachine;
using Enums;
using Keys;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager: MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        public CinemachineVirtualCamera RunnerCamera;
        public CinemachineVirtualCamera IdleGameCamera;
        private GameStates _gameCurrentState;
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables
        
        [ShowInInspector] private Vector3 _initialPosition;
        private CinemachineTransposer _miniGameTransposer;
        private  Animator _animator;
        private CameraStatesType _cameraStatesType = CameraStatesType.Runner;
       
        #endregion
        
        #endregion

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _miniGameTransposer = IdleGameCamera.GetCinemachineComponent<CinemachineTransposer>();
            GetInitialPosition();
        }

        #region Event Subscriptions
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += SetCameraTarget;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            CameraSignals.Instance.onEnterMiniGame += OnPlayerEnterMiniGame;
            CameraSignals.Instance.onExitMiniGame+= OnPlayerExitMiniGame;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= SetCameraTarget;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            CameraSignals.Instance.onEnterMiniGame -= OnPlayerEnterMiniGame;
            CameraSignals.Instance.onExitMiniGame -= OnPlayerExitMiniGame;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        
        private void GetInitialPosition()
        {
            _initialPosition = transform.localPosition;
        }
        private void OnMoveToInitialPosition()
        {
            transform.localPosition = _initialPosition;
        }
        private void SetCameraTarget()
        {
            CameraSignals.Instance.onSetCameraTarget?.Invoke();
        }
        private void OnSetCameraTarget()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            RunnerCamera.Follow = playerManager;
            IdleGameCamera.Follow = playerManager;
        }
        private void OnPlayerEnterMiniGame()
        {
            RunnerCamera.Follow = null;
        } 
        private void OnPlayerExitMiniGame()
        {
            var playerManager = FindObjectOfType<PlayerManager>().transform;
            RunnerCamera.Follow = playerManager;
            IdleGameCamera.Follow = playerManager;
        }
        private void OnReset()
        {
            RunnerCamera.Follow = null;
            RunnerCamera.LookAt = null;
            OnMoveToInitialPosition();
        }
        void OnChangeGameState(GameStates Current)
        {
            _gameCurrentState = Current;
            SetCameraState(_gameCurrentState);
        }
        private void SetCameraState(GameStates Current)
        {
            if (Current == GameStates.Idle)
            {
                _cameraStatesType = CameraStatesType.Idle;
                _animator.Play("IdleCam");
            }
            if (Current == GameStates.Runner)
            {
                _cameraStatesType = CameraStatesType.Runner;
                _animator.Play("RunnerCam");
            }
        }
    }
}
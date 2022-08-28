using System.Threading.Tasks;
using Cinemachine;
using Enums;
using Extentions;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class CameraManager: MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables

        public CinemachineStateDrivenCamera StateCam;
        
        #endregion

        #region Serialized Variables

        [SerializeField] private LookCinemachineAxis lookCinemachineAxis ;
        
        #endregion

        #region Private Variables
        
        
        [ShowInInspector] private Vector3 _initialPosition;
        private  Animator _animator;
        private CameraStatesType _cameraStatesType = CameraStatesType.Runner;
        private PlayerManager _playerManager;
        
        #endregion
        
        #endregion

        private void Awake()
        {
            GetReferences();
            GetInitialPosition();
        }
        private void GetReferences()
        {
            _animator = GetComponent<Animator>();
        }

        #region Event Subscriptions
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay += OnPlay;
            CoreGameSignals.Instance.onChangeGameState += OnChangeGameState;
            CameraSignals.Instance.onSetCameraTarget += OnSetCameraTarget;
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
        }
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onPlay -= OnPlay;
            CoreGameSignals.Instance.onChangeGameState -= OnChangeGameState;
            CameraSignals.Instance.onSetCameraTarget -= OnSetCameraTarget;
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
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
        private void OnPlay()
        {
            SetCameraTarget();
        }

        private void SetCameraTarget()
        {
            CameraSignals.Instance.onSetCameraTarget?.Invoke(CameraStatesType.Runner);
        }

        private void OnSetCameraTarget(CameraStatesType Currentstate)
        {
            if (!_playerManager)
            { 
                _playerManager = FindObjectOfType<PlayerManager>();
            }
            if (Currentstate == CameraStatesType.Runner)
            {
                StateCam.Follow = _playerManager.transform;
                StateCam.LookAt = null;
                lookCinemachineAxis.enabled = true;
            }
            else 
            {
                StateCam.LookAt = _playerManager.transform;
                lookCinemachineAxis.enabled = false;
            }
            _animator.Play(Currentstate.ToString());
        }
        private void OnReset()
        {
            CameraTargetSetting();
        }
        void OnChangeGameState(GameStates currentGameState)
        {
            switch (currentGameState)
            {
                case GameStates.Roullette:
                    _cameraStatesType = CameraStatesType.Idle;
                    OnSetCameraTarget(_cameraStatesType);
                    break;
                case GameStates.Runner:
                    _cameraStatesType = CameraStatesType.Runner; 
                    OnSetCameraTarget(_cameraStatesType); 
                    break;
            }
        }
        private  void OnNextLevel()
        {
            CameraTargetSetting();
        }
        private async void CameraTargetSetting()
        {  
            await Task.Delay(50);
            SetCameraTarget();
        }
    }
}
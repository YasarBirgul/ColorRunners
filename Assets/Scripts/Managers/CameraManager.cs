using Cinemachine;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{ 
    public class CameraManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Variables
        public CinemachineVirtualCamera RunnerCamera;
        public CinemachineVirtualCamera IdleGameCamera;
        
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables
        
        [ShowInInspector] private Vector3 _initialPosition;
        private CinemachineTransposer _miniGameTransposer;
        private  Animator _animator;
        private GameStates _cameraStatesType = GameStates.Runner;
       
        #endregion
        
        #endregion
    }
}


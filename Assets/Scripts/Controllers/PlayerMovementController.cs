using Datas.ValueObject;
using DG.Tweening;
using Keys;
using Managers;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Controllers
{
    public class PlayerMovementController : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables
        
        [SerializeField] private new Rigidbody rigidbody;
       
        [SerializeField] private PlayerManager playerManager;
        
        #endregion
        
        #region Private Variables
        
        [Header("Data")][ShowInInspector] private PlayerMovementData _movementData;
        
        [ShowInInspector] private bool _isReadyToMove, _isReadyToPlay,isMovingVertical ;
        
        [ShowInInspector] private float _inputValueX;
       
        [ShowInInspector] private float _inputValueZ;
        
        [ShowInInspector] private Vector2 _clampValues;

        private GameStates _currentState = GameStates.Runner;
        
        #endregion
        
        #endregion
        public void SetMovementData(PlayerMovementData dataMovementData)
        {
            _movementData = dataMovementData;
        }
        public void EnableMovement()
        {
            _isReadyToMove = true;
            
        }
        public void DeactiveMovement()
        {
            _isReadyToMove = false;
            
        }
        public void UpdateRunnerInputValue(RunnerGameInputParams inputParam)
        {
            _inputValueX = inputParam.XValue;
            _clampValues = inputParam.ClampValues;
        }
        public void UpdateIdleInputValue(IdleGameInputParams inputParam)
        {
            _inputValueX = inputParam.XValue;
            _inputValueZ = inputParam.ZValue;
            
        }
        public void IsReadyToPlay(bool state)
        {
            _isReadyToPlay = state;
            Stop();
        }
        private void FixedUpdate()
        {
           if (_isReadyToPlay)
           {
               if (_isReadyToMove)
               {
                    if (playerManager.CurrentGameState == GameStates.Runner)
                    {
                        RunnerMove();
                    }
                    if (playerManager.CurrentGameState == GameStates.Idle)
                    {
                        IdleMove();
                    } 
               }
               else
               {
                   if (playerManager.CurrentGameState == GameStates.Runner)
                   {
                       StopSideways(); 
                       
                   }
                   else if(playerManager.CurrentGameState == GameStates.Idle)
                   {
                       Stop();  
                   }
               } 
           }
        }
        private void RunnerMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValueX * _movementData.SidewaysSpeed, velocity.y,_movementData.forwardSpeed);
            rigidbody.velocity = velocity;

            Vector3 position;
            position = new Vector3( Mathf.Clamp(rigidbody.position.x, _clampValues.x,_clampValues.y),(position = rigidbody.position).y,position.z);
            rigidbody.position = position;
        }
        private void IdleMove()
        {
            var velocity = rigidbody.velocity;
            velocity = new Vector3(_inputValueX * _movementData.forwardSpeed, velocity.y,
                _inputValueZ * _movementData.forwardSpeed);
            rigidbody.velocity = velocity;
        }
        private void StopSideways()
        {
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y, _movementData.forwardSpeed);
            rigidbody.angularVelocity = Vector3.zero;
        }
        private void Stop()
        {
            rigidbody.velocity = Vector3.zero;
            rigidbody.angularVelocity = Vector3.zero;
        }
       
        public void SetRunnerMovementValues(float X,float Z)
        {
            _inputValueX = X;
            _inputValueZ = Z;
        }
        public void StopVerticalMovement()
        {
            rigidbody.angularVelocity = Vector3.zero;
            _movementData.forwardSpeed = 0;
        }

        public void StartVerticalMovement(GameObject other)
        {
            playerManager.transform.DOMove(new Vector3(0,playerManager.transform.position.y,
                other.transform.position.z + other.gameObject.transform.localScale.z), 2f).OnComplete(
                () => {
                    _movementData.forwardSpeed = 10;
                });
        }
        public void Reset()
        {
            Stop();
            _isReadyToPlay = false;
            _isReadyToMove = false;
        }
    }
}


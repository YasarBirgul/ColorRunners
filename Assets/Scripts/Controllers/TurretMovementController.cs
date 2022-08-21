using System;
using Enums;
using Keys;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;


namespace Controllers
{
    public class TurretMovementController : MonoBehaviour
    {
        #region Self Variables
        #region Public Variables
        #endregion
        #region Serialized Variables
    
        [SerializeField] private Transform tarretAreaTransform;
        [SerializeField] private float TimeIncreasedSpeed;
        [SerializeField] private float InvokeRate;
        #endregion
    
        #region Private Variables
    
        private Quaternion _rotation;
        private Vector3 _shotPos;
        private Vector3 _collectablePos;
        private Vector3 _relationPos;
        private float _randomClampStartPos;
        private float _randomClampEndPos;
        private TurretState _turretState;
        private float _turretStartXPos;
        private float _turretEndXpos;
        private float _turretStartZpos;
        private float _turretEndZpos;
        #endregion
        #endregion


        private void Awake()
        {
          _turretStartXPos = tarretAreaTransform.transform.parent.transform.position.x - tarretAreaTransform.GetChild(0).transform.localScale.x;
          _turretEndXpos = tarretAreaTransform.transform.parent.transform.position.x + tarretAreaTransform.GetChild(1).transform.localScale.x;
          _turretStartZpos = tarretAreaTransform.transform.parent.transform.position.z- tarretAreaTransform.GetChild(0).transform.localScale.z/2;
          _turretEndZpos = tarretAreaTransform.transform.parent.transform.position.z + tarretAreaTransform.GetChild(1).transform.localScale.z/2; 
        }
        private void Start()
        {
            InvokeRepeating("TurretPatrolling",0,InvokeRate);
        }
    
        public void DetectCollectable(GameObject detectedCollectable)
        {
            _collectablePos = detectedCollectable.transform.position;
            _turretState = TurretState.Active;
        }
        public void StopTurret()
        {
           CancelInvoke("TurretPatrolling");
           _turretState = TurretState.Patrol;
        } 
        private void TurretPatrolling()
        {
            _randomClampStartPos = Random.Range(_turretStartXPos, _turretEndXpos);
            _randomClampEndPos = Random.Range(_turretStartZpos, _turretEndZpos);
        } 
        private void FixedUpdate()
        {
            ChangeTurretMovementWithState(_turretState);
        } 
        private void ChangeTurretMovementWithState(TurretState turretState)
        {
            switch (turretState)
            {
                case TurretState.Patrol:
                    _shotPos = new Vector3(_randomClampStartPos, 0, _randomClampEndPos);
                    _relationPos = _shotPos - transform.position;
                    _rotation = Quaternion.LookRotation(_relationPos);
                    transform.rotation = Quaternion.Lerp(transform.rotation,_rotation,Mathf.Lerp(0,1,TimeIncreasedSpeed*10));
                    break;
                
                case TurretState.Active:
                    
                    _shotPos = _collectablePos + new Vector3(0, 1, 0);
                    _relationPos = _shotPos - transform.position;
                    _rotation = Quaternion.LookRotation(_relationPos);
                    transform.rotation = Quaternion.Lerp(transform.rotation,_rotation,Mathf.Lerp(0,1,TimeIncreasedSpeed*10));
                    ShotTheColletable();
                    break;
            }
        } 
        private void ShotTheColletable()
        { 
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.transform.name);
                Debug.DrawRay(transform.position, transform.forward*25,Color.green);
                int RandomInt = Random.Range(0, 100);
                if (hit.transform.CompareTag("Collected"))
                {
                    if (RandomInt <= 3)
                    {
                        StackSignals.Instance.onDecreaseStack?.Invoke(new ObstacleCollisionGOParams()
                        {
                            Collected = hit.transform.gameObject,
                        });
                    }
                }
            }
        }
    }

}

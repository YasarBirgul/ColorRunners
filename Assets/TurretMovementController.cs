using System;
using Enums;
using Managers;
using Signals;
using UnityEngine;
using Random = UnityEngine.Random;


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
    #endregion
    #endregion

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
      //  gameObject.SetActive(false);
    } 
    private void TurretPatrolling()
    {
        float TurretStartXPos =tarretAreaTransform.transform.parent.transform.position.x - tarretAreaTransform.GetChild(0).transform.localScale.x;
        float TurretEndXpos = tarretAreaTransform.transform.parent.transform.position.x + tarretAreaTransform.GetChild(1).transform.localScale.x;
        float TurretStartZpos = tarretAreaTransform.transform.parent.transform.position.z- tarretAreaTransform.GetChild(0).transform.localScale.z/2;
        float TurretEndZpos = tarretAreaTransform.transform.parent.transform.position.z + tarretAreaTransform.GetChild(1).transform.localScale.z/2;

        _randomClampStartPos = Random.Range(TurretStartXPos, TurretEndXpos);
        _randomClampEndPos = Random.Range(TurretStartZpos, TurretEndZpos);
        
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
                transform.rotation = Quaternion.Lerp(transform.rotation,_rotation,Mathf.Lerp(0,1,TimeIncreasedSpeed));
                ShotTheColletable();
                break;
            
        }
        
        Debug.LogWarning("Current : " + _turretState);
        
    } private void ShotTheColletable()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit,25f))
        {
            Debug.DrawRay(transform.position,transform.forward*15f,Color.red,0.5f);
            if (hit.transform.CompareTag("Collected"))
            {
                Debug.DrawRay(transform.position,transform.forward*15f,Color.green,0.5f);
                hit.transform.GetComponent<CollectableManager>().DelistFromStack();
            }
        }
    }
}

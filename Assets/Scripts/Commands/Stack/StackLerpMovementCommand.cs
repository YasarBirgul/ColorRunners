using System.Collections.Generic;
using UnityEngine;

namespace Commands.Stack
{
    public class StackLerpMovementCommand 
    {
        public void StackLerpMove(ref List<GameObject> _collectedList,Transform _playerManager,Vector3 _lerpSpeed,ref float _offSet)
        {
            if (_collectedList.Count > 0 )
            {
                _collectedList[0].transform.localPosition= new Vector3(
                    Mathf.Lerp(_collectedList[0].transform.localPosition.x, _playerManager.position.x,_lerpSpeed.x*Time.deltaTime),
                    Mathf.Lerp(_collectedList[0].transform.localPosition.y, _playerManager.position.y, _lerpSpeed.y*Time.deltaTime),
                    Mathf.Lerp(_collectedList[0].transform.localPosition.z, _playerManager.position.z -_offSet, _lerpSpeed.z*Time.deltaTime));
                        
                for (int i = 1; i < _collectedList.Count; i++)
                {
                    _collectedList[i].transform.position = new Vector3(
                        Mathf.Lerp(_collectedList[i].transform.localPosition.x, _collectedList[i-1].transform.localPosition.x,_lerpSpeed.x*Time.deltaTime),
                        Mathf.Lerp(_collectedList[i].transform.localPosition.y,_collectedList[i-1].transform.localPosition.y, _lerpSpeed.y*Time.deltaTime),
                        Mathf.Lerp(_collectedList[i].transform.localPosition.z, _collectedList[i-1].transform.localPosition.z -_offSet, _lerpSpeed.z*Time.deltaTime));
                }
            }
        }
    }
}
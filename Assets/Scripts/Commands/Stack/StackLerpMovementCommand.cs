using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Commands.Stack
{
    public class StackLerpMovementCommand 
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stackList;
        private StackData _stackData;
       
        #endregion
        
        #endregion
        public StackLerpMovementCommand(ref List<GameObject> stackList, ref StackData stackData)
        {
            _stackList = stackList;
            _stackData = stackData;
        }
        public void Execute(Transform _playerManager)
        {
            if (_stackList.Count > 0 )
            {
                _stackList[0].transform.localPosition= new Vector3(
                    Mathf.Lerp(_stackList[0].transform.localPosition.x, _playerManager.position.x,_stackData.LerpSpeed.x*Time.deltaTime),
                    Mathf.Lerp(_stackList[0].transform.localPosition.y, _playerManager.position.y, _stackData.LerpSpeed.y*Time.deltaTime),
                    Mathf.Lerp(_stackList[0].transform.localPosition.z, _playerManager.position.z -_stackData.StackDistanceZ, _stackData.LerpSpeed.z*Time.deltaTime));
                        
                for (int i = 1; i < _stackList.Count; i++)
                {
                    _stackList[i].transform.position = new Vector3(
                        Mathf.Lerp(_stackList[i].transform.localPosition.x, _stackList[i-1].transform.localPosition.x,_stackData.LerpSpeed.x*Time.deltaTime),
                        Mathf.Lerp(_stackList[i].transform.localPosition.y,_stackList[i-1].transform.localPosition.y, _stackData.LerpSpeed.y*Time.deltaTime),
                        Mathf.Lerp(_stackList[i].transform.localPosition.z, _stackList[i-1].transform.localPosition.z -_stackData.StackDistanceZ, _stackData.LerpSpeed.z*Time.deltaTime));
                }
            }
        }
    }
}
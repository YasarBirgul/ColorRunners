using System.Collections.Generic;
using Keys;
using Managers;
using UnityEngine;

namespace Commands.Stack
{
    public class CollectableRemoveOnStackCommand
    {
        #region Self Variables

        #region Private Variables
        private List<GameObject> _stackList;
        private StackManager _manager;
        #endregion

        #endregion

        public CollectableRemoveOnStackCommand(ref List<GameObject> stackList)
        {
            _stackList = stackList;
        }

        public void Execute(ObstacleCollisionGOParams obstacleCollisionGOParams)
        { 
            int CollidedObjectIndex = obstacleCollisionGOParams.Collected.transform.parent.GetSiblingIndex();
            _stackList[CollidedObjectIndex].SetActive(false);
            _stackList[CollidedObjectIndex].transform.parent = null;
            _stackList.RemoveAt(CollidedObjectIndex);
            _stackList.TrimExcess();
        }
    }
}
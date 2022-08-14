using System.Collections.Generic;
using Managers;
using UnityEngine;

namespace Commands.Stack
{
    public class CollectableAddOnStackCommand
    {
        #region Self Variables

        #region Private Variables

        private StackManager _stackManager;
        private List<GameObject> _stacklist;

        #endregion

        #endregion
        public CollectableAddOnStackCommand(ref StackManager stackManager, ref List<GameObject> stacklist)
        {
            _stacklist = stacklist;
            _stackManager = stackManager;
            
        }
        public void Execute(GameObject _collectable)
        {
            _collectable.transform.parent = _stackManager.transform;
            _stacklist.Add(_collectable);
        }
    }
}
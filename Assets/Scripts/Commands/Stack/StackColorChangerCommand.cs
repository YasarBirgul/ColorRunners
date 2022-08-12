using System.Collections.Generic;
using UnityEngine;

namespace Commands.Stack
{
    public class StackColorChangerCommand
    {
        #region Self Variables

        #region Private Variables

        private List<GameObject> _stacklist;

        #endregion

        #endregion

        public StackColorChangerCommand(ref List<GameObject> stacklist)
        {
            _stacklist = stacklist;
        }
        
        public void Execute(GameObject ColorChangerWall)
        {
            var collectableColor = ColorChangerWall.GetComponent<Renderer>().material.color;
            foreach (var item in _stacklist)
            {
                item.GetComponent<Renderer>().material.color = collectableColor;
            }
            if(ColorChangerWall.GetComponent<Collider>().enabled)
            {
                ColorChangerWall.GetComponent<Collider>().enabled = false;
            }
        }
    }
}
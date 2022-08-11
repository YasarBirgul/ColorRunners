using System.Collections.Generic;
using UnityEngine;

namespace Commands.Stack
{
    public class StackLerpMovementCommand 
    {
        public void StackLerpMove(ref List<GameObject> collectedList,Transform playerManager,ref float LerpSpeedX ,ref float LerpSpeedY,ref float LerpSpeedZ,ref float offset)
        {
            if (collectedList.Count > 0 )
            {
                collectedList[0].transform.localPosition= new Vector3(
                    Mathf.Lerp(collectedList[0].transform.localPosition.x, playerManager.position.x,LerpSpeedX),
                    Mathf.Lerp(collectedList[0].transform.localPosition.y, playerManager.position.y, LerpSpeedY),
                    Mathf.Lerp(collectedList[0].transform.localPosition.z, playerManager.position.z -offset, LerpSpeedZ));
                        
                for (int i = 1; i < collectedList.Count; i++)
                {
                    collectedList[i].transform.position = new Vector3(
                        Mathf.Lerp(collectedList[i].transform.localPosition.x, collectedList[i-1].transform.localPosition.x,LerpSpeedX),
                        Mathf.Lerp(collectedList[i].transform.localPosition.y,collectedList[i-1].transform.localPosition.y, LerpSpeedY),
                        Mathf.Lerp(collectedList[i].transform.localPosition.z, collectedList[i-1].transform.localPosition.z -offset, LerpSpeedZ));
                }
            }
        }
    }
}
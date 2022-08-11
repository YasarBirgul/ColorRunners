using System;
using System.Collections.Generic;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData
    {
        public int StackMemberAmount = 5;
        public List<GameObject> InitializedStack;
        public Vector3 LerpSpeed = new Vector3(0.2f, 0.2f, 1f);
        public float ScaleFactor = 1.5f;
        public float StackDistanceZ = 1f;
        public float StackScaleUpDelay = 0.2f;
    }
}
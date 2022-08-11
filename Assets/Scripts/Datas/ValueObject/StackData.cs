using System;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData
    {
        public int StackMemberAmount = 5;
        public float LerpSpeedX = 8;
        public float LerpSpeedY = 4;
        public float LerpSpeedZ = 15;
        public float ScaleFactor = 1.5f;
        public float StackDistanceZ = 1f;
    }
}
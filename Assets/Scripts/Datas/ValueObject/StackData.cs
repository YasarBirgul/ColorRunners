using System;

namespace Datas.ValueObject
{
    [Serializable]
    public class StackData
    {
        public int StackMemberAmount = 5;
        public float LerpSpeed;
        public float LerpDuration;
    }
}
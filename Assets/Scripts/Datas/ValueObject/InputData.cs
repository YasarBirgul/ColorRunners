using System;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class InputData
    {
        public float HorizontalInputSpeed = 2f;
        public Vector2 ClampSides = new Vector2(-3, 3);
        public float ClampSpeed = 0.007f;
    }
}
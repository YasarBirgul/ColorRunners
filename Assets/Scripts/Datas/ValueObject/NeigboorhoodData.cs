using System;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public class NeigboorhoodData
    {
        public int RequiredBuildings;

        public int PayedAmount;

        public IdleLevelStateType StateType;
    }
}
using System;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public class BuildingsData
    {
        public int BuildingCost;
        public int PayedAmount;
        public IdleLevelStateType IdleLevelStateType;
    }
}
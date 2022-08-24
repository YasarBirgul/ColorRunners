using System;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public class SideObjectData
    {
        public int MarketPrice;

        public int PayedAmount;

        public int BuildingAddressId;
        
        public float Saturation;

        public IdleLevelStateType IdleLevelStateType = IdleLevelStateType.Uncompleted;

    }
}
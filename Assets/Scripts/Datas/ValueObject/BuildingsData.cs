using System;
using Enums;

namespace Datas.ValueObject
{
    [Serializable] 
    public class BuildingsData
    {
        public int AddressId;
        public bool isDepended;
        public int buildingMarketPrice;
        public int PayedAmount;
        public float Saturation;
        public IdleLevelStateType IdleLevelStateType;
        public SideObjectData SideObjectData;
    }
}
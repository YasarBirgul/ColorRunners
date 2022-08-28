using System;
using Abstract;
using Enums;

namespace Datas.ValueObject
{
    [Serializable] 
    public class BuildingsData : SaveableEntity 
    {
        public string Key = "IdleBuildingDataKey";

        public bool IsDepended;

        public SideObjectData SideObject;
        
        public int BuildingMarketPrice;

        public int PayedAmount;

        public int BuildingAdressId;
        
        public float Saturation;
        
        public IdleLevelStateType idleLevelState;
        
        public BuildingsData() { }
        
        public BuildingsData(bool isDepended,SideObjectData sideObject,int buildingAdressId,int buildingMarketPrice,int payedAmount,float saturation,IdleLevelStateType idleLevelState)
        {
            IsDepended = isDepended;
            SideObject = sideObject;
            BuildingAdressId = buildingAdressId;
            BuildingMarketPrice = buildingMarketPrice;
            PayedAmount = payedAmount;
            Saturation = saturation;
            this.idleLevelState = idleLevelState;
        }
        public override string GetKey()
        {
            return Key;
        }
    }
}
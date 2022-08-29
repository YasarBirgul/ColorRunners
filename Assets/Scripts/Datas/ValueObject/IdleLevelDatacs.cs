using System;
using System.Collections.Generic;
using Abstract;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public class IdleLevelData : SaveableEntity
    {
        public string key = "IdleLevelDataKey";
        public List<BuildingsData> BuildingsDatas;
        public IdleLevelStateType IdleLevelStateType;
        public int CompletedCount;
        public IdleLevelData() {}
        public IdleLevelData(IdleLevelStateType idleLevelStateType,int completedCount)
        {
            IdleLevelStateType = idleLevelStateType;
            CompletedCount = completedCount;
        }
        public override string GetKey()
        {
            return key;
        }
    }
}
using System;
using System.Collections.Generic;
using Abstract;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public class IdleLevelData : SaveableEntity
    {
        public string key = "IdleLevelData";
        public int CompletedCount;
        
        public List<BuildingsData> BuildingsDatas;
        
        public IdleLevelStateType IdleLevelStateType;
        
        public IdleLevelData() { }
        public IdleLevelData(int count,IdleLevelStateType IdleLevelState)
        {
            CompletedCount = count;
            IdleLevelStateType = IdleLevelState;
        }
         public override string GetKey()
         {
             return key;
         }
       
    }
}
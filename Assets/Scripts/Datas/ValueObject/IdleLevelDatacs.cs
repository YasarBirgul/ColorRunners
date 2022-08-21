using System;
using System.Collections.Generic;
using Enums;

namespace Datas.ValueObject
{
    [Serializable]
    public class IdleLevelData 
    {
        public List<BuildingsData> BuildingsDatas;
        public NeigboorhoodData NeigboorhoodData;
        public IdleLevelStateType IdleLevelStateType;
    }
}
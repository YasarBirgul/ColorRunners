using System;
using Datas.ValueObject;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction<IdleLevelData,int> onSaveIdleLevelData = delegate{  };

        public Func<String, int, IdleLevelData> onLoadIdleLevelData;

        public UnityAction<LevelIdData,int> onSaveGameData = delegate { };
        
        public UnityAction<BuildingsData,int> onSaveBuildingsData = delegate { };

        public Func<string, int, LevelIdData> onLoadGameData;
        
        public Func<string, int, BuildingsData> onLoadBuildingsData;
        
    }
}
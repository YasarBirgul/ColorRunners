using System;
using Datas.ValueObject;
using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction<SaveStates, int> onSaveRunnerLevelData=delegate {  };
        public  Func<SaveStates,int> onLoadGameData= delegate { return 0;};
        public UnityAction<SaveStates,int> onSaveIdleLevelData = delegate{  };
        public Func<SaveStates,int> onLoadIdleData = delegate { return 0;};
        public Func<SaveStates,IdleLevelData,IdleLevelData> onLoadIdleLevelProgressData = delegate { return new IdleLevelData();};
        public UnityAction<SaveStates,IdleLevelData> onSaveIdleLevelProgressData = delegate { };
    }
}
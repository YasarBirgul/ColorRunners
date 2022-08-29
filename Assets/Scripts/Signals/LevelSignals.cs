using System;
using Extentions;
using UnityEngine.Events;

namespace Signals
{ 
    public class LevelSignals : MonoSingleton<LevelSignals>
    {
        public UnityAction onLevelInitialize = delegate { };
        public UnityAction onClearActiveLevel = delegate { };
        public UnityAction onLevelFailed = delegate { };
        public UnityAction onLevelSuccessful = delegate { };
        public UnityAction onNextLevel = delegate { };
        public UnityAction onRestartLevel = delegate { };
        public Func<int> onGetLevel= delegate { return 0; };
        public Func<int> onGetIdleLevelID= delegate { return 0; };
        public UnityAction onClearActiveIdleLevel = delegate {  };
        public UnityAction onInitializeIdleLevel = delegate {  };
    
    }
}
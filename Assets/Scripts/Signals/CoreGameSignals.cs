using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CoreGameSignals : MonoSingleton<CoreGameSignals>
    { 
        public UnityAction onGameOpen = delegate {  };
        public UnityAction onGameClose = delegate {  };
        public UnityAction onGamePause = delegate {  };
        public UnityAction onPlay = delegate {  };
        public UnityAction<GameStates> onChangeGameState=delegate {  };
        public UnityAction onReset=delegate { };
        public UnityAction onApplicatiponQuit = delegate {  };
        public Func<int> onGetIdleLevelID = delegate { return 0; };
    }
}
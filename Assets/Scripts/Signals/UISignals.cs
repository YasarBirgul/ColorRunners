using Enums;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class UISignals : MonoSingleton<UISignals>
    { 
        public UnityAction<UIPanels> onOpenPanel=delegate {  };
        public UnityAction<UIPanels> onClosePanel=delegate {  };
        public UnityAction<int> onSetLevelText = delegate {  };
    }
}


using Extentions;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Signals
{
    public class DroneAreaSignals: MonoSingleton<DroneAreaSignals>
    {
        public UnityAction<GameObject> onDroneAreaEnter = delegate {  };
        
        public UnityAction onDroneAreaCollectablesDeath = delegate {  };
        
        public UnityAction<GameObject> onRebuildStack = delegate {  };
        
        public UnityAction onColliderDisable = delegate {  };
        
        public UnityAction onEnableFinalCollider = delegate {  };
    }
}
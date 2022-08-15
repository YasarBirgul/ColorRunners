using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class DroneAreaSignals: MonoSingleton<DroneAreaSignals>
    {
        public UnityAction<GameObject> onDroneAreaEnter = delegate {  };
        
        public UnityAction onDroneAreaCollectablesDeath = delegate {  };
        
        public UnityAction<GameObject> onRebuildStack = delegate {  };
    }
}
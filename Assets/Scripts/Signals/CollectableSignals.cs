using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    { 
        public UnityAction<GameObject> onMansCollection=delegate {  };
    }
}
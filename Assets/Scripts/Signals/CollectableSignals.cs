using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{ 
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    { 
        
        public UnityAction<GameObject> onEnterGroundCheck=delegate {  }; //player yavaslatma ıslemı yapılacak. YAPILMADI.
        public UnityAction<GameObject> onExitGroundCheck=delegate {  }; //stackta killendi 
    }
}
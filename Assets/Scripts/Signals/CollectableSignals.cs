using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class CollectableSignals : MonoSingleton<CollectableSignals>
    { 
        public UnityAction<GameObject> onMansCollection=delegate {  };
        
        
        //PLAYERDA İŞ YAPACAK. BAŞKA SINYAL AÇILABILIR.
        public UnityAction<GameObject> onEnterGroundCheck=delegate {  }; //player yavaslatma ıslemı yapılacak. YAPILMADI.
        public UnityAction<GameObject> onExitGroundCheck=delegate {  }; //stackta killendi 
    }
}
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class PlayerSignal : MonoSingleton<PlayerSignal>
    {
        public UnityAction onIncreaseScale = delegate {  }; 
        public UnityAction onDecreaseScale = delegate {  }; 
    }
}
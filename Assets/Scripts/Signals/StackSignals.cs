using Enums;
using Extentions;
using Keys;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals: MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onIncreaseStack = delegate {  };
        public UnityAction<ObstacleCollisionGOParams> onDecreaseStack = delegate {  };
        public UnityAction<ColorType> onColorChange = delegate {  };
        public UnityAction<GameObject> onTurrentGroundControll = delegate {  };
        public UnityAction<int> OnDroneArea = delegate {  };
    }
}
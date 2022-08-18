using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class ColoredAreaSignals: MonoSingleton<ColoredAreaSignals>
    {
        public UnityAction<GameObject> onTurretDetect = delegate {  };
        public UnityAction onExitTurretArea = delegate {  };
    }
}
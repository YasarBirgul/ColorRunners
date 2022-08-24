using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class BuildingSignals : MonoSingleton<BuildingSignals>
    {
        public UnityAction<int> onBuildingsCompleted = delegate {  };
        public UnityAction onDataReadyToUse = delegate {  };
    }
}
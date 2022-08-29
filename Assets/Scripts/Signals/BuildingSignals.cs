using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class BuildingSignals : MonoSingleton<BuildingSignals>
    {
        public UnityAction<int> onBuildingsCompleted = delegate {  };
        public UnityAction<int> onSideBuildingsCompleted = delegate {  };
        public UnityAction onDataReadyToUse = delegate {  };
    }
}
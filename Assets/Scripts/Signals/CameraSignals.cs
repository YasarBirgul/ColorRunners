using Enums;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class CameraSignals: MonoSingleton<CameraSignals>
    {
        public UnityAction<CameraStatesType> onSetCameraState = delegate {  };
        public UnityAction onSetCameraTarget = delegate {  };
        public UnityAction onEnterMiniGame = delegate {  };
        public UnityAction onExitMiniGame = delegate {  };
    }
}
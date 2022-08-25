using Datas.ValueObject;
using Extentions;
using UnityEngine.Events;

namespace Signals
{
    public class ScoreSignals : MonoSingleton<ScoreSignals>
    {
        public UnityAction onIncreaseScore = delegate {  };
        public UnityAction onDecreaseScore = delegate {  };
        public UnityAction<bool> onPlayerScoreSetActive = delegate {  };
    }
}
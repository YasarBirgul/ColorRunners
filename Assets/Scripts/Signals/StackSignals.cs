using System;
using Extentions;
using UnityEngine;
using UnityEngine.Events;

namespace Signals
{
    public class StackSignals: MonoSingleton<StackSignals>
    {
        public UnityAction<GameObject> onIncreaseStack = delegate {  };
    }
}
using System;
using Datas.ValueObject;
using Enums;
using Extentions;
using UnityEditor;
using UnityEngine.Events;

namespace Signals
{
    public class SaveSignals : MonoSingleton<SaveSignals>
    {
        public UnityAction<SaveTypes, int> onChangeSaveData=delegate {  };
        public UnityAction<SaveTypes> onSaveDataToDatabase = delegate { };
        public UnityAction<SaveData> onSendDataToManagers = delegate { };
    }
}
using Datas.ValueObject;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Save", menuName = "ColorRunners/CD_Save", order = 0)]
    public class CD_Save : ScriptableObject
    {
        public SaveData SaveData;
    }
}
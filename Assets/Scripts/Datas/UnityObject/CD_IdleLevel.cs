using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_IdleLevel", menuName = "ColorRunners/CD_IdleLevel", order = 0)]
    public class CD_IdleLevel : ScriptableObject
    {
        public List<IdleLevelData> IdleLevelList;
    }
}
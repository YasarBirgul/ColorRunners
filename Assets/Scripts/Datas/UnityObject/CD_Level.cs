using System.Collections.Generic;
using Datas.ValueObject;
using UnityEngine;

namespace Datas.UnityObject
{
    [CreateAssetMenu(fileName = "CD_Level", menuName = "ColorRunners/CD_Level", order = 0)]
    public class CD_Level : ScriptableObject
    {
        public List<LevelData> Levels;
    }
}
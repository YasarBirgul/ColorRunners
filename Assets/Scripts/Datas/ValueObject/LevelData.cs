using System;
using Abstract;
using UnityEngine;

namespace Datas.ValueObject
{
    [Serializable]
    public class LevelData : SaveableEntity
    {
        public static string LevelKey = "Level";
        
        public GameObject Level;

        public int LevelId;

        public int IdleLevelId;
        
        public LevelData() {}

        public LevelData(int _idleLevelId, int _levelId)
        {
            IdleLevelId = _idleLevelId;
            LevelId = _levelId;
        }
        public override string GetKey()
        {
            return LevelKey;
        }
    }
}
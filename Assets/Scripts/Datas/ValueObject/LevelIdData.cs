using Abstract;

namespace Datas.ValueObject
{
    public class LevelIdData : SaveableEntity
    {
        public static string LevelKey = "Level";

        public int LevelId;
        
        public int IdleLevelId;
        
        public LevelIdData() { }
        
        public LevelIdData(int _idleLevelId,int _levelId)
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
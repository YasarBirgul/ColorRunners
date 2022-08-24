using Datas.ValueObject;
using Enums;

namespace Commands.Save
{
    public class SaveIdleLevelProcessCommand
    {
        
       public void Execute(SaveStates saveStates, IdleLevelData idleLevelData)
       {
           ES3.Save("IdleLevelProgressData",idleLevelData,"idleLevelData/IdleLevelProgressData.es3");
       }
        
    }
}
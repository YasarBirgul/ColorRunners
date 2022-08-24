using Datas.ValueObject;
using Enums;

namespace Commands.Save
{
    public class LoadIdlelevelProgressCommand
    {
        public IdleLevelData Execute(SaveStates saveStates, IdleLevelData idleLevelData)
        {

            return ES3.Load<IdleLevelData>("IdleLevelProgressData", "IdleLevelData/IdleLevelProgressData.es3");
        }
    }
}
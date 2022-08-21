using Enums;

namespace Commands.Save
{
    public class LoadGameCommand
    {
        public int OnLoadGameData(SaveStates currentStates)
        {
            if (currentStates == SaveStates.level) return ES3.Load<int>("Level");
            
            else return 0;
        }
    }
}
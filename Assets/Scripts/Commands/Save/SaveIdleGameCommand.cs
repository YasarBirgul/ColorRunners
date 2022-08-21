using Enums;

namespace Commands.Save
{
    public class SaveIdleGameCommand
    {
        public void OnSaveIdleGameData(SaveStates saveStates, int idleLevelData)
        { 
            if (saveStates == SaveStates.idle)
            {
                ES3.Save("IdleLevel",idleLevelData);
            }
        }
    }
}
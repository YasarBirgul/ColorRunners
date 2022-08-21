using Enums;

namespace Commands.Save
{
    public class SaveGameCommand
    {
        public void OnSaveGameData(SaveStates state, int levelData)
        {
            ES3.Save("level",levelData);
        }
    }
}
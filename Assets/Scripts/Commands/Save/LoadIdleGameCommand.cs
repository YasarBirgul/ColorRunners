using Enums;
using UnityEngine;

namespace Commands.Save
{
    public class LoadIdleGameCommand
    {
        public int OnLoadBuildingsData(SaveStates state)
        {
            if (state == SaveStates.idle) return ES3.Load<int>("IdleLevel");
            else return 0;
        }
    }
}
using UnityEngine;

namespace Commands.Level
{
    public class IdleLevelLoaderCommand
    {
        #region Self Variables

        #region Private Variables

        private GameObject _IdlelevelHolder;

        #endregion

        #endregion
        
        public IdleLevelLoaderCommand(ref GameObject levelHolder)
        {
            _IdlelevelHolder = levelHolder;
        }
        public void Execute(int _levelID)
        {
            Object.Instantiate(Resources.Load<GameObject>($"Prefabs/IdleLevelPrefabs/IdleLevel {_levelID}"), _IdlelevelHolder.transform);
        }
    }
}
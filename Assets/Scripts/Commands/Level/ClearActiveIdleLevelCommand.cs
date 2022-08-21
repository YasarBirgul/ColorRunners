using UnityEngine;

namespace Commands.Level
{
    public class ClearActiveIdleLevelCommand
    {
        #region Self Variables

        #region Private Variables

        private GameObject _idleLevelHolder;

        #endregion

        #endregion
        public ClearActiveIdleLevelCommand(ref GameObject levelHolder)
        {
            _idleLevelHolder = levelHolder;

        }
        public void Execute()
        {
            Object.Destroy(_idleLevelHolder .transform.GetChild(0).gameObject);
        }
    }
}
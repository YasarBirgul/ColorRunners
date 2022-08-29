using Commands.Save;
using Datas.ValueObject;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class SaveManager: MonoBehaviour
    {
        #region Self Variables

        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;
        
        #endregion
        
        #endregion
        private void Awake()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
        }
        
        #region Event Subscription
        
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadGameData += _loadGameCommand.Execute<LevelIdData>;
            SaveSignals.Instance.onSaveBuildingsData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadBuildingsData += _loadGameCommand.Execute<BuildingsData>;
            SaveSignals.Instance.onSaveIdleLevelData += _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadIdleLevelData += _loadGameCommand.Execute<IdleLevelData>;
        }

        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveGameData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadGameData -= _loadGameCommand.Execute<LevelIdData>;
            SaveSignals.Instance.onSaveBuildingsData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadBuildingsData -= _loadGameCommand.Execute<BuildingsData>;
            SaveSignals.Instance.onSaveIdleLevelData -= _saveGameCommand.Execute;
            SaveSignals.Instance.onLoadIdleLevelData -= _loadGameCommand.Execute<IdleLevelData>;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
    }
}
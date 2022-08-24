using Commands.Save;
using Signals;
using UnityEngine;

namespace Managers
{ 
    public class SaveManager: MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables
        
        #endregion

        #region Serialized Variables
        
        #endregion

        #region Private Variables

        private LoadGameCommand _loadGameCommand;
        private SaveGameCommand _saveGameCommand;
        private LoadIdleGameCommand _loadIdleGameCommand;
        private SaveIdleGameCommand _saveIdleGameCommand;
        private SaveIdleLevelProcessCommand _saveIdleLevelProcessCommand;
        private LoadIdlelevelProgressCommand _loadIdlelevelProgressCommand;

        #endregion

        #endregion
        
        private void Awake()
        {
            _loadGameCommand = new LoadGameCommand();
            _saveGameCommand = new SaveGameCommand();
            _loadIdleGameCommand = new LoadIdleGameCommand();
            _saveIdleGameCommand = new SaveIdleGameCommand();
                
            if (!ES3.FileExists())
            {
                ES3.Save("level",0,"RunnerLevelData/RunnerLevelData.es3");
            }
            
            if (!ES3.FileExists())
            {
                ES3.Save("IdleLevel",0,"IdleLevelData/IdleLevelData.es3");
            }
        }
        
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            SaveSignals.Instance.onSaveRunnerLevelData += _saveGameCommand.OnSaveGameData;
            SaveSignals.Instance.onLoadGameData += _loadGameCommand.OnLoadGameData;
            SaveSignals.Instance.onSaveIdleLevelData += _saveIdleGameCommand.OnSaveIdleGameData;
            SaveSignals.Instance.onLoadIdleData += _loadIdleGameCommand.OnLoadBuildingsData;
            SaveSignals.Instance.onSaveIdleLevelProgressData += _saveIdleLevelProcessCommand.Execute;
            SaveSignals.Instance.onLoadIdleLevelProgressData += _loadIdlelevelProgressCommand.Execute;
        } 
        private void UnsubscribeEvents()
        {
            SaveSignals.Instance.onSaveRunnerLevelData -= _saveGameCommand.OnSaveGameData;
            SaveSignals.Instance.onLoadGameData -= _loadGameCommand.OnLoadGameData;
            SaveSignals.Instance.onSaveIdleLevelData -= _saveIdleGameCommand.OnSaveIdleGameData;
            SaveSignals.Instance.onLoadIdleData -= _loadIdleGameCommand.OnLoadBuildingsData;
            SaveSignals.Instance.onSaveIdleLevelProgressData -= _saveIdleLevelProcessCommand.Execute;
            SaveSignals.Instance.onLoadIdleLevelProgressData -= _loadIdlelevelProgressCommand.Execute;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
    }
}
using System.Threading.Tasks;
using Abstract;
using Commands.Level;
using Datas.UnityObject;
using Datas.ValueObject;
using Enums;
using Signals;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Managers
{
    public class LevelManager : MonoBehaviour
    {
        #region Self Variables
    
        #region Public Variables

        [Header("Data")] public LevelData LevelData;
        public IdleLevelData IdleLevelData;
        
        #endregion

        #region Serialized Variables
        
        [SerializeField] private GameObject levelHolder;
        [SerializeField] private GameObject idleLevelHolder;
       
        #endregion

        #region Private Variables

        private ClearActiveLevelCommand _clearActiveLevel;
        private LevelLoaderCommand _levelLoader;
        private IdleLevelLoaderCommand _idleLevelLoader;
        private ClearActiveIdleLevelCommand _clearActiveIdleLevel;
        [ShowInInspector] private int _levelID;
        [ShowInInspector] private int _idleLevelID;

        #endregion

        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            CoreGameSignals.Instance.onReset += OnReset;
            LevelSignals.Instance.onLevelInitialize += OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel += OnClearActiveLevel;
            LevelSignals.Instance.onInitializeIdleLevel += InitializeIdleLevel;
            LevelSignals.Instance.onClearActiveIdleLevel += OnClearActiveIdleLevel;
            LevelSignals.Instance.onNextLevel += OnNextLevel;
            LevelSignals.Instance.onRestartLevel += OnRestartLevel;
            LevelSignals.Instance.onGetLevel += OnGetLevel;
            LevelSignals.Instance.onGetIdleLevelID += OnGetIdleLevelID;
            LevelSignals.Instance.onIdleLevelChange += OnIdleLevelChange;
        } 
        private void UnsubscribeEvents()
        {
            CoreGameSignals.Instance.onReset -= OnReset;
            LevelSignals.Instance.onLevelInitialize -= OnInitializeLevel;
            LevelSignals.Instance.onClearActiveLevel -= OnClearActiveLevel;
            LevelSignals.Instance.onInitializeIdleLevel -= InitializeIdleLevel;
            LevelSignals.Instance.onClearActiveIdleLevel -= OnClearActiveIdleLevel;
            LevelSignals.Instance.onNextLevel -= OnNextLevel;
            LevelSignals.Instance.onRestartLevel -= OnRestartLevel;
            LevelSignals.Instance.onGetLevel -= OnGetLevel;
            LevelSignals.Instance.onGetIdleLevelID -= OnGetIdleLevelID;
            LevelSignals.Instance.onIdleLevelChange += OnIdleLevelChange;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion

        private void Awake()
        {
            _levelID = GetActiveLevel();
            _idleLevelID = GetActiveIdleLevel();
            LevelData = GetLevelData();
            IdleLevelData = GetIdleLevelData();
            _clearActiveLevel = new ClearActiveLevelCommand(ref levelHolder);
            _levelLoader = new LevelLoaderCommand(ref levelHolder);
            _idleLevelLoader = new IdleLevelLoaderCommand(ref idleLevelHolder);
            _clearActiveIdleLevel = new ClearActiveIdleLevelCommand(ref idleLevelHolder);
        }

        private void Start()
        {
            OnInitializeLevel();
            InitializeIdleLevel();
        }
        private int OnGetLevel() => _levelID;
        private int OnGetIdleLevelID()
        {
            return _idleLevelID;
        } 
        private void OnIdleLevelChange()
        {
            _idleLevelID++;
        }
        private int GetActiveLevel()
        {
            if (!ES3.FileExists()) return 0;
            return ES3.KeyExists("Level") ? ES3.Load<int>("Level") : 0;
        }
        private int GetActiveIdleLevel()
        {
            if (ES3.FileExists()) return 0;
            return ES3.KeyExists("IdleLevel") ? ES3.Load<int>("IdleLevel") : 0;
        }
        private LevelData GetLevelData()
        {
            int newLevelData = _levelID%Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            return Resources.Load<CD_Level>("Data/CD_Level").Levels[newLevelData];
        }
        private IdleLevelData GetIdleLevelData()
        {
            int newIdleLevelData = _idleLevelID%Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;
            return Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList[newIdleLevelData];
        }

        private void OnInitializeLevel()
        {
            int newLevelData = _levelID%Resources.Load<CD_Level>("Data/CD_Level").Levels.Count;
            _levelLoader.Execute(newLevelData);
            UISignals.Instance.onSetLevelText?.Invoke(_levelID + 1);
        }
        private void InitializeIdleLevel()
        {
            int newIdleLevelData =_idleLevelID%Resources.Load<CD_IdleLevel>("Data/CD_IdleLevel").IdleLevelList.Count;
            _idleLevelLoader.Execute(newIdleLevelData);
        } 
        private async void OnNextLevel()
        {
            _levelID++;
            Debug.Log(_idleLevelID); 
            //LevelSignals.Instance.onClearActiveIdleLevel?.Invoke();
            //LevelSignals.Instance.onInitializeIdleLevel?.Invoke();
            await Task.Delay(50);
            OnRestartLevel();
        } 
        
        private void OnClearActiveLevel()
        {
            _clearActiveLevel.Execute();
        }
        private void OnClearActiveIdleLevel()
        {
            _clearActiveIdleLevel.Execute();
        }
        private void OnRestartLevel()
        {
            CoreGameSignals.Instance.onReset?.Invoke();
        }
        private void OnReset()
        {
          LevelSignals.Instance.onClearActiveLevel?.Invoke();
          LevelSignals.Instance.onLevelInitialize?.Invoke();
          LevelSignals.Instance.onClearActiveIdleLevel?.Invoke();
          LevelSignals.Instance.onInitializeIdleLevel?.Invoke();  
          //SaveSignals.Instance.onSaveRunnerLevelData?.Invoke(SaveStates.level, _levelID);
        }
    }
}
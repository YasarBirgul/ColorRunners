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
    public class LevelManager : MonoBehaviour, ISaveable
    {
        #region Self Variables
    
        #region Public Variables

        [Header("Data")] public LevelData LevelData;
        public IdleLevelData IdleLevelData;
        public LevelIdData LevelIdData = new LevelIdData();

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
        [ShowInInspector] private int _uniqueID = 0;

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
            GetData();
            _clearActiveLevel = new ClearActiveLevelCommand(ref levelHolder);
            _levelLoader = new LevelLoaderCommand(ref levelHolder);
            _idleLevelLoader = new IdleLevelLoaderCommand(ref idleLevelHolder);
            _clearActiveIdleLevel = new ClearActiveIdleLevelCommand(ref idleLevelHolder);
        }

        private void GetData()
        {
            if (!ES3.FileExists($"Level{_uniqueID}.es3"))
            {
                if (!ES3.KeyExists("Level"))
                {
                    IdleLevelData = GetIdleLevelData();
                    Save(_uniqueID);
                }
            }
            Load(_uniqueID);
            LevelData = GetLevelData();
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
            Save(_uniqueID);
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
            Save(_uniqueID);
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
        } 
        public void Save(int uniqueId)
        {
            LevelIdData levelIdData= new LevelIdData(_idleLevelID,_levelID);
            SaveSignals.Instance.onSaveGameData.Invoke(levelIdData,uniqueId);
        }
        public void Load(int uniqueId)
        {
            LevelIdData levelIdData= SaveSignals.Instance.onLoadGameData.Invoke(LevelIdData.LevelKey, uniqueId);
            _idleLevelID = levelIdData.IdleLevelId;
            _levelID = levelIdData.LevelId;
        }
    }
}
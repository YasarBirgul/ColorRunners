using Datas.UnityObject;
using Datas.ValueObject;
using Signals;
using TMPro;
using UnityEngine;

namespace Managers
{
    public class ScoreManager : MonoBehaviour
    {
          #region Self Variables
  
          #region Public Variables
          
          #endregion
  
          #region Serialized Variables
  
          [SerializeField] private GameObject playerScoreHolder;
          [SerializeField] private TextMeshPro scoreText;
          private StackData _stackData;
          #endregion
          
          #region Private Variables

          private int _score;
          private Transform _playerManager;
          #endregion
        
          #endregion
          
          #region Event Subscription
          private void Awake()
          {
              _stackData = Resources.Load<CD_Stack>("Data/CD_Stack").StackData;
          }
          private void OnEnable()
          {
              SubscribeEvents();
          }
          private void SubscribeEvents()
          {
              CoreGameSignals.Instance.onPlay += OnPlay;
              ScoreSignals.Instance.onIncreaseScore += OnIncreaseScore;
              ScoreSignals.Instance.onDecreaseScore += OnDecreaseScore;
              ScoreSignals.Instance.onPlayerScoreSetActive += OnPlayerScoreSetActive;
          } 
          private void UnsubscribeEvents()
          {
              CoreGameSignals.Instance.onPlay -= OnPlay;
              ScoreSignals.Instance.onIncreaseScore -= OnIncreaseScore;
              ScoreSignals.Instance.onDecreaseScore -= OnDecreaseScore;
              ScoreSignals.Instance.onPlayerScoreSetActive -= OnPlayerScoreSetActive;
          }
          private void OnDisable()
          {
              UnsubscribeEvents();
          }
          #endregion
          private void OnPlay()
          {
              playerScoreHolder.SetActive(true);
              FindPlayer();
              _score = _stackData.InitializedStack.Count;
              scoreText.text = _score.ToString();
          }
          private void FindPlayer()
          {
              if (!_playerManager)
              {
                  _playerManager = FindObjectOfType<PlayerManager>().transform;
                  playerScoreHolder.transform.SetParent(_playerManager);
              }
          } 
          private void OnIncreaseScore()
          {
              _score++;
              scoreText.text = _score.ToString();
          }
          private void OnDecreaseScore()
          {
              _score--;
              scoreText.text = _score.ToString();
          }
          private void OnPlayerScoreSetActive(bool OnPlayerScoreSetActive)
          {
              if (OnPlayerScoreSetActive)
              {
                  playerScoreHolder.SetActive(true);
              }
              else
              {
                  playerScoreHolder.SetActive(false);
              }
          }
    }
}
using Controllers;
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
          [SerializeField] private TextMeshProUGUI UIScoreText;
          [SerializeField] private TextMeshPro scoreText;
          [SerializeField] private ScorePhysicsController physicsController;
          
          #endregion
          
          #region Private Variables

          private int _score;
          private Transform _playerManager;
          private StackData _stackData;
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
              CoreGameSignals.Instance.onReset += OnReset;
              CoreGameSignals.Instance.onPlay += OnPlay;
              ScoreSignals.Instance.onIncreaseScore += OnIncreaseScore;
              ScoreSignals.Instance.onDecreaseScore += OnDecreaseScore;
              ScoreSignals.Instance.onPlayerScoreSetActive += OnPlayerScoreSetActive;
              ScoreSignals.Instance.onMultiplyScore += OnMultiplyScore;
          } 
          private void UnsubscribeEvents()
          {   
              CoreGameSignals.Instance.onReset -= OnReset;
              CoreGameSignals.Instance.onPlay -= OnPlay;
              ScoreSignals.Instance.onIncreaseScore -= OnIncreaseScore;
              ScoreSignals.Instance.onDecreaseScore -= OnDecreaseScore;
              ScoreSignals.Instance.onPlayerScoreSetActive -= OnPlayerScoreSetActive;
              ScoreSignals.Instance.onMultiplyScore -= OnMultiplyScore;
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
              SetScoreToText();
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
              if (_score <= 1)
              {
                  physicsController.SetColliderActive(true);
              }
              SetScoreToText();
          }
          public void OnDecreaseScore()
          {
              if (_score > 0)
              {
                  _score--;
                  PlayerSignal.Instance.onDecreaseScale?.Invoke();
              }
              else if (_score == 0)
              {
                  physicsController.SetColliderActive(false);
              }
              SetScoreToText();
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
          private void OnMultiplyScore(int multiplyFactor)
          {
              _score *= multiplyFactor;
              SetScoreToText();
          }
          private void SetScoreToText()
          {
              scoreText.text = _score.ToString();
              UIScoreText.text = _score.ToString();
          } private void OnReset()
          {
              _score = 0;
              playerScoreHolder.transform.parent = transform;
              playerScoreHolder.transform.position = Vector3.up*2.5f;
          }
    }
}
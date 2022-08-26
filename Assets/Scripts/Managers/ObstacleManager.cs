using Signals;
using UnityEngine;
using Controllers;

namespace Managers
{
    public class ObstacleManager : MonoBehaviour
    {
        #region Self Variables

        #region Public Variables

        #endregion

        #region Serialized Variables

        [SerializeField] private TurretMovementController _turretMovementController;

        #endregion

        #region Private Variables
        
        #endregion

        #endregion
        
        #region Event Subscriptions
        
        private void OnEnable()
        {
            SubscribeEvents();
        }

        private void SubscribeEvents()
        {
            ColoredAreaSignals.Instance.onTurretDetect += OnTurretDetection;
            ColoredAreaSignals.Instance.onExitTurretArea += OnTurretExit;
        }
        private void UnsubscribeEvents()
        {
            ColoredAreaSignals.Instance.onTurretDetect -= OnTurretDetection;
            ColoredAreaSignals.Instance.onExitTurretArea -= OnTurretExit;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private void OnTurretDetection(GameObject DetectedCollectable)
        {
            _turretMovementController.DetectCollectable(DetectedCollectable);
        }
        private void OnTurretExit()
        {
            _turretMovementController.StopTurret();
        }
    } 
}
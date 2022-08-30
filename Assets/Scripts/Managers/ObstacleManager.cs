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

        [SerializeField] private TurretMovementController turretMovementController;

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
        private void OnTurretDetection(GameObject detectedCollectable)
        {
            turretMovementController.DetectCollectable(detectedCollectable);
        }
        private void OnTurretExit()
        {
            turretMovementController.StopTurret();
        }
    } 
}
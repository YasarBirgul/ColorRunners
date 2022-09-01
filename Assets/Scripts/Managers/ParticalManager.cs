using System.Collections.Generic;
using System.Threading.Tasks;
using Controllers;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ParticalManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Vars
        
        #endregion
        
        #region Serializefield Variables
       
        [SerializeField] private List<ParticleEmitController> emitControllers;
        
        #endregion

        #region Private Vars
        
        #endregion
        
        #endregion

        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        }
        private void SubscribeEvents()
        {
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
            ParticleSignals.Instance.onPlayerDeath += OnParticleDeath;
            ParticleSignals.Instance.onParticleBurst += OnParticalBurst;
            ParticleSignals.Instance.onParticleStop += OnParticalStop;
            ParticleSignals.Instance.onParticleLookRotation += OnParticleLookRotation;
        } 
        private void UnsubscribeEvents()
        {
            StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
            ParticleSignals.Instance.onPlayerDeath -= OnParticleDeath;
            ParticleSignals.Instance.onParticleBurst -= OnParticalBurst;
            ParticleSignals.Instance.onParticleStop -= OnParticalStop;
            ParticleSignals.Instance.onParticleLookRotation -= OnParticleLookRotation;
        } 
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        #endregion
        private async void OnParticalBurst(Vector3 transform)
        {
            Vector3 newTransform = new Vector3(Random.Range(transform.x - 1, transform.x + 1),transform.y,transform.z);
            
            emitControllers[0].EmitParticle(newTransform);
            await Task.Delay(100);
            emitControllers[1].EmitParticle(newTransform + new Vector3(0,4,3));
        } 
        private void OnParticleLookRotation(Quaternion toRotation)
        {
            emitControllers[0].LookRotation(toRotation);
        }
        private void OnDecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            var collectedTransform = obstacleCollisionGOParams.Collected.transform.position;
            OnParticleDeath(collectedTransform);
        }
        private void OnParticleDeath(Vector3 collectedTransform)
        {
            emitControllers[1].EmitParticle(collectedTransform);
        }
        private void OnParticalStop()
        {
            for (int i = 0; i < emitControllers.Count; i++)
            {
                emitControllers[i].EmitStop();
            }
        }
    }
}
using System.Threading.Tasks;
using Controllers;
using Enums;
using Keys;
using Signals;
using UnityEngine;

namespace Managers
{
    public class ParticleManager : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Vars
        
        #endregion
        
        #region Serializefield Variables
        
        #endregion

        #region Private Vars
        
        [SerializeField] ParticleSystem _particleSystem;
        [SerializeField] private ParticleSystem _particleSystemGradientExplode;
        
        private GameObject _data;
        
        #endregion
        
        #endregion
        
        private void Awake()
        {
            Init();
        }

        private void Init()
        {
            transform.Rotate(0, 0, 0);
            _particleSystem.Stop();
        }
        #region Event Subscription
        private void OnEnable()
        {
            SubscribeEvents();
        } 
        private void SubscribeEvents()
        {
            ParticleSignals.Instance.onParticleBurst += OnParticleBurst;
            StackSignals.Instance.onDecreaseStack += OnDecreaseStack;
        }
        private void UnsubscribeEvents()
        {
            ParticleSignals.Instance.onParticleBurst -= OnParticleBurst;
            StackSignals.Instance.onDecreaseStack -= OnDecreaseStack;
        }
        private void OnDisable()
        {
            UnsubscribeEvents();
        }
        
        #endregion
        private void OnParticleBurst(Vector3 burstPos)
        {
            EmitParticle(burstPos);
        }
        private async void EmitParticle(Vector3 burstPos)
        {
            transform.position = burstPos;
            transform.GetChild(0).gameObject.SetActive(true);
            _particleSystem.Play();
            _particleSystemGradientExplode.Play();
            await Task.Delay(2000);
            _particleSystem.Stop();
        }
        
        private async void OnDecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            transform.position = obstacleCollisionGOParams.Collected.transform.position;
            transform.GetChild(1).gameObject.SetActive(true);
            _particleSystemGradientExplode.Play();
            await Task.Delay(1500);
            _particleSystem.Stop();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Keys;
using Signals;
using UnityEngine;

namespace Controllers
{
    public class ParticleController : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Vars
        
        #endregion
        
        #region Serializefield Variables
        
        #endregion

        #region Private Vars
        
        private ParticleSystem _particleSystem;
        private int uniqueID;
       
        private GameObject _data;
        
        #endregion
        
        #endregion
        
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _particleSystem = GetComponent<ParticleSystem>();
        }
        private void Start()
        {
            
        }
        
        private void OnParticleBurst(Vector3 burstPos)
        {
            EmitParticle(burstPos);
        }
        private async void EmitParticle(Vector3 burstPos)
        {
            transform.position = burstPos;
            transform.GetChild(0).gameObject.SetActive(true);
          //  _particleDeath.Play();
           // _particleSystemGradientExplode.Play();
            await Task.Delay(2000);
          //  _particleDeath.Stop();
        }
        
        private async void OnDecreaseStack(ObstacleCollisionGOParams obstacleCollisionGOParams)
        {
            transform.position = obstacleCollisionGOParams.Collected.transform.position;
            transform.GetChild(1).gameObject.SetActive(true);
          //  _particleSystemGradientExplode.Play();
            await Task.Delay(1500);
          //  _particleDeath.Stop();
        }
    }
}
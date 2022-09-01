using System.Threading.Tasks;
using Keys;

using UnityEngine;
using UnityEngine.Serialization;

namespace Controllers
{
    public class ParticleEmitController : MonoBehaviour
    {
        #region Self Variables
        
        #region Public Vars
        
        #endregion
        
        #region Serializefield Variables

        [SerializeField] private Vector3 emitPositionAdjust;
        [SerializeField] private int particleStartSize;
        [SerializeField] private int burstCount;
        
        #endregion

        #region Private Vars
        
        private ParticleSystem _particleSystem;

        #endregion
        
        #endregion
        private void Awake()
        {
            Init();
        }
        private void Init()
        {
            _particleSystem = GetComponent<ParticleSystem>();
            _particleSystem.Stop();
            transform.Rotate(0,0,0);
        }
        public void EmitParticle(Vector3 burstPos)
        {
            gameObject.SetActive(true);
            var emitParams = new ParticleSystem.EmitParams
           {
               position = burstPos + emitPositionAdjust,
               startSize = particleStartSize,
           };
           _particleSystem.Emit(emitParams,burstCount);
           _particleSystem.Play();
        }
        public void EmitStop()
        {
            _particleSystem.Stop();
            gameObject.SetActive(false);
        }
        public void LookRotation(Quaternion toRotation)
        {
            var mainModule = new ParticleSystem().shape;
            var rotationEuler = mainModule.rotation; 
            Quaternion rotation = Quaternion.Euler(rotationEuler);
            rotation = Quaternion.RotateTowards( rotation,toRotation,30);
        }
    }
}
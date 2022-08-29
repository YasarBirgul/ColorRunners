using UnityEngine;
using UnityEngine.AI;
using Worker;
using Idle = Worker.Idle;

namespace AIInitialize
{
    public class AIInit : MonoBehaviour
    {
        NavMeshAgent _agent;
        Animator anim;
        
        WorkerAI _currentState;
        
        public int currentScore;

     
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            
            anim = GetComponent<Animator>();
            
            _currentState = new Idle(gameObject, anim, _agent,currentScore);
        
        }
        
        private void Update()
        {
            _currentState = _currentState.Process();
            
            

        }
    }
}
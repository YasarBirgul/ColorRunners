using System;
using Abstract;
using UnityEngine;
using StateMachineComponents;
using UnityEngine.AI;

namespace Controllers
{ 
    public class StickmanAIController : MonoBehaviour
    {
        private StateMachine _stateMachine;
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        public AITargetStationController Target;
        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _stateMachine = new StateMachine();
            
            var idleState = new StickmanAIIdleState(this, _animator, _navMeshAgent);
            var walkState = new StickmanAIWalkingState(this, _animator, _navMeshAgent);
            
            At(idleState,walkState,HasTarget());
            
            At(walkState,idleState,reachedTarget());
            
            void At(IState to, IState from, Func<bool> condition) =>
                _stateMachine.AddTransition(to, from, condition);
                                                                                       
            Func<bool> HasTarget()=> ()=>Target != null;
            
            Func<bool> reachedTarget() => () => 
                Target != null && Vector3.Distance(transform.position, Target.transform.position) < 1f;
        }
        

        private void Update()
        {
            _stateMachine.Setup();
        }

        private void PlayAnim()
        {
            
        }
    }
}
using Abstract;
using Cinemachine;
using Controllers;
using UnityEngine;
using UnityEngine.AI;

namespace StateMachineComponents
{
    public class StickmanAIWalkingState : IState
    {
        
        private readonly StickmanAIController _stickmanAIController;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;

        private Vector3 _lastPos= Vector3.zero;
        public float WaitingTime;

        public StickmanAIWalkingState(StickmanAIController stickmanAIController,Animator animator,NavMeshAgent navMeshAgent)
        {
            _stickmanAIController = stickmanAIController;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }
        public void OnSetup()
        {
            if (Vector3.Distance(_stickmanAIController.Target.transform.position, _lastPos) <= 0f)
            {
                WaitingTime += Time.deltaTime;
            }
            _lastPos = _stickmanAIController.transform.position;
        }
        public void OnEnter()
        {
            WaitingTime = 0f;
            _navMeshAgent.enabled = true;
            _navMeshAgent.SetDestination(_stickmanAIController.Target.transform.position);
           // _animator.SetFloat("Speed",1f);
        }

        public void OnExit()
        {
            _navMeshAgent.enabled = false;
            // _animator.SetFloat("Speed",0f);
        }
    }
}
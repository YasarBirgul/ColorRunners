using Abstract;
using Controllers;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

namespace StateMachineComponents
{
    public class StickmanAIIdleState : IState
    {
        private readonly StickmanAIController _stickmanAIController;
        private readonly Animator _animator;
        private readonly NavMeshAgent _navMeshAgent;

        public StickmanAIIdleState(StickmanAIController stickmanAIController,Animator animator,NavMeshAgent navMeshAgent)
        {
            _stickmanAIController = stickmanAIController;
            _animator = animator;
            _navMeshAgent = navMeshAgent;
        }
        public void OnSetup()
        {
            _stickmanAIController.Target = choseOneOfTheNearestPlace(5);
        } 
        public AITargetStationController choseOneOfTheNearestPlace(int pickFromNearest)
        {
            return Object.FindObjectsOfType<AITargetStationController>().OrderBy(t =>
                    Vector3.Distance(_stickmanAIController.transform.position, t.transform.position))
                .Where(t => t.IsTargetAvailable == true).Take(pickFromNearest).
                OrderBy(t => Random.Range(0, int.MaxValue)).FirstOrDefault();
        }
        public void OnEnter()
        {
            
        }

        public void OnExit()
        {
           
        }
    }
}
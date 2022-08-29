using UnityEngine;
using UnityEngine.AI;
using CheckPoints;
using Enums;
using UnityTemplateProjects.Worker;

namespace Worker
{
    public class Patrol: WorkerAI
    {
        private int _currentIndex = -1;

        public Patrol(GameObject _worker,Animator _anim, NavMeshAgent _agent,int _currentScore) : base(_worker, _anim, _agent,_currentScore)
        {
            name = NPC_AiState.PATROL;
            Agent.speed = 5;
        }

        protected override void Enter()
        {   
            Debug.Log("Patrol ENTER!");
            
            float lastDist = Mathf.Infinity;
            
            for (int i = 0; i < GameEnvironment.Singleton.Checkpoints.Count; i++)
            {
                GameObject thisWayPoint = GameEnvironment.Singleton.Checkpoints[i];
                
                float distance = Vector3.Distance(Worker.transform.position, thisWayPoint.transform.position);
                
                if (distance < lastDist)
                {
                    _currentIndex = i - 1;
                    
                    lastDist = distance;
                }
            }
            base.Enter();
        }

        protected override void Update()
        {   
            if (Agent.remainingDistance < 1)
            {
                if (_currentIndex >= GameEnvironment.Singleton.Checkpoints.Count - 1)
                    _currentIndex = 0;
                else
                    _currentIndex++;

                Agent.SetDestination(GameEnvironment.Singleton.Checkpoints[_currentIndex].transform.position);
                
            }

        }

        protected override void Exit()
        {
            base.Exit();
        }
    }
}
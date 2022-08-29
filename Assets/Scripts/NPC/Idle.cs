using Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityTemplateProjects;
using UnityTemplateProjects.Worker;

namespace Worker
{
    public class Idle : WorkerAI
    {
        public Idle(GameObject _worker, Animator _anim, NavMeshAgent _agent,int _currentScore) : base(_worker, _anim, _agent,_currentScore)
        {
            name = NPC_AiState.IDLE;
        }
        
        protected override void Enter()
        {   
            Debug.Log("IDLE ENTER!");
            base.Enter();
        }

        protected override void Update()
        {
            
            nextState = new Patrol(Worker,anim,Agent,currentScore);
            stage = NPC_AiEvent.Exit;
            
        }

        protected override void Exit()
        {   
            //anim.isIdle kill
            base.Exit();
        }
    }
}
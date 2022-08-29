using Enums;
using UnityEngine;
using UnityEngine.AI;
using UnityTemplateProjects;
using UnityTemplateProjects.Worker;

namespace Worker
{
    public class WorkerAI
    {

        public NPC_AiState name;

        public NPC_AiEvent stage;

        protected Animator anim;
        

        public GameObject Worker;

        protected NavMeshAgent Agent;

        public int currentScore;

        private float visibleDistance = 15.0f;
        private float visibleAngle = 60.0f;

        protected WorkerAI nextState;

        protected WorkerAI(GameObject _worker, Animator _anim,NavMeshAgent _agent,int _currentScore)
        {
            Worker = _worker;
            anim = _anim;
            Agent = _agent;
            stage = NPC_AiEvent.Enter;
            currentScore = _currentScore;
        }
        

        protected virtual void Enter()
        {
            stage = NPC_AiEvent.Update;
        }

        protected virtual void Update()
        {
            stage = NPC_AiEvent.Update;
        }

        protected virtual void Exit()
        {
            stage = NPC_AiEvent.Exit;
        }

        public WorkerAI Process()
        {
            if (stage == NPC_AiEvent.Enter)
            {
                Enter();
            }
        
            if (stage == NPC_AiEvent.Update)
            {
                Update();
            }

            if (stage == NPC_AiEvent.Exit)
            {
                Exit();    
                return nextState;
            }

            return this;
        }
    }
}
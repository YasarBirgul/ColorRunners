using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        public void ChangeCollectableAnimation(CollectableAnimationStates collectableAnimationStates)
        {
            switch (collectableAnimationStates)
            {
                case CollectableAnimationStates.Idle:
                    break;
                case CollectableAnimationStates.Running:
                    animator.SetTrigger(("isRunning"));
                    break;
                case CollectableAnimationStates.Crouching:
                    animator.SetTrigger(("isCrouching"));
                    break;
                case CollectableAnimationStates.CrouchWalking:
                   animator.SetTrigger("isCrouchWalking");
                    break; 
            }
        }
    }
}



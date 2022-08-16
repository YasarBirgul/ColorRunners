using System.Collections;
using System.Collections.Generic;
using Enums;
using UnityEngine;

namespace Controllers
{
    public class CollectableAnimationController : MonoBehaviour
    {
        
        #region Self Variables
    
        #region Public Variables
    
        #endregion
    
        #region Serialized Variables

        [SerializeField] private Animator animator;
        
        #endregion
    
        #endregion
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
                case CollectableAnimationStates.Dead:
                       animator.SetTrigger("idDead");
                       break; 
            }
        }
    }
}



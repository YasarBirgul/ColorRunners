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
                    animator.SetTrigger(collectableAnimationStates.ToString());
                    break;
                case CollectableAnimationStates.Running:
                    animator.SetTrigger(collectableAnimationStates.ToString());
                    break;
                case CollectableAnimationStates.Crouching:
                    animator.SetTrigger(collectableAnimationStates.ToString());
                    break;
                case CollectableAnimationStates.CrouchWalking:
                   animator.SetTrigger(collectableAnimationStates.ToString());
                    break;
                case CollectableAnimationStates.Dead:
                       animator.Play(collectableAnimationStates.ToString());
                    break; 
            }
        }
    }
}



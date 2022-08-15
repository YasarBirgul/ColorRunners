using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class CollectableMovementController : MonoBehaviour
    {
        public void MoveToColorArea(Transform Area)
        {
            var RandomZ = Random.Range(-(Area.localScale.z/2-6),(Area.localScale.z/2 - 2));
            Vector3 newPos = new Vector3(Area.position.x,Area.position.y + 0.5f,
                Area.position.z + RandomZ);
            gameObject.transform.DOMove(newPos, 2f).OnComplete(() =>
            {
                transform.GetComponent<CollectableManager>().SetAnim(CollectableAnimationStates.Crouching);
            });
        }
    }
}
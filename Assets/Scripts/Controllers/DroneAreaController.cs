using DG.Tweening;
using Enums;
using Managers;
using UnityEngine;

namespace Controllers
{
    public class DroneAreaController : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Collected"))
            {
                var RandomZ = Random.Range(-((transform.localScale.z/2 +-6)), ((transform.localScale.z/2- 2)));
                Vector3 newPos = new Vector3(transform.position.x,other.transform.parent.gameObject.transform.position.y,
                    transform.position.z + RandomZ);
               
                other.transform.parent.gameObject.transform.DOMove(newPos, 2f).OnComplete(() =>
                {
                      other.CompareTag("Collectable");
                      other.GetComponentInParent<CollectableManager>().SetAnim(CollectableAnimationStates.Crouching);
                });
            }
        }
    }
}


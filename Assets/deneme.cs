using UnityEngine;
using DG.Tweening;
public class deneme : MonoBehaviour
{
    // Start is called before the first frame update
    public bool login=false; 
    private void Update()
    {
        Scale(login);
    }
    public void Scale(bool login2)
    {
        if (login2)
        {
           // transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y,
           //     Mathf.Lerp(transform.localScale.z, 0, 0.01f));
           transform.DOScaleZ(0, 1f).OnComplete(() =>
           {
               transform.gameObject.SetActive(false);
           });
        }
    }
}

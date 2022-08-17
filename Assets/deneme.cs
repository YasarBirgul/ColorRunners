using UnityEngine;
using DG.Tweening;
public class deneme : MonoBehaviour
{
    public bool login=false; 
    private void Update()
    {
        Scale(login);
    }
    public void Scale(bool login2)
    {
        if (login2)
        {
            transform.DOScaleZ(0, 1f).OnComplete(() =>
           {
               transform.gameObject.SetActive(false);
           });
        }
    }
}

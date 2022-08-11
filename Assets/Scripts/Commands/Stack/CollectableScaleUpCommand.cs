using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Commands.Stack
{
    public class CollectableScaleUpCommand
    {
        public IEnumerator CollectableScaleUp(List<GameObject> _collectedList,float _scaleFactor,float _scaleDelay)
        {
            for (int i = _collectedList.Count -1; i >= 0; i--)
            {
                int index = i;
                Vector3 scale = Vector3.one * _scaleFactor;
                _collectedList[index].transform.DOScale(scale, _scaleDelay).SetEase(Ease.Flash);
                _collectedList[index].transform.DOScale(Vector3.one, _scaleDelay).SetDelay(_scaleDelay).SetEase(Ease.Flash);
                yield return new WaitForSeconds(0.05f);
            }
        }
    }
}
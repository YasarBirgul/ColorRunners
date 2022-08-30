using DG.Tweening;
using UnityEngine;

namespace Controllers
{
    public class UIRoulletteController : MonoBehaviour
    {
        #region Self Veriables

        #region SerializeField Variables

  
        [SerializeField] RectTransform rectTransformArrow;

        #endregion

        #region Private Variables
        
        private string _multiply;
        
        #endregion
        #endregion

        public void CursorMovement()
        {
            Sequence sequence = DOTween.Sequence();
            sequence.Join(rectTransformArrow.DORotate(new Vector3(0, 0, 15), 1f).SetEase(Ease.Linear));
            sequence.Join(rectTransformArrow.DOLocalMoveX(-320, 1f).SetEase(Ease.Linear));
            sequence.SetLoops(-1, LoopType.Yoyo).onPlay();
        }
    }
}
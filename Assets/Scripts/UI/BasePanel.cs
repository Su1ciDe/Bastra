using DG.Tweening;
using UnityEngine;

namespace UI
{
	public class BasePanel : MonoBehaviour
	{
		[SerializeField] protected RectTransform panel;

		public virtual void Show()
		{
			panel.DOComplete();
			
			panel.gameObject.SetActive(true);
			panel.localScale = Vector3.zero;
			
			var seq = DOTween.Sequence();
			seq.SetTarget(panel);
			seq.Append(panel.DOScale(1, .35f).SetEase(Ease.OutBounce));
		}

		public virtual void Hide()
		{
			panel.DOComplete();
			var seq = DOTween.Sequence();
			seq.SetTarget(panel);
			seq.Append(panel.DOScale(0, .35f).SetEase(Ease.InBack));
			seq.AppendCallback(() => panel.gameObject.SetActive(false));
		}
	}
}
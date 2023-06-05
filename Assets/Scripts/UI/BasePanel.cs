using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class BasePanel : MonoBehaviour
	{
		[SerializeField] protected RectTransform panel;
		[SerializeField] protected Button background;

		protected virtual void Awake()
		{
			background.onClick.AddListener(Hide);
		}

		public virtual void Show()
		{
			background.gameObject.SetActive(true);

			panel.DOComplete();

			panel.gameObject.SetActive(true);
			panel.localScale = Vector3.zero;

			var seq = DOTween.Sequence();
			seq.SetTarget(panel);
			seq.Append(panel.DOScale(1, .35f).SetEase(Ease.OutBounce));
		}

		public virtual void Hide()
		{
			background.gameObject.SetActive(false);

			panel.DOComplete();
			var seq = DOTween.Sequence();
			seq.SetTarget(panel);
			seq.Append(panel.DOScale(0, .35f).SetEase(Ease.InBack));
			seq.AppendCallback(() => panel.gameObject.SetActive(false));
		}
	}
}
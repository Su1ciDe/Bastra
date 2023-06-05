using DG.Tweening;
using Gameplay;
using Interfaces;
using UnityEngine;

namespace UI
{
	public class CardSlot : MonoBehaviour, ISlot
	{
		private void OnDestroy()
		{
			this.DOKill();
		}

		public Sequence Place(Card card)
		{
			card.transform.SetParent(transform);

			var seq = DOTween.Sequence();
			seq.SetTarget(this);
			seq.Append(card.transform.DOLocalRotate(Vector3.zero, .5f).SetEase(Ease.InExpo));
			seq.Join(card.transform.DOLocalMove(Vector3.zero, .5f).SetEase(Ease.InOutQuad));
			return seq;
		}
	}
}
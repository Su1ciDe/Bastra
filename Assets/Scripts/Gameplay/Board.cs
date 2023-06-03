using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Utilities;

namespace Gameplay
{
	public class Board : Singleton<Board>
	{
		public List<Card> CardsInBoard { get; set; } = new List<Card>();

		public Tween PlayCardOnBoard(Card card)
		{
			CardsInBoard.Add(card);
			card.transform.SetParent(transform);
			var seq = DOTween.Sequence();

			seq.Append(card.transform.DORotate(Random.Range(-20f, 20f) * Vector3.forward, .5f).SetEase(Ease.InExpo));
			seq.Join(card.transform.DOMove(transform.position, .5f).SetEase(Ease.InOutSine));
			return seq;
		}
	}
}
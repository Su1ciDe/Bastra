using System.Collections.Generic;
using DG.Tweening;
using Interfaces;
using Managers;
using UnityEngine;
using Utilities;

namespace Gameplay
{
	public class Board : Singleton<Board>, ISlot
	{
		public List<Card> CardsInBoard { get; set; } = new List<Card>();

		public Sequence Place(Card card)
		{
			CardsInBoard.Add(card);
			card.transform.SetParent(transform);

			var seq = DOTween.Sequence();
			seq.Append(card.transform.DORotate(Random.Range(-20f, 20f) * Vector3.forward, .5f).SetEase(Ease.InExpo));
			seq.Join(card.transform.DOMove(transform.position, .5f).SetEase(Ease.InOutQuad));

			seq.AppendCallback(() =>
			{
				if (CardsInBoard.Count.Equals(2) && CardsInBoard[0].IsOpen && CardsInBoard[1].IsOpen && Card.IsCardsSame(CardsInBoard[0], CardsInBoard[1]))
				{
					Bastra(CardsInBoard[0], CardsInBoard[1]);
				}
				else if (CardsInBoard.Count >= 2 && CardsInBoard[0].IsOpen && CardsInBoard[1].IsOpen && Card.IsCardsSame(CardsInBoard[^1], CardsInBoard[^2]))
				{
					FishCards();
				}
			});

			return seq;
		}

		public void FishCards()
		{
			// last card is the played card
			var owner = CardsInBoard[^1].Owner;
			CardsInBoard[^1].Owner = null;

			int totalScore = 0;
			for (int i = 0; i < CardsInBoard.Count; i++)
				totalScore += CardsInBoard[i].CardSO.Score;

			owner.Score(CardsInBoard, totalScore);
			CardsInBoard.Clear();
			GameManager.Instance.PlayerScored(owner);
		}

		public void Bastra(Card card1, Card card2)
		{
			// card2 is the played card
			var owner = card2.Owner;
			card2.Owner = null;

			int totalScore = GameManager.Instance.BastraScore + card1.CardSO.Score;

			owner.Score(card1, card2, totalScore);
			CardsInBoard.Clear();
			GameManager.Instance.PlayerScored(owner);
		}
	}
}
using System.Collections.Generic;
using DG.Tweening;
using Gameplay.Players;
using Interfaces;
using Managers;
using UnityEngine;
using Utilities;

namespace Gameplay
{
	public class Board : Singleton<Board>, ISlot
	{
		public List<Card> CardsInBoard { get; set; } = new List<Card>();

		private void OnDestroy()
		{
			this.DOKill();
		}

		public Sequence Place(Card card)
		{
			CardsInBoard.Add(card);
			card.transform.SetParent(transform);

			var seq = DOTween.Sequence();
			seq.SetTarget(this);
			seq.Append(card.transform.DORotate(Random.Range(-20f, 20f) * Vector3.forward, .5f).SetEase(Ease.InExpo));
			seq.Join(card.transform.DOMove(transform.position, .5f).SetEase(Ease.InOutQuad));

			seq.AppendCallback(() =>
			{
				// if only 2 card played and they have the same rank
				if (CardsInBoard.Count.Equals(2) && CardsInBoard[0].IsOpen && CardsInBoard[1].IsOpen && Card.IsCardsSame(CardsInBoard[0], CardsInBoard[1]))
				{
					Bastra(CardsInBoard[0], CardsInBoard[1]);
				}
				// if board has more than 2 cards and the played cards have the same rank
				else if (CardsInBoard.Count > 2 && CardsInBoard[^1].IsOpen && CardsInBoard[^2].IsOpen && Card.IsCardsSame(CardsInBoard[^1], CardsInBoard[^2]))
				{
					var playedCard = CardsInBoard[^1];
					FishCards(playedCard.Owner);
					playedCard.Owner = null;
				}
				// if board has more than 2 cards and the played card is jack
				else if (CardsInBoard.Count >= 2 && CardsInBoard[^1].IsOpen && CardsInBoard[^2].IsOpen && CardsInBoard[^1].CardSO.CardRank == CardRank.Jack)
				{
					var playedCard = CardsInBoard[^1];
					FishCards(playedCard.Owner);
					playedCard.Owner = null;
				}
			});

			return seq;
		}

		public void FishCards(CardPlayer cardPlayer)
		{
			int totalScore = 0;
			for (int i = 0; i < CardsInBoard.Count; i++)
				totalScore += CardsInBoard[i].CardSO.Score;

			cardPlayer.AddScore(CardsInBoard, totalScore);
			CardsInBoard.Clear();
			GameManager.Instance.PlayerScored(cardPlayer);
		}

		public void Bastra(Card card1, Card card2)
		{
			// card2 is the played card
			var owner = card2.Owner;
			card2.Owner = null;

			int totalScore = GameManager.Instance.BastraScore + card1.CardSO.Score;

			owner.AddScore(card1, card2, totalScore);
			CardsInBoard.Clear();
			GameManager.Instance.PlayerScored(owner);
		}
	}
}
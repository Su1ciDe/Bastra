using System.Collections.Generic;
using DG.Tweening;
using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Players
{
	public class CardPlayer : MonoBehaviour
	{
		public int TotalScore { get; set; }

		public virtual string Name { get; set; }
		public virtual int Money { get; set; }

		public List<Card> Hand { get; set; } = new List<Card>();
		public List<Card> CollectedCards { get; set; } = new List<Card>();
		public List<Card> BastraCards { get; set; } = new List<Card>();

		private PlayerSlot playerSlot;

		public event UnityAction<int> OnScore;

		public virtual void Setup(PlayerSlot _playerSlot)
		{
			playerSlot = _playerSlot;
			playerSlot.Setup(Name, Money);
		}

		public virtual Sequence DealCard(Card card)
		{
			var cardSlot = playerSlot.CardSlots[Hand.Count];
			var seq = cardSlot.Place(card);

			Hand.Add(card);
			card.Owner = this;

			return seq;
		}

		public virtual void PlayCard(Card card)
		{
			Board.Instance.Place(card);
			Hand.Remove(card);
		}

		public void Score(List<Card> cards, int score)
		{
			TotalScore += score;
			// TODO: animations

			CollectedCards.AddRange(cards);

			OnScore?.Invoke(TotalScore);
		}

		public void Score(Card card1, Card bastraCard, int score)
		{
			TotalScore += score;
			// TODO: animations

			CollectedCards.Add(card1);
			BastraCards.Add(bastraCard);

			OnScore?.Invoke(TotalScore);
		}
	}
}
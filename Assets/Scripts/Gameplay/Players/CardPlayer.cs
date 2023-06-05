using System.Collections.Generic;
using DG.Tweening;
using Managers;
using UI;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Gameplay.Players
{
	public class CardPlayer : MonoBehaviour
	{
		public bool IsTurnToPlay { get; set; }
		public int TotalScore { get; set; }

		public virtual string PlayerName { get; set; }
		public virtual int PlayerMoney { get; set; }

		public List<Card> Hand { get; set; } = new List<Card>();
		public List<Card> CollectedCards { get; set; } = new List<Card>();
		public List<Card> BastraCards { get; set; } = new List<Card>();

		private PlayerSlot playerSlot;

		public static event UnityAction<ScoreType> OnScore;
		public event UnityAction OnPlayed;

		private void OnDestroy()
		{
			this.DOKill();
		}

		public virtual void Setup(PlayerSlot _playerSlot)
		{
			playerSlot = _playerSlot;
			playerSlot.Setup(PlayerName, PlayerMoney);
		}

		public virtual Sequence DealCard(Card card)
		{
			var cardSlot = playerSlot.CardSlots[Hand.Count];
			var seq = cardSlot.Place(card);

			Hand.Add(card);
			card.Owner = this;

			return seq;
		}

		public virtual void OnCardsDealt()
		{
		}

		public virtual void TurnToPlay()
		{
			IsTurnToPlay = true;
			playerSlot.ShowIndicator();
		}

		public virtual void PlayCard(Card card)
		{
			var sequence = Board.Instance.Place(card);
			Hand.Remove(card);
			IsTurnToPlay = false;
			playerSlot.HideIndicator();

			sequence.AppendCallback(() => OnPlayed?.Invoke());
		}

		public void AddScore(List<Card> cards, int score)
		{
			TotalScore += score;
			playerSlot.Score(TotalScore);

			const float delay = .15f;
			// animations
			var seq = DOTween.Sequence();
			seq.SetTarget(this);
			for (int i = 0; i < cards.Count; i++)
			{
				var card = cards[cards.Count - i - 1];
				card.Open(false);
				seq.Join(card.transform.DOMove(transform.position, .35f).SetDelay(delay).SetEase(Ease.InCirc).OnComplete(() => card.gameObject.SetActive(false)));
			}

			seq.AppendCallback(() => GameManager.Instance.CurrentState = GameManager.State.GamePlaying);

			CollectedCards.AddRange(cards);

			OnScore?.Invoke(ScoreType.Fish);
		}

		// bastra
		public void AddScore(Card card1, Card bastraCard, int score)
		{
			TotalScore += score;
			playerSlot.Score(TotalScore);

			// animations
			const float delay = .15f;
			bastraCard.transform.DOMove(transform.position, .5f).SetEase(Ease.InCirc).OnComplete(() => bastraCard.gameObject.SetActive(false));
			card1.transform.DOMove(transform.position, .5f).SetDelay(delay).SetEase(Ease.InCirc).OnComplete(() =>
			{
				card1.gameObject.SetActive(false);
				GameManager.Instance.CurrentState = GameManager.State.GamePlaying;
			});

			CollectedCards.Add(card1);
			BastraCards.Add(bastraCard);

			OnScore?.Invoke(ScoreType.Bastra);
		}

		public void AddScore(int score)
		{
			TotalScore += score;
			playerSlot.Score(TotalScore);
		}
	}
}
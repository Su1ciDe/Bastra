using System.Collections;
using System.Collections.Generic;
using Managers;
using UI;
using UnityEngine;
using Utilities;

namespace Gameplay.Players
{
	public class BotPlayer : CardPlayer
	{
		private List<Card> jacksInHand;

		public override void Setup(PlayerSlot _playerSlot)
		{
			PlayerMoney = Random.Range(5, GameManager.Instance.Bet / 100) * 100;
			PlayerName = "bot_" + _playerSlot.transform.GetSiblingIndex();
			base.Setup(_playerSlot);
		}

		public override void TurnToPlay()
		{
			base.TurnToPlay();
			StartCoroutine(ThinkACardToPlay());
		}

		private IEnumerator ThinkACardToPlay()
		{
			yield return new WaitForSeconds(Random.Range(.5f, 1.5f));

			if (Board.Instance.CardsInBoard.Count > 0)
			{
				// if has at least one jack & board has a score play with jack
				if (jacksInHand.Count > 0)
				{
					for (int i = 0; i < Board.Instance.CardsInBoard.Count; i++)
					{
						var cardInBoard = Board.Instance.CardsInBoard[i];
						if (cardInBoard.IsOpen && cardInBoard.CardSO.Score > 0)
						{
							PlayCard(jacksInHand[^1]);
							yield break;
						}
					}
				}

				for (int i = 0; i < Hand.Count; i++)
				{
					// Looks if can fish cards with same card
					if (Card.IsCardsSame(Board.Instance.CardsInBoard[^1], Hand[i]))
					{
						PlayCard(Hand[i]);
						yield break;
					}
				}
			}

			var randomCard = Hand[Random.Range(0, Hand.Count)];
			// To not drop the jack on an empty board
			while (randomCard.CardSO.CardRank == CardRank.Jack && Board.Instance.CardsInBoard.Count.Equals(0) && !jacksInHand.Count.Equals(Hand.Count))
			{
				randomCard = Hand[Random.Range(0, Hand.Count)];
			}

			PlayCard(randomCard);
		}

		public override void PlayCard(Card card)
		{
			card.Open();
			if (card.CardSO.CardRank == CardRank.Jack)
				jacksInHand.Remove(card);

			base.PlayCard(card);
		}

		public override void OnCardsDealt()
		{
			base.OnCardsDealt();
			jacksInHand = new List<Card>();

			foreach (var card in Hand)
			{
				if (card.CardSO.CardRank == CardRank.Jack)
					jacksInHand.Add(card);
			}
		}
	}
}
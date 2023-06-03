using System.Collections.Generic;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using Utilities;

namespace Gameplay
{
	public class Deck : Singleton<Deck>
	{
		public List<Card> Cards { get; private set; }

		[SerializeField] private DeckSO deckSO;

		[Space]
		[SerializeField] private Card cardPrefab;
		[SerializeField] private Transform cardsParent;
		[Space]
		[SerializeField] private TMP_Text txtDeckCount;

		private void Awake()
		{
			DealDeck();
		}

		public void DealDeck()
		{
			SpawnDeck();
			txtDeckCount.SetText(Cards.Count.ToString());
		}

		private void SpawnDeck()
		{
			Cards = new List<Card>();
			var cards = new List<CardSO>(deckSO.CardSOs);
			cards.Shuffle();
			for (int i = 0; i < cards.Count; i++)
			{
				var card = SpawnCard(cards[i]);
				// card.transform.localPosition = new Vector3(i * -0.5f, 0, 0);
				Cards.Add(card);
			}
		}

		public Card SpawnCard(CardSO cardSO)
		{
			var cardObj = Instantiate(cardPrefab, cardsParent);
			cardObj.Setup(cardSO);
			return cardObj;
		}

		public Card PickCard()
		{
			var card = Cards[^1];
			Cards.RemoveAt(Cards.Count - 1);
			txtDeckCount.SetText(Cards.Count.ToString());
			return card;
		}
	}
}
using System.Collections.Generic;
using ScriptableObjects;
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

		private void Start()
		{
			DealDeck();
		}

		public void DealDeck()
		{
			SpawnDeck();
		}

		private void SpawnDeck()
		{
			Cards = new List<Card>();
			var cards = new List<CardSO>(deckSO.CardSOs);
			cards.Shuffle();
			for (int i = 0; i < cards.Count; i++)
			{
				var card = SpawnCard(cards[i]);
				card.transform.localPosition = new Vector3(i * -0.5f, 0, 0);
				Cards.Add(card);
			}
		}

		public Card SpawnCard(CardSO cardSO)
		{
			var cardObj = Instantiate(cardPrefab, transform);
			cardObj.Setup(cardSO);
			return cardObj;
		}
	}
}
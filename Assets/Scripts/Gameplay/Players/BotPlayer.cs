using System.Collections;
using UI;
using UnityEngine;

namespace Gameplay.Players
{
	public class BotPlayer : CardPlayer
	{
		public override void Setup(PlayerSlot _playerSlot)
		{
			Money = Random.Range(5, 50) * 100;
			Name = "bot_" + _playerSlot.transform.GetSiblingIndex();
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

			PlayCard(Hand[Random.Range(0, Hand.Count)]);
		}

		public override void PlayCard(Card card)
		{
			card.Open();
			base.PlayCard(card);
		}
	}
}
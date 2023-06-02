using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace Gameplay
{
	public class Card : MonoBehaviour
	{
		public CardSO CardSO { get; private set; }

		[SerializeField] private Image frontFace;
		[SerializeField] private Image backFace;

		public void Setup(CardSO cardSO)
		{
			CardSO = cardSO;
			frontFace.sprite = cardSO.CardFaceSprite;
		}
	}
}
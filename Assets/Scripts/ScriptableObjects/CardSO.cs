using UnityEngine;
using Utilities;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Card", menuName = "Pişti/Card", order = 0)]
	public class CardSO : ScriptableObject
	{
		public CardSuit CardSuit;
		public CardRank CardRank;
		public Sprite CardFaceSprite;
		public string Name;
		public int Score;
	}
}
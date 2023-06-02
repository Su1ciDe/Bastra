using System.Collections.Generic;
using UnityEngine;

namespace ScriptableObjects
{
	[CreateAssetMenu(fileName = "Deck", menuName = "Pişti/Deck", order = 1)]
	public class DeckSO : ScriptableObject
	{
		public List<CardSO> CardSOs = new List<CardSO>();
	}
}
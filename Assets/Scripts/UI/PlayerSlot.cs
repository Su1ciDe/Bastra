using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerSlot : MonoBehaviour
	{
		public CardSlot[] CardSlots { get; private set; } = new CardSlot[4];

		[SerializeField] private TMP_Text txtScore;
		[SerializeField] private Transform handHolder;

		private PlayerProfileMini playerProfile;

		private void Awake()
		{
			playerProfile = GetComponentInChildren<PlayerProfileMini>();
			CardSlots = GetComponentsInChildren<CardSlot>();
		}

		public void Setup(string playerName, int money)
		{
			playerProfile.Setup(playerName, money);
		}

		public void Score(int score)
		{
			txtScore.SetText("Score: " + score.ToString());
		}
	}
}
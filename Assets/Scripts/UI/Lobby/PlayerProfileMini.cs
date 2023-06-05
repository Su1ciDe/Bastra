using Gameplay.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class PlayerProfileMini : MonoBehaviour
	{
		[SerializeField] private Image profilePicture;
		[SerializeField] private TMP_Text txtPlayerName;
		[SerializeField] private TMP_Text txtPlayerMoney;

		private void OnEnable()
		{
			Player.OnPlayerNameChanged += OnPlayerNameChanged;
		}

		private void OnDisable()
		{
			Player.OnPlayerNameChanged -= OnPlayerNameChanged;
		}

		private void OnPlayerNameChanged()
		{
			txtPlayerName.SetText(Player.Name);
		}

		public void Setup(string playerName, int money)
		{
			txtPlayerName.SetText(playerName);
			txtPlayerMoney.SetText("$" + money.ToString());
		}
	}
}
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

		public void Setup(string playerName, int money)
		{
			txtPlayerName.SetText(playerName);
			txtPlayerMoney.SetText("$" + money.ToString());
		}
	}
}
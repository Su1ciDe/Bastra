using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class PlayerProfile : MonoBehaviour
	{
		[SerializeField] private Image profilePicture;
		[SerializeField] private TMP_Text txtPlayerName;
		[SerializeField] private TMP_Text txtPlayerMoney;
		[SerializeField] private TMP_Text txtWinCount;
		[SerializeField] private TMP_Text txtLossCount;

		public void Setup(string playerName, int money, int winCount, int lossCount)
		{
			txtPlayerName.SetText(playerName);
			txtPlayerMoney.SetText("$" + money.ToString());
			txtWinCount.SetText(winCount.ToString());
			txtLossCount.SetText(lossCount.ToString());
		}
	}
}
using Managers;
using TMPro;
using UnityEngine;

namespace UI
{
	public class BetUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text txtBet;

		private void Start()
		{
			Setup(GameManager.Instance.Bet);
		}

		private void Setup(int betAmount)
		{
			txtBet.SetText("Bet: " + betAmount.ToString());
		}
	}
}
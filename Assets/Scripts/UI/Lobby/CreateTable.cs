using Gameplay.Players;
using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI.Lobby
{
	public class CreateTable : BasePanel
	{
		[Space]
		[SerializeField] private Slider sliderBetAmount;
		[SerializeField] private TMP_Text txtCurrentBetAmount;
		[SerializeField] private TMP_Text txtMinBet;
		[SerializeField] private TMP_Text txtMaxBet;
		[Space]
		[SerializeField] private Toggle tggl2Players;
		[SerializeField] private Toggle tggl4Players;
		[Space]
		[SerializeField] private Button btnCreateTable;

		private int sliderStep;

		protected override void Awake()
		{
			base.Awake();
			btnCreateTable.onClick.AddListener(CreateTableClicked);
			sliderBetAmount.onValueChanged.AddListener(OnSliderChanged);
		}

		public void Setup(TableSO tableSO)
		{
			sliderStep = tableSO.MinBet;
			sliderBetAmount.minValue = 1;
			sliderBetAmount.maxValue = Mathf.RoundToInt((float)tableSO.MaxBet / sliderStep);
			txtMinBet.SetText(tableSO.MinBet.ToString());
			txtMaxBet.SetText(tableSO.MaxBet.ToString());
			txtCurrentBetAmount.SetText((tableSO.MinBet * sliderStep).ToString());
		}

		private void OnSliderChanged(float sliderValue)
		{
			txtCurrentBetAmount.SetText(((int)sliderValue * sliderStep).ToString());
		}

		private void CreateTableClicked()
		{
			int betAmount = (int)sliderBetAmount.value * sliderStep;
			// if (betAmount > Player.Money)
			// {
			// 	MessageBoxUI.Instance.ShowMessage("Not Enough Money!");
			// 	return;
			// }

			if (tggl2Players.isOn)
				GameManager.Instance.PlayerCount = 2;
			else if (tggl4Players.isOn)
				GameManager.Instance.PlayerCount = 4;
			GameManager.Instance.Bet = betAmount;

			SceneLoader.Load(SceneLoader.Scenes.GameScene);
		}
	}
}
using Gameplay.Players;
using Managers;
using ScriptableObjects;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using Utilities;

namespace UI.Lobby
{
	public class TableUI : MonoBehaviour
	{
		public TableSO TableSO { get; private set; }

		[SerializeField] private TMP_Text txtTableName;
		[SerializeField] private TMP_Text txtBetRange;
		[SerializeField] private Button btnPlay;
		[SerializeField] private Button btnCreateTable;

		public event UnityAction<TableSO> OnCreateTable;

		private void Awake()
		{
			btnPlay.onClick.AddListener(PlayNow);
			btnCreateTable.onClick.AddListener(CreateTable);
		}

		public void Setup(TableSO tableSO)
		{
			TableSO = tableSO;

			txtTableName.SetText(tableSO.TableName);
			txtBetRange.SetText("Bet Range: " + Extensions.FormatBigNumber(tableSO.MinBet) + "-" + Extensions.FormatBigNumber(tableSO.MaxBet));

			// if players money not enough, cant create table or play
			if (Player.Money < tableSO.MinBet)
			{
				btnPlay.interactable = false;
				btnCreateTable.interactable = false;
			}
		}

		private void PlayNow()
		{
			// if players money not enough, its the max amount of the bet
			int maxBet = Player.Money > TableSO.MaxBet ? TableSO.MaxBet : Player.Money;
			int step = TableSO.MinBet;
			// random bet amount
			int randomBet = Random.Range(1, Mathf.RoundToInt((float)maxBet / step));

			GameManager.Instance.Bet = randomBet * step;
			GameManager.Instance.PlayerCount = 2;

			SceneLoader.Load(SceneLoader.Scenes.GameScene);
		}

		private void CreateTable()
		{
			OnCreateTable?.Invoke(TableSO);
		}
	}
}
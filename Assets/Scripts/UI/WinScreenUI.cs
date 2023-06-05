using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
	public class WinScreenUI : BaseScreen
	{
		[SerializeField] private TMP_Text txtBetWon;
		[SerializeField] private Button btnBackToLobby;
		[SerializeField] private Button btnNewGame;

		private void Awake()
		{
			btnBackToLobby.onClick.AddListener(BackToLobby);
			btnNewGame.onClick.AddListener(NewGame);
		}

		private void Start()
		{
			GameManager.OnGameWin += OnGameWon;
			Hide();
		}

		private void OnDestroy()
		{
			GameManager.OnGameWin -= OnGameWon;
		}

		private void OnGameWon(int betWon)
		{
			txtBetWon.SetText("Bet Won: $" + betWon.ToString());
			Show();
		}

		private void BackToLobby()
		{
			SceneLoader.Load(SceneLoader.Scenes.LobbyScene);
		}

		private void NewGame()
		{
			SceneLoader.Load(SceneLoader.Scenes.GameScene);
		}
	}
}
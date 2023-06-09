﻿using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
	public class LoseScreenUI : BaseScreen
	{
		[SerializeField] private TMP_Text txtBetLost;
		[SerializeField] private Button btnBackToLobby;
		[SerializeField] private Button btnNewGame;

		private void Awake()
		{
			btnBackToLobby.onClick.AddListener(BackToLobby);
			btnNewGame.onClick.AddListener(NewGame);
		}

		private void Start()
		{
			GameManager.OnGameLose += OnGameLost;
			Hide();
		}

		private void OnDestroy()
		{
			GameManager.OnGameLose -= OnGameLost;
		}

		private void OnGameLost(int betLost)
		{
			txtBetLost.SetText("Bet Lost: $" + betLost.ToString());
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
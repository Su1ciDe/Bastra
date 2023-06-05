using System;
using System.Threading.Tasks;
using DG.Tweening;
using Gameplay.Players;
using Managers;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
	public class MenuUI : BaseScreen
	{
		[SerializeField] private RectTransform menuPanel;
		[Space]
		[SerializeField] private Button btnOpenMenu;
		[SerializeField] private Button btnNewGame;
		[SerializeField] private Button btnBackToLobby;
		[Space]
		[SerializeField] private Button btnClose;
		[SerializeField] private Button btnClosePanel;

		private float panelStartingPosition;

		private void Awake()
		{
			panelStartingPosition = menuPanel.anchoredPosition.x;

			btnOpenMenu.onClick.AddListener(Show);
			btnBackToLobby.onClick.AddListener(BackToLobby);
			btnNewGame.onClick.AddListener(NewGame);

			btnClose.onClick.AddListener(Hide);
			btnClosePanel.onClick.AddListener(Hide);
			btnClosePanel.gameObject.SetActive(false);
		}

		private void OnDestroy()
		{
			menuPanel.DOKill();
		}

		public override void Show()
		{
			menuPanel.gameObject.SetActive(true);
			btnClosePanel.gameObject.SetActive(true);

			menuPanel.DOComplete();
			menuPanel.DOAnchorPosX(0, .5f).SetEase(Ease.OutCubic);
		}

		public override void Hide()
		{
			btnClosePanel.gameObject.SetActive(false);
			menuPanel.DOComplete();
			menuPanel.DOAnchorPosX(panelStartingPosition, .5f).SetEase(Ease.OutCubic).OnComplete(() => menuPanel.gameObject.SetActive(false));
		}

		private void BackToLobby()
		{
			//TODO: ask before leaving
			AskIfSure(() =>
			{
				Player.Money -= GameManager.Instance.Bet;
				SceneLoader.Load(SceneLoader.Scenes.LobbyScene);
			});
			// SceneLoader.Load(SceneLoader.Scenes.LobbyScene);
		}

		private void NewGame()
		{
			AskIfSure(() =>
			{
				Player.Money -= GameManager.Instance.Bet;
				SceneLoader.Load(SceneLoader.Scenes.GameScene);
			});

			// SceneLoader.Load(SceneLoader.Scenes.GameScene);
		}

		private void AskIfSure(Action action)
		{
			MessageBoxUI.Instance.ShowMessageDialog("If you leave, you are going to lose the bet.\nAre you sure you want to leave?",action);
		}
	}
}
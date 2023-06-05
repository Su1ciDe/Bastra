using Gameplay.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class PlayerProfile : BasePanel
	{
		[SerializeField] private Button background;
		[Space]
		[SerializeField] private Button btnOpenProfile;
		[Header("Player Profile Panel")]
		[SerializeField] private Button btnClosePanel;
		[SerializeField] private Image profilePicture;
		[SerializeField] private TMP_Text txtPlayerName;
		[SerializeField] private TMP_Text txtPlayerMoney;
		[SerializeField] private TMP_Text txtWinCount;
		[SerializeField] private TMP_Text txtLossCount;
		[SerializeField] private Button btnEditPlayerName;

		private PlayerProfileMini playerProfileMini;
		private EditPlayerNameUI editPlayerName;

		private void Awake()
		{
			playerProfileMini = GetComponentInChildren<PlayerProfileMini>();
			editPlayerName = GetComponentInChildren<EditPlayerNameUI>(true);

			btnOpenProfile.onClick.AddListener(OpenProfile);
			btnClosePanel.onClick.AddListener(CloseProfile);
			background.onClick.AddListener(CloseProfile);
			btnEditPlayerName.onClick.AddListener(editPlayerName.Show);
		}

		private void Start()
		{
			panel.gameObject.SetActive(false);
			background.gameObject.SetActive(false);

			playerProfileMini.Setup(Player.Name, Player.Money);
			Setup(Player.Name, Player.Money, Player.WinCount, Player.LossCount);
		}

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

		public void Setup(string playerName, int money, int winCount, int lossCount)
		{
			txtPlayerName.SetText(playerName);
			txtPlayerMoney.SetText("$" + money.ToString());
			txtWinCount.SetText(winCount.ToString());
			txtLossCount.SetText(lossCount.ToString());
		}

		private void OpenProfile()
		{
			background.gameObject.SetActive(true);
			Show();
		}

		private void CloseProfile()
		{
			background.gameObject.SetActive(false);
			Hide();
		}
	}
}
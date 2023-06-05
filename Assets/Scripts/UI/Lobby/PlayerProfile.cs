using Gameplay.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Lobby
{
	public class PlayerProfile : BasePanel
	{
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

		protected override void Awake()
		{
			base.Awake();

			playerProfileMini = GetComponentInChildren<PlayerProfileMini>();
			editPlayerName = GetComponentInChildren<EditPlayerNameUI>(true);

			btnOpenProfile.onClick.AddListener(Show);
			btnClosePanel.onClick.AddListener(Hide);
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
	}
}
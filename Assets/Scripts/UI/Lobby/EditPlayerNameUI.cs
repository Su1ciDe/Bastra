using Gameplay.Players;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Lobby
{
	public class EditPlayerNameUI : BasePanel
	{
		[SerializeField] private TMP_InputField inputName;
		[SerializeField] private Button btnOk;
		[SerializeField] private Button btnClose;

		protected override void Awake()
		{
			base.Awake();

			btnOk.onClick.AddListener(SetPlayerName);
			btnClose.onClick.AddListener(Hide);
			inputName.text = Player.Name;
		}

		private void SetPlayerName()
		{
			Player.Name = inputName.text;
			Hide();
		}
	}
}
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Utilities;

namespace UI
{
	public class MessageBoxUI : BasePanel
	{
		public static MessageBoxUI Instance;

		[Space]
		[SerializeField] private TMP_Text txtMessage;
		[SerializeField] private Button btnOk;

		protected override void Awake()
		{
			Instance = this;
			base.Awake();

			btnOk.onClick.AddListener(OkClicked);
		}

		private void OkClicked()
		{
			Hide();
		}

		public void ShowMessage(string message)
		{
			txtMessage.SetText(message);
			Show();
		}
	}
}
using System;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class MessageBoxUI : BasePanel
	{
		public static MessageBoxUI Instance;

		[Space]
		[SerializeField] private TMP_Text txtMessage;
		[SerializeField] private Button btnOk;
		[SerializeField] private Button btnCancel;

		private bool okClicked;
		private Action currentAction;

		protected override void Awake()
		{
			Instance = this;
			base.Awake();

			btnOk.onClick.AddListener(OkClicked);
			btnCancel.onClick.AddListener(CancelClicked);
		}

		private void OkClicked()
		{
			okClicked = true;
			Hide();
		}

		private void CancelClicked()
		{
			okClicked = false;
			Hide();
		}

		public void ShowMessage(string message)
		{
			txtMessage.SetText(message);
			Show();
			btnCancel.gameObject.SetActive(false);
		}

		public void ShowMessageDialog(string message, Action action)
		{
			txtMessage.SetText(message);
			btnCancel.gameObject.SetActive(true);
			Show();

			currentAction = action;
		}

		public override void Hide()
		{
			base.Hide();
			if (okClicked)
				currentAction?.Invoke();

			currentAction = null;
		}
	}
}
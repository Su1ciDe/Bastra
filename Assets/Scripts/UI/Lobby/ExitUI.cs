using UnityEngine;
using UnityEngine.UI;

namespace UI.Lobby
{
	public class ExitUI : MonoBehaviour
	{
		[SerializeField] private Button btnExit;

		private void Awake()
		{
			btnExit.onClick.AddListener(Application.Quit);
		}
	}
}
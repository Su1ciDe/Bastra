using UnityEngine;
using UnityEngine.UI;

namespace UI
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
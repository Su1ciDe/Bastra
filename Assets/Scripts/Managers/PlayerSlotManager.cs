using UI;
using Utilities;

namespace Managers
{
	public class PlayerSlotManager : Singleton<PlayerSlotManager>
	{
		public PlayerSlot[] PlayerSlots { get; private set; }

		private void Awake()
		{
			if (GameManager.Instance.PlayerCount.Equals(2))
			{
				transform.GetChild(1).gameObject.SetActive(false);
				transform.GetChild(3).gameObject.SetActive(false);
			}

			PlayerSlots = GetComponentsInChildren<PlayerSlot>();
		}
	}
}
using UI;
using UnityEngine;

namespace Gameplay.Players
{
	public class BotPlayer : CardPlayer
	{
		public override void Setup(PlayerSlot _playerSlot)
		{
			Money = Random.Range(5, 50) * 100;
			Name = "bot_" + _playerSlot.transform.GetSiblingIndex();
			base.Setup(_playerSlot);
		}

		public void ThinkACardToPlay()
		{
			
		}
	}
}
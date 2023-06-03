using UnityEngine;

namespace Gameplay
{
	public class Player : CardPlayer
	{
		public static Player Instance;

		private void Awake()
		{
			Instance = this;
		}
	}
}
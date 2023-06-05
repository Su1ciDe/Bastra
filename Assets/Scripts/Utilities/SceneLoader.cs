using UnityEngine.SceneManagement;

namespace Utilities
{
	public static class SceneLoader
	{
		public enum Scenes
		{
			LobbyScene = 0,
			GameScene = 1,
		}

		public static void Load(Scenes targetSceneName)
		{
			SceneManager.LoadSceneAsync((int)targetSceneName);
		}
	}
}
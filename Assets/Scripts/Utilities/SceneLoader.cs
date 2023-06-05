using UnityEngine;
using UnityEngine.Events;
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

		private static Scenes targetScene;
		public static event UnityAction<Scenes> OnSceneLoad;

		public static void Load(Scenes _targetScene)
		{
			targetScene = _targetScene;
			var loadSceneAsync = SceneManager.LoadSceneAsync((int)_targetScene);
			loadSceneAsync.completed += SceneLoaded;
		}

		private static void SceneLoaded(AsyncOperation op)
		{
			OnSceneLoad?.Invoke(targetScene);
		}
	}
}
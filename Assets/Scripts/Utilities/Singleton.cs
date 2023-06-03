﻿using UnityEngine;

namespace Utilities
{
	public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
	{
		private static object _lock = new object();
		private static T _instance;

		public static T Instance
		{
			get
			{
				lock (_lock)
				{
					if (!_instance)
						_instance = FindAnyObjectByType<T>();

					return _instance;
				}
			}
		}
	}
}
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace Utilities
{
	public static class Extensions
	{
		public static void Shuffle<T>(this IList<T> list)
		{
			var rng = new global::System.Random(Random.Range(0, 1000));
			var n = list.Count;
			while (n > 1)
			{
				n--;
				var k = rng.Next(n + 1);
				(list[k], list[n]) = (list[n], list[k]);
			}
		}

		/// <summary>
		/// Picks a random item from the list.
		/// </summary>
		/// <returns>A random item</returns>
		public static T RandomItem<T>(this IList<T> list)
		{
			if (list.Count.Equals(0)) return default;

			var index = Random.Range(0, list.Count);
			return list[index];
		}

		/// <summary>
		/// Picks a random item and removes it from the list.
		/// </summary>
		/// <returns>A random item</returns>
		public static T PickRandomItem<T>(this List<T> list)
		{
			var picked = list.RandomItem();
			list.Remove(picked);
			return picked;
		}
		
		public static string FormatBigNumber(long number)
		{
			return number switch
			{
				> 999999999999999 => number.ToString("0,,,,,.##Q", CultureInfo.InvariantCulture),
				> 999999999999 => number.ToString("0,,,,.##T", CultureInfo.InvariantCulture),
				> 999999999 => number.ToString("0,,,.##B", CultureInfo.InvariantCulture),
				> 999999 => number.ToString("0,,.##M", CultureInfo.InvariantCulture),
				> 999 => number.ToString("0,.##K", CultureInfo.InvariantCulture),
				_ => number.ToString(CultureInfo.InvariantCulture)
			};
		}
	}
}
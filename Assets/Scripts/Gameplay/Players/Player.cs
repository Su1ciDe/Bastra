using DG.Tweening;
using UnityEngine;

namespace Gameplay.Players
{
	public class Player : CardPlayer
	{
		public static Player Instance;

		public override string Name
		{
			get => PlayerPrefs.GetString("PlayerName", "Player");
			set => PlayerPrefs.SetString("PlayerName", value);
		}
		public override int Money
		{
			get => PlayerPrefs.GetInt("Money", 1000);
			set => PlayerPrefs.SetInt("Money", value);
		}
		public int WinCount
		{
			get => PlayerPrefs.GetInt("WinCount", 0);
			set => PlayerPrefs.SetInt("WinCount", value);
		}
		public int LossCount
		{
			get => PlayerPrefs.GetInt("LossCount", 0);
			set => PlayerPrefs.SetInt("LossCount", value);
		}

		private void Awake()
		{
			Instance = this;
		}

		public override Sequence DealCard(Card card)
		{
			var seq = base.DealCard(card);
			seq.AppendCallback(() => card.Open());
			return seq;
		}
	}
}
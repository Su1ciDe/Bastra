using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

namespace Gameplay.Players
{
	public class Player : CardPlayer
	{
		public static Player Instance;

		public static string Name
		{
			get => PlayerPrefs.GetString("PlayerName", "Player");
			set
			{
				PlayerPrefs.SetString("PlayerName", value);
				OnPlayerNameChanged?.Invoke();
			}
		}
		public static int Money
		{
			get => PlayerPrefs.GetInt("Money", 1000);
			set => PlayerPrefs.SetInt("Money", value);
		}
		public override string PlayerName
		{
			get => Name;
			set => Name = value;
		}
		public override int PlayerMoney
		{
			get => Money;
			set => Money = value;
		}
		public static int WinCount
		{
			get => PlayerPrefs.GetInt("WinCount", 0);
			set => PlayerPrefs.SetInt("WinCount", value);
		}
		public static int LossCount
		{
			get => PlayerPrefs.GetInt("LossCount", 0);
			set => PlayerPrefs.SetInt("LossCount", value);
		}

		public static event UnityAction OnPlayerNameChanged;

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

		public void WinMatch(int wonMoney)
		{
			WinCount++;
			Money += wonMoney;
		}

		public void LoseMatch()
		{
			LossCount++;
		}
	}
}
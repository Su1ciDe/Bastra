using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class PlayerSlot : MonoBehaviour
	{
		public CardSlot[] CardSlots { get; private set; } = new CardSlot[4];

		[SerializeField] private TMP_Text txtScore;
		[SerializeField] private Transform handHolder;
		[SerializeField] private Image turnIndicator;
		private Vector3 turnIndicatorStartPosition;

		private PlayerProfileMini playerProfile;

		private void Awake()
		{
			playerProfile = GetComponentInChildren<PlayerProfileMini>();
			CardSlots = handHolder.GetComponentsInChildren<CardSlot>();
			turnIndicatorStartPosition = turnIndicator.transform.localPosition;
		}

		public void Setup(string playerName, int money)
		{
			playerProfile.Setup(playerName, money);
		}

		public void Score(int score)
		{
			txtScore.transform.DOComplete();
			txtScore.transform.DOScale(1.25f, .25f).SetEase(Ease.InOutSine).SetLoops(2,LoopType.Yoyo);
			txtScore.SetText("Score: " + score.ToString());
		}

		public void ShowIndicator()
		{
			turnIndicator.gameObject.SetActive(true);
			turnIndicator.transform.DOLocalMoveY(-25, .5f).SetEase(Ease.InOutSine).SetRelative(true).SetLoops(-1, LoopType.Yoyo);
		}

		public void HideIndicator()
		{
			turnIndicator.transform.DOKill();
			turnIndicator.transform.localPosition = turnIndicatorStartPosition;
			turnIndicator.gameObject.SetActive(false);
		}
	}
}
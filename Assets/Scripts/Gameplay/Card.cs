using DG.Tweening;
using Gameplay.Players;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Gameplay
{
	public class Card : MonoBehaviour, IPointerClickHandler
	{
		public CardSO CardSO { get; private set; }
		public CardPlayer Owner { get; set; }

		public bool IsOpen => !backFace.gameObject.activeSelf;

		[SerializeField] private Image frontFace;
		[SerializeField] private Image backFace;

		private void OnDestroy()
		{
			transform.DOKill();
		}

		public void Setup(CardSO cardSO)
		{
			CardSO = cardSO;
			frontFace.sprite = cardSO.CardFaceSprite;
		}

		public void Open(bool isAnimated = true)
		{
			if (isAnimated)
			{
				transform.DOScaleX(0, .25f).SetEase(Ease.InQuad).OnComplete(() =>
				{
					backFace.gameObject.SetActive(false);
					transform.DOScaleX(1, .25f).SetEase(Ease.OutQuad);
				});
			}
			else
			{
				backFace.gameObject.SetActive(false);
			}
		}

		public void Close(bool isAnimated = true)
		{
			if (isAnimated)
			{
				transform.DOScaleX(0, .25f).SetEase(Ease.InQuad).OnComplete(() =>
				{
					backFace.gameObject.SetActive(true);
					transform.DOScaleX(1, .25f).SetEase(Ease.OutQuad);
				});
			}
			else
			{
				backFace.gameObject.SetActive(true);
			}
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (GameManager.Instance.CurrentState != GameManager.State.GamePlaying) return;
			if (!Owner.Equals(Player.Instance)) return;
			if (!Player.Instance.IsTurnToPlay) return;

			Player.Instance.PlayCard(this);
		}

		public static bool IsCardsSame(Card card1, Card card2)
		{
			return card1.CardSO.CardRank == card2.CardSO.CardRank;
		}
	}
}
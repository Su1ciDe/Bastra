using DG.Tweening;
using Managers;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Gameplay
{
	public class Card : MonoBehaviour, IPointerClickHandler
	{
		public CardSO CardSO { get; private set; }
		public CardPlayer Owner { get; set; }

		[SerializeField] private Image frontFace;
		[SerializeField] private Image backFace;

		public void Setup(CardSO cardSO)
		{
			CardSO = cardSO;
			frontFace.sprite = cardSO.CardFaceSprite;
		}

		public void Open()
		{
			transform.DOScaleX(0, .25f).SetEase(Ease.InQuad).OnComplete(() =>
			{
				backFace.gameObject.SetActive(false);
				transform.DOScaleX(1, .25f).SetEase(Ease.OutQuad);
			});
		}

		public void Close()
		{
			backFace.gameObject.SetActive(true);
		}

		public void OnPointerClick(PointerEventData eventData)
		{
			if (GameManager.Instance.CurrentState != GameManager.State.GamePlaying) return;
			if (!Owner.Equals(Player.Instance)) return;
		}
	}
}
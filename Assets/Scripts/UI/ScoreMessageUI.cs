using DG.Tweening;
using Gameplay.Players;
using TMPro;
using UnityEngine;
using Utilities;

namespace UI
{
	public class ScoreMessageUI : MonoBehaviour
	{
		[SerializeField] private TMP_Text txtMessage;

		private void OnEnable()
		{
			CardPlayer.OnScore += ShowMessage;
		}

		private void OnDestroy()
		{
			CardPlayer.OnScore -= ShowMessage;
			
			txtMessage.transform.DOComplete();
			txtMessage.DOComplete();
		}

		private void ShowMessage(ScoreType scoreType)
		{
			string scoreMessage = scoreType.ToString().ToUpper();
			txtMessage.SetText(scoreMessage);
			txtMessage.gameObject.SetActive(true);
			txtMessage.transform.DOComplete();
			txtMessage.transform.DOScale(2, 1).SetEase(Ease.OutQuad).OnComplete(() =>
			{
				txtMessage.transform.localScale = Vector3.one;
				txtMessage.gameObject.SetActive(false);
			});
			txtMessage.DOComplete();
			txtMessage.DOFade(0, 1).SetEase(Ease.OutQuad).OnComplete(() => txtMessage.alpha = 1);
		}
	}
}
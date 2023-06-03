using System;
using DG.Tweening;
using Gameplay;
using UnityEngine;
using UnityEngine.Events;
using Utilities;

namespace Managers
{
	public class GameManager : Singleton<GameManager>
	{
		public int PlayerCount { get; set; }

		public enum State
		{
			WaitingToStart,
			Cutting,
			DealingCardsToBoard,
			DealingCards,
			GamePlaying,
			GameOver
		}

		public State CurrentState
		{
			get => currentState;
			set
			{
				currentState = value;
				OnStateChanged?.Invoke(currentState);
			}
		}
		private State currentState;
		public event UnityAction<State> OnStateChanged;

		private void Start()
		{
			CurrentState = State.DealingCardsToBoard;
		}

		private void Update()
		{
			switch (CurrentState)
			{
				case State.WaitingToStart:

					break;
				case State.Cutting:
					break;
				case State.DealingCardsToBoard:
					DealCardsToBoard();
					break;
				case State.DealingCards:
					DealCardsToPlayers();
					break;
				case State.GamePlaying:
					break;
				case State.GameOver:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void DealCardsToBoard()
		{
			Tween tween = default;
			for (int i = 0; i < 4; i++)
				tween = Board.Instance.PlayCardOnBoard(Deck.Instance.PickCard()).SetDelay(i * .25f);

			tween.OnComplete(() => Board.Instance.CardsInBoard[^1].Open());

			CurrentState = State.DealingCards;
		}

		private void DealCardsToPlayers()
		{
			for (int i = 0; i < PlayerCount; i++)
			{
			}
		}
	}
}
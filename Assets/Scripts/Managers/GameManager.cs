using System;
using System.Collections.Generic;
using DG.Tweening;
using Gameplay;
using Gameplay.Players;
using UnityEngine.Events;
using Utilities;

namespace Managers
{
	public class GameManager : Singleton<GameManager>
	{
		public int PlayerCount { get; set; } = 4;
		public List<CardPlayer> Players { get; private set; } = new List<CardPlayer>();
		public int Bet { get; set; }

		public CardPlayer LastScoredPlayer { get; set; }

		public int CurrentTurnCount { get; set; }

		public int BastraScore = 10;

		public enum State
		{
			WaitingToStart,
			DealingCardsToBoard,
			DealingCards,
			GamePlaying,
			Scoring,
			GameOver
		}

		public State CurrentState
		{
			get => currentState;
			set
			{
				currentState = value;
				UpdateState();
			}
		}
		private State currentState;
		public event UnityAction<State> OnStateChanged;

		private void Awake()
		{
			DontDestroyOnLoad(this);
		}

		private void Start()
		{
			SetupPlayers();

			CurrentState = State.DealingCardsToBoard;
		}

		private void UpdateState()
		{
			switch (CurrentState)
			{
				case State.WaitingToStart:
					break;
				case State.DealingCardsToBoard:
					DealCardsToBoard();

					break;
				case State.DealingCards:
					DealCardsToPlayers();

					break;
				case State.GamePlaying:
					InformPlayerToPlay();

					break;
				case State.Scoring:
					break;
				case State.GameOver:
					FinishGame();

					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			OnStateChanged?.Invoke(currentState);
		}

		private void SetupPlayers()
		{
			Player.Instance.Setup(PlayerSlotManager.Instance.PlayerSlots[0]);
			Players.Add(Player.Instance);
			Player.Instance.OnPlayed += NextPlayerTurn;

			for (int i = 1; i < PlayerCount; i++)
			{
				var bot = PlayerSlotManager.Instance.PlayerSlots[i].gameObject.AddComponent<BotPlayer>();
				bot.Setup(PlayerSlotManager.Instance.PlayerSlots[i]);
				bot.OnPlayed += NextPlayerTurn;
				Players.Add(bot);
			}
		}

		private void DealCardsToBoard()
		{
			Sequence tween = default;
			for (int i = 0; i < 4; i++)
			{
				tween = Board.Instance.Place(Deck.Instance.PickCard()).SetDelay(i * .25f);
				Board.Instance.CardsInBoard[i].Close(false);
			}

			tween.OnComplete(() =>
			{
				Board.Instance.CardsInBoard[^1].Open();
				CurrentState = State.DealingCards;
			});
		}

		private void DealCardsToPlayers()
		{
			if (Deck.Instance.Cards.Count <= 0)
			{
				CurrentState = State.GameOver;
				return;
			}

			Sequence seq = default;
			for (int i = 0; i < Players.Count; i++)
			{
				// deal 4 cards to each player
				for (int j = 0; j < 4; j++)
				{
					var card = Deck.Instance.PickCard();
					seq = Players[i].DealCard(card).SetDelay(i + j * .25f);
					card.Close(false);
				}
			}

			seq.OnComplete(() => CurrentState = State.GamePlaying);
		}

		public void PlayerScored(CardPlayer cardPlayer)
		{
			LastScoredPlayer = cardPlayer;
			CurrentState = State.Scoring;
		}

		private void NextPlayerTurn()
		{
			CurrentTurnCount++;

			if (CurrentState != State.GamePlaying) return;
			InformPlayerToPlay();
		}

		private void InformPlayerToPlay()
		{
			var nextPlayer = Players[CurrentTurnCount % Players.Count];
			if (nextPlayer.Hand.Count > 0)
				nextPlayer.TurnToPlay();
			else
				CurrentState = State.DealingCards;
		}

		private void FinishGame()
		{
			Board.Instance.FishCards(LastScoredPlayer);

			CalculateMostCardCollected();

			CardPlayer winner = null;
			int mostScore = 0;
			foreach (var player in Players)
			{
				if (player.TotalScore > mostScore)
				{
					mostScore = player.TotalScore;
					winner = player;
				}
			}

			if (winner is Player)
			{
				// player wins
				Player.Instance.WinMatch(Bet * Players.Count);
			}
			else
			{
				// player loses
				Player.Instance.LoseMatch();
			}
		}

		private void CalculateMostCardCollected()
		{
			CardPlayer mostCardPlayer = null;
			int mostCardsCount = 0;
			foreach (var player in Players)
			{
				if (player.CollectedCards.Count > mostCardsCount)
				{
					mostCardsCount = player.CollectedCards.Count;
					mostCardPlayer = player;
				}
			}

			mostCardPlayer.AddScore(3);
		}
	}
}
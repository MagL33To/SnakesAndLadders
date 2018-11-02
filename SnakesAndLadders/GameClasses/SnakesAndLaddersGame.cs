using System.Collections.Generic;
using System.Linq;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.GameClasses
{
    public class SnakesAndLaddersGame : ISnakesAndLaddersGame
    {
        private readonly IBoard _gameBoard;
        private readonly int _maxBoardSpace;
        private readonly IDice _dice;
        private readonly IList<int> _playerIds;

        public SnakesAndLaddersGame(IBoard gameBoard, IDice dice, int numberOfPlayers)
        {
            _gameBoard = gameBoard;
            _gameBoard.NewGame(numberOfPlayers);
            _maxBoardSpace = _gameBoard.Spaces.Count - 1;
            _dice = dice;
            _playerIds = new List<int>();
            for (var i = 1; i <= numberOfPlayers; i++)
            {
                _playerIds.Add(i);
            }
        }

        public Dictionary<int, int> GetPlayerPositions()
        {
            var positons = _playerIds.ToDictionary(playerId => playerId, playerId => _gameBoard.GetPlayerPosition(playerId));
            return positons;
        }

        public MoveResult AttemptMove(int playerId)
        {
            var moveAmount = _dice.RollDice();

            var currentPosition = _gameBoard.GetPlayerPosition(playerId);
            if (currentPosition == -1)
                return new MoveResult(playerId, 0, "Invalid playerId");
            var potentialPosition = currentPosition + moveAmount;
            var finalPosition = _gameBoard.MakeMove(playerId, potentialPosition);

            string flavourText;
            if (finalPosition == -1)
            {
                flavourText = potentialPosition > _maxBoardSpace
                    ? $"Player {playerId} went too far and was returned to their position."
                    //shouldn't actually be able to happen
                    : "Invalid move";
                return new MoveResult(playerId, currentPosition, flavourText);
            }

            flavourText = finalPosition == _maxBoardSpace
                ? $"Player {playerId} wins!"
                : $"Player {playerId} moved from square {currentPosition + 1} to square {potentialPosition + 1}";

            return new MoveResult(playerId, finalPosition, flavourText);
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Moq;
using NUnit.Framework;
using SnakesAndLadders.GameClasses;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.Tests
{
    [TestFixture]
    public class SnakesAndLaddersTests
    {
        private Mock<IDice> _diceMock;
        private ISnakesAndLaddersGame _game;

        [Test]
        public void GetPlayerPositions_AtStartOfGame_PlayersAreOnFirstSquare()
        {
            //set up a 100 space board
            var board = new Board(100);
            _diceMock = new Mock<IDice>();
            _game = new SnakesAndLaddersGame(board, _diceMock.Object, 2);

            var positions = _game.GetPlayerPositions();

            Assert.IsTrue(positions.All(p => p.Value == 0));
        }

        [Test]
        public void AttemptMove_FromSquare1Move3Sapaces_PlayerIsOnSquare4()
        {
            var board = new Board(100);
            _diceMock = new Mock<IDice>();
            //mock the dice roll to return 3
            _diceMock.Setup(x => x.RollDice()).Returns(3);
            _game = new SnakesAndLaddersGame(board, _diceMock.Object, 1);

            var result = _game.AttemptMove(1);

            Assert.AreEqual(3, result.ResultingSquare);
        }

        [Test]
        public void AttemptMove_FromSquare1Move3SapacesThen4More_PlayerIsOnSquare8()
        {
            const int boardSize = 100;
            var board = new Board(boardSize);
            _diceMock = new Mock<IDice>();
            _diceMock.Setup(x => x.RollDice()).Returns(3);
            _game = new SnakesAndLaddersGame(board, _diceMock.Object, 1);

            //mock the dice roll to return three, make a move, then mock it to return 4
            _game.AttemptMove(1);
            _diceMock.Setup(x => x.RollDice()).Returns(4);
            var result = _game.AttemptMove(1);

            Assert.AreEqual(7, result.ResultingSquare);
        }

        [Test]
        public void AttemptMove_FromSquare97Move3Sapaces_PlayerIsOnSquare100()
        {
            const int boardSize = 100;
            var board = new Board(boardSize);
            _diceMock = new Mock<IDice>();
            _diceMock.Setup(x => x.RollDice()).Returns(3);
            _game = new SnakesAndLaddersGame(board, _diceMock.Object, 1);
            //load a custom game state where the player is on square 97
            var gameState = CreateGameState(boardSize, new Dictionary<int, HashSet<int>> { { 96, new HashSet<int> { 1 } } });
            board.LoadGameState(gameState);

            var result = _game.AttemptMove(1);

            Assert.AreEqual(99, result.ResultingSquare);
        }

        [Test]
        public void AttemptMove_FromSquare97Move4Sapaces_PlayerIsOnSquare97()
        {
            const int boardSize = 100;
            var board = new Board(boardSize);
            _diceMock = new Mock<IDice>();
            _diceMock.Setup(x => x.RollDice()).Returns(4);
            _game = new SnakesAndLaddersGame(board, _diceMock.Object, 1);
            var gameState = CreateGameState(boardSize, new Dictionary<int, HashSet<int>> { { 96, new HashSet<int> { 1 } } });
            board.LoadGameState(gameState);

            var result = _game.AttemptMove(1);

            Assert.AreEqual(96, result.ResultingSquare);
        }

        [Test]
        public void AttemptMove_InvalidPlayerId_InvalidPlayerId()
        {
            const int boardSize = 100;
            var board = new Board(boardSize);
            _game = new SnakesAndLaddersGame(board, new D6(), 1);

            var result = _game.AttemptMove(2);

            Assert.AreEqual("Invalid playerId", result.FlavourText);
        }

        private static IList<HashSet<int>> CreateGameState(int boardSize, Dictionary<int, HashSet<int>> positions)
        {
            var gameState = new List<HashSet<int>>(boardSize);
            for (var i = 0; i < boardSize; i++)
            {
                gameState.Add(positions.ContainsKey(i) ? positions[i] : new HashSet<int>());
            }
            return gameState;
        }
    }
}
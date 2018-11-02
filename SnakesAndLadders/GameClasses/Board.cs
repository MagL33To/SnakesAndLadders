using System.Collections.Generic;
using System.Linq;
using SnakesAndLadders.Interfaces;

namespace SnakesAndLadders.GameClasses
{
    public class Board : IBoard
    {
        public IList<HashSet<int>> Spaces { get; private set; }

        public Board(int size)
        {
            Spaces = new List<HashSet<int>>(size);
            for (var i = 0; i < size; i++)
            {
                Spaces.Add(new HashSet<int>());
            }
        }

        public void NewGame(int numberOfPlayers)
        {
            for (var i = 1; i <= numberOfPlayers; i++)
            {
                Spaces[0].Add(i);
            }
        }

        public void LoadGameState(IList<HashSet<int>> gameState)
        {
            Spaces = gameState;
        }

        public int GetPlayerPosition(int playerId)
        {
            return Spaces.IndexOf(Spaces.SingleOrDefault(space => space.Contains(playerId)));
        }

        public int MakeMove(int playerId, int toPos)
        {
            var pos = GetPlayerPosition(playerId);
            if (pos == -1 || toPos > Spaces.Count - 1)
                return -1;

            Spaces[pos].Remove(playerId);
            Spaces[toPos].Add(playerId);

            return toPos;
        }
    }
}
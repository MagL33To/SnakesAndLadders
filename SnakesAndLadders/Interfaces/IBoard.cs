using System.Collections.Generic;

namespace SnakesAndLadders.Interfaces
{
    public interface IBoard
    {
        IList<HashSet<int>> Spaces { get; }
        void NewGame(int numberOfPlayers);
        void LoadGameState(IList<HashSet<int>> gameState);
        int GetPlayerPosition(int playerId);
        int MakeMove(int playerId, int toPos);
    }
}
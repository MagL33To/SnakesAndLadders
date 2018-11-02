using System.Collections.Generic;
using SnakesAndLadders.GameClasses;

namespace SnakesAndLadders.Interfaces
{
    public interface ISnakesAndLaddersGame
    {
        Dictionary<int, int> GetPlayerPositions();
        MoveResult AttemptMove(int playerId);
    }
}
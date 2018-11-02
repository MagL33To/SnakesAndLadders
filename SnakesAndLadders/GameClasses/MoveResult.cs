namespace SnakesAndLadders.GameClasses
{
    public class MoveResult
    {
        public int PlayerId { get; }
        public int ResultingSquare { get; }
        public string FlavourText { get; }

        public MoveResult(int playerId, int resultingSquare, string flavourText)
        {
            PlayerId = playerId;
            ResultingSquare = resultingSquare;
            FlavourText = flavourText;
        }
    }
}
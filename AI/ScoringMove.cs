
/// <summary>
/// Clase Scoring move, contiene el move que se hace y el score que vale ese scoringMove
/// </summary>
public class ScoringMove
{
    public int score;
    public Move move;

    public ScoringMove(int _score, Move _mov)
    {
        score = _score;
        move = _mov;
    }
}

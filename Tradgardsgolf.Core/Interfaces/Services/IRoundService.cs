namespace Tradgardsgolf.Core.Interfaces.Services
{
    public interface IRoundService
    {
        bool Validate(object createRoundModel);
        object CreateRound(int courseId);
        void CreateRoundScore(object roundScoreModel);
    }
}

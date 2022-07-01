namespace Ncfe.NcfeCodeTestCore.Services
{
    public interface ILearnerService
    {
        Learner GetLearner(int learnerId, bool isLearnerArchived);
    }
}

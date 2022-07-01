using System;
using System.Configuration;
using Ncfe.NcfeCodeTestCore.Model;
using Ncfe.NcfeCodeTestCore.DataConstant;

namespace Ncfe.NcfeCodeTestCore.Services
{
    public class LearnerService : ILearnerService
    {

        #region Declaration

        private readonly ArchivedDataService _archivedDataService;
        private readonly FailoverRepository _failoverRepository;
        private readonly LearnerDataAccess _learnerDataAccess;

        #endregion

        #region Methods

        public LearnerService(ArchivedDataService archivedDataService, FailoverRepository failoverRepository, LearnerDataAccess learnerDataAccess)
        {
            _archivedDataService = archivedDataService;
            _failoverRepository = failoverRepository;
            _learnerDataAccess = learnerDataAccess;
        }

        public Learner GetLearner(int learnerId, bool isLearnerArchived)
        {
            Learner learner = null;
            try
            {
                if (learnerId > 0)
                {
                    if (isLearnerArchived)
                    {
                        learner = _archivedDataService.GetArchivedLearner(learnerId);
                        return learner;
                    }
                    else
                    {
                        var learnerFromFailOverDB = GetLearnerFromFailOverDB(learnerId);
                        if (learnerFromFailOverDB == null)
                        {
                            var learnerFromThirdParty = GetLearnerFromThirdParty(learnerId);
                            return learnerFromThirdParty;
                        }
                        else
                        {
                            return learnerFromFailOverDB;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return learner;
        }


        private Learner GetLearnerFromFailOverDB(int learnerId)
        {
            var failedRequests = 0;
            LearnerResponse learnerResponse = null;

            var failoverEntries = _failoverRepository.GetFailOverEntries();

            if (failoverEntries != null && failoverEntries.Count > 0)
            {
                foreach (var failoverEntry in failoverEntries)
                {
                    if (failoverEntry.DateTime > DateTime.Now.AddMinutes(-10))
                    {
                        failedRequests++;
                    }
                }
            }
            if (failedRequests > FailoverConstant.FailedRequests && ConfigurationManager.AppSettings[FailoverConstant.IsFailoverModeEnabled].ToString().ToLower() == "true")
            {
                learnerResponse = FailoverLearnerDataAccess.GetLearnerById(learnerId);
            }
            return learnerResponse.Learner;
        }

        private Learner GetLearnerFromThirdParty(int learnerId)
        {
            Learner learnerFromThirdParty = null;
            var learnerResponse = _learnerDataAccess.LoadLearner(learnerId);
            if (learnerResponse.IsArchived)
                learnerFromThirdParty = _archivedDataService.GetArchivedLearner(learnerId);
            return learnerFromThirdParty;
        }

        #endregion
    }
}

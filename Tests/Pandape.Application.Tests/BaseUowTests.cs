using Pandape.Infrastructure.Domain.Dto;

namespace Pandape.Application
{
    public class BaseUowTests
    {
        protected MockClockManager _clockManager = default!;

        public static int CreateCandidate(string name, string surname, DateTime birthDate, string email, DateTime insertDate, DateTime? modifyDate = null)
        {
            using (var toCreate = OurServiceLocator.GetUnitOfWork())
            {
                var newCandite = new Candidate { Name = name, Surname = surname, Birthdate = birthDate, Email = email, InsertDate = insertDate, ModifyDate = modifyDate };
                newCandite = toCreate.Cadidates.Add(newCandite);
                toCreate.Commit();
                return newCandite.IdCandidate;
            }
        }

        public void ClearCandidateDDBB()
        {
            using (var toDelete = OurServiceLocator.GetUnitOfWork())
            {
                var candidates = toDelete.Cadidates.GetAll();
                foreach (var candidate in candidates)
                {
                    toDelete.Cadidates.Delete(candidate);
                }
                toDelete.Commit();
            }
        }
    }
}

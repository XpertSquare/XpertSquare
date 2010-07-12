using System;

using XpertSquare.Core.Repository;
using XpertSquare.Core.Model;

namespace XpertSquare.Data.NH.Repository
{
    public class VoteRepository : AbstractNHibernateRepository<XsVote, long>, IVoteRepository
    {
    }
}

using System;

using XpertSquare.Core.Repository;
using XpertSquare.Core.Model;


namespace XpertSquare.Data.NH.Repository
{
    public class AnswerRepository : AbstractNHibernateRepository<XsAnswer, long>, IAnswerRepository
    {
    }
}

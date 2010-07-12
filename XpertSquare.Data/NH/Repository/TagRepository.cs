using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XpertSquare.Core.Repository;
using XpertSquare.Core.Model;

namespace XpertSquare.Data.NH.Repository
{
    public class TagRepository : AbstractNHibernateRepository<XsTag , long>, ITagRepository
    {
        public XsTag GetTagByName(String tagName)
        {
            String invariantTagName = tagName.ToLowerInvariant();
            return (from t in Linq()
                    where t.Name == invariantTagName
                    select t).FirstOrDefault();
        }

        public IEnumerable<XsTag> GetTagsByQuestionTotal()
        {
            return (from t in Linq()
                    where t.QuestionCount!=0
                    orderby t.QuestionCount descending
                    select t);
        }
    }
}

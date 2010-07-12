using System;
using System.Collections.Generic;

using XpertSquare.Core.Model;

namespace XpertSquare.Core.Repository
{
    public interface ITagRepository : IRepository<XpertSquare.Core.Model.XsTag, long>
    {
        XsTag GetTagByName(String tagName);
        IEnumerable<XsTag> GetTagsByQuestionTotal();
    }
}

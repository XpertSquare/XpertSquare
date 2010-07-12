using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XpertSquare.Core.Repository
{
    public interface IArticleRepository : IRepository<XpertSquare.Core.Model.WikiArticle,long>
    {
    }
}

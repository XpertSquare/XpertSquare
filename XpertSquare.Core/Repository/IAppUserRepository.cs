using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using XpertSquare.Core.Model;

namespace XpertSquare.Core.Repository
{
    public interface IXsUserRepository:IRepository<XsUser,long>
    {
        XsUser GetByUsername(String username);
        
    }

    
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResponsiBoss.Data.Abstractions
{
    public interface IUserContext
    {
        Guid CurrentUserIdentifier { get; }
    }
}
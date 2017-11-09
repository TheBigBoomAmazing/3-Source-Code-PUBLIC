using System;
using System.Collections.Generic;
using System.Text;

namespace Suzsoft.Smart.EntityCore
{
    public interface IQuery
    {
        WhereBuilder ParseSQL();
    }
}

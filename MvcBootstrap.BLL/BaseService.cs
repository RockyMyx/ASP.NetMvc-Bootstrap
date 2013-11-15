using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.IDAO;
using System.Data.Objects.DataClasses;

namespace MvcBootstrap.BLL
{
    public class BaseService<T> where T : EntityObject
    {
    }
}

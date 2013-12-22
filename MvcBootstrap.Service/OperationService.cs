﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.DAO;

namespace MvcBootstrap.Service
{
    public class OperationService : BaseService<Operation, IOperationDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new OperationDao();
        }

        public IDictionary<int, string> GetOperations()
        {
            return base.dao.GetOperations();
        }
    }
}

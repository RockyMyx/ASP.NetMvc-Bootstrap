using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.ViewModels;
using System;

namespace MvcBootstrap.Service
{
    public class UserNodeService : BaseService<UserNode, IUserNodeDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new UserNodeDao();
        }

        public List<TreeViewModel> GetCategoryNodes(int userId)
        {
            string idString = base.dao.GetEntity(n => n.UserID == userId).AisCategoryID;
            AisCategoryService categoryService = new AisCategoryService();
            return categoryService.GetCategoryNodes(idString.StrToList());
        }
    }
}

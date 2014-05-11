using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Service
{
    public class AisCategoryService : BaseService<T_AisCategory, IAisCategoryDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new AisCategoryDao();
        }

        public List<TreeViewModel> GetCategoryNodes(int userId)
        {
            /*UserCacheService unService = new UserCacheService();
            string distributedIds = unService.GetEntity(un => un.UserId == userId).AisCategoryId;

            List<TreeViewModel> categoryNodes = new List<TreeViewModel>();
            TreeViewModel model = null;
            using (DBEntity db = new DBEntity())
            {
                IEnumerable<T_AisCategory> categoryInfo = this.GetAll();
                foreach (T_AisCategory category in categoryInfo)
                {
                    model = new TreeViewModel
                    {
                        id = category.Id.ToString(),
                        pId = category.ParentId.ToString(),
                        name = category.Name,
                        open = true
                    };
                    if (!string.IsNullOrWhiteSpace(distributedIds) &&
                        distributedIds.Contains(category.Id.ToString()))
                    {
                        model.isChecked = true;
                    }
                    categoryNodes.Add(model);
                }
            }

            return categoryNodes;*/

            return null;
        }
    }
}

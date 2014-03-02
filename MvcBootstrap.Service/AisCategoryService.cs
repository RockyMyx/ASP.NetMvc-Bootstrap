using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Service
{
    public class AisCategoryService : BaseService<AisCategory, IAisCategoryDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new AisCategoryDao();
        }

        public List<TreeViewModel> GetCategoryNodes(int userId)
        {
            UserNodeService unService = new UserNodeService();
            string distributedIds = unService.GetEntity(un => un.UserID == userId).AisCategoryID;

            List<TreeViewModel> categoryNodes = new List<TreeViewModel>();
            TreeViewModel model = null;
            using (DBEntity db = new DBEntity())
            {
                IEnumerable<AisCategory> categoryInfo = this.GetAll();
                foreach (AisCategory category in categoryInfo)
                {
                    model = new TreeViewModel
                    {
                        id = category.ID.ToString(),
                        pId = category.ParentID.ToString(),
                        name = category.Name,
                        open = true
                    };
                    if (!string.IsNullOrWhiteSpace(distributedIds) &&
                        distributedIds.Contains(category.ID.ToString()))
                    {
                        model.isChecked = true;
                    }
                    categoryNodes.Add(model);
                }
            }

            return categoryNodes;
        }
    }
}

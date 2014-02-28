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

        public List<TreeViewModel> GetCategoryNodes(List<string> idList)
        {
            List<TreeViewModel> categoryNodes = new List<TreeViewModel>();
            using (DBEntity db = new DBEntity())
            {
                IEnumerable<AisCategory> categoryInfo = this.GetAll();
                foreach (AisCategory category in categoryInfo)
                {
                    if (idList.Contains(category.ID.ToString()))
                    {
                        categoryNodes.Add(new TreeViewModel
                        {
                            id = category.ID.ToString(),
                            pId = category.ParentID.ToString(),
                            name = category.Name
                        });
                    }
                }
            }

            return categoryNodes;
        }
    }
}

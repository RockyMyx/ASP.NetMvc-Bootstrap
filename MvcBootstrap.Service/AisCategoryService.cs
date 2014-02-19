using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class AisCategoryService : BaseService<AisCategory, IAisCategoryDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new AisCategoryDao();
        }
    }
}

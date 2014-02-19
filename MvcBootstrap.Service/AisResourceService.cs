using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;

namespace MvcBootstrap.Service
{
    public class AisResourceService : BaseService<AisResource, IAisResourceDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new AisResourceDao();
        }
    }
}

using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.IDAO;
//using MvcBootstrap.MssqlEFModel;
using MvcBootstrap.MysqlEFModel;
//using MvcBootstrap.OracleEFModel;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Service
{
    public class AisResourceService : BaseService<T_AisResource, IAisResourceDao>
    {
        protected override void SetCurrentDao()
        {
            base.dao = new AisResourceDao();
        }

        public List<TreeViewModel> GetResourceNodes(List<string> idList)
        {
            List<TreeViewModel> resourceNodes = new List<TreeViewModel>();
            using (DBEntity db = new DBEntity())
            {
                IEnumerable<T_AisResource> resourceInfo = this.GetAll();
                foreach (T_AisResource resource in resourceInfo)
                {
                    if (idList.Contains(resource.ID.ToString()))
                    {
                        resourceNodes.Add(new TreeViewModel
                        {
                            id = resource.ID.ToString(),
                            pId = resource.ParentID.ToString(),
                            name = resource.ResourceName
                        });
                    }
                }
            }

            return resourceNodes;
        }
    }
}

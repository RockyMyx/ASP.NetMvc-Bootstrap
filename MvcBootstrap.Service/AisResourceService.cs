using System.Collections.Generic;
using MvcBootstrap.DAO;
using MvcBootstrap.EFModel;
using MvcBootstrap.IDAO;
using MvcBootstrap.ViewModels;

namespace MvcBootstrap.Service
{
    public class AisResourceService : BaseService<AisResource, IAisResourceDao>
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
                IEnumerable<AisResource> resourceInfo = this.GetAll();
                foreach (AisResource resource in resourceInfo)
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

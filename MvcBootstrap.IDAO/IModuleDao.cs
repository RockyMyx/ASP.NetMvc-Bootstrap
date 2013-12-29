using System.Linq;
using MvcBootstrap.EFModel;

namespace MvcBootstrap.IDAO
{
    public interface IModuleDao : IBaseDao<Module>
    {
        IQueryable<Module> GetSortedModules();
    }
}

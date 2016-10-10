using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities;
using RefactorThis.GraphDiff;

namespace Data.DAL
{
    public static class UpdateConfiguration
    {

        private static IUpdateConfigurationManager _manager;

        public static void Init(IUpdateConfigurationManager manager)
        {
            _manager = manager;

            manager.AddConfiguration<ProductCategory>(x => x.OwnedEntity(z => z.File));
            manager.AddConfiguration<InCome>(x => x.OwnedCollection(z => z.Incoms, with => with.AssociatedEntity(e => e.Product)));
            manager.AddConfiguration<Product>(x => x.OwnedCollection(z => z.ProductFiles, with => with.OwnedEntity(e => e.File)));
        }

        public static IUpdateConfigurationManager GetConfigurationManager()
        {
            return _manager;
        }
    }
}

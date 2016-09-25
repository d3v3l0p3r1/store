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

            manager.AddConfiguration<InCome>(x =>                 
                x.OwnedCollection(z => z.Incoms, with => with.AssociatedEntity(e => e.Product)));


        }

        public static IUpdateConfigurationManager GetConfigurationManager()
        {
            return _manager;
        }
    }
}

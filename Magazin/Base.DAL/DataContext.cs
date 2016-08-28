using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base.DAL
{
    public interface IDataContext
    {
        DbSet<T> GetDbSet<T>() where T : BaseEntity;
    }
    public class DataContext : DbContext, IDataContext
    {
        private readonly EntityConfiguration _configuration = new EntityConfiguration();
        public DataContext()
        {
            Configuration.LazyLoadingEnabled = true;            
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var type in _configuration.GetTypes())
            {
                modelBuilder.RegisterEntityType(type);
            }
        }        

        public DbSet<T> GetDbSet<T>() where T : BaseEntity
        {
            return base.Set<T>();
        }

    }
}

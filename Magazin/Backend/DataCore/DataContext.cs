using System.Reflection;
using System.Threading.Tasks;
using BaseCore.Security.Entities;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Extensions;
using IdentityServer4.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BaseCore.DAL.Implementations
{
    public class DataContext : IdentityDbContext<User, Role, long>, IPersistedGrantDbContext
    {
        public DbSet<PersistedGrant> PersistedGrants { get; set; }
        public DbSet<DeviceFlowCodes> DeviceFlowCodes { get; set; }

        private readonly IOptions<OperationalStoreOptions> _operationalStoreOptions;

        public DataContext()
        {

        }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }

        public DataContext(DbContextOptions<DataContext> options, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options)
        {
            _operationalStoreOptions = operationalStoreOptions;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(GetType().Assembly);
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);

            if (_operationalStoreOptions != null)
            {
                builder.ConfigurePersistedGrantContext(_operationalStoreOptions.Value);
            }
        }

        int IPersistedGrantDbContext.SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        Task<int> IPersistedGrantDbContext.SaveChangesAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}

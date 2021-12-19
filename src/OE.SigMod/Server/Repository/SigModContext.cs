using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using Oqtane.Modules;
using Oqtane.Repository;
using Oqtane.Infrastructure;
using Oqtane.Repository.Databases.Interfaces;

namespace OE.SigMod.Repository
{
    public class SigModContext : DBContextBase, IService, IMultiDatabase
    {
        public virtual DbSet<Models.SigMod> SigMod { get; set; }

        public SigModContext(ITenantManager tenantManager, IHttpContextAccessor accessor) : base(tenantManager, accessor)
        {
            // ContextBase handles multi-tenant database connections
        }
    }
}

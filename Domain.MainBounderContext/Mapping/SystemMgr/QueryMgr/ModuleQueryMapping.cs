using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Mapping
{
    public class ModuleQueryMapping : EntityTypeConfiguration<ModuleQuery>
    {
        public ModuleQueryMapping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);

            this.ToTable("SYS_MODULE_QUERY");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Mapping
{
    public class RoleModuleMapping : EntityTypeConfiguration<RoleModule>
    {
        public RoleModuleMapping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m=>m.RoleId).HasColumnName("ROLE_ID").IsRequired();
            this.Property(m => m.ModuleId).HasColumnName("MODULE_ID").HasColumnType("CHAR").HasMaxLength(36).IsRequired();
            this.HasRequired(m => m.ModuleModel).WithMany(m => m.RoleModuleList).HasForeignKey(m => m.ModuleId);
            this.HasRequired(m => m.RoleModel).WithMany(m => m.RoleModuleList).HasForeignKey(m => m.RoleId);

            this.ToTable("SYS_ROLE_MODULE");
        }
    }
}

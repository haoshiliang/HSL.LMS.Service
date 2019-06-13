using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Mapping
{
    public class RoleMapping : EntityTypeConfiguration<Role>
    {
        public RoleMapping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.HasMany(m => m.UserRoleList).WithRequired(m => m.RoleModel).HasForeignKey(m => m.RoleId).WillCascadeOnDelete();
            this.HasMany(m => m.RoleModuleList).WithRequired(m => m.RoleModel).HasForeignKey(m => m.RoleId).WillCascadeOnDelete();

            this.ToTable("SYS_ROLE");
        }
    }
}

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
            this.Property(m=>m.RoleCode).HasColumnName("ROLE_CODE").HasMaxLength(64).IsRequired();
            this.Property(m => m.RoleName).HasColumnName("ROLE_NAME").HasMaxLength(128).IsRequired();
            this.Property(m => m.PyCode).HasMaxLength(64).HasColumnName("PY_CODE").IsRequired();
            this.Property(m => m.CreateDate).HasColumnName("CREATE_DATE").IsRequired();
            this.Property(m => m.LastUpdateDate).HasColumnName("LAST_UPDATE_DATE").IsRequired();
            this.HasMany(m => m.UserRoleList).WithRequired(m => m.RoleModule).HasForeignKey(m => m.RoleId).WillCascadeOnDelete();
            this.HasMany(m => m.RoleModuleList).WithRequired(m => m.RoleModel).HasForeignKey(m => m.RoleId).WillCascadeOnDelete();

            this.ToTable("SYS_ROLE");
        }
    }
}

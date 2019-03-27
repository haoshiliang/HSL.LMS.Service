using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Mapping
{
    public class UserRoleMapping : EntityTypeConfiguration<UserRole>
    {
        public UserRoleMapping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m=>m.RoleId).HasColumnName("ROLE_ID").IsRequired().HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m => m.UserId).HasColumnName("USER_ID").IsRequired().HasColumnType("CHAR").HasMaxLength(36);
            this.HasRequired(m => m.RoleModule).WithMany(m => m.UserRoleList).HasForeignKey(m => m.RoleId);
            this.HasRequired(m => m.UserModule).WithMany(m => m.UserRoleList).HasForeignKey(m => m.UserId);
            this.Ignore(m => m.IsRoleMaster);

            this.ToTable("SYS_ROLE_USER");
        }
    }
}

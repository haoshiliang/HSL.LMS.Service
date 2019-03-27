using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m=>m.Code).HasColumnName("CODE").HasMaxLength(64).IsRequired();
            this.Property(m => m.Name).HasColumnName("NAME").HasMaxLength(128).IsRequired();
            this.Property(m => m.LoginName).HasColumnName("LOGIN_NAME").HasMaxLength(64).IsRequired();
            this.Property(m => m.Password).HasColumnName("PASSWORD").HasMaxLength(64).IsRequired();
            this.Property(m => m.SaltValue).HasColumnName("SALT_VALUE").HasMaxLength(36).IsRequired();
            this.Property(m => m.PyCode).HasMaxLength(64).HasColumnName("PY_CODE").IsRequired();
            this.Property(m => m.CreateDate).HasColumnName("CREATE_DATE").IsRequired();
            this.Property(m => m.LastUpdateDate).HasColumnName("LAST_UPDATE_DATE").IsRequired();
            this.HasMany(m => m.UserRoleList).WithRequired(m => m.UserModule).HasForeignKey(m => m.UserId).WillCascadeOnDelete();

            this.ToTable("SYS_USER");
        }
    }
}

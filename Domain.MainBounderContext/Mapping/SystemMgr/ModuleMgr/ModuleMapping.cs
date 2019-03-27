using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.ModuleMgr.Mapping
{
    public class ModuleMapping : EntityTypeConfiguration<Module>
    {
        public ModuleMapping()
        {
            this.HasKey(m => m.Id).Property(m=>m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m => m.ParentId).HasColumnName("PARENT_ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m => m.Code).HasMaxLength(128).HasColumnName("CODE").IsRequired();
            this.Property(m => m.Name).HasMaxLength(64).HasColumnName("NAME").IsRequired();
            this.Property(m => m.Icon).HasMaxLength(64).HasColumnName("ICON");
            this.Property(m => m.ModulePath).HasMaxLength(128).HasColumnName("MODULE_PATH");
            this.Property(m => m.IsFunction).HasColumnName("IS_FUNCTION");
            this.Property(m => m.IsEnabled).HasColumnName("IS_ENABLED");
            this.Property(m => m.CreateDate).HasColumnName("CREATE_DATE").IsRequired();
            this.Property(m => m.LastUpdateDate).HasColumnName("LAST_UPDATE_DATE").IsRequired();
            this.HasMany(m => m.ChildList).WithOptional(m => m.ParentModule).HasForeignKey(m=>m.ParentId).WillCascadeOnDelete();
            this.HasMany(m => m.RoleModuleList).WithRequired(m => m.ModuleModel).HasForeignKey(m => m.ModuleId).WillCascadeOnDelete();

            this.ToTable("SYS_MODULE");
        }
    }
}

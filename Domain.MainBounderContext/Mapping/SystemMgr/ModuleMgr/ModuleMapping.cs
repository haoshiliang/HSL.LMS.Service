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
            this.HasMany(m => m.ChildList).WithOptional(m => m.ParentModule).HasForeignKey(m=>m.ParentId).WillCascadeOnDelete();
            this.HasMany(m => m.RoleModuleList).WithRequired(m => m.ModuleModel).HasForeignKey(m => m.ModuleId).WillCascadeOnDelete();

            this.ToTable("SYS_MODULE");
        }
    }
}

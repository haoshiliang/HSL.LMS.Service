using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Mapping
{
    public class DepartmentMapping : EntityTypeConfiguration<Department>
    {
        public DepartmentMapping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m=>m.DepartCode).HasColumnName("DEPART_CODE").HasMaxLength(64).IsRequired();
            this.Property(m => m.DepartName).HasColumnName("DEPART_NAME").HasMaxLength(128).IsRequired();
            this.Property(m => m.PyCode).HasMaxLength(64).HasColumnName("PY_CODE").IsRequired();
            this.Property(m => m.IsDel).HasColumnName("IS_DEL").IsRequired();
            this.Property(m => m.CreateDate).HasColumnName("CREATE_DATE").IsRequired();
            this.Property(m => m.LastUpdateDate).HasColumnName("LAST_UPDATE_DATE").IsRequired();
            this.ToTable("SYS_DEPARTMENT");
        }
    }
}

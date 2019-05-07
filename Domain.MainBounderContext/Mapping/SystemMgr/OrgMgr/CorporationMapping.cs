using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Mapping
{
    public class CorporationMapping : EntityTypeConfiguration<Corporation>
    {
        public CorporationMapping()
        {
            this.HasKey(m => m.Id).Property(m=>m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m => m.AutomaticCode).HasColumnName("AUTOMATIC_CODE").HasMaxLength(128);
            this.Property(m => m.CorpCode).HasMaxLength(128).HasColumnName("CORP_CODE").IsRequired();
            this.Property(m => m.CorpName).HasMaxLength(64).HasColumnName("CORP_NAME").IsRequired();
            this.Property(m => m.PyCode).HasMaxLength(64).HasColumnName("PY_CODE").IsRequired();
            this.Property(m => m.IsDel).HasColumnName("IS_DEL").IsRequired();
            this.Property(m => m.CreateDate).HasColumnName("CREATE_DATE").IsRequired();
            this.Property(m => m.LastUpdateDate).HasColumnName("LAST_UPDATE_DATE").IsRequired();
            this.Property(m => m.ParentId).HasColumnName("PARENT_ID").HasColumnType("CHAR").HasMaxLength(36);
            this.HasMany(m => m.ChildCorpList).WithOptional(m => m.ParentCorp).HasForeignKey(m=>m.ParentId).WillCascadeOnDelete();
            this.ToTable("SYS_CORPORATION");
        }
    }
}

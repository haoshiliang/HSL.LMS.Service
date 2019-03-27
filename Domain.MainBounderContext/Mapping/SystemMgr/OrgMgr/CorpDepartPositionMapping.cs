using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Mapping
{
    public class CorpDepartPositionMapping : EntityTypeConfiguration<CorpDepartPosition>
    {
        public CorpDepartPositionMapping()
        {
            this.Ignore(m => m.Id);
            this.HasKey(m => new { m.CorpId, m.DepartId, m.PositionId });
            this.Property(m => m.CorpId).HasColumnName("CORP_ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m => m.DepartId).HasColumnName("DEPART_ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m => m.PositionId).HasColumnName("POSITION_ID").HasColumnType("CHAR").HasMaxLength(36);
            this.Property(m => m.CreateDate).HasColumnName("CREATE_DATE").IsRequired();
            this.Property(m => m.LastUpdateDate).HasColumnName("LAST_UPDATE_DATE").IsRequired();
            this.ToTable("SYS_CORP_DEPART_POSITION");
        }
    }
}

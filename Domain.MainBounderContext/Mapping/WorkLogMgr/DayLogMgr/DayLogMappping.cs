using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Mapping
{
    public class DayLogMappping : EntityTypeConfiguration<DayLog>
    {
        public DayLogMappping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);
            this.HasMany(m => m.DayLogDetailList).WithRequired(m => m.DayLogModel).HasForeignKey(m => m.DayLogId).WillCascadeOnDelete();

            this.ToTable("WL_DAY_LOG");
        }
    }
}

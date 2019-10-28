using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Mapping
{
    public class DayLogDetailMapping : EntityTypeConfiguration<DayLogDetail>
    {
        public DayLogDetailMapping()
        {
            this.HasKey(m => m.Id).Property(m => m.Id).HasColumnName("ID").HasColumnType("CHAR").HasMaxLength(36);

            this.ToTable("WL_DAY_LOG_DETAIL");
        }
    }
}

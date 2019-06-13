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

            this.ToTable("SYS_CORP_DEPART_POSITION");
        }
    }
}

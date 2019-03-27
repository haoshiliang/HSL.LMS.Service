using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.ModelConfiguration;
using LMS.Domain.MainBounderContext.SystemMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.Mapping
{
    public class CorporationMapping : EntityTypeConfiguration<Corporation>
    {
        public CorporationMapping()
        {
            this.HasKey(m => m.Id);
        }
    }
}

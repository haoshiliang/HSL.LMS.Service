﻿using System;
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
            this.HasMany(m => m.ChildCorpList).WithOptional(m => m.ParentCorp).HasForeignKey(m=>m.ParentId).WillCascadeOnDelete();

            this.ToTable("SYS_CORPORATION");
        }
    }
}

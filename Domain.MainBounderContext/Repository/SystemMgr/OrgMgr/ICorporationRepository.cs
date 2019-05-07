﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.Seedwork;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository
{
    public interface ICorporationRepository : IRepository<Corporation>
    {
        /// <summary>
        /// 获取生成的最大编码
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        string GetAutomaticCode(string parentId);
    }
}

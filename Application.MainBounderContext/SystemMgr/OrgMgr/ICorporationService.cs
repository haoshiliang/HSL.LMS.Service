﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr;
using LMS.Application.MainBounderContext.DTO.Common;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public interface ICorporationService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(Corporation model);

        /// <summary>
        /// 取出公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Corporation FindById(string id);

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// 取出公司列表
        /// </summary>
        /// <returns></returns>
        ICollection<CorporationDTO> FindList(string id);

        /// <summary>
        /// 取出公司树列表
        /// </summary>
        /// <returns></returns>
        ICollection<TreeDTO> FindTreeList();
    }
}

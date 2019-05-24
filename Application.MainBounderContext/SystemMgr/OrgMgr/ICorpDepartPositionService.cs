using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public interface ICorpDepartPositionService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="modelList"></param>
        void Add(IList<CorpDepartPosition> modelList);

        /// <summary>
        /// 获取所有部门职位
        /// </summary>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        IList<DeptPositionDTO> FindAllList(string corpId);

        /// <summary>
        /// 获取内关联部门职位
        /// </summary>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        IList<CorpDeptPositionDTO> FindList(string corpId);
    }
}

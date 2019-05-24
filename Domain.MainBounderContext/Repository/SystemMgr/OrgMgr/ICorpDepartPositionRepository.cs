using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;

namespace LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository
{
    public interface ICorpDepartPositionRepository : IRepository<CorpDepartPosition>
    {
        /// <summary>
        /// 移除数据
        /// </summary>
        /// <param name="corpId">公司编号</param>
        /// <returns></returns>
        void RemoveByCorpId(string corpId);

        /// <summary>
        /// 获取公司-部门-职位列表
        /// </summary>
        /// <param name="corpId">公司编号</param>
        IList<DTO> GetAllList<DTO>(string corpId) where DTO : class;

        /// <summary>
        /// 获取公司-部门-职位列表
        /// </summary>
        /// <param name="corpId">公司编号</param>
        IList<DTO> GetList<DTO>(string corpId) where DTO : class;
    }
}

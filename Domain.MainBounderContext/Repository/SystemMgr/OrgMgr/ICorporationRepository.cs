using System;
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

        /// <summary>
        /// 设置自动生成编号
        /// </summary>
        /// <param name="oldCode">旧编号</param>
        /// <param name="newCode">新编号</param>
        /// <param name="id">当前ID</param>
        /// <returns></returns>
        void SetAutomaticCode(string oldCode, string newCode, string id);

        /// <summary>
        /// 获取用于生成树的公司列表
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <returns></returns>
        IList<DTO> GetTreeList<DTO>();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public interface IPositionService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(Position model);

        /// <summary>
        /// 取出职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Position FindById(string id);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// 取出部门列表
        /// </summary>
        /// <returns></returns>
        ICollection<Position> FindList(Pagination pagination, QueryParam queryParam);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public interface IDepartmentService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(Department model);

        /// <summary>
        /// 取出部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Department FindById(Guid id);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);

        /// <summary>
        /// 取出部门列表
        /// </summary>
        /// <returns></returns>
        ICollection<Department> FindList(Pagination pagination, QueryParam queryParam);
    }
}

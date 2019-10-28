using LMS.Application.MainBounderContext.DTO.WorkLogMgr.DayLogMgr;
using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.WorkLogMgr.DayLogMgr
{
    public interface IDayLogService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(DayLog model);

        /// <summary>
        /// 取出信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        DayLog FindById(string id);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// 取出列表
        /// </summary>
        /// <returns></returns>
        ICollection<DayLogDTO> FindList(Pagination pagination, QueryParam queryParam);
    }
}

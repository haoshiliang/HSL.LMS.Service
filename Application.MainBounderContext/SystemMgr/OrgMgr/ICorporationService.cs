using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr;

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
        CorporationDTO FindById(Guid id);

        /// <summary>
        /// 删除公司信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(Guid id);

        /// <summary>
        /// 取出公司列表
        /// </summary>
        /// <returns></returns>
        ICollection<CorporationDTO> FindList(string id);
    }
}

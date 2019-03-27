using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public interface ICorpDepartPositionService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="modelList"></param>
        void Add(IList<CorpDepartPosition> modelList);
    }
}

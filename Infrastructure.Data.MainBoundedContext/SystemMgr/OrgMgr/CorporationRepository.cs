using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.Seedwork;
using LMS.Infrastructure.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;

namespace LMS.Infrastructure.Data.MainBoundedContext.SystemMgr.OrgMgr
{
    public class CorporationRepository : Repository<Corporation>, ICorporationRepository
    {
        public CorporationRepository(MainUnitOfWork unitWork) : base(unitWork)
        {

        }

        /// <summary>
        /// 获取生成的最大编码
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public string GetAutomaticCode(string parentId)
        {            
            var parentModel = base.GetAll().Where(m => m.Id == parentId).FirstOrDefault();
            var automaticCode = base.GetAll().Where(m => m.ParentId == parentId).Max(m => m.AutomaticCode);
            var parentCode = (parentModel != null ? parentModel.AutomaticCode : "");

            if (string.IsNullOrEmpty(automaticCode))
            {
                return parentCode + "00001";
            }
            else
            {
                return parentCode + (Int32.Parse(automaticCode) + 1).ToString().PadLeft(4, '0');
            }
        }
    }
}

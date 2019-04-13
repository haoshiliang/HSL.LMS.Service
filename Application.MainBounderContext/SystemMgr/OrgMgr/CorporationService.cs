using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Domain.Seedwork;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;
using LMS.Application.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public class CorporationService : ICorporationService
    {
        #region 私有变量

        /// <summary>
        /// 公司仓储
        /// </summary>
        private readonly ICorporationRepository corpRepository;

        #endregion

        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public CorporationService(ICorporationRepository corpRepository)
        {
            this.corpRepository = corpRepository;
        }

        #endregion

        #region 接口方法

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(Corporation model)
        {
            model.PyCode = model.CorpName.ToConvertPyCode();
            if (!string.IsNullOrEmpty(model.Id))
            {
                model.LastUpdateDate = DateTime.Now;
                this.corpRepository.Modity(model);
            }
            else
            {
                model.GenerateNewIdentity();
                model.CreateDate = DateTime.Now;
                model.LastUpdateDate = DateTime.Now;
                this.corpRepository.Add(model);
            }
            this.corpRepository.SaveChanges();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name=""></param>
        public void Delete(Guid id)
        {
            var model = corpRepository.Get(id);
            model.IsDel = true;
            corpRepository.Modity(model);
            corpRepository.SaveChanges();
        }

        /// <summary>
        /// 取出公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public CorporationDTO FindById(Guid id)
        {
            var model = this.corpRepository.Get(id);
            var dto = model.ProjectedAs<CorporationDTO>();
            if (model.ParentCorp != null)
            {
                dto.ParentId = model.ParentCorp.Id;
                dto.ParentName = model.ParentCorp.CorpName;
            }
            return dto;
        }

        /// <summary>
        /// 取出公司列表
        /// </summary>
        /// <returns></returns>
        public ICollection<CorporationDTO> FindList()
        {
            var list = this.corpRepository.GetAll().Where(m => m.ParentId == null);
            var dtoList = new List<CorporationDTO>();
            foreach(var corp in list)
            {
                var dto = corp.ProjectedAs<CorporationDTO>();
                if (corp.ParentCorp != null)    
                {
                    dto.ParentId = corp.ParentCorp.Id;
                    dto.ParentName = corp.ParentCorp.CorpName;
                }
                this.SetChildList(corp, dto);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        #endregion

        #region 私有方法

        private void SetChildList(Corporation corp,CorporationDTO dto)
        {
            if(corp.ChildCorpList!=null && corp.ChildCorpList.Count > 0)
            {
                foreach(var m in corp.ChildCorpList)
                {
                    var d = m.ProjectedAs<CorporationDTO>();
                    if (m.ParentCorp != null)
                    {
                        d.ParentId = m.ParentCorp.Id;
                        d.ParentName = m.ParentCorp.CorpName;
                    }
                    this.SetChildList(m, d);
                    dto.ChildList.Add(d);
                }
            }
        }

        #endregion
    }
}

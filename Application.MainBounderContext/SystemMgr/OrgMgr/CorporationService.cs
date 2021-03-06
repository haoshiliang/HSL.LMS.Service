﻿using System;
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
using LMS.Application.MainBounderContext.DTO.Common;

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
            try
            {
                this.corpRepository.UnitOfWork.BeginTrans();

                model.ParentId = (string.IsNullOrEmpty(model.ParentId) ? Guid.Empty.ToString() : model.ParentId);
                model.OldParentId = (string.IsNullOrEmpty(model.OldParentId) ? Guid.Empty.ToString() : model.OldParentId);
                model.PyCode = model.CorpName.ToConvertPyCode();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    model.LastUpdateDate = DateTime.Now;
                    if (model.ParentId != model.OldParentId)
                    {
                        var oldAutomaticCode = "";
                        oldAutomaticCode = model.AutomaticCode;
                        model.AutomaticCode = this.corpRepository.GetAutomaticCode(model.ParentId);
                        this.corpRepository.SetAutomaticCode(oldAutomaticCode, model.AutomaticCode, model.Id);
                    }
                    this.corpRepository.Modity(model);
                }
                else
                {
                    model.GenerateNewIdentity();
                    model.CreateDate = DateTime.Now;
                    model.LastUpdateDate = DateTime.Now;
                    model.AutomaticCode = this.corpRepository.GetAutomaticCode(model.ParentId);
                    model.ParentCorp = null;
                    this.corpRepository.Add(model);
                }
                this.corpRepository.SaveChanges();
                this.corpRepository.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.corpRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name=""></param>
        public void Delete(string id)
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
        public Corporation FindById(string id)
        {
            var model = this.corpRepository.Get(id);
            var returnModel = model.ProjectedAs<Corporation>();
            if (model.ParentCorp != null)
            {
                returnModel.ParentCorp = new Corporation() {Id =model.ParentCorp.Id, CorpName = model.ParentCorp.CorpName };
            }
            return returnModel;
        }

        /// <summary>
        /// 取出公司列表
        /// </summary>
        /// <returns></returns>
        public ICollection<CorporationDTO> FindList(string id)
        {
            var list = this.corpRepository.GetAll().Where(m => m.ParentId == Guid.Empty.ToString() && m.Id != id && m.IsDel == false).ToList();
            var dtoList = new List<CorporationDTO>();
            foreach(var corp in list)
            {
                var dto = corp.ProjectedAs<CorporationDTO>();
                if (corp.ParentCorp != null)    
                {
                    dto.ParentId = corp.ParentCorp.Id;
                    dto.ParentName = corp.ParentCorp.CorpName;
                }
                this.SetChildList(corp, dto, id);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        /// <summary>
        /// 取出树列表
        /// </summary>
        /// <returns></returns>
        public ICollection<TreeDTO> FindTreeList()
        {
            var list = this.corpRepository.GetTreeList<CorporationDTO>();
            var dtoList = new List<TreeDTO>();
            var rootList = list.Where(m=>m.ParentId==Guid.Empty.ToString());
            foreach (var corp in rootList)
            {
                var dto = new TreeDTO()
                {
                    Id=corp.Id,
                    Name=corp.CorpName,
                    ParentId=corp.ParentId
                };
                this.SetTreeChildList(list, dto);
                dtoList.Add(dto);
            }
            return dtoList;
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 
        /// </summary>
        /// <param name="corp"></param>
        /// <param name="dto"></param>
        /// <param name="id"></param>
        private void SetChildList(Corporation corp, CorporationDTO dto, string id)
        {
            if (corp.ChildCorpList != null && corp.ChildCorpList.Count > 0)
            {
                var childList = corp.ChildCorpList.Where(m => m.Id != id && m.IsDel == false);
                foreach (var m in childList)
                {
                    var d = m.ProjectedAs<CorporationDTO>();
                    if (m.ParentCorp != null)
                    {
                        d.ParentId = m.ParentCorp.Id;
                        d.ParentName = m.ParentCorp.CorpName;
                    }
                    this.SetChildList(m, d, id);
                    dto.ChildList.Add(d);
                }
            }
        }

        /// <summary>
        /// 设置公司树子列表
        /// </summary>
        /// <param name="corp"></param>
        /// <param name="dto"></param>
        private void SetTreeChildList(IList<CorporationDTO> corpList, TreeDTO dto)
        {
            var list = corpList.Where(m => m.ParentId == dto.Id);
            if (list != null && list.Count() > 0)
            {
                foreach (var m in list)
                {
                    var d = new TreeDTO()
                    {
                        Id=m.Id,
                        Name = m.CorpName,
                        ParentId=m.ParentId
                    };
                    this.SetTreeChildList(corpList, d);
                    dto.ChildList.Add(d);
                }
            }
        }

        #endregion
    }
}

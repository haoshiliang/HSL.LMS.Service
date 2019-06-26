using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Application.Seedwork;
using LMS.Application.MainBounderContext.DTO.SystemMgr.OrgMgr;
using LMS.Application.MainBounderContext.DTO.Common;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public class CorpDepartPositionService : ICorpDepartPositionService
    {
        #region 私有变量

        /// <summary>
        /// 公司部门职位仓储
        /// </summary>
        private readonly ICorpDepartPositionRepository corpDepartPositionRepository;
        /// <summary>
        /// 公司仓储
        /// </summary>
        private readonly ICorporationRepository corpRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="corpDepartPositionRepository"></param>
        /// <param name="corpRepository"></param>
        public CorpDepartPositionService(ICorpDepartPositionRepository corpDepartPositionRepository, ICorporationRepository corpRepository)
        {
            this.corpDepartPositionRepository = corpDepartPositionRepository;
            this.corpRepository = corpRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="modelList"></param>
        public void Add(IList<CorpDepartPosition> modelList)
        {
            try
            {
                this.corpDepartPositionRepository.UnitOfWork.BeginTrans();

                var corpDepartList = modelList.GroupBy(m => new { m.CorpId }).Select(m => new { m.Key.CorpId });
                foreach (var m in corpDepartList)
                {
                    this.corpDepartPositionRepository.RemoveByCorpId(m.CorpId);
                }
                foreach (var m in modelList)
                {
                    m.CreateDate = DateTime.Now;
                    m.LastUpdateDate = DateTime.Now;
                    this.corpDepartPositionRepository.Add(m);
                }
                this.corpDepartPositionRepository.SaveChanges();

                this.corpDepartPositionRepository.UnitOfWork.Commit();
            }
            catch(Exception ex)
            {
                this.corpDepartPositionRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 获取所有部门职位
        /// </summary>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public IList<DeptPositionDTO> FindAllList(string corpId)
        {
            var returnList = new List<DeptPositionDTO>();
            var list= this.corpDepartPositionRepository.GetAllList<CorpDeptPositionDTO>(corpId);
            var deptList = list.GroupBy(m => new { m.DepartId, m.DepartName })
                               .Select(m => new { DepartId = m.Key.DepartId, DepartName = m.Key.DepartName });

            foreach(var deptModel in deptList)
            {
                var positionList = list.Where(m => m.DepartId == deptModel.DepartId).ToList();
                var dto = new DeptPositionDTO() { Id = deptModel.DepartId, Name = deptModel.DepartName, IsChecked = positionList.Any(p=>p.IsSelected==1) };
                foreach(var pModel in positionList)
                {
                    dto.PositionList.Add(new DeptPositionDTO() { Id = pModel.PositionId, Name = pModel.PositionName, IsChecked = pModel.IsSelected==1 });
                }
                returnList.Add(dto);
            }
            return returnList;
        }

        /// <summary>
        /// 获取所有公司部门
        /// </summary>
        /// <returns></returns>
        public IList<SelectDTO> FindAllDeptList()
        {
            var returnList = new List<SelectDTO>();
            var list = this.corpDepartPositionRepository.GetAllList<CorpDeptPositionDTO>();
            var deptList = list.GroupBy(m => new { m.CorpId, m.DepartId, m.DepartName })
                               .Select(m => new { CorpId = m.Key.CorpId, DepartId = m.Key.DepartId, DepartName = m.Key.DepartName });

            foreach (var deptModel in deptList)
            {
                var dto = new SelectDTO()
                {
                    Id = deptModel.DepartId,
                    Name = deptModel.DepartName,
                    RelationId_1 = deptModel.CorpId
                };
                returnList.Add(dto);
            }
            return returnList;
        }

        /// <summary>
        /// 获取所有公司部门职位
        /// </summary>
        /// <returns></returns>
        public IList<SelectDTO> FindAllPositionList()
        {
            var returnList = new List<SelectDTO>();
            var list = this.corpDepartPositionRepository.GetAllList<CorpDeptPositionDTO>();
            var deptList = list.GroupBy(m => new { m.CorpId, m.DepartId, m.PositionId, m.PositionName })
                               .Select(m => new { CorpId = m.Key.CorpId, DepartId = m.Key.DepartId, PositoinId = m.Key.PositionId, PositionName = m.Key.PositionName });

            foreach (var deptModel in deptList)
            {
                var dto = new SelectDTO()
                {
                    Id = deptModel.PositoinId,
                    Name = deptModel.PositionName,
                    RelationId_1 = deptModel.CorpId,
                    RelationId_2 = deptModel.DepartId
                };
                returnList.Add(dto);
            }
            return returnList;
        }

        /// <summary>
        /// 获取内关联部门职位
        /// </summary>
        /// <param name="corpId">公司ID</param>
        /// <returns></returns>
        public IList<CorpDeptPositionDTO> FindList(string corpId)
        {
            return this.corpDepartPositionRepository.GetList<CorpDeptPositionDTO>(corpId);
        }

        /// <summary>
        /// 获取组织结构树
        /// </summary>
        /// <returns></returns>
        public IList<TreeDTO> FindOrgList()
        {
            var returnList = new List<TreeDTO>();
            var allList = this.corpDepartPositionRepository.GetAllList<CorpDeptPositionDTO>();
            var corpList = this.corpRepository.GetTreeList<CorporationDTO>();
            var rootCorpList = corpList.Where(m => m.ParentId == Guid.Empty.ToString());
            foreach(var cModel in rootCorpList)
            {
                var treeDto = new TreeDTO() { Id = cModel.Id, Name = cModel.CorpName };
                returnList.Add(treeDto);
                //设置公司子列表
                this.SetCorpChild(corpList,allList, treeDto);
                //设置公司部门
                this.SetDeptList(allList, treeDto);
            }

            return returnList;
        }

        #endregion

        #region 私有方法
        
        /// <summary>
        /// 设置子公司
        /// </summary>
        /// <param name="corpList"></param>
        /// <param name="treeDto"></param>
        private void SetCorpChild(IList<CorporationDTO> corpList,IList<CorpDeptPositionDTO> list,TreeDTO treeDto)
        {
            var childList = corpList.Where(m => m.ParentId == treeDto.Id);
            foreach (var cModel in childList)
            {
                var childTreeDto = new TreeDTO() { Id = cModel.Id, Name = cModel.CorpName };
                treeDto.ChildList.Add(childTreeDto);
                this.SetCorpChild(corpList, list, childTreeDto);
                this.SetDeptList(list, childTreeDto);
            }
        }

        /// <summary>
        /// 设置公司部门
        /// </summary>
        /// <param name="list"></param>
        /// <param name="treeDto"></param>
        private void SetDeptList(IList<CorpDeptPositionDTO> list,TreeDTO treeDto)
        {
            var childList = list.Where(m => m.CorpId == treeDto.Id).GroupBy(m => new { m.DepartId, m.DepartName })
                                .Select(m => new { DeptId = m.Key.DepartId,DeptName = m.Key.DepartName });
            foreach(var mModel in childList)
            {
                var childTreeDto = new TreeDTO() { Id = mModel.DeptId, Name = mModel.DeptName };
                treeDto.ChildList.Add(childTreeDto);
                this.SetPostionList(list, childTreeDto, treeDto.Id);
            }
        }

        /// <summary>
        /// 设置部门下职位
        /// </summary>
        /// <param name="list"></param>
        /// <param name="treeDto"></param>
        /// <param name="corpId"></param>
        private void SetPostionList(IList<CorpDeptPositionDTO> list, TreeDTO treeDto,string corpId)
        {
            var childList = list.Where(m => m.CorpId == corpId && m.DepartId == treeDto.Id).GroupBy(m => new { m.PositionId, m.PositionName, m.FullPositionId })
                    .Select(m => new { PositionId = m.Key.PositionId, PositionName = m.Key.PositionName, FullId = m.Key.FullPositionId });
            foreach (var mModel in childList)
            {
                var childTreeDto = new TreeDTO() { Id = mModel.PositionId, Name = mModel.PositionName, FullId = mModel.FullId, IsLeaf = true };
                treeDto.ChildList.Add(childTreeDto);
            }
        }
         
        #endregion
    }
}

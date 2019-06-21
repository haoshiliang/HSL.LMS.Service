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

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="corpDepartPositionRepository"></param>
        public CorpDepartPositionService(ICorpDepartPositionRepository corpDepartPositionRepository)
        {
            this.corpDepartPositionRepository = corpDepartPositionRepository;
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

        #endregion
    }
}

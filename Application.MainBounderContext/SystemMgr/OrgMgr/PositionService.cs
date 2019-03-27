using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Application.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public class PositionService : IPositionService
    {
        #region 私有变量

        /// <summary>
        /// 职位仓储
        /// </summary>
        private readonly IPositionRepository positionRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="positionRepository"></param>
        public PositionService(IPositionRepository positionRepository)
        {
            this.positionRepository = positionRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(Position model)
        {
            model.PyCode = model.PositionName.ToConvertPyCode();
            if (!string.IsNullOrEmpty(model.Id))
            {
                model.LastUpdateDate = DateTime.Now;
                positionRepository.Modity(model);
            }
            else
            {
                model.GenerateNewIdentity();
                model.CreateDate = DateTime.Now;
                model.LastUpdateDate = DateTime.Now;
                positionRepository.Add(model);
            }
            positionRepository.SaveChanges();
        }

        /// <summary>
        /// 取出部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Position FindById(Guid id)
        {
            return positionRepository.Get(id);
        }

        /// <summary>
        /// 取出部门列表
        /// </summary>
        /// <returns></returns>
        public ICollection<Position> FindList()
        {
            return positionRepository.GetAll().ToList();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(Guid id)
        {
            var model = FindById(id);
            model.IsDel = true;
            positionRepository.Modity(model);
            positionRepository.SaveChanges();
        }

        #endregion
    }
}

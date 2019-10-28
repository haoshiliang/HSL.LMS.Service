using LMS.Application.MainBounderContext.DTO.WorkLogMgr.DayLogMgr;
using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Entity;
using LMS.Domain.MainBounderContext.WorkLogMgr.DayLogMgr.Repository;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.WorkLogMgr.DayLogMgr
{
    public class DayLogService : IDayLogService
    {
        #region 私有变量

        public IDayLogRepository dayLogRepository;

        #endregion

        #region 构造方法

        public DayLogService(IDayLogRepository dayLogRepository)
        {
            this.dayLogRepository = dayLogRepository;
        }

        #endregion

        #region 接口方法

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(DayLog model)
        {
            try
            {
                this.dayLogRepository.UnitOfWork.BeginTrans();
                if (!string.IsNullOrEmpty(model.Id))
                {
                    this.dayLogRepository.Modity(model);
                }
                else
                {
                    model.GenerateNewIdentity();
                    model.CreateDate = DateTime.Now;
                    this.dayLogRepository.Add(model);
                }
                this.dayLogRepository.SaveChanges();
                this.dayLogRepository.UnitOfWork.Commit();
            }
            catch (Exception ex)
            {
                this.dayLogRepository.UnitOfWork.Rollback();
                throw ex;
            }
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name=""></param>
        public void Delete(string id)
        {
            var model = dayLogRepository.Get(id);
            dayLogRepository.Modity(model);
            dayLogRepository.SaveChanges();
        }

        /// <summary>
        /// 取出公司信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public DayLog FindById(string id)
        {
            return this.dayLogRepository.Get(id);
        }

        /// <summary>
        /// 取出日志列表
        /// </summary>
        /// <returns></returns>
        public ICollection<DayLogDTO> FindList(Pagination pagination, QueryParam queryParam)
        {
            var list = dayLogRepository.GetPaged<DayLogDTO>(pagination, queryParam).ToList();
            return list;
        }

        #endregion
    }
}

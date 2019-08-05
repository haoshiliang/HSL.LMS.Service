using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.OrgMgr.Entity;
using LMS.Application.Seedwork;
using LMS.Domain.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.OrgMgr
{
    public class DepartmentService : IDepartmentService
    {
        #region 私有变量

        /// <summary>
        /// 部门仓储
        /// </summary>
        private readonly IDepartmentRepository deptRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="deptRepository"></param>
        public DepartmentService(IDepartmentRepository deptRepository)
        {
            this.deptRepository = deptRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(Department model)
        {
            deptRepository.UnitOfWork.BeginTrans();
            model.PyCode = model.DepartName.ToConvertPyCode();
            if (!string.IsNullOrEmpty(model.Id))
            {
                model.LastUpdateDate = DateTime.Now;
                deptRepository.Modity(model);
            }
            else
            {
                model.GenerateNewIdentity();
                model.CreateDate = DateTime.Now;
                model.LastUpdateDate = DateTime.Now;
                deptRepository.Add(model);
            }
            deptRepository.SaveChanges();
            deptRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// 取出部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Department FindById(string id)
        {
            return deptRepository.Get(id);
        }

        /// <summary>
        /// 取出部门列表
        /// </summary>
        /// <returns></returns>
        public ICollection<Department> FindList(Pagination pagination, QueryParam queryParam)
        {
            queryParam.WhereList.Add(
                new WhereParam()
                {
                    Field = "IsDel",
                    IsDefaultQuery = true,
                    Operator = "=",
                    DataType = QueryConfig.DataType.Bool.ToString(),
                    Value = "False"
                });
            return deptRepository.GetPaged(pagination, queryParam).ToList();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var model = FindById(id);
            model.IsDel = true;
            deptRepository.Modity(model);
            deptRepository.SaveChanges();
        }

        #endregion
    }
}

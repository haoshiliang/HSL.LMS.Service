using LMS.Domain.MainBounderContext.Repository.SystemMgr.QueryMgr;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS.Application.MainBounderContext.SystemMgr.QueryMgr
{
    public class ModuleQueryService : IModuleQueryService
    {
        #region 私有变量

        /// <summary>
        /// 模块查询
        /// </summary>
        private readonly IModuleQueryRepository moduleQueryRepository;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="moduleQueryRepository"></param>
        public ModuleQueryService(IModuleQueryRepository moduleQueryRepository)
        {
            this.moduleQueryRepository = moduleQueryRepository;
        }

        #endregion

        #region 接口实现

        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        public void AddOrModity(ModuleQuery model)
        {
            model.PyCode = model.ModuleQueryName.ToConvertPyCode();
            if (!string.IsNullOrEmpty(model.Id))
            {
                model.LastUpdateDate = DateTime.Now;
                moduleQueryRepository.Modity(model);
            }
            else
            {
                model.GenerateNewIdentity();
                model.CreateDate = DateTime.Now;
                model.LastUpdateDate = DateTime.Now;
                moduleQueryRepository.Add(model);
            }
            moduleQueryRepository.SaveChanges();
        }

        /// <summary>
        /// 取出部门信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ModuleQuery FindById(string id)
        {
            return moduleQueryRepository.Get(id);
        }

        /// <summary>
        /// 取出部门列表
        /// </summary>
        /// <returns></returns>
        public ICollection<ModuleQuery> FindList(Pagination pagination, QueryParam queryParam)
        {
            queryParam.WhereList.Add(
            new WhereParam()
            {
                Field = "IsDel",
                IsDefaultQuery = true,
                Operator = "=",
                DataType = QueryConfig.DataType.Bool,
                Value = "False"
            });
            return moduleQueryRepository.GetPaged(pagination, queryParam).ToList();
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var model = FindById(id);
            model.IsDel = true;
            moduleQueryRepository.Modity(model);
            moduleQueryRepository.SaveChanges();
        }

        #endregion
    }
}

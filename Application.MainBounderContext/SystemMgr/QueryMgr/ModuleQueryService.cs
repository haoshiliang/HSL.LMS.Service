using LMS.Application.MainBounderContext.DTO.SystemMgr.QueryMgr;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Repository;
using LMS.Domain.MainBounderContext.SystemMgr.QueryMgr.Entity;
using LMS.Domain.Seedwork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Application.Seedwork;

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
        public ICollection<ModuleQueryDTO> FindList(Pagination pagination, QueryParam queryParam)
        {
            var list = moduleQueryRepository.GetPaged<ModuleQueryDTO>(pagination, queryParam).ToList();
            var dataTypeList = QueryConfig.GetEnumList(typeof(QueryConfig.DataType));
            var contrlTypeList = QueryConfig.GetEnumList(typeof(QueryConfig.ControlType));
            var defaultValueList = QueryConfig.GetEnumList(typeof(QueryConfig.DateDefalutValueType));
            foreach(var m in list)
            {
                m.DataType = dataTypeList.Where(d => d["Id"] == m.DataType).FirstOrDefault()["Name"];
                m.ControlType = contrlTypeList.Where(d => d["Id"] == m.ControlType).FirstOrDefault()["Name"];
                if (m.ControlType == "2")
                {
                    m.DefaultValue = defaultValueList.Where(d => d["Id"] == m.DefaultValue).FirstOrDefault()["Name"];
                }
            }
            return list;
        }

        /// <summary>
        /// 根据模块ID获取列表
        /// </summary>
        /// <param name="mId"></param>
        /// <returns></returns>
        public ICollection<ModuleQueryDTO> FindByModuleList(string mId, string id)
        {
            var returnList = new List<ModuleQueryDTO>();
            var list = moduleQueryRepository.GetList(m => m.ModuleId == mId && m.Id != id);
            foreach (var m in list)
            {
                returnList.Add(m.ProjectedAs<ModuleQueryDTO>());
            }
            return returnList;
        }

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        public void Delete(string id)
        {
            var model = FindById(id);
            moduleQueryRepository.Remove(model);
            moduleQueryRepository.SaveChanges();
        }

        #endregion
    }
}

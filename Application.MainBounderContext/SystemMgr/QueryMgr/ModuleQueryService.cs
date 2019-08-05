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
using LMS.Application.MainBounderContext.SystemMgr.OrgMgr;
using LMS.Application.MainBounderContext.DTO.Common;

namespace LMS.Application.MainBounderContext.SystemMgr.QueryMgr
{
    public class ModuleQueryService : IModuleQueryService
    {
        #region 私有变量

        /// <summary>
        /// 模块查询
        /// </summary>
        private readonly IModuleQueryRepository moduleQueryRepository;

        /// <summary>
        /// 公司服务类
        /// </summary>
        private readonly ICorporationService corporationService;

        #endregion

        #region 构造方法

        /// <summary>
        /// 构造方法
        /// </summary>
        /// <param name="moduleQueryRepository"></param>
        public ModuleQueryService(IModuleQueryRepository moduleQueryRepository, ICorporationService corporationService)
        {
            this.moduleQueryRepository = moduleQueryRepository;
            this.corporationService = corporationService;
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
            foreach (var m in list)
            {
                m.DataTypeLabel = dataTypeList.Where(d => d["Id"].ToString() == m.DataType.ToString()).FirstOrDefault()["Name"].ToString();
                m.ControlTypeLabel = contrlTypeList.Where(d => d["Id"].ToString() == m.ControlType.ToString()).FirstOrDefault()["Name"].ToString();
                if (m.ControlType.ToString() == "2")
                {
                    m.DefaultValue = defaultValueList.Where(d => d["Id"].ToString() == m.DefaultValue).FirstOrDefault()["Name"].ToString();
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
        /// 取出查询条件
        /// </summary>
        /// <param name="mId">模块ID</param>
        /// <returns></returns>
        public QueryParam FindQueryParam(string mId, string userId)
        {
            var returnValue = new QueryParam();
            var list = moduleQueryRepository.GetListByModuleId<ModuleQueryDTO>(mId);
            foreach (var m in list)
            {
                var whereParam = m.ProjectedAs<ModuleQueryDTO, WhereParam>();
                whereParam.IsDefaultQuery = (m.IsDefaultQuery == 1);
                whereParam.Exists = m.ExistsValue;
                whereParam.ControlType = ((QueryConfig.ControlType)m.ControlType).ToString();
                whereParam.DataType = ((QueryConfig.DataType)m.DataType).ToString();
                //获取绑定列表
                this.GetBinderList(whereParam, m, list, userId);
                //获取默认值
                this.GetDefaultValue(whereParam, m);
                returnValue.WhereList.Add(whereParam);
            }
            return returnValue;
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

        #region 私有函数

        #region 获取绑定列表

        /// <summary>
        /// 获取绑定列表
        /// </summary>
        /// <param name="whereParam"></param>
        private void GetBinderList(WhereParam whereParam, ModuleQueryDTO m,IList<ModuleQueryDTO> mList,string userId)
        {
            switch (m.ControlType)
            {
                case (int)QueryConfig.ControlType.ComboTreeBox:
                    whereParam.BinderList = corporationService.FindTreeList();
                    whereParam.AllBinderList = new List<string>();
                    break;
                case (int)QueryConfig.ControlType.ComboMultipleBox:
                case (int)QueryConfig.ControlType.ComboRadioBox:
                    if(!string.IsNullOrEmpty(m.RelationId_1) || !string.IsNullOrEmpty(m.RelationId_2))
                    {
                        whereParam.AllBinderList = this.GetComboList(m.DropdownDataSource, m.DownListValue, userId);
                        whereParam.BinderList = this.GetCurrentBinder(mList, m, whereParam);
                    }
                    else
                    {
                        whereParam.BinderList = this.GetComboList(m.DropdownDataSource, m.DownListValue, userId);
                        whereParam.AllBinderList = new List<string>();
                    }
                    break;
                default:
                    whereParam.BinderList = new List<string>();
                    whereParam.AllBinderList = new List<string>();
                    break;
            }
        }

        /// <summary>
        /// 获取下拉选项值
        /// </summary>
        /// <param name="dataSource">数据来源 0 自定议,1 数据库</param>
        /// <param name="dropDownValue">下拉选项值</param>
        /// <returns></returns>
        private object GetComboList(int dataSource,string dropDownValue,string userId)
        {
            var returnList = new List<SelectDTO>();
            if (dataSource == 0)
            {
                string[] rowValueList = dropDownValue.Split(new char[] { '\r', '\n' });
                foreach (string rowValue in rowValueList)
                {
                    var selDto = new SelectDTO() { Id = rowValue.Split('|')[0], Name = rowValue.Split('|')[1] };
                    if (rowValue.Split('|').Length > 2)
                        selDto.RelationId_1 = rowValue.Split('|')[2];
                    if (rowValue.Split('|').Length > 3)
                        selDto.RelationId_2 = rowValue.Split('|')[3];
                    returnList.Add(selDto);
                }
            }
            else
            {
                dropDownValue = dropDownValue.Replace("@UserId", "'" + userId + "'");
                returnList = this.moduleQueryRepository.ExecuteQuerySql<SelectDTO>(dropDownValue, new string[] { }, new object[] { }).ToList();
            }
            return returnList;
        }

        /// <summary>
        /// 获取下拉列表当前绑定数据
        /// 如果关联字段有默认值则重新获取绑定值
        /// </summary>
        /// <param name="mList"></param>
        /// <param name="m"></param>
        /// <param name="whereParam"></param>
        /// <returns></returns>
        private object GetCurrentBinder(IList<ModuleQueryDTO> mList, ModuleQueryDTO m, WhereParam whereParam)
        {
            var returnList = new List<SelectDTO>();
            if(!string.IsNullOrEmpty(m.RelationId_1) && string.IsNullOrEmpty(m.RelationId_2))
            {
                var mqModel = mList.Where(g => g.ParamName == m.RelationId_1).FirstOrDefault();
                if (mqModel != null && !string.IsNullOrEmpty(mqModel.DefaultValue))
                {
                    returnList = ((List<SelectDTO>)whereParam.AllBinderList).Where(g=>g.RelationId_1==mqModel.DefaultValue).ToList();
                }
            }
            else if(!string.IsNullOrEmpty(m.RelationId_1) && !string.IsNullOrEmpty(m.RelationId_2))
            {
                var mqModel1 = mList.Where(g => g.ParamName == m.RelationId_1).FirstOrDefault();
                var mqModel2 = mList.Where(g => g.ParamName == m.RelationId_2).FirstOrDefault();
                if (mqModel1 != null && !string.IsNullOrEmpty(mqModel1.DefaultValue) && mqModel2 != null && !string.IsNullOrEmpty(mqModel2.DefaultValue))
                {
                    returnList = ((List<SelectDTO>)whereParam.AllBinderList).Where(g => g.RelationId_1 == mqModel1.DefaultValue && g.RelationId_2 == mqModel2.DefaultValue).ToList();
                }
            }
            return returnList;
        }

        #endregion

        #region 获取默认值

        /// <summary>
        /// 获取默认值
        /// </summary>
        /// <param name="whereParam"></param>
        /// <param name="m"></param>
        private void GetDefaultValue(WhereParam whereParam, ModuleQueryDTO m)
        {
            if (m.DataType == (int)QueryConfig.DataType.DateTime)
            {
                var dValue = 0;
                Int32.TryParse(m.DefaultValue, out dValue);
                whereParam.Value = this.GetDefaultValue((QueryConfig.DateDefalutValueType)dValue);
            }
            else
            {
                whereParam.Value = m.DefaultValue;
            }
        }

        /// <summary>
        /// 获取默认值类型
        /// </summary>
        /// <param name="defaultValueType"></param>
        /// <returns></returns>
        private string GetDefaultValue(QueryConfig.DateDefalutValueType defaultValueType)
        {
            string returnValue = "";
            switch (defaultValueType)
            {
                case QueryConfig.DateDefalutValueType.CurrentDate:
                    returnValue = DateTime.Now.ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.PreCurrentDate:
                    returnValue = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.NextMonthFirstDay:
                    returnValue = DateTime.Now.AddMonths(1).ToString("yyyy-MM-01");
                    break;
                case QueryConfig.DateDefalutValueType.NextMonthLastDay:
                    returnValue = DateTime.Parse(DateTime.Now.AddMonths(2).ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.PreviousMonthFirstDay:
                    returnValue = DateTime.Now.AddMonths(-1).ToString("yyyy-MM-01");
                    break;
                case QueryConfig.DateDefalutValueType.PreviousMonthLastDay:
                    returnValue = DateTime.Parse(DateTime.Now.ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.ThisMonthFirstDay:
                    returnValue = DateTime.Now.ToString("yyyy-MM-01");
                    break;
                case QueryConfig.DateDefalutValueType.ThisMonthLastDay:
                    returnValue = DateTime.Parse(DateTime.Now.AddMonths(1).ToString("yyyy-MM-01")).AddDays(-1).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.ThisWeekFirstDay:
                    returnValue = GetWeekFirstDayMon(DateTime.Now).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.ThisWeekLastDay:
                    returnValue = GetWeekLastDaySun(DateTime.Now).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.PreviousWeekFirstDay:
                    returnValue = GetWeekFirstDayMon(DateTime.Now.AddDays(-7)).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.PreviousWeekLastDay:
                    returnValue = GetWeekLastDaySun(DateTime.Now.AddDays(-7)).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.NextWeekFirstDay:
                    returnValue = GetWeekFirstDayMon(DateTime.Now.AddDays(7)).ToString("yyyy-MM-dd");
                    break;
                case QueryConfig.DateDefalutValueType.NextWeekLastDay:
                    returnValue = GetWeekLastDaySun(DateTime.Now.AddDays(7)).ToString("yyyy-MM-dd");
                    break;
                default:
                    returnValue = "";
                    break;
            }
            return returnValue;
        }

        /// <summary>  
        /// 得到本周第一天(以星期一为第一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        private static DateTime GetWeekFirstDayMon(DateTime datetime)
        {
            //星期一为第一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            //因为是以星期一为第一天，所以要判断weeknow等于0时，要向前推6天。  
            weeknow = (weeknow == 0 ? (7 - 1) : (weeknow - 1));
            int daydiff = (-1) * weeknow;

            //本周第一天  
            string FirstDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(FirstDay);
        }
        /// <summary>  
        /// 得到本周最后一天(以星期天为最后一天)  
        /// </summary>  
        /// <param name="datetime"></param>  
        /// <returns></returns>  
        public static DateTime GetWeekLastDaySun(DateTime datetime)
        {
            //星期天为最后一天  
            int weeknow = Convert.ToInt32(datetime.DayOfWeek);
            weeknow = (weeknow == 0 ? 7 : weeknow);
            int daydiff = (7 - weeknow);

            //本周最后一天  
            string LastDay = datetime.AddDays(daydiff).ToString("yyyy-MM-dd");
            return Convert.ToDateTime(LastDay);
        }

        #endregion

        #endregion
    }
}

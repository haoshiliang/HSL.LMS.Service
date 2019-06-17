using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LMS.Domain.MainBounderContext.SystemMgr.UserRoleMgr.Entity;
using LMS.Application.MainBounderContext.DTO.SystemMgr.UserRoleMgr;
using LMS.Domain.Seedwork;

namespace LMS.Application.MainBounderContext.SystemMgr.UserRoleMgr
{
    public interface IUserService
    {
        /// <summary>
        /// 添加或修改
        /// </summary>
        /// <param name="model"></param>
        void AddOrModity(User model);

        /// <summary>
        /// 取出职位信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        User FindById(string id);

        /// <summary>
        /// 根据用户名取出用户信息
        /// </summary>
        /// <param name="loginName"></param>
        /// <returns></returns>
        UserDTO FindByName(string loginName);

        /// <summary>
        /// 删除信息
        /// </summary>
        /// <param name="id"></param>
        void Delete(string id);

        /// <summary>
        /// 取出角色列表
        /// </summary>
        /// <returns></returns>
        ICollection<UserDTO> FindList(Pagination pagination, QueryParam queryParam);

        /// <summary>
        /// 设置Token
        /// </summary>
        /// <param name="key">键值</param>
        /// <param name="token">token值</param>
        /// <param name="cacheTime">过期时间</param>
        void SetToken(string key, string token, int cacheTime);

        /// <summary>
        /// 获取Token
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetToken(string key);

        /// <summary>
        /// 是否存在Token
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool IsExistToken(string key);
        /// <summary>
        /// 设置Token有效期
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool SetExpire(string key,int cacheTime);
        /// <summary>
        /// 移除Token
        /// </summary>
        /// <param name="key"></param>
        void RemoveToken(string key);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_Oper
    {


        #region 添加操作记录

        public bool AddOperationLog(string Mno, string Logtype, string Uno)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //try
                //{
                //    _DataEntities.Configuration.ProxyCreationEnabled = false;
                //    t_operation_log model = new t_operation_log();
                //    model.Menu_no = Mno;
                //    model.Log_type = Logtype;
                //    model.mader = Uno;
                //    model.marder_time = DateTime.Now;
                //    model.made_dist = "A";
                //    _DataEntities.t_operation_log.Add(model);
                //    if (_DataEntities.SaveChanges() > 0)
                //    {
                //        return true;
                //    }
                //}
                //catch (Exception)
                //{
                //    return false;
                //    //throw;
                //}
            }
            return false;
        }

        #endregion



        #region 登录验证


        public t_user CheckLogin(string _userno, string _password)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_user.FirstOrDefault(o => o.UserCode == _userno && o.PassWord == _password && o.Disabled == 0);
            }
        }
        #endregion


        #region 权限操作

        /// <summary>
        /// 根据角色编号查询菜单权限
        /// </summary>
        /// <param name="RoleNo"></param>
        /// <returns></returns>
        public List<t_menu> GetMenuByRole(string RoleCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                t_role_menu rm = _DataEntities.t_role_menu.FirstOrDefault(o => o.RoleCode == RoleCode && o.Disabled == 0);
                string[] strArray = rm.MenuCode.Split(',');
                List<t_menu> list = _DataEntities.t_menu.Where(o => o.IsWebPub == "1" && o.Disabled == 0).ToList();
                return list.Where(o => strArray.Contains(o.MenuCode)).OrderBy(p => p.SortID).ToList();
            }
        }

        /// <summary>
        /// 查询所有菜单权限
        /// </summary>
        /// <param name="RoleNo"></param>
        /// <returns></returns>
        public List<t_menu> GetMenu()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_menu.Where(o => o.IsWebPub != "2" && o.Disabled == 0).ToList();
            }
        }

        /// <summary>
        /// 查询操作权限
        /// </summary>
        /// <param name="RoleNo"></param>
        /// <returns></returns>
        public t_role_menu GetAuthorityByRole(string RoleCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_role_menu.FirstOrDefault(o => o.RoleCode == RoleCode && o.Disabled == 0);
            }
        }
        /// <summary>
        /// 添加权限
        /// </summary>
        /// <param name="rolemenu"></param>
        /// <returns></returns>
        public bool AddAuthority(t_role_menu rolemenu)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {
                    _DataEntities.t_role_menu.Add(rolemenu);
                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    //throw;
                }
            }
            return false;
        }
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public bool UpdateAuthority(t_role_menu entity)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var model = _DataEntities.t_role_menu.FirstOrDefault(o => o.RoleCode == entity.RoleCode && o.Disabled == 0);
                if (model != null)
                {
                    try
                    {
                        model.RoleCode = entity.RoleCode;
                        model.MenuCode = entity.MenuCode;
                        model.Disabled = entity.Disabled;
                        model.ModifyBy = entity.ModifyBy;
                        model.ModifyTime = entity.ModifyTime;
                        if (_DataEntities.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }
            }
            return false;
        }


        #endregion

        #region 用户管理

        /// <summary>
        /// 查询所有用户信息
        /// </summary>
        /// <returns></returns>
        public List<t_user> NewGetUser()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_user.ToList();
                //return _DataEntities.v_user.Where(p => p.Disabled >= 0).ToList();
            }
        }
        public List<v_user> GetUser()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.v_user.ToList();
            }
        }


        public List<t_user> GetUserList()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.t_user.Where(p => p.Disabled == 0).OrderByDescending(u => u.ModifyTime).ToList();
            }
        }
        /// <summary>
        /// 根据员工编号查询用户对象
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public t_user GetUserByNo(string no)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.t_user.FirstOrDefault(c => c.UserCode == no && c.Disabled == 0);
            }
        }
        /// <summary>
        /// 添加用户信息
        /// </summary>
        /// <param name="user"></param>
        /// <returns>bool</returns>
        public bool AddUser(t_user user)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                _DataEntities.t_user.Add(user);
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool UpdateUser(t_user model)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                t_user user = _DataEntities.t_user.FirstOrDefault(o => o.UserCode == model.UserCode && o.Disabled == 0);
                if (user != null)
                {
                    user.UserName = model.UserName;
                    user.PassWord = model.PassWord;
                    user.Email = model.Email;
                    user.IsAdmin = model.IsAdmin;
                    user.Sex = model.Sex;
                    user.Telephone = model.Telephone;
                    user.Addr = model.Addr;
                    user.UserRoles = model.UserRoles;
                    user.ModifyTime = model.ModifyTime;
                    user.ModifyBy = model.ModifyBy;
                    user.Disabled = model.Disabled;
                    user.Remark = model.Remark;
                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="Nos"></param>
        /// <param name="Uno"></param>
        /// <returns></returns>
        public bool DelUser(string ids, string userId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {
                    string[] user_ids = ids.Split(',');
                    _DataEntities.Configuration.ProxyCreationEnabled = false;
                    for (int i = 0; i < user_ids.Length; i++)
                    {
                        string user_id = user_ids[i];
                        t_user t_user = _DataEntities.t_user.FirstOrDefault(o => o.UserId == user_id && o.Disabled == 0);
                        t_user.Disabled = 1;
                        t_user.ModifyBy = userId;
                        t_user.ModifyTime = DateTime.Now;
                    }
                    if (_DataEntities.SaveChanges() == user_ids.Length)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    //throw;
                }

            }
            return false;
        }


        #endregion

        #region 角色管理
        /// <summary>
        /// 查询角色信息
        /// </summary>
        /// <returns></returns>
        public List<t_roles> GetRole()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_roles.Where(o => o.Disabled == 0).ToList();
            }
        }
      
        /// <summary>
        /// 根据角色编号查询角色信息
        /// </summary>
        /// <param name="No">角色编号</param>
        /// <returns></returns>
        public t_roles GetRoleByNo(string RoleCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return _DataEntities.t_roles.FirstOrDefault(o => o.RoleCode == RoleCode && o.Disabled == 0);
            }
        }
        /// <summary>
        /// 添加角色信息
        /// </summary>
        /// <param name="type">角色对象</param>
        /// <returns></returns>
        public bool AddRole(t_roles role)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {
                    _DataEntities.t_roles.Add(role);
                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    //throw;
                }
            }
            return false;
        }
        /// <summary>
        /// 修改角色信息
        /// </summary>
        /// <param name="type">角色对象</param>
        /// <returns></returns>
        public bool UpdateRole(t_roles role)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var model = _DataEntities.t_roles.FirstOrDefault(o => o.RoleCode == role.RoleCode && o.Disabled == 0);
                if (model != null)
                {
                    try
                    {
                        model.RoleName = role.RoleName;
                        model.ModifyBy = role.ModifyBy;
                        model.ModifyTime = role.ModifyTime;
                        if (_DataEntities.SaveChanges() > 0)
                        {
                            return true;
                        }
                    }
                    catch (Exception)
                    {
                        //throw;
                    }
                }
            }
            return false;
        }


        /// <summary>
        /// 删除角色信息
        /// </summary>
        /// <param name="Nos"></param>
        /// <param name="Uno"></param>
        /// <returns></returns>
        public bool DelRoles(string ids, string userId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {
                    string[] role_ids = ids.Split(',');
                    _DataEntities.Configuration.ProxyCreationEnabled = false;
                    for (int i = 0; i < role_ids.Length; i++)
                    {
                        int role_id = Int32.Parse(role_ids[i]);
                        t_roles t_role = _DataEntities.t_roles.FirstOrDefault(o => o.RoleId == role_id && o.Disabled == 0);
                        t_role.Disabled = 1;
                        t_role.ModifyBy = userId;
                        t_role.ModifyTime = DateTime.Now;
                    }
                    if (_DataEntities.SaveChanges() == role_ids.Length)
                    {
                        return true;
                    }
                }
                catch (Exception)
                {
                    return false;
                    //throw;
                }
            }
            return false;
        }
        #endregion


    }
}

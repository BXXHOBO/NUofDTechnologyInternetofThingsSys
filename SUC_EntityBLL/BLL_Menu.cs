using Common;
using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUC_EntityBLL
{
    public class BLL_Menu
    {

        /// <summary>
        /// 获取菜单集合
        /// </summary>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="search">搜索字段</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="sidx">排序字段</param>
        /// <param name="sord">升降序</param>
        /// <returns></returns>
        public PageRes GetMenus(string start, string end, string search, int pageIndex, int pageSize, string sidx, string sord)
        {
            try
            {
                using (var _DataEntities = new SUC_SYSContainer())
                {

                    PageRes res = new PageRes();
                    res.page = pageIndex;
                    res.pageSize = pageSize;
                    var menus = from m in _DataEntities.t_menu
                                join u in _DataEntities.t_user
                                on m.CreateBy equals u.UserId
                                where u.Disabled != 1 && m.Disabled != 1
                                select new
                                {
                                    m.MenuId,
                                    m.MenuCode,
                                    m.MenuName,
                                    m.LinkUrl,
                                    m.ParentNo,
                                    m.Disabled,
                                    CreateUser = u.UserName,
                                    m.CreateTime,
                                    m.ModifyTime,
                                    m.Icon
                                };

                    if (!string.IsNullOrEmpty(start) && !string.IsNullOrEmpty(end))
                    {
                        DateTime startTime = Convert.ToDateTime(start);
                        DateTime endTime = Convert.ToDateTime(end);
                        menus = menus.Where(m => m.ModifyTime >= startTime && m.ModifyTime <= endTime);
                    }
                    if (!string.IsNullOrEmpty(search))
                    {
                        menus = menus.Where(m => m.MenuName.Contains(search));
                    }
                    if (!string.IsNullOrEmpty(sidx) && !string.IsNullOrEmpty(sord))
                    {
                        if ("desc".Equals(sord))
                        {
                            menus = menus.OrderByDescending(m => m.CreateTime);
                        }
                        else
                        {
                            menus = menus.OrderBy(m => m.CreateTime);
                        }
                    }

                    res.total = menus.Count();
                    //分页后
                    var menus2 = menus.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                    int i = 1;
                    List<object> resMenus = new List<object>();
                    foreach (var menu in menus2)
                    {
                        string createTime = menu.CreateTime != null ? menu.CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        string modifyTime = menu.ModifyTime != null ? menu.ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : "";
                        resMenus.Add(new
                        {
                            RowNo = i++,
                            menu.MenuId,
                            menu.MenuCode,
                            menu.MenuName,
                            menu.LinkUrl,
                            menu.ParentNo,
                            menu.Disabled,
                            menu.CreateUser,
                            CreateTime = createTime,
                            ModifyTime = modifyTime,
                            menu.Icon
                        });
                    }

                    res.rows = resMenus;
                    return res;
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        /// <summary>
        /// 假删除菜单
        /// </summary>
        /// <param name="menuId"></param>
        /// <returns></returns>
        public bool Delete(int menuId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.t_menu.FirstOrDefault(m=> m.MenuId == menuId);
                if (entity == null)
                {
                    return false;
                }
                entity.Disabled = 1;
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// 提交菜单
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        public t_menu SubmitMenu(t_menu menu, t_user user)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                //修改
                if (menu.MenuId != 0)
                {
                    //获取之前的数据
                    var entity = _DataEntities.t_menu.FirstOrDefault(m=> m.MenuId == menu.MenuId);
                    entity.MenuName = menu.MenuName;
                    entity.MenuCode = menu.MenuCode;
                    entity.ParentNo = menu.ParentNo;
                    entity.SortID = menu.SortID;
                    entity.Icon = menu.Icon;
                    entity.LinkUrl = menu.LinkUrl;
                    entity.ModifyBy = user.UserId;
                    entity.ModifyTime = DateTime.Now;
                }
                else //创建
                {
                    //menu.MenuId = Guid.NewGuid();
                    var entity = _DataEntities.t_menu.OrderByDescending(m => m.MenuCode).FirstOrDefault();
                    if (entity != null)
                    {
                        var codeStr = entity.MenuCode.Substring(1);
                        var codeInt = Convert.ToInt32(codeStr) + 1;
                        codeStr = codeInt.ToString();
                        var strLength = codeStr.Length;
                        switch (strLength)
                        {
                            case 1:
                                codeStr = "M000" + codeStr;
                                break;
                            case 2:
                                codeStr = "M00" + codeStr;
                                break;
                            case 3:
                                codeStr = "M0" + codeStr;
                                break;
                            case 4:
                                codeStr = "M" + codeStr;
                                break;
                            default:
                                return null;
                        }
                        menu.MenuCode = codeStr;
                    }
                    menu.IsWebPub= "1";
                    menu.CreateTime = DateTime.Now;
                    menu.CreateBy = user.UserId;
                    menu.ModifyTime = DateTime.Now;
                    menu.ModifyBy = user.UserId;
                    _DataEntities.t_menu.Add(menu); 
                }
                if (_DataEntities.SaveChanges() > 0)
                {
                    return menu;
                }
                else
                {
                    return null;
                }
              
            }
        }

        /// <summary>
        /// 根据菜单编号获取菜单
        /// </summary>
        /// <param name="menuCode">菜单编号</param>
        /// <returns></returns>
        public object GetMenuByCode(string menuCode)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                var entity = _DataEntities.t_menu.FirstOrDefault(m => m.MenuCode == menuCode);
                if (entity != null)
                {
                    return new
                    {
                        entity.MenuId,
                        entity.MenuName,
                        entity.MenuCode,
                        entity.ParentNo,
                        entity.LinkUrl,
                        entity.Icon,
                        entity.CreateBy,
                        CreateTime = entity.CreateTime != null ? entity.CreateTime.ToString("yyyy-MM-dd HH-mm:ss") : "",
                        entity.Disabled,
                        entity.SortID,
                        ModifyTime = entity.ModifyTime != null ? entity.ModifyTime.Value.ToString("yyyy-MM-dd HH-mm:ss") : ""
                    };
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// 根据编号修改课件权限
        /// </summary>
        /// <param name="Code"></param>
        /// <returns></returns>
        public string UpdateCodeId(string Code)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                return "";
            }
        }
    }
}

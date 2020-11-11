using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZFine.Code;
using Common;
using ZFine.Data;
using SY_DataEntity;
using ZFine.Domain._04_IRepository.EPManage;
using ZFine.Repository;
using ZFine.Repository.EPManage;
namespace SY_EntityBLL
{
    public class BLL_MonitorInfo
    {
    
        public List<EP_MonitorInfo> GetList(Pagination pagination, string keyword = "")
        {

            using (var _DataEntities = new SYEntities())
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    var Data = _DataEntities.EP_MonitorInfo.Where(t => t.DataTypeCode.Contains(keyword) || t.Longtitude.Contains(keyword)).ToList();
                    return Data;
                }
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.EP_MonitorInfo.Where(p => p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }

        public List<EP_MonitorInfo> GetDataList()
        {

            using (var _DataEntities = new SYEntities())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.EP_MonitorInfo.Where(p => p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();


            }
        }
        public EP_MonitorInfo GetInfoById(Guid id)
        {
            using (var _DataEntities = new SYEntities())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.EP_MonitorInfo.Where(p => p.MonitorInfoId == id && p.Disabled == 0).FirstOrDefault();
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="standard"></param>
        /// <returns>bool</returns>
        public bool Add(EP_MonitorInfo monitorinfo)
        {
            using (var _DataEntities = new SYEntities())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                _DataEntities.EP_MonitorInfo.Add(monitorinfo);
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Update(EP_MonitorInfo model)
        {
            using (var _DataEntities = new SYEntities())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                EP_MonitorInfo monitorinfo = _DataEntities.EP_MonitorInfo.FirstOrDefault(p => p.MonitorInfoId == model.MonitorInfoId && p.Disabled == 0);
                if (monitorinfo != null)
                {
                    monitorinfo.MonitorDate = model.MonitorDate;
                    monitorinfo.DataTypeCode = model.DataTypeCode;
                    monitorinfo.Longtitude = model.Longtitude;
                    monitorinfo.Latitude = model.Latitude;
                    monitorinfo.Aerosol = model.Aerosol;
                    monitorinfo.Ozone = model.Ozone;
                    monitorinfo.VOC = model.VOC;
                    monitorinfo.OzoneFlag = model.OzoneFlag;
                    monitorinfo.Remark = model.Remark;
                    monitorinfo.Rec_CreateTime = model.Rec_CreateTime;
                    monitorinfo.Rec_CreateBy = model.Rec_CreateBy;
                    monitorinfo.Rec_ModifyTime = model.Rec_ModifyTime;
                    monitorinfo.Rec_ModifyBy = model.Rec_ModifyBy;
                    if (_DataEntities.SaveChanges() > 0)
                    {
                        return true;
                    }
                }
                return false;
            }
        }
        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="Nos"></param>
        /// <param name="Uno"></param>
        /// <returns></returns>
        public bool Del(string Nos, string Uno)
        {
            using (var _DataEntities = new SYEntities())
            {
                try
                {
                    string[] strs = Nos.Split(',');
                    _DataEntities.Configuration.ProxyCreationEnabled = false;
                    List<EP_MonitorInfo> list = _DataEntities.EP_MonitorInfo.Where(p => p.Disabled == 0).AsEnumerable().Where(p => strs.Contains(p.MonitorInfoId.ToString())).ToList();
                    foreach (var item in list)
                    {
                        item.Disabled = 1;
                        item.Rec_ModifyTime = DateTime.Now;
                        item.Rec_ModifyBy = Uno;
                    }
                    if (_DataEntities.SaveChanges() == list.Count)
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

    }
}

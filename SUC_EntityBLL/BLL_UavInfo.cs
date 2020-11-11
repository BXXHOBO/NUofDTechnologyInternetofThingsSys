
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_UavInfo
    {
        public List<V_SortiePatition> GetAllList(Pagination pagination, string keyword="")
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                if (!string.IsNullOrEmpty(keyword))
                {

                    var Data = _DataEntities.V_SortiePatition.Where(t => t.Sortie == keyword).ToList();
                    return Data;
                }
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.V_SortiePatition.OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }

        public List<SUC_UavInfo> GetList(Pagination pagination, string keyword = "")
        {
            //搜索数据筛选
            using (var _DataEntities = new SUC_SYSContainer())
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    //var Data = _DataEntities.SUC_UavInfo.Where(t => t.Sortie.Contains(keyword)||t.Rec_CreateBy.Contains(keyword)).ToList();
                    var Data = _DataEntities.SUC_UavInfo.Where(t => t.Sortie==keyword || t.Rec_CreateBy==keyword).ToList();
                    return Data;
                }
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.SUC_UavInfo.Where(p => p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }

        public List<SUC_UavInfo> GetTheuavSerialNO(string uavSerialNO)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavInfo.Where(p => p.UavSerialNO ==uavSerialNO&&p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                return Data;
            }
        }

        public SUC_UavInfo GetTheUavInfo()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavInfo.Where(p => p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).FirstOrDefault();
                    return Data;  
            }
        }

        public List<SUC_UavInfo> GetUavListByUandS(string uavSerialNO,string sortie)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavInfo.Where(p => p.UavSerialNO == uavSerialNO&&p.Sortie==sortie&&p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                return Data;
            }
        }


        public SUC_UavInfo GetUavLastTimeBySortie(string UavSerialNO,string sortie)//得到最后时间
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavInfo.Where(p =>p.UavSerialNO==UavSerialNO&&p.Sortie == sortie && p.Disabled == 0).OrderByDescending(n => n.Rec_CreateTime).FirstOrDefault();//取创建时间的最后一个数据
                return Data;
            }
        }

        public SUC_UavInfo GetUavListBysortiefirst(string UavSerialNO,string sortie)//得到开始时间
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavInfo.Where(p => p.UavSerialNO == UavSerialNO && p.Sortie == sortie && p.Disabled == 0).OrderBy(n => n.Rec_CreateTime).FirstOrDefault();//取创建时间的最后一个数据
                return Data;
            }
        }

        public SUC_UavInfo GetInfoByUavSerialNOandSortie(string uavSerialNO,string sortie)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavInfo.Where(p => p.UavSerialNO == uavSerialNO&& p.Sortie == sortie && p.Disabled == 0).FirstOrDefault();
            }
        }
        public List<SUC_UavInfo> GetDataList()
        {

            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavInfo.Where(p => p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();


            }
        }
       
        public SUC_UavInfo GetDataListbyCreateTime(DateTime currentTime, string Sortie)
        {

            using (var _DataEntities = new SUC_SYSContainer())
            {
                    _DataEntities.Configuration.ProxyCreationEnabled = false;
                    SUC_UavInfo model = _DataEntities.SUC_UavInfo.Where(p => p.Rec_CreateTime <= currentTime && p.Sortie == Sortie&& p.Disabled == 0).OrderByDescending(u => u.Rec_CreateTime).FirstOrDefault();
                   
                    return model;

            } 

        }

        public SUC_UavInfo GetLastMonitorData()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                SUC_UavInfo model = _DataEntities.SUC_UavInfo.OrderByDescending(n => n.Rec_CreateTime).Where(p => p.Disabled == 0).FirstOrDefault();
                return model;
            }
        }
        public SUC_UavInfo GetData()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                SUC_UavInfo model = _DataEntities.SUC_UavInfo.OrderByDescending(n =>n.UavInfoId).FirstOrDefault();
                return model;
            }
        }
      
        public SUC_UavInfo GetInfoById(Guid id)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavInfo.Where(p => p.UavInfoId == id && p.Disabled == 0).FirstOrDefault();
            }
        }


        public SUC_UavInfo GetUavInfoByNo(string no)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavInfo.Where(p => p.Disabled == 0 && p.UavSerialNO == no).OrderByDescending(p => p.Rec_CreateTime).FirstOrDefault();
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="standard"></param>
        /// <returns>bool</returns>
        public bool Add(SUC_UavInfo uavinfo)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                _DataEntities.SUC_UavInfo.Add(uavinfo);
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Update(SUC_UavInfo model)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                SUC_UavInfo uavinfo = _DataEntities.SUC_UavInfo.FirstOrDefault(p => p.UavInfoId == model.UavInfoId && p.Disabled == 0);
                if (uavinfo != null)
                {
                    uavinfo.UavSerialNO = model.UavSerialNO;
                    uavinfo.OperationDate = model.OperationDate;
                    uavinfo.Sortie = model.Sortie;
                    uavinfo.VideoAddr = model.VideoAddr;
                    uavinfo.Longitude = model.Longitude;   
                    uavinfo.Latitude = model.Latitude;
                    uavinfo.Altitude = model.Altitude;
                    uavinfo.Temperature = model.Temperature;
                    uavinfo.Humidity = model.Humidity;
                    uavinfo.Speed = model.Speed;
                    uavinfo.AtmosPressure = model.AtmosPressure;
                    uavinfo.Remark = model.Remark;
                    uavinfo.Rec_CreateTime = model.Rec_CreateTime;
                    uavinfo.Rec_CreateBy = model.Rec_CreateBy;
                    uavinfo.Rec_ModifyTime = model.Rec_ModifyTime;
                    uavinfo.Rec_ModifyBy = model.Rec_ModifyBy;
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
            using (var _DataEntities = new SUC_SYSContainer())
            {
                try
                {
                    string[] strs = Nos.Split(',');
                    _DataEntities.Configuration.ProxyCreationEnabled = false;
                    List<SUC_UavInfo> list = _DataEntities.SUC_UavInfo.Where(p => p.Disabled == 0).AsEnumerable().Where(p => strs.Contains(p.UavInfoId.ToString())).ToList();
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


        public object GetList()
        {
            throw new NotImplementedException();
        }

        public SUC_UavInfo GetUavLastTimeByUandS(string UavSerialNO, string Sortie)//得到最后时间数据
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavInfo.Where(p => p.Sortie == Sortie && p.UavSerialNO == UavSerialNO && p.Disabled == 0).OrderByDescending(n => n.Rec_CreateTime).FirstOrDefault();//取创建时间的最后一个数据
                return Data;
            }
        }
    }
}

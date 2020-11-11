using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using SUC_DataEntity;

namespace SUC_EntityBLL
{
    public class BLL_UavSortieData
    {
        public List<SUC_UavSortieData> GetList(Pagination pagination, string keyword = "")
        {
            //搜索数据筛选
            using (var _DataEntities = new SUC_SYSContainer())
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    //var Data = _DataEntities.SUC_UavInfo.Where(t => t.Sortie.Contains(keyword)||t.Rec_CreateBy.Contains(keyword)).ToList();
                    var Data = _DataEntities.SUC_UavSortieData.Where(t => t.Sortie == keyword || t.Rec_CreateBy == keyword||t.UavSerialNO==keyword&&t.Disabled==0).ToList();
                    return Data;
                }
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.SUC_UavSortieData.Where(p => p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }
       // 实时数据
        public List<SUC_UavSortieData> GetLatestList(Pagination pagination, DateTime date1, string keyword = "")
        {
            //搜索数据筛选
            using (var _DataEntities = new SUC_SYSContainer())
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    
                    var Data = _DataEntities.SUC_UavSortieData.Where(t => t.Sortie == keyword || t.Rec_CreateBy == keyword || t.UavSerialNO == keyword && t.Disabled == 0).ToList();
                    return Data;
                }
               
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.SUC_UavSortieData.Where(p => date1 < p.Rec_CreateTime && p.Disabled ==0).OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }

        // 实时数据
        public List<SUC_UavSortieData> GetUavFirstList(Pagination pagination,string keyword = "")
        {
            //搜索数据筛选
            using (var _DataEntities = new SUC_SYSContainer())
            {
                if (!string.IsNullOrEmpty(keyword))
                {

                    var Data = _DataEntities.SUC_UavSortieData.Where(t => t.Sortie == keyword || t.Rec_CreateBy == keyword || t.UavSerialNO == keyword && t.Disabled == 0).ToList();
                    return Data;
                }

                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.SUC_UavSortieData.Where(p => p.UavState=="1"&& p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }

        // 历史数据
        public List<SUC_UavSortieData> GetUavLatestList(Pagination pagination, string keyword = "")
        {
            //搜索数据筛选
            using (var _DataEntities = new SUC_SYSContainer())
            {
                if (!string.IsNullOrEmpty(keyword))
                {

                    var Data = _DataEntities.SUC_UavSortieData.Where(t => t.Sortie == keyword || t.Rec_CreateBy == keyword || t.UavSerialNO == keyword && t.Disabled == 0).ToList();
                    return Data;
                }

                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.SUC_UavSortieData.Where(p => p.UavState == "0" && p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }

        //// 实时数据
        //public List<SUC_UavSortieData> GetLatestData(DateTime date1)
        //{
        //    using (var _DataEntities = new SYEntities())
        //    {

        //        var tempData = _DataEntities.SUC_UavSortieData.Where(p => date1 < p.Rec_CreateTime && p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
               
        //        return tempData;
        //    }
        //}
        //实时数据
        public List<SUC_UavSortieData> GetUavWorkData()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var tempData = _DataEntities.SUC_UavSortieData.Where(p => p.UavState=="1"&&p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();

                return tempData;
            }
        }
        public List<SUC_UavSortieData> GetUavListById()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var Data = _DataEntities.SUC_UavSortieData.Where(p => p.Disabled == 0).OrderBy(u => u.UavSortieId).ToList();
                return Data;
            }
        }

        public SUC_UavSortieData GetUavSortieByNUMandS(string UavSerialNO, string sortie)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var Data = _DataEntities.SUC_UavSortieData.Where(p => p.UavSerialNO == UavSerialNO&&p.Sortie == sortie && p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).FirstOrDefault();
                return Data;
            }
        }

        public List<SUC_UavSortieData> GetUavListByNUMandS(string UavSerialNO, string sortie)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var Data = _DataEntities.SUC_UavSortieData.Where(p => p.UavSerialNO == UavSerialNO && p.Sortie == sortie && p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                return Data;
            }
        }

        public SUC_UavSortieData GetData()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                SUC_UavSortieData model = _DataEntities.SUC_UavSortieData.Where(p => p.Disabled == 0).OrderByDescending(n => n.Rec_CreateTime).FirstOrDefault();
                return model;
            }
        }

        public SUC_UavSortieData GetInfoById(Guid id)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavSortieData.Where(p => p.UavSortieId == id && p.Disabled == 0).FirstOrDefault();
            }
        }


        public SUC_UavSortieData GetUavInfoByNo(string no)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavSortieData.Where(p => p.Disabled == 0 && p.UavSerialNO == no).OrderByDescending(p => p.Rec_CreateTime).FirstOrDefault();
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="standard"></param>
        /// <returns>bool</returns>
        public bool Add(SUC_UavSortieData uavinfo)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                _DataEntities.SUC_UavSortieData.Add(uavinfo);
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Update(SUC_UavSortieData model)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                SUC_UavSortieData uavsortiedata = _DataEntities.SUC_UavSortieData.FirstOrDefault(p => p.UavSortieId == model.UavSortieId && p.Disabled == 0);
                if (uavsortiedata != null)
                {
                    uavsortiedata.UavSerialNO = model.UavSerialNO;
                    uavsortiedata.Sortie = model.Sortie;
                    uavsortiedata.VideoAddr = model.VideoAddr;
                    uavsortiedata.UavState = model.UavState;
                    uavsortiedata.HistoryAddr = model.HistoryAddr;
                    uavsortiedata.InitialOperTime = model.InitialOperTime;
                    uavsortiedata.OperationEndTime = model.OperationEndTime;
                    uavsortiedata.OperateAddr = model.OperateAddr;
                    uavsortiedata.WorkContents = model.WorkContents;
                    uavsortiedata.Remark = model.Remark;
                    uavsortiedata.Rec_CreateTime = model.Rec_CreateTime;
                    uavsortiedata.Rec_CreateBy = model.Rec_CreateBy;
                    uavsortiedata.Rec_ModifyTime = model.Rec_ModifyTime;
                    uavsortiedata.Rec_ModifyBy = model.Rec_ModifyBy;
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
                    List<SUC_UavSortieData> list = _DataEntities.SUC_UavSortieData.Where(p => p.Disabled == 0).AsEnumerable().Where(p => strs.Contains(p.UavSortieId.ToString())).ToList();
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

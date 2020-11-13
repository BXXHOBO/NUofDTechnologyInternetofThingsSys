using SUC_DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace SUC_EntityBLL
{
    public class BLL_UavData
    {
       
        public List<SUC_UavData> GetList(Pagination pagination, string keyword = "")
        {
            //搜索数据筛选
            using (var _DataEntities = new SUC_SYSContainer())
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    //var Data = _DataEntities.SUC_UavInfo.Where(t => t.Sortie.Contains(keyword)||t.Rec_CreateBy.Contains(keyword)).ToList();
                    var Data = _DataEntities.SUC_UavData.Where(t => t.UavSerialNO == keyword || t.Rec_CreateBy == keyword).ToList();
                    return Data;
                }
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                var tempData = _DataEntities.SUC_UavData.Where(p => p.Disabled == 0).OrderBy(u => u.Rec_CreateTime).ToList();
                bool isAsc = pagination.sord.ToLower() == "asc" ? true : false;
                string[] _order = pagination.sidx.Split(',');
                pagination.records = tempData.Count();
                tempData = tempData.Skip(pagination.rows * (pagination.page - 1)).Take(pagination.rows).ToList();
                return tempData;
            }
        }

        public List<SUC_UavData> GetUavListById(string uavdataId)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavData.Where(p => p.Disabled == 0).OrderBy(u => u.UavDataId).ToList();
                return Data;
            }
        }
        public List<SUC_UavData> GetUavListByUavWork()
        {
            string uavwork = "0";
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavData.Where(p => p.WorkState==uavwork&&p.Disabled == 0).OrderBy(u => u.UavDataId).ToList();
                return Data;
            }
        }


        public List<SUC_UavData> GetUavListByWorkState()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                var Data = _DataEntities.SUC_UavData.Where(p => p.Disabled == 0 && p.WorkState=="0").OrderBy(u => u.UavDataId).ToList();
                return Data;
            }
        }
        //根据序列号来获取某行数据
        public SUC_UavData GetInfoByuavSerialNO(string uavSerialNO)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavData.Where(p => p.UavSerialNO == uavSerialNO && p.Disabled == 0).FirstOrDefault();
            }
        }

        public SUC_UavData GetData()
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {

                SUC_UavData model = _DataEntities.SUC_UavData.OrderByDescending(n => n.UavDataId).FirstOrDefault();
                return model;
            }
        }

        public SUC_UavData GetInfoById(Guid id)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavData.Where(p => p.UavDataId == id && p.Disabled == 0).FirstOrDefault();
            }
        }


        public SUC_UavData GetUavInfoByNo(string no)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                return _DataEntities.SUC_UavData.Where(p => p.Disabled == 0 && p.UavSerialNO == no).OrderByDescending(p => p.Rec_CreateTime).FirstOrDefault();
            }
        }


        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="standard"></param>
        /// <returns>bool</returns>
        public bool Add(SUC_UavData uavinfo)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                _DataEntities.SUC_UavData.Add(uavinfo);
                if (_DataEntities.SaveChanges() > 0)
                {
                    return true;
                }
                return false;
            }
        }
        public bool Update(SUC_UavData model)
        {
            using (var _DataEntities = new SUC_SYSContainer())
            {
                _DataEntities.Configuration.ProxyCreationEnabled = false;
                SUC_UavData uavdata = _DataEntities.SUC_UavData.FirstOrDefault(p => p.UavDataId == model.UavDataId && p.Disabled == 0);
                if (uavdata != null)
                {
                    uavdata.UavSerialNO = model.UavSerialNO;
                    uavdata.WorkState = model.WorkState;
                    uavdata.ImgPath = model.ImgPath;
                    uavdata.ProductDate = model.ProductDate;             
                    uavdata.Remark = model.Remark;
                    uavdata.Rec_CreateTime = model.Rec_CreateTime;
                    uavdata.Rec_CreateBy = model.Rec_CreateBy;
                    uavdata.Rec_ModifyTime = model.Rec_ModifyTime;
                    uavdata.Rec_ModifyBy = model.Rec_ModifyBy;
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
                    List<SUC_UavData> list = _DataEntities.SUC_UavData.Where(p => p.Disabled == 0).AsEnumerable().Where(p => strs.Contains(p.UavDataId.ToString())).ToList();
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

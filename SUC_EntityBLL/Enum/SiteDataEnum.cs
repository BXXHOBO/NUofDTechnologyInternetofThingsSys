using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SUC_EntityBLL.Enum
{
    public enum SiteDataEnum
    {
        站点编号 = 0,
        //ICell tempCell = tempRow.CreateCell(0);
        //tempCell.SetCellValue(tempData.SiteCode);
        设备编号 = 1,
        //ICell tempCell1 = tempRow.CreateCell(1);
        //tempCell1.SetCellValue(tempData.DeviceCode);
        发布时间 = 2,
        //ICell tempCel2 = tempRow.CreateCell(2);
        //tempCel2.SetCellValue(tempData.ReleaseTime);
        AQI = 3,
        //ICell tempCell3 = tempRow.CreateCell(3);
        //tempCell3.SetCellValue(Convert.ToDouble(tempData.AQI));
        空气质量等级 = 4,
        //ICell tempCell4 = tempRow.CreateCell(4);
        //tempCell4.SetCellValue(tempData.AQIGrade);
        PM25 = 5,
        //ICell tempCell5 = tempRow.CreateCell(5);
        //tempCell5.SetCellValue(Convert.ToDouble(tempData.PM_25));
        PM25_AQI = 6,
        //ICell tempCell6 = tempRow.CreateCell(6);
        //tempCell6.SetCellValue(Convert.ToDouble(tempData.PM_25_AQI));
        PM10 = 7,
        //ICell tempCell7 = tempRow.CreateCell(7);
        //tempCell7.SetCellValue(Convert.ToDouble(tempData.PM10));
        PM10_AQI = 8,
        //ICell tempCell8 = tempRow.CreateCell(8);
        //tempCell8.SetCellValue(Convert.ToDouble(tempData.PM10_AQI));
        SO2 = 9,
        //ICell tempCell9 = tempRow.CreateCell(9);
        //tempCell9.SetCellValue(Convert.ToDouble(tempData.SO2));
        SO2_AQI = 10,
        //ICell tempCell10 = tempRow.CreateCell(10);
        //tempCell10.SetCellValue(Convert.ToDouble(tempData.SO2_AQI));
        NO2 = 11,
        //ICell tempCell11 = tempRow.CreateCell(11);
        //tempCell11.SetCellValue(Convert.ToDouble(tempData.NO2));
        NO2_AQI = 12,
        //ICell tempCell12 = tempRow.CreateCell(12);
        //tempCell12.SetCellValue(Convert.ToDouble(tempData.NO2_AQI));
        O3 = 13,
        //ICell tempCell13 = tempRow.CreateCell(13);
        //tempCell13.SetCellValue(Convert.ToDouble(tempData.O3));
        O3_AQI = 14,
        //ICell tempCell14 = tempRow.CreateCell(14);
        //tempCell14.SetCellValue(Convert.ToDouble(tempData.O3_AQI));
        CO = 15,
        //ICell tempCell15 = tempRow.CreateCell(15);
        //tempCell15.SetCellValue(Convert.ToDouble(tempData.CO));
        CO_AQI = 16,
        //ICell tempCell16 = tempRow.CreateCell(16);
        //tempCell16.SetCellValue(Convert.ToDouble(tempData.CO_AQI));
        主要污染物 = 17,
        //ICell tempCell17 = tempRow.CreateCell(17);
        //tempCell17.SetCellValue(tempData.MainPollutant);
        TVOC = 18,
        //ICell tempCell18 = tempRow.CreateCell(18);
        //tempCell18.SetCellValue(Convert.ToDouble(tempData.TVOC));
        温度 = 19,
        //ICell tempCell19 = tempRow.CreateCell(19);
        //tempCell19.SetCellValue(Convert.ToDouble(tempData.Temperature));
        湿度 = 20,
        //ICell tempCell20 = tempRow.CreateCell(20);
        //tempCell20.SetCellValue(Convert.ToDouble(tempData.Humidity));
        压力 = 21,
        //ICell tempCell21 = tempRow.CreateCell(21);
        //tempCell21.SetCellValue(Convert.ToDouble(tempData.Press));
        风速 = 22,
        //ICell tempCell22 = tempRow.CreateCell(22);
        //tempCell22.SetCellValue(Convert.ToDouble(tempData.WindSpeed));
        风向 = 23,
        //ICell tempCell23 = tempRow.CreateCell(23);
        //tempCell23.SetCellValue(tempData.WindDirection);
        备注 = 24,
        //ICell tempCell24 = tempRow.CreateCell(24);
        //tempCell24.SetCellValue(tempData.Remark);
        创建时间 = 25,
        //ICell tempCel25 = tempRow.CreateCell(25);
        //tempCel25.SetCellValue(tempData.CreateTime);
        创建人 = 26
        //ICell tempCel26 = tempRow.CreateCell(26);
        //tempCel26.SetCellValue(tempData.UserName);
    }
}

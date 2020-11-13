<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index1.aspx.cs" Inherits="Index1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/echarts.min.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
  <div class="topPanel">
                <div class="search">
                    <table>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <span>
                                        <input id="sortie" type="text" class="form-control" placeholder="架次" style="width: 200px;height:30px;"/>
                                        <button id="btn_search" type="button" class="btn  btn-primary" style="font-size:22px" onclick="getajaxdata();">查询</button>
                                    </span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div></div>
            <div id="main" style="width: 600px;height:400px;float :left ;margin-right:80px;"></div>
            
    </div>
    </form>
    <script src="js/jquery-1.11.0.js"></script>
    <script>
        var myChart = echarts.init(document.getElementById('main'));
        var option = {
            title: {
                text: '温湿度数据报表'
            },
            tooltip: {
                trigger: 'axis'
            },
            legend: {
                data: ['温度（℃）', '湿度（%）']
            },
            grid: {
                left: '3%',
                right: '4%',
                bottom: '3%',
                containLabel: true
            },
            toolbox: {
                feature: {
                    saveAsImage: {}
                }
            },
            xAxis: {
                type: 'category',
                boundaryGap: false,
                data: []
            },
            yAxis: {
                type: 'value'
            },
            series: [
                {
                    name: '温度（℃）',
                    type: 'line',
                    stack: '总量',
                    data: []
                },
                {
                    name: '湿度（%）',
                    type: 'line',
                    stack: '总量',
                    data: []
                }
               
            ]
        };

        myChart.setOption(option);
       
        function getajaxdata() {
            var sortie = $("#sortie").val();
            $.ajax({
                type: "post",
                url: "Ajax.ashx",
                data: { w: "GetTempandHumidityBySortie", sortie: sortie },
                timeout: 5000,
                dataType: "json",
                async: true,//默认设置为true，所有请求均为异步请求
                //cache：true,//默认为true（当dataType为script时，默认为false）设置为false将不会从浏览器缓存中加载请求信息。
                success: function (data) {

                    var optionhasvalue = {
                        xAxis: {
                            type: 'category',
                            data: data.datatime
                        },
                        yAxis: {
                            type: 'value'
                        },
                        series: [{
                            data: data.dataitems1,
                            type: 'line'

                        },
                        {
                            data: data.dataitems2,
                            type: 'line'

                        },
                        ],
                       
                    };
                    myChart.setOption(optionhasvalue);
                }
            });

        }
      

       

      
    </script>
</body>
</html>

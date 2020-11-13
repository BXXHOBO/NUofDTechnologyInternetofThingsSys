<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Index"  %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" lang="zh-CN">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

    <script src="js/echarts.min.js"></script>
 
</head>
<body>
   
    <form id="form1" runat="server">
    

       

           <div class="topPanel">
                <div class="search">
                    <table>
                        <tr>
                            <td>
                                <div class="input-group">
                                    <span>
                                        <input id="sortie" type="text" class="form-control" placeholder="架次" style="width: 200px;height:30px;"/>
                                        <button id="btn_search" type="button" class="btn  btn-primary" style="font-size:24px" onclick="getajaxdata();">查询</button>
                                    </span>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div></div>
            <div id="main" style="width: 600px;height:400px;float :left ;margin-right:50px;"></div>
            <div id="main1" style="width: 600px;height:400px;float:left;"></div> 

    </form>
    <script src="js/jquery-1.11.0.js"></script>
    <script type ="text/javascript">

     


        var myChart = echarts.init(document.getElementById('main'));
        var option = {
            title: {
                text: '温度（℃）'
            },
            xAxis: {
                type: 'category',
                data: []
            },
            yAxis: {
                type: 'value'
            },
            series: [{
                data: [],
                type: 'line'
            }]
        };
        myChart.setOption(option);
        var myChart1 = echarts.init(document.getElementById('main1'));
        var option1 = {
            title: {
                text: '湿度（%）'
            },
            xAxis: {
                type: 'category',
                data: []
            },
            yAxis: {
                type: 'value'
            },
            series: [{
                data: [],
                type: 'line'
            }]
        };
        myChart1.setOption(option1);
        function getajaxdata() {
            var sortie = $("#sortie").val();
            $.ajax({
                type: "post",
                url: "Ajax.ashx",
                data: { w: "GetTempBySortie", sortie: sortie },
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
                            data: data.dataitems,
                            type: 'line'

                        }]
                    };
                    myChart.setOption(optionhasvalue);
                }
            });

            $.ajax({
                type: "post",
                url: "Ajax.ashx",
                data: { w: "GetHumidityBySortie", sortie: sortie },
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

                        }]
                    };
                    myChart1.setOption(optionhasvalue);
                }
            });

        }
      

       

      
    </script>
</body>
</html>

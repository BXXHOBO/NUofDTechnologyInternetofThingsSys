<%@ Page Language="C#" AutoEventWireup="true" CodeFile="barIndex.aspx.cs" Inherits="barIndex" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/echarts.min.js"></script>
    <style >
    .main{
    text-align: center; /*让div内部文字居中*/
    background-color: #fff;
    border-radius: 20px;
    width: 800px;
    height: 600px;
    margin: auto;
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    }
</style>
</head>
<body>
     
    <form id="form1" runat="server">
   
  <div class="topPanel">     
          
            <div id="main" class="main"></div>
            
    </div>
    </form>
    
    <script src="js/jquery-1.11.0.js"></script>
    <script>
        var myChart = echarts.init(document.getElementById('main'));
        var option = {
            title: {
                text: '温湿度数据报表'
            },
            tooltip: {},
            legend: {
                data: ['温度', '湿度'],
                x: '338px',
                y: "30px"
            },
            xAxis: {
                data: []
            },
            yAxis: {},
            series: [{
                name: '温度',
                type: 'bar',
                data: []
            }, {
                name: '湿度',
                type: 'bar',
                data: []
            }]
        };

        myChart.setOption(option);
       
        function getajaxdata() {
           
            $.ajax({
                type: "post",
                url: "Ajax.ashx",
                data: { w: "GetTandHavgInfo" },
                timeout: 5000,
                dataType: "json",
                async: true,//默认设置为true，所有请求均为异步请求
                //cache：true,//默认为true（当dataType为script时，默认为false）设置为false将不会从浏览器缓存中加载请求信息。
                success: function (data) {
                    console.log(data.datatime);
                    console.log(data.dataitems1);
                    console.log(data.dataitems2);
                    var optionhasvalue = {
                        title: {
                            text: '温湿度数据报表',
                        },
                        tooltip: {},
                        legend: {
                            data: ['温度', '湿度'],

                        },
                        xAxis: {
                            data: data.datatime
                        },
                        yAxis: {},
                        series: [{
                            name: '温度',
                            type: 'bar',
                            data: data.dataitems1
                        },
                        {
                            name: '湿度',
                            type: 'bar',
                            data: data.dataitems2
                        }
                        ]
                    };
                    myChart.setOption(optionhasvalue);
                }
            });

        }
      
        getajaxdata();
       

      
    </script>
</body>
</html>

$(function () {
    $(document).ready(function () {
        var produceflowCharts = {
            chart: {
                type: 'column',
                
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: ['0','1000', '2000', '3000', '4000', '5000', '6000', '7000', '8000', '9000', '10000', '12000', '15000', '20000'],
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">大于{point.key}小于下一个刻度</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">概率: </td>' + '<td style="padding:0"><b>{point.y:.1f} %</b></td></tr>',
                footerFormat: '</table>',
                shared: true,
                useHTML: true
            },
            plotOptions: {
                column: {
                    pointPadding: 0.2,
                    borderWidth: 0
                }
            },
            series: [{
                name: '概率',
                data: [2,1, 8, 7,6, 1, 8, 16, 22, 53, 50, 48, 56,0 ]
            }]
        };
        function getFrequency() {
            var xarr = produceflowCharts.xAxis.categories;
            var xarrJson = JSON.stringify(xarr);
            
            $.ajax({
                url: "/EfficiencyAnalysis/ProduceFlow",
                type: "GET",
                data: { strJson: xarrJson,time:startDate,aftertime:endDate },
                success: function (data) {


                    var jsondata = JSON.parse(data);
                    var arr = [];
                    $.each(jsondata, function (index, item) {

                        arr[index] = item;
                    })

                    produceflowCharts.series[0].data = arr;

                }

            });
            $('#produceflow').highcharts(produceflowCharts);
        }
        
       
        if (startDate != null && endDate != null) {
            getFrequency();
        }
        else {
             getFrequency();
             setInterval(getFrequency, 3000);
        }
           
        
        
        
    });
});
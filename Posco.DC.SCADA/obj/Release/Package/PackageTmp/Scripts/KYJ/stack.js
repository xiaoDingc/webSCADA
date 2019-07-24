$(function () {
    $(document).ready(function () {
        //能效图
        var stackCharts = {
            chart: {
                type: 'bar'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                type: 'category',

                //labels: {
                //    rotation: -45,
                //    style: {
                //        fontSize: '5px',
                //        fontFamily: 'Verdana, sans-serif'
                //    }
                //}
            },
            yAxis: {
                min: 0,
                title: {
                    text: '能效UPI(KWh/m³)'
                },
                tickInterval: 0.05
            },
            legend: {
                enabled: false
            },
            tooltip: {
                valueSuffix: 'KWh/m³'
            },
            plotOptions: {
                bar: {
                    dataLabels: {
                        enabled: true,
                        allowOverlap: true // 允许数据标签重叠
                    }
                }
            },
            series: [{
                name: 'UPI',
                data: cstacarr,
                //dataLabels: {
                //    enabled: true,
                //    rotation: -90,
                //    color: '#FFFFFF',
                //    align: 'right',
                //    format: '{point.y:.4f}', // one decimal
                //    y: 10, // 10 pixels down from the top
                //    style: {
                //        fontSize: '13px',
                //        fontFamily: 'Verdana, sans-serif'
                //    }
                //}
            }]
        };
        $("#stack").highcharts(stackCharts);
        function getStack() {
           
            $.ajax({
                url: "/EnergySys/Stack",
                type:"GET",
                
                success: function (data) {
                  
                    var stackarr = [];
                    
                    $.each(data, function (index,item) {
                        stackarr[index] = [item.Str, item.Number];
                    })
                   
                    stackCharts.series[0].data = stackarr;
                }
            })
            $("#stack").highcharts(stackCharts)
        }
        setInterval(getStack, 300*1000);
    });
});
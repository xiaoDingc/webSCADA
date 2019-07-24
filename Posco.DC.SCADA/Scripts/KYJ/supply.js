$(function () {
    $(document).ready(function () {
    
   var supplyCharts= {
        chart: {
            type: 'spline'
        },
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
           type:'datetime'
        },
        yAxis: {
            title: {
                text: '气量，Km³/h'
            },
            labels: {
                formatter: function() {
                    return this.value + '';
                }
            }
        },
        tooltip: {
            crosshairs: true,
            shared: true
        },
        plotOptions: {
            spline: {
                marker: {
                    radius: 4,
                    lineColor: '#666666',
                    lineWidth: 1
                }
            }
        },
        series: [{
            name: '供应能力',
            marker: {
                symbol: 'square'
            },
            data:cinarr
        }, {
            name: '需求气量',
            marker: {
                symbol: 'diamond'
            },
            data: coutarr
        }]
   };
   $("#supply").highcharts(supplyCharts);
   function getSupply() {
       var xarr=supplyCharts.xAxis.categories;
       var jsonx=JSON.stringify(xarr);
       $.ajax({
           url: "/EnergySys/Supply",
           type: "GET",
           data:{strJson:jsonx},
           success: function (data) {
               var inarr = [];
               var outarr = [];
               $.each(data, function (index, item) {
                   inarr[index] = [item.time, item.main];
                   outarr[index] = [item.time, item.usef];
               })

               supplyCharts.series[0].data = inarr;
               supplyCharts.series[1].data = outarr;
           }
       });
       $("#supply").highcharts(supplyCharts)
   };
   setInterval(getSupply, 300*1000);
    });
});
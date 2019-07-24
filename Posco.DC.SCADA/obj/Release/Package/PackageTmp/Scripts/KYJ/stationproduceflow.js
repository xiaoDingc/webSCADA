$(function () {
    $(document).ready(function () {
        var stationproduceflowCharts = {
            chart: {
                type: 'column'
            },
            title: {
                text: ''
            },
            subtitle: {
                text: ''
            },
            xAxis: {
                categories: ['0', '5', '5.5', '6', '6.5', '7', '7.5', '8', '8.5', '9', '9.5', '10', '10.5', '11', '11.5', '12', '12.5', '13', '13.5', '14', '15'],
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: ''
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">放散率: </td>' + '<td style="padding:0"><b>{point.y:.1f} kw</b></td></tr>',
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
            series: [
                {
                name: '1#放散率',
                data: [0, 0, 0, 0, 1, 8, 16, 22, 53, 50, 48, 56, 25, 9, 1, 0, 0, 0, 0, 0, ]
                },
                {
                    name: '2#放散率',
                    data: [0, 0, 0, 0, 1, 8, 16, 22, 53, 50, 48, 56, 25, 9, 1, 0, 0, 0, 0, 0, ]
                }, {
                    name: '3#放散率',
                    data: [0, 0, 0, 0, 1, 8, 16, 22, 53, 50, 48, 56, 25, 9, 1, 0, 0, 0, 0, 0, ]
                }, {
                    name: '4#放散率',
                    data: [0, 0, 0, 0, 1, 8, 16, 22, 53, 50, 48, 56, 25, 9, 1, 0, 0, 0, 0, 0, ]
                }
            ]
        };
        function getFrequency() {
            //var xarr = frequencyCharts.xAxis.categories;
            //var xarrJson = JSON.stringify(xarr);
            //$.ajax({
            //    url: "/EnergySys/Frequency",
            //    type: "GET",
            //    data: { strJson: xarrJson },
            //    success: function (data) {


            //        var jsondata = JSON.parse(data);
            //        var arr = [];
            //        $.each(jsondata, function (index, item) {

            //            arr[index] = item;
            //        })

            //        frequencyCharts.series[0].data = arr;

            //    }

            //});
            $('#stationproduceflow').highcharts(stationproduceflowCharts);
        }
        getFrequency();
        setInterval(getFrequency, 8000);

    });
});
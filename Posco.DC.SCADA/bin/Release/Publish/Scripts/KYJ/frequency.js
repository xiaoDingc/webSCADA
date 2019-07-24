$(function () {
    $(document).ready(function () {
        var frequencyCharts = {
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
                categories: ['0','5', '5.5', '6', '6.5', '7', '7.5', '8', '8.5', '9', '9.5', '10', '10.5', '11', '11.5', '12', '12.5', '13', '13.5', '14', '15'],
                crosshair: true
            },
            yAxis: {
                min: 0,
                title: {
                    text: '次数(/次)'
                }
            },
            tooltip: {
                headerFormat: '<span style="font-size:10px">{point.key}</span><table>',
                pointFormat: '<tr><td style="color:{series.color};padding:0">次数: </td>' + '<td style="padding:0"><b>{point.y:.0f}</b></td></tr>',
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
                name: '放散率(%)',
                data: str.freq
            }]
        };
        $('#frequency').highcharts(frequencyCharts);
        function getFrequency() {
            var xarr = frequencyCharts.xAxis.categories;
             var xarrJson=JSON.stringify(xarr);
            $.ajax({
                url: "/EnergySys/Frequency",
                type: "GET",
                data:{strJson:xarrJson},
                success: function (data) {
                    frequencyCharts.series[0].data = data;
                }
            });
            $('#frequency').highcharts(frequencyCharts);
        }
        setInterval(getFrequency, 300*1000);
    });
});
$(function () {
    $(document).ready(function () {
        var spiderCharts = {
            chart: {
                polar: true,
                type: 'line'
            },
            title: {
                text: '',
                x: -80
            },
            pane: {
                size: '80%'
            },
            xAxis: {
                categories: ['BOR', 'DRE', 'ERE', 'IFLP', 'PCE', 'OFLP'],
                tickmarkPlacement: 'on',
                lineWidth: 0
            },
            yAxis: {
                gridLineInterpolation: 'polygon',
                lineWidth: 0,
                min: 0
            },
            tooltip: {
                shared: true,
                pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
            },
            legend: {},
            series: [{
                name: '1#空压站',
                data: str.spid[0],
                pointPlacement: 'on'
            }]
        };
        var spider1Charts = {
            chart: {
                polar: true,
                type: 'line'
            },
            title: {
                text: '',
                x: -80
            },
            pane: {
                size: '80%'
            },
            xAxis: {
                categories: ['BOR', 'DRE', 'ERE', 'IFLP', 'PCE', 'OFLP'],
                tickmarkPlacement: 'on',
                lineWidth: 0
            },
            yAxis: {
                gridLineInterpolation: 'polygon',
                lineWidth: 0,
                min: 0
            },
            tooltip: {
                shared: true,
                pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
            },
            legend: {},
            series: [{
                name: '2#空压站',
                data: str.spid[1],
                pointPlacement: 'on'
            }]
        };
       var spider2Charts={
            chart: {
                polar: true,
                type: 'line'
            },
            title: {
                text: '',
                x: -80
            },
            pane: {
                size: '80%'
            },
            xAxis: {
                categories: ['BOR', 'DRE', 'ERE', 'IFLP', 'PCE', 'OFLP'],
                tickmarkPlacement: 'on',
                lineWidth: 0
            },
            yAxis: {
                gridLineInterpolation: 'polygon',
                lineWidth: 0,
                min: 0
            },
            tooltip: {
                shared: true,
                pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
            },
            legend: {},
            series: [{
                name: '3#空压站',
                data: str.spid[2],
                pointPlacement: 'on'
            }]
        };
        var spider3Charts = {
            chart: {
                polar: true,
                type: 'line'
            },
            title: {
                text: '',
                x: -80
            },
            pane: {
                size: '80%'
            },
            xAxis: {
                categories: ['BOR', 'DRE', 'ERE', 'IFLP', 'PCE', 'OFLP'],
                tickmarkPlacement: 'on',
                lineWidth: 0
            },
            yAxis: {
                gridLineInterpolation: 'polygon',
                lineWidth: 0,
                min: 0
            },
            tooltip: {
                shared: true,
                pointFormat: '<span style="color:{series.color}">{series.name}: <b>{point.y:,.0f}</b><br/>'
            },
            legend: {},
            series: [{
                name: '4#空压站',
                data: str.spid[3],
                pointPlacement: 'on'
            }]
        };
        $("#spider").highcharts(spiderCharts);
        $("#spider1").highcharts(spider1Charts);
        $("#spider2").highcharts(spider2Charts);
        $("#spider3").highcharts(spider3Charts);
        function getSpider() {
            $.ajax({
                url: "/EnergySys/Spider",
                type: "GET",
                success: function (data) {
                    spiderCharts.series[0].data = data[0];
                    spider1Charts.series[0].data = data[1];
                    spider2Charts.series[0].data = data[2];
                    spider3Charts.series[0].data = data[3];
                }
            })
            $("#spider").highcharts(spiderCharts);
            $("#spider1").highcharts(spider1Charts);
            $("#spider2").highcharts(spider2Charts);
            $("#spider3").highcharts(spider3Charts);
        }
        setInterval(getSpider, 300*1000);
    });
});
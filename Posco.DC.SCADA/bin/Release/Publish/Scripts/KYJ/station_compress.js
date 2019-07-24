$(function () {

    var stationcompressorCharts1 = {
        chart: {
            type: 'scatter',
            zoomType: 'xy'
        },
        title: {
            text: ''
        },
        subtitle: {
            text: ''
        },
        xAxis: {
            title: {
                enabled: true,
                text: ''
            },
            startOnTick: true,
            endOnTick: true,
            showLastLabel: true
        },
        yAxis: {
            title: {
                text: 'UPI,KWh/m³'
            }
        },
        legend: {
        },
        plotOptions: {
            scatter: {
                marker: {
                    radius: 5,
                    states: {
                        hover: {
                            enabled: true,
                            lineColor: 'rgb(100,100,100)'
                        }
                    }
                },
                states: {
                    hover: {
                        marker: {
                            enabled: false
                        }
                    }
                },
                tooltip: {
                    headerFormat: '<b>{series.name}</b><br>',
                    pointFormat: '{point.x}kw/min'
                }
            }
        },
        series: [{
            name: '放散率（%）',
            color: 'rgba(223, 83, 83, .5)',//8-13,0.12-.013
            data: [[7.8, 0.13], [7.89, 0.121], [8.1, 0.126], [9.1, 0.135], [9.6, 0.130],
            [8.7, 0.128], [9.4, 0.130], [8.12, 0.124], [7.66, 0.128], [7.89, 0.132],
            [9.7, 0.132], [10.4, 0.126], [6.12, 0.152], [7.14, 0.156], [8.26, 0.115],
            [9.26, 0.125], [8.66, 0.128], [8.76, 0.124], [9.56, 0.114], [12.6, 0.128]
            [9.16, 0.124], [7.66, 0.118], [6.76, 0.184], [8.6, 0.126], [9.45, 0.120]
           [9.44, 0.123], [8.78, 0.131], [9.77, 0.142], [8.88, 0.168], [11.5, 0.121]
            ]

        }, ]
    };

    $("#stationcompressor").highcharts(stationcompressorCharts1);

});
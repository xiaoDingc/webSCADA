$(function() {
    var gaugeOptions = {
        chart: {
            type: 'solidgauge'
        },
        title: null,
        pane: {
            center: ['50%', '95%'],
            size: '190%',
            startAngle: -90,
            endAngle: 90,
            background: {
                backgroundColor: (Highcharts.theme && Highcharts.theme.background2) || 'none',
                innerRadius: '100%',
                outerRadius: '60%',
                shape: 'arc'
            }
        },
        tooltip: {
            enabled: false
        },
        // the value axis
        yAxis: {
            stops: [
                [0.1, '#669933'], // green
                [0.5, '#FFCC66'], // yellow
                [0.9, '#993333'] // red
            ],
            lineWidth: 0,
            minorTickInterval: null,
            tickPixelInterval: 400,
            tickWidth: 0,
            title: {
                y: -70
            },
            labels: {
                y: 16
            }
        },
        plotOptions: {
            solidgauge: {
                dataLabels: {
                    y: 5,
                    borderWidth: 0,
                    useHTML: true
                }
            }
        }
    };
    // 实时功率
    $('#container-a').highcharts(Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 40000,
            title: {
                text: ''
            }
        },
        credits: {
            enabled: false
        },
        series: [{
            name: '',
            data: [22400],
            dataLabels: {
                format: '<div style="text-align:center"><span style="font-size:18px;color:' + ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'white') + '">{y}</span><br/>' + '<span style="font-size:12px;color:silver">kw</span></div>'
            },
            tooltip: {
                valueSuffix: 'kw'
            }
        }]
    }));
    // 瞬时流量
    $('#container-b').highcharts(Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 5000,
            title: {
                text: ''
            }
        },
        series: [{
            name: '',
            data: [3275],
            dataLabels: {
                format: '<div style="text-align:center"><span style="font-size:18px;color:' + ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'white') + '">{y:.1f}</span><br/>' + '<span style="font-size:12px;color:silver">m³/min</span></div>'
            },
            tooltip: {
                valueSuffix: ''
            }
        }]
    }));
    // 本月累计流量
    $('#container-c').highcharts(Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 2500,
            title: {
                text: 'c'
            }
        },
        series: [{
            name: 'c',
            data: [500],
            dataLabels: {
                format: '<div style="text-align:center"><span style="font-size:18px;color:' + ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'white') + '">{y:.1f}</span><br/>' + '<span style="font-size:12px;color:silver">m³</span></div>'
            },
            tooltip: {
                valueSuffix: ''
            }
        }]
    }));
    // 单位能耗
    $('#container-d').highcharts(Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 0.5,
            title: {
                text: 'b'
            }
        },
        series: [{
            name: 'b',
            data: [0.125],
            dataLabels: {
                format: '<div style="text-align:center"><span style="font-size:18px;color:' + ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'white') + '">{y:.1f}</span><br/>' + '<span style="font-size:12px;color:silver">kwh/m³</span></div>'
            },
            tooltip: {
                valueSuffix: ''
            }
        }]
    }));
    // 管网压力
    $('#container-e').highcharts(Highcharts.merge(gaugeOptions, {
        yAxis: {
            min: 0,
            max: 10,
            title: {
                text: 'c'
            }
        },
        series: [{
            name: 'c',
            data: [6.3],
            dataLabels: {
                format: '<div style="text-align:center;margin-top:20px;"><span style="font-size:18px;color:' + ((Highcharts.theme && Highcharts.theme.contrastTextColor) || 'white') + '">{y:.1f}</span><br/>' + '<span style="font-size:12px;color:silver">bar</span></div>'
            },
            tooltip: {
                valueSuffix: ''
            }
        }]
    }));
    // 
    setInterval(function() {
        // 实时功率
        var chart = $('#container-a').highcharts(),
            point,
            newVal,
            inc;
        if (chart) {
            point = chart.series[0].points[0];
            inc = Math.round((Math.random() - 0.5) * 100);
            newVal = point.y + inc;
            if (newVal < 0 || newVal > 20) {
                newVal = point.y - inc;
            }
            point.update(newVal);
        }
        // 瞬时流量
        chart = $('#container-b').highcharts();
        if (chart) {
            point = chart.series[0].points[0];
            inc = Math.random() - 0.5;
            newVal = point.y + inc;
            if (newVal < 0 || newVal > 0.5) {
                newVal = point.y - inc;
            }
            point.update(newVal);
        }
        // 本月累计流量
        chart = $('#container-c').highcharts();
        if (chart) {
            point = chart.series[0].points[0];
            inc = Math.random() - 0.5;
            newVal = point.y + inc;
            if (newVal < 0 || newVal > 5) {
                newVal = point.y - inc;
            }
            point.update(newVal);
        }
        // 单位能耗
        chart = $('#container-d').highcharts();
        if (chart) {
            point = chart.series[0].points[0];
            inc = Math.random() - 0.5;
            newVal = point.y + inc;
            if (newVal < 0 || newVal > 5) {
                newVal = point.y - inc;
            }
            point.update(newVal);
        }
        // 管网压力
        chart = $('#container-e').highcharts();
        if (chart) {
            point = chart.series[0].points[0];
            inc = Math.random() - 0.5;
            newVal = point.y + inc;
            if (newVal < 0 || newVal > 5) {
                newVal = point.y - inc;
            }
            point.update(newVal);
        }
    }, 2000);
});
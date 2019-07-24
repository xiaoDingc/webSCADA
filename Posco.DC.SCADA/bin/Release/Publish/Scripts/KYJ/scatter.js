$(function () {
    $(document).ready(function () {
        var scatterCharts = {
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
                    text: '放散率(%)'
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
                name: 'UPI(KWh/m³)',
                color: 'rgba(223, 83, 83, .5)',//8-13,0.12-.013
                data: cscaarr
            }, ]
        };
        $("#scatter").highcharts(scatterCharts);
        function getScatter() {
            $.ajax({
                url: "/EnergySys/Scatter",
                type: "GET",
                success: function (Data) {

                    var scaarr = [];
                    $.each(Data, function (index, item) {
                        scaarr[index] = [item.uPI, item.lossRatio]
                    })
                    scatterCharts.series[0].data = scaarr;
                }
                });
            $("#scatter").highcharts(scatterCharts)
        };
        setInterval(getScatter, 300*1000);
    });
});


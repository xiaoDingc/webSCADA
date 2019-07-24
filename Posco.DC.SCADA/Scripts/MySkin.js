var my_skin = {
    //定义颜色
    //colors: ["#7cb5ec,#0099cc,#6699cc,#333366,#669999,#999999,#e4d354,#666666,#663366,#999999"],
    //chart: {
    //    borderColor: "#4572A7",
    //    borderRadius: 0,
    //    defaultSeriesType: "line",
    //    ignoreHiddenSeries: !0,
    //    spacing: [10, 10, 15, 10],
    //    backgroundColor: "none",
    //    plotBorderColor: "#C0C0C0",
    //    resetZoomButton: {
    //        theme: {
    //            zIndex: 20
    //        },
    //        position: {
    //            align: "right",
    //            x: -10,
    //            y: 10
    //        }
    //    }
    //},
    //title: {
    //    text: "Chart title",
    //    align: "center",
    //    margin: 5,
    //    style: {
    //        color: "#1ffafd",
    //        fontSize: "18px"
    //    }
    //},
    xAxis: {
        title: {
            style: { "color": "#FFFFFF" }
        },
        labels: {
            style: {
                color: '#FFFFFF'
            }
        },
        //gridLineColor: '#33FF00',
        //gridLineWidth: 1,
        //lineColor: '#33FF00',
        //lineWidth: 1
    },
    yAxis: {
        title: {
            style: { "color": "#FFFFFF" }
        },
        markable: { enabled: false },//不显示每一个点的实心  
        labels: {
            style: {
                color: '#FFFFFF'
            }
        },
        gridLineColor: '#00f6ff',
        gridLineWidth:1,
        //lineColor: '#33FF00',
        //lineWidth:1
    },
  
  
    //labels: {
    //    style: {
    //        position: "absolute",
    //        color: "#3E576F"
    //    }
    //},
    legend: {
        enabled: !0,
        align: "center",
        layout: "horizontal",
        labelFormatter: function() {
            return this.name
        },
        borderColor: "#909090",
        borderRadius: 0,
        navigation: {
            activeColor: "#274b6d",
            inactiveColor: "#CCC"
        },
        shadow: !1,
        itemStyle: {
            color: "#FFF",
            fontSize: "12px",
            fontWeight: "bold"
        },
        itemHoverStyle: {
            color: "#1ffafd"
        },
        itemHiddenStyle: {
            color: "#CCC"
        },
        itemCheckboxStyle: {
            position: "absolute",
            width: "13px",
            height: "13px"
        },
        symbolPadding: 5,
        verticalAlign: "bottom",
        x: 0,
        y: 0,
        title: {
            style: {
                fontWeight: "bold"
            }
        }
    },
    loading: {
        labelStyle: {
            fontWeight: "bold",
            position: "relative",
            top: "45%"
        },
        style: {
            position: "absolute",
            backgroundColor: "white",
            opacity: 0.5,
            textAlign: "center"
        }
    },
    tooltip: {
        enabled: !0,
        
        backgroundColor: "rgba(0, 0, 0, .7)", //弹出显示背景
        borderWidth: 0,
        borderRadius: 3,
        dateTimeLabelFormats: {
            millisecond: "%A, %b %e, %H:%M:%S.%L",
            second: "%A, %b %e, %H:%M:%S",
            minute: "%A, %b %e, %H:%M",
            hour: "%A, %b %e, %H:%M",
            day: "%A, %b %e, %Y",
            week: "Week from %A, %b %e, %Y",
            month: "%B %Y",
            year: "%Y"
        },
        footerFormat: "",
        headerFormat: '<span style="font-size: 14px">{point.key}</span><br/>',
        pointFormat: '<span style="color:{point.color}">\u25cf</span> {series.name}: <b>{point.y}</b><br/>',
        shadow: !0,
        style: {
            color: "#FFFFFF", //弹出显示文字颜色
            cursor: "default",
            fontSize: "14px",
            padding: "10px",
            whiteSpace: "nowrap"
        }
    },
    credits: {
        enabled: !0,
        text: "",
        href: "",
        position: {
            align: "right",
            x: -10,
            verticalAlign: "bottom",
            y: -5
        },
        style: {
            cursor: "pointer",
            color: "#909090",
            fontSize: "9px"
        }
    }
};

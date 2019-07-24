$(function () {
	$(document).ready(function () {
		// 用气比
		var dom = document.getElementById("gasuse");
		var myChart = echarts.init(dom);
		var option = null;
		option = {
			tooltip: {
				trigger: 'item',
				formatter: "{a} <br/>{b}: {c} ({d}%)"
			},
			title: {
				text: '压缩空气产气比例',
				textStyle: {
					color: "#00e4ff",
				}
			},
			series: [
				{
					name: '来源',
					type: 'pie',
					selectedMode: 'single',
					radius: [0, '50%'],
					label: {
						normal: {
							position: 'inside',
						}
					},
					labelLine: {
						normal: {
							show: true
						}
					},
					data: [{
						value: 15,
						name: '1#空压站',
						selected: true
					},
					{
						value: 20,
						name: '2#空压站'
					},
					{
						value: 30,
						name: '3#空压站'
					}, {
						value: 40,
						name: '4#空压站'
					}
					]
				},
				{
					name: '来源',
					type: 'pie',
					// radius: ['37%', '55%'],
					radius: ['60%', '90%'],
					label: {
						normal: {
							position: 'inner',
						}
					},
					data: [
						{
							value: 10.37763405,
							name: '1BF'
						},
						{
							value: 7.38,
							name: '2BF'
						},
						{
							value: 12.45,
							name: '1550冷轧'
						},
						{
							value: 6.75,
							name: '4200厚板\n加热炉'
						},
						{
							value: 21.08,
							name: '2030冷轧'
						},
						{
							value: 21.51,
							name: '2250冷轧'
						},
						{
							value: 0 + 0.15 + 0.006 + 1.04 + 0.84 + 1.93,
							name: '其他',
							//自定义特殊 tooltip，仅对该数据项有效
							// tooltip:{
							// 	 formatter: function(params) {
							// 		 
							// 	},
							// },
						},
						{
							value: 5.91,
							name: '焦炉'
						},
						{
							value: 13.89,
							name: '4200厚板\n热处理'
						},
						{
							value: 29.45,
							name: '炼钢北区'
						},
						{
							value: 4.14,
							name: '球团'
						},
						{
							value: 20.6,
							name: '烧结'
						},
						{
							value: 3.32,
							name: '原料'
						},

					]
				}
			]
		};
		function getIntervall() {
			// 产气及用气比例
			var others=0;
			$.get("/EnergySys/GasUse").done(function (res) {
				//console.log(res);
				let stations=res.stations;
				let bbareaList=res.bbareaList;

				$.each(bbareaList,(index, value)=>{							
					if (value.name.indexOf("o") != -1) {						
						others += value.value;						
						}
					value.name = value.name.replace("n", "\n");
					//console.log(index + "  value:" + value.name);		
				});
				//console.log(bbareaList);
				console.log("others :" + others);
				// 填入数据
				myChart.setOption({
					series: [
						{
							data: [{
								value: stations[0].value,
								name: '1#空压站',
								selected: true
							},
							{
								value: stations[1].value,
								name: '2#空压站'
							},
							{
								value: stations[2].value,
								name: '3#空压站'
							}, {
								value: stations[3].value,
								name: '4#空压站'
							}
							]
						},
						{
							data: [{
							value: bbareaList[0].value,
							name: '1BF'
						},
						{
							value: bbareaList[1].value,
							name: '2BF'
						},
						{
							value: bbareaList[2].value,
							name: '1550冷轧'
						},
						{
							value: bbareaList[5].value,
							name: '4200厚板\n加热炉'
						},
						{
							value: bbareaList[3].value,
							name: '2030冷轧'
						},
						{
							value: bbareaList[4].value,
							name: '2250冷轧'
						},
						{
							value: others,
							name: '其他',
							//自定义特殊 tooltip，仅对该数据项有效
							// tooltip:{
							// 	 formatter: function(params) {
							// 		 
							// 	},
							// },
						},
						{
							value: bbareaList[11].value,
							name: '焦炉'
						},
						{
							value: bbareaList[6].value,
							name: '4200厚板\n热处理'
						},
						{
							value: bbareaList[12].value,
							name: '炼钢北区'
						},
						{
							value: bbareaList[15].value,
							name: '球团'
						},
						{
							value: bbareaList[16].value,
							name: '烧结'
						},
						{
							value: bbareaList[18].value,
							name: '原料'
						}
						]
						}

					]
				 });
			});
		};
		getIntervall();
		setInterval(getIntervall, 1000 * 60 * 5);
		if (option && typeof option === "object") {
			myChart.setOption(option, true);
		}
	});
});
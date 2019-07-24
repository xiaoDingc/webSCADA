
layui.use(['layer', 'form', 'element', 'jquery', 'dialog'], function () {
	var layer = layui.layer;
	var element = layui.element;
	var form = layui.form;
	var $ = layui.jquery;
	var dialog = layui.dialog;
	var hideBtn = $('#hideBtn');
	var mainLayout = $('#main-layout');
	var mainMask = $('.main-mask');
	
    $(document).ready(function () {
        var mainpage = document.getElementById("iframe");
        //mainpage.style.display = "none";
	    
        var c = layer.load(1, {
            shade: [0.1, '#fff'] //0.1透明度的白色背景
        });
        var mas1;
        mainpage.onload = function () {
            layer.close(c);
            //mainpage.style.display = "block";
            var alert = parseInt($("#alert").html());
            console.log(alert);
            if (alert == 0) {
                 mas1 = layer.open({
                    title: "报警提示",
                    type: 1,
                    skin: 'layui-layer-molv', //样式类名
                    closeBtn: 1, //不显示关闭按钮
                    anim: 2,
                    shadeClose: false, //开启遮罩关闭
                    shade: 0,
                    area: ["500px", "100px"],
                    offset: '50px',                   
                    content: "有"+alert+"条未处理报警信息，请及时查看"
                });
            }
            function real() {
                $.ajax({
                    type: "GET",
                    url: "/Warn/WarnTip",
                    success: function (data) {
                        //if (data != "0") {
                        //    layer.close(mas1);
                        //    var mas = layer.open({
                        //        title: "报警提示",
                        //        type: 1,
                        //        skin: 'layui-layer-molv', //样式类名
                        //        closeBtn: 1, //不显示关闭按钮
                        //        anim: 2,
                        //        shadeClose: false, //开启遮罩关闭
                        //        shade: 0,
                        //        area: ["500px", "100px"],
                        //        offset: '50px',
                        //        content: '<div>11111111111111111111</div>'
                        //    });
                        //}
                    }
                })
            }
            real();
            setInterval(real, 6000);
        }
    })
	
	//监听导航点击
	element.on('nav(leftNav)', function(elem) {
	    var navA = $(elem);
        console.log(navA);
		var id = navA.attr('data-id');
		var url = navA.attr('data-url');
		var text = navA.attr('data-text');
		if(!url){
			return;
		}
		var isActive = $('.main-layout-tab .layui-tab-title').find("li[lay-id=" + id + "]");
		var noActive = $('.main-layout-tab .layui-tab-title').find("li[lay-id!=" + id + "]");
		if (noActive.length > 1) {
		    $.each(noActive, function (index, item) {
		        var layid = $(item).attr("lay-id");
		        element.tabDelete('tab', layid);
		    })
		}
		var test = $('.main-layout-tab .layui-tab-title');
		//console.log(test);
		var show = $('.layui-tab-item').find("iframe");
		//console.log(show);
		if(isActive.length > 0) {
			//切换到选项卡
			element.tabChange('tab', id);
		} else { 
		    var inload = layer.load(1, {
		        shade: [0.1, '#fff'] //0.1透明度的白色背景
		    });
			element.tabAdd('tab', {
				title: text,
				content: '<iframe src="' + url + '" name="iframe' + id + '" class="iframe" framborder="0" data-id="' + id + '" scrolling="auto" width="100%"  height="100%"></iframe>',
				id: id
			});
			var bbb="iframe"+id;		
			var aaaa = window.frames["iframe"];														   						
			    element.tabChange('tab', id);           			
		}
		var aaa = window.frames;			
		var ddd = aaa.length;					
		var eee = aaa[ddd - 1].frameElement;
		eee.onload = function () {
		    layer.close(inload);
		}		
		mainLayout.removeClass('hide-side');
	});
	//监听导航点击
	element.on('nav(rightNav)', function(elem) {
	    //var navA = $(elem).find('a');
	    var navA = $(elem);
	    console.log(navA);
		var id = navA.attr('data-id');
		var url = navA.attr('data-url');
		var text = navA.attr('data-text');
		var name = navA.attr('name');
		var tt = navA.html();
	
		if(!url){
			return;
		}
		if ("undefined"!=typeof(name)) {
		    url += "?account=" + name;
		}
		var isActive = $('.main-layout-tab .layui-tab-title').find("li[lay-id=" + id + "]");
		var noActive = $('.main-layout-tab .layui-tab-title').find("li[lay-id!=" + id + "]");
		if (noActive.length > 1) {
		    $.each(noActive, function (index, item) {
		        var layid = $(item).attr("lay-id");
		        element.tabDelete('tab', layid);
		    })
		}
		if(isActive.length > 0) {
			//切换到选项卡
			element.tabChange('tab', id);
		} else {
		    var bb = window.frames[''];
		    console.log(bb);
			element.tabAdd('tab', {
				title: text,
				content: '<iframe src="' + url + '" name="iframe' + id + '" class="iframe" framborder="0" data-id="' + id + '" scrolling="auto" width="100%"  height="100%"></iframe>',
				id: id
			});
			element.tabChange('tab', id);
		}
		mainLayout.removeClass('hide-side');
	});
	//菜单隐藏显示
	hideBtn.on('click', function() {
		if(!mainLayout.hasClass('hide-side')) {
			mainLayout.addClass('hide-side');
		} else {
			mainLayout.removeClass('hide-side');
		}
	});
	//遮罩点击隐藏
	mainMask.on('click', function() {
		mainLayout.removeClass('hide-side');
	})

});
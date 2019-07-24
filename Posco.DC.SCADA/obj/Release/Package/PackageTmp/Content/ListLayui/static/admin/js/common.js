layui.config({
    base: '../../Content/ListLayui/static/admin/js/module/'
}).extend({
    dialog: 'dialog',
});

layui.use(['form', 'jquery', 'laydate', 'layer', 'laypage', 'dialog', 'element'], function () {
    var form = layui.form,
		layer = layui.layer,
		$ = layui.jquery,
		dialog = layui.dialog;
    elem = layui.element;
    laydate = layui.laydate;

    //获取当前iframe的name值
    var iframeObj = $(window.frameElement).attr('name');
    console.log(iframeObj);
    //全选
    form.on('checkbox(allChoose)', function (data) {
        var child = $(data.elem).parents('table').find('tbody input[type="checkbox"]');
        child.each(function (index, item) {
            item.checked = data.elem.checked;
        });
        form.render('checkbox');
    });  
    //渲染表单
    form.render();  
    //顶部添加
    $('.addBtn').click(function () {
        var url = $(this).attr('data-url');
        //将iframeObj传递给父级窗口,执行操作完成刷新
        parent.page("菜单添加", url, iframeObj, w = "700px", h = "620px");
        return false;

    }).mouseenter(function () {

        dialog.tips('添加', '.addBtn');

    })
    //顶部排序
    $('.listOrderBtn').click(function () {
        var url = $(this).attr('data-url');
        dialog.confirm({
            message: '您确定要进行排序吗？',
            success: function () {
                layer.msg('确定了')
            },
            cancel: function () {
                layer.msg('取消了')
            }
        })
        return false;

    }).mouseenter(function () {

        dialog.tips('批量排序', '.listOrderBtn');

    })
    //顶部批量删除
    $('.delBtn').click(function () {
        var url = $(this).attr('data-url');
        dialog.confirm({
            message: '您确定要删除选中项',
            success: function () {
                layer.msg('删除了')
            },
            cancel: function () {
                layer.msg('取消了')
            }
        })
        return false;

    }).mouseenter(function () {

        dialog.tips('批量删除', '.delBtn');

    })
   
    //列表添加
    $('#table-list').on('click', '.add-btn', function () {
        var url = $(this).attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("菜单添加", url, iframeObj, w = "700px", h = "620px");
        return false;
    })
    //人员列表删除
    $('#table-list').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删除后，该人员下所有角色全部释放，您确定要删除该人员吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('取消成功')
            }
        })
        return false;
    })
    //角色删除
    $('#table-char').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删除后，该角色下所有用户全部释放，您确定要删除该角色吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('取消成功')
            }
        })
        return false;
    })
    //批量删除
    $('#table-batch').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删除后，选中的用户下所有角色全部释放，您确定要删除吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('取消成功')
            }
        })
        return false;
    })
    //列表跳转
    $('#table-list,.tool-btn').on('click', '.go-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        window.location.href = url + "?id=" + id;
        return false;
    })
    //编辑用户
    $('#table-list').on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
       page("编辑", url + "?id=" + id, iframeObj, w = "700px", h = "620px");
        return false;
    })
    //添加人员
    $('#table-form').on('click', '.add-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');

        //将iframeObj传递给父级窗口
        parent.page("添加人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })
    //创建新角色
    $('#table-form').on('click', '.new-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("创建新角色", url + "?id=" + id, iframeObj, w = "600px", h = "400px");
        return false;
    })
    //编辑角色
    $('#table-char').on('click', '.char-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("编辑角色", url + "?id=" + id, iframeObj, w = "600px", h = "400px");
        return false;
    })
    //权限管理
    $('#table-list').on('click', '.part-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("分配权限", url + "?id=" + id, iframeObj, w = "1000px", h = "620px");
        return false;
    })
    //角色权限管理
    $('#table-char').on('click', '.part-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("分配权限", url + "?id=" + id, iframeObj, w = "1000px", h = "620px");
        return false;
    })
    //角色人员管理
    $('#table-char').on('click', '.member-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("分配人员", url + "?id=" + id, iframeObj, w = "1000px", h = "620px");
        return false;
    })
    //批量分配权限
    $('#table-batch').on('click', '.part-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("批量分配权限", url + "?id=" + id, iframeObj, w = "1000px", h = "620px");
        return false;
    });
    /**
    *后台管理页面
    */

    //客户列表-添加客户
 

    //客户列表-编辑客户
    $("#table-client").on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        page("编辑客户", url + "?id=" + id, iframeObj, w = "600px", h = "400px");
        
        return false;
    })

    //客户列表-删除客户
    $('#table-client').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删您确定要删除该客户吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('已取消')
            }
        })
        return false;
    })

    //人员列表-添加人员
    $(document).on('click', '.radd-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("添加人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })

    //人员列表-编辑人员
    $('#table-r').on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("编辑人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })
    //人员列表-删除客户
    $('#table-r').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删您确定要删除该人员吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('已取消')
            }
        })
        return false;
    })

    //工厂列表-添加人员
    $(document).on('click', '.gadd-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("添加工厂", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })

    //工厂列表-编辑人员
    $('#table-g').on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("编辑工厂", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })
    //工厂列表-删除客户
    $('#table-g').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删您确定要删除该工厂吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('已取消')
            }
        })
        return false;
    })

    //空压站列表-添加人员
    $(document).on('click', '.sadd-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("添加人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })

    //空压站列表-编辑人员
    $('#table-s').on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("编辑人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })
    //空压站列表-删除客户
    $('#table-s').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删您确定要删除该人员吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('已取消')
            }
        })
        return false;
    })

    //区域列表-添加人员
    $(document).on('click', '.qadd-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("添加人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })

    //区域列表-编辑人员
    $('#table-q').on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("编辑人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })
    //区域列表-删除客户
    $('#table-q').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删您确定要删除该人员吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('已取消')
            }
        })
        return false;
    })

    //空压设备列表-添加人员
    $(document).on('click', '.kadd-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("添加人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })

    //空压设备列表-编辑人员
    $('#table-k').on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("编辑人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })
    //空压设备列表-删除客户
    $('#table-k').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删您确定要删除该人员吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('已取消')
            }
        })
        return false;
    })

    //检测仪表列表-添加人员
    $(document).on('click', '.jadd-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("添加人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })

    //检测仪表列表-编辑人员
    $('#table-j').on('click', '.edit-btn', function () {
        var That = $(this);
        var id = That.attr('data-id');
        var url = That.attr('data-url');
        //将iframeObj传递给父级窗口
        parent.page("编辑人员", url + "?id=" + id, iframeObj, w = "700px", h = "500px");
        return false;
    })
    //检测仪表列表-删除客户
    $('#table-j').on('click', '.del-btn', function () {
        var url = $(this).attr('data-url');
        var id = $(this).attr('data-id');
        dialog.confirm({
            message: '删您确定要删除该人员吗？',
            success: function () {
                layer.msg('删除成功')
            },
            cancel: function () {
                layer.msg('已取消')
            }
        })
        return false;
    })

});

/**
 * 控制iframe窗口的刷新操作
 */
var iframeObjName;

//父级弹出页面
function page(title, url, obj, w, h) {
    if (title == null || title == '') {
        title = false;
    };
    if (url == null || url == '') {
        url = "404.html";
    };
    if (w == null || w == '') {
        w = '700px';
    };
    if (h == null || h == '') {
        h = '350px';
    };
    iframeObjName = obj;
    //如果手机端，全屏显示
    if (window.innerWidth <= 768) {
        var index = layer.open({
            type: 2,
            title: title,
            area: [320, h],
            fixed: false, //不固定
            content: url
        });
        layer.full(index);
    } else {
        var index = layer.open({
            type: 2,
            title: title,
            area: [w, h],
            fixed: false, //不固定
            content: url
        });
    }
}

/**
 * 刷新子页,关闭弹窗
 */
function refresh() {
    var iframeObj = $(window.frameElement).attr('name');
    //根据传递的name值，获取子iframe窗口，执行刷新
    if (window.frames[iframeObjName]) {
        var rere = window.frames[iframeObjName];
        console.log(rere);
        window.frames[iframeObjName].location.reload();

    } else {
        window.location.reload();
    }

    layer.closeAll();
}
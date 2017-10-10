function successEvent(data, func) {
    if (data.stateCode == 200) {
        func(data);
    } else if (data.stateCode == 401) {
        modelConfirm('用户无该功能授权，是否重新登陆？', {
            submit: function () {
                window.top.location = '/login/index';
            }
        });

    } else if (data.stateCode == 500) {
        modelMessage(data.error);
    }
}


$(function () {
    $(".EditLabel").click(function () {
        var id = $(this).attr("data-bind");
        var actionName = $(this).attr("action-name");
        var height = $(this).attr('model-height');
        var url = actionName;
        if (id && id != 0) {
            url = actionName + '/' + id;
        }
        modelIfram(url, { height: height, title: '编辑' });
    })


    $(".DeleteLabel").click(function () {
        var id = $(this).attr("data-bind");
        var actionName = $(this).attr("action-name");
        modelConfirm('是否删除所选?', {
            title: '确认删除',
            submit: function () {
                $.post(actionName + '/' + id,
                    { ID: id },
                    function (data) {
                        successEvent(data, function (data) {
                            modelMessage("删除成功", {
                                closeReload: true
                            });
                        })
                    })
            }
        })
    })
})

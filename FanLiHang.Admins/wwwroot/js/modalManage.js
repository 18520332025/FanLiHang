var _buttonType = {
    Close: 0,
    CloseAndSub: 1,
    OK: 2,
    NoBtn: 3
}

var modelIframOptions = {
    buttonType: _buttonType.NoBtn,
    submit: null,
    close: null,
    height: 200,
    title: '',
    closeReload: false
}

var modelMessageOptions = {
    buttonType: _buttonType.OK,
    submit: null,
    close: null,
    height: 200,
    title: '信息',
    closeReload: false
}


var modelConfirmOptions = {
    buttonType: _buttonType.CloseAndSub,
    submit: null,
    close: null,
    height: 200,
    title: '确认',
    closeReload: false
}

function modelIfram(url, options) {
    options = $.extend(modelIframOptions, options);
    createModel('modelIfram', '<iframe src=' + url + '  style="width: 100%;height:' + options.height + 'px" frameborder="0" name="modelFrame"></iframe>', options);
}

function modelMessage(message, options) {
    options = $.extend(modelMessageOptions, options);
    createModel('modelMessage', message, options);
}

function modelConfirm(message, options) {
    options = $.extend(modelConfirmOptions, options);
    createModel('modelConfirm', message, options);
}



function createModel(id, body, options) {

    if ($("#" + id).length == 0) {
        //var options = $.extend(modelIframOptions, options);
        //头部信息，同时会注册Close事件
        var html = '<div class="modal fade in" id="' + id + '"> <div class="modal-dialog"> <div class="modal-content box  box-primary" style="border-top:3px solid #dd4b39;border-radius:3px;"> <div class="modal-header ">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-label="Close">x</button><h4>' + options.title + '</h4></div>';
        html += '<div class="modal-body  box-body"> ' + body + '</iframe></div>';
        if (options.buttonType != _buttonType.NoBtn) {
            html += '<div class="modal-footer">';
            html += createBtns(options);
            html += "</div>";
        }
        html += '</div></div></div>';
        $("section.content").append(html);
    }
    if ($("#" + id + " .box-body").html() != body) {
        $("#" + id + " .box-body").html(body);
    }

    $("#" + id).modal({
        backdrop: 'static'
    });
    var hasIFrame = body.indexOf('iframe');
    updateEvents(id, options, hasIFrame);
}

function createBtns(options) {
    var buttonType = options.buttonType;
    if (buttonType == _buttonType.Close) {
        return createCloseBtn();;
    } else if (buttonType == _buttonType.CloseAndSub) {
        return createCloseBtn() + createSubBtn();
    } else if (buttonType == _buttonType.OK) {
        return createOKBtn();
    }
}

function updateEvents(id, options, isIFrame) {
    bindClickEvent(id, 'modelClose', options.close, isIFrame);
    bindClickEvent(id, 'close', options.close, isIFrame);
    bindClickEvent(id, 'modelSub', options.submit, isIFrame);
    if (options.closeReload) {
        $("#" + id + ' *[data-dismiss="modal"]').each(function () {
            $(this).click(function () {
                $("div[id!=" + id + "]").modal("hide");
                reloadThisPage();
            });
        });
    }
}
function createCloseBtn(text) {
    return '<button type="button"  class="btn btn-default pull-left modelClose" data-dismiss="modal" >关闭</button>'
}

function createSubBtn(text) {
    return '<button type="button"  class="btn btn-danger modelSub">确定</button>';
}
function createOKBtn(text) {
    return '<button type="button" class="btn btn-danger" data-dismiss="modal">确定</button>';
}

function bindClickEvent(id, className, event, isIFrame) {
    //if (eventName == null) {
    //    eventName = 'javscript:;';
    //} else if (isIFrame) {
    //    eventName = 'javascript:modelFrame.window.' + eventName + '();'
    //}
    $("#" + id + " ." + className).click(event);
}
var _buttonType = {
    Close: 0,
    CloseAndSub: 1,
    OK: 2,
    NoBtn:3
}

var modelIframOptions = {
    buttonType: _buttonType.NoBtn,
    submit: null,
    close: null,
    height: 200,
    title: ''
}

function modelIfram(url, options) {
    var options = $.extend(modelIframOptions, options);
    if (options.submit == null) {
        options.submit = "submit";
    }
    createModel('modelIfram', '<iframe src=' + url + '  style="width: 100%;height:' + options.height + 'px" frameborder="0" name="modelFrame"></iframe>', options);
}

function modelMessage(message, options) {
    createModel('modelMessage', message, {
        buttonType: _buttonType.OK,
        title: '信息'
    });
}

function modelConfirm(message, options) {
    createModel('modelConfirm', message, {
        buttonType: _buttonType.CloseAndSub,
        title: '确认'
    });
}

function createModel(id, body, options) {
    if ($("#" + id).length == 0) {
        var options = $.extend(modelIframOptions, options);
        //头部信息，同时会注册Close事件
        var html = '<div class="modal fade in" id="' + id + '"> <div class="modal-dialog"> <div class="modal-content box  box-primary" style="border-top:3px solid #dd4b39;border-radius:3px;"> <div class="modal-header ">';
        html += '<button type="button" class="close" data-dismiss="modal" aria-label="Close">x</button><h4>' + options.title + '</h4></div>';
        html += '<div class="modal-body  box-body"> ' + body + '</iframe></div>';
        if(options.buttonType != _buttonType.NoBtn){
            html += '<div class="modal-footer">';
            html += createBtns(options);
            html +="</div>";
        }
        html += '</div></div></div>';
        $("section.content").append(html);
    }
    if ($("#" + id + ".box-body").html() != body) {
        $("#" + id + ".box-body").html(body);
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
        return createCloseBtn(options.close);;
    } else if (buttonType == _buttonType.CloseAndSub) {
        return createCloseBtn(options.close) + createSubBtn(options.submit);
    } else if (buttonType == _buttonType.OK) {
        return createOKBtn();
    }
}

function updateEvents(id, options, isIFrame) {
    bindClickEvent(id, 'modelClose', options.close, isIFrame);
    bindClickEvent(id, 'close', options.close, isIFrame);
    bindClickEvent(id, 'modelSub', options.submit, isIFrame);
}

function createCloseBtn() {
    return '<button type="button"  class="btn btn-default pull-left modelClose" data-dismiss="modal" >关闭</button>'
}

function createSubBtn() {
    return '<button type="button"  class="btn btn-danger modelSub">保 存</button>';
}
function createOKBtn() {
    return '<button type="button" class="btn btn-danger" data-dismiss="modal">确定</button>';
}

function bindClickEvent(id, className, eventName, isIFrame) {
    if (eventName == null) {
        eventName = 'javscript:;';
    } else if (isIFrame) {
        eventName = 'javascript:modelFrame.window.' + eventName + '();'
    }
    $("#" + id + " ." + className).attr("onClick", eventName);
}
var browserVersion = window.navigator.userAgent.toUpperCase();
var isOpera = browserVersion.indexOf("OPERA") > -1 ? true : false;
var isFireFox = browserVersion.indexOf("FIREFOX") > -1 ? true : false;
var isChrome = browserVersion.indexOf("CHROME") > -1 ? true : false;
var isSafari = browserVersion.indexOf("SAFARI") > -1 ? true : false;
var isIE = (!!window.ActiveXObject || "ActiveXObject" in window);
var isIE9More = (!-[1,] == false);


function reinitIframe(iframeId, minHeight) {
    try {
        var iframe = document.getElementById(iframeId);
        var bHeight = 0;
        if (isChrome == false && isSafari == false) bHeight = iframe.contentWindow.document.body.scrollHeight;

        var dHeight = 0;
        if (isFireFox == true) dHeight = iframe.contentWindow.document.documentElement.offsetHeight + 2;
        else if (isIE == false && isOpera == false) dHeight = iframe.contentWindow.document.documentElement.scrollHeight;
        else if (isIE == true && isIE9More) { //ie9+
            var heightDeviation = bHeight - eval("window.IE9MoreRealHeight" + iframeId);
            if (heightDeviation == 0) {
                bHeight += 3;
            } else if (heightDeviation != 3) {
                eval("window.IE9MoreRealHeight" + iframeId + "=" + bHeight);
                bHeight += 3;
            }
        } else //ie[6-8]、OPERA
            bHeight += 3;
        if (bHeight > 10) {
            eval('window.clearInterval(' + iframeId.replace('if', 'int') + ')');
        }
        var height = Math.max(bHeight, dHeight);
        if (height < minHeight) height = minHeight;
        iframe.style.height = height + "px";

    } catch (ex) { }
}



function startIframeInit(iframeId, minHeight) {
    eval("window.IE9MoreRealHeight" + iframeId + "=0");
    return window.setInterval("reinitIframe('" + iframeId + "'," + minHeight + ")", 100);
}

function reloadTab(name) {
    var href = name == undefined ? $("#tabList li.active a").attr('href').replace('#', '') : name;
    window.frames[href].document.location.reload(true);
    //$("#if_" + href).attr('src', $("#if_" + href).attr('src'));
    eval('int_' + href + '=startIframeInit("if_" + href, 700)');
}



function closeTab(id, li) {
    var isNext = false;
    isNext = $(li).hasClass('active');
    var next = $(li).next().find('a');
    $(li).remove();
    $("#if_" + id).parent().remove();
    if (isNext) {
        next.click();
    }
}

function switchTab(option) {
    if ($('#tab_' + option.name).length == 0) {
        var tab = '<li id="tab_' + option.name + '"><a href="#' + option.name + '" data-toggle="tab">' + option.title + '　<i class="fa fa-close" style="cursor:pointer"></i></a></li>';
        $("#tab-refresh").before(tab);
        var content = '<div class="tab-pane active" id="' + option.name + '"><iframe name="' + option.name + '" src="' + option.url + '" scrolling="no" frameborder="0" id="if_' + option.name + '"  style="width:100%"></iframe></div>';
        $('#tab-content').append(content);
        eval('int_' + option.name + '= startIframeInit("if_" + option.name, 700)');
    }
    $('#tab_' + option.name).find('a').click();
    bindCloseTab('#tab_' + option.name);
}

function bindCloseTab(li) {
    var href = $(li).find('a').attr('href').replace('#', '');
    $(li).find('i').click(function () {
        closeTab(href, li);
    });
}

$(function () {
    $("#tabList li ").each(function () {
        if ($(this).find('a').attr('data-toggle') == 'tab') {
            bindCloseTab(this);
        }
    })
})
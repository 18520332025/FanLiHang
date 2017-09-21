function imageUpdatePreview(fileInputBox, imageBox) {

    $("#" + fileInputBox).change(function () {
        if (window.FileReader) {
            var reader = new FileReader();
        } else {
            alert("您的设备不支持图片预览功能，如需该功能请升级您的设备！");
        }
        var imageType = /^image\//;
        var img = $(this).get(0).files[0];
        if (!imageType.test(img.type)) {
            alert("请选择图片！");
            return;
        }
        reader.onload = function (e) {
            //获取图片dom
            var setImg = document.getElementById(imageBox);
            //图片路径设置为读取的图片
            setImg.src = e.target.result;
        };
        reader.readAsDataURL(img);
    });
}
 
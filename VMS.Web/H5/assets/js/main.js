$(function () {
    function getRequest() {
        var url = window.location.search;
        var jsonList = {};
        if (url.indexOf("?") > -1) {
            var str = url.slice(url.indexOf("?") + 1);
            var strs = str.split("&");
            for (var i = 0; i < strs.length; i++) {
                jsonList[strs[i].split("=")[0]] = strs[i].split("=")[1];//如果出现乱码的话，可以用decodeURI()进行解码
            }
        }
        return jsonList;
    }
    $('#qrcode').bind('click', function () {
        var url = "/api/DCShowApi/QrCodeMake";
        var data = {did:1,cid:''};
        $.post(url, data, function (ret) {
            if (ret.success) {
                $('#base64').attr('src', ret.message).show();
            } else {
                if ($.trim(ret.message) != '') {
                    alert(ret.message);
                } else {
                   alert(msg + '失败!');
                }
            }
        });

    })
    var url = "/api/DCShowApi/GetDCInfo";
    var req = getRequest();
    var type = req.type ?req.type: ""  ;
    var cid = req.cid ?  req.cid:"" ;
    var did = req.did ? req.did : "";
    //cid = 2733;
    //did = 294;
    type = 0;
    if (cid === '' && did === '') {
        alert('无法获取到参数信息，请重新扫码！');
    } else {
        if (cid == '') {
            $('.xsz,.pic').hide();
        } else {
            $('.jsz').hide();
        }
        $.get(url, { type: type, cid: cid, did: did }, function (ret) {
            if (ret.success) {
                var data = ret.data;
                var dl = data.dl;
                var dp = data.dp;
                var imgs = [];

                if (dp != undefined) {
                    $('#motor_no').html(dp.motor_no == undefined ? '' : dp.motor_no);
                    $('#carframe_no').html(dp.carframe_no == undefined ? '' : dp.carframe_no);
                    $('#car_owner').html(dp.name == undefined ? '' : dp.name);
                    $('#car_tel').html(dp.phone == undefined ? '' : dp.phone);
                    $('#car_addr').html(dp.addr == undefined ? '' : dp.addr);

                    if (dp.user_photo_path != undefined)
                        imgs.push({ key: dp.user_photo_path, value: dp.user_photo_base64 });
                    if (dp.car_1_img_path != undefined)
                        imgs.push({ key: dp.car_1_img_path, value: dp.car_1_value });
                    if (dp.car_2_img_path != undefined)
                        imgs.push({ key: dp.car_2_img_path, value: dp.car_2_value });
                    if (dp.engine_no_img_path != undefined)
                        imgs.push({ key: dp.engine_no_img_path, value: dp.engine_no_value });
                    if (dp.vin_no_img_path != undefined)
                        imgs.push({ key: dp.vin_no_img_path, value: dp.vin_no_value });
                }

                if (dl != undefined) {
                    $('#name').html(dl.name == undefined ? '' : dl.name);
                    $('#permitted_card_type').html(dl.permitted_card_type_no == undefined ? '' : dl.permitted_card_type_no);
                    if (dl.tel_no != undefined)
                    $('#tel_no').html(dl.tel_no == undefined ? '' : dl.tel_no);
                    $('#addr').html(dl.addr == undefined ? '' : dl.addr);

                }
                var html = '<div class=\"col-sm-6 col-md-4\"><div class=\"thumbnail\"><img src=\"_img_src\" style=\"width:100%;height:300px\" alt=\"...\"\/><\/div><\/div >';
                var default_prefix = "data:image/jpeg;base64,";
                if (imgs.length > 0) {
                    for (var i = 0; i < imgs.length; i++) {
                        var key = imgs[i].key;
                        if (key.indexOf(".png") >= 0) {
                            default_prefix = "data:image/png;base64,";
                        } else {
                            default_prefix = "data:image/jpeg;base64,";
                        }
                        var t = html.replace(/_img_src/g, default_prefix + imgs[i].value);
                        $(t).appendTo($('#collapseThree>.panel-body>.row'));
                    }
                }
            } else {
                if ($.trim(ret.message) == '') {
                    alert('无法查询到相关车主/车辆信息！');
                } else {
                    alert(ret.message);
                }
            }



        })
    }
    
    console.log(window.href);
})
﻿$(function () {
    //选中tab
    function selectTab(index) {
        $('.tab-title-item').removeClass('tab-title-selected');
        $('.tab-body-item').removeClass('tab-body-selected');
        $('.tab-title-item').eq(index).addClass('tab-title-selected');
        $('.tab-body-item').eq(index).addClass('tab-body-selected');
    }
    function addTab(title, url) {
        var li = [];
        li.push('<li class="tab-title-item">');
        li.push('<span>' + title + '</span>');
        li.push('<i class="tab-close"></i>');
        li.push('</li>');
        $('.tab-title-item').removeClass('tab-title-selected');
        $('.tabs-title>ul').append(li.join(''));
        $('.tab-title-item:last').addClass('tab-title-selected');

        var tabItem = [];
        tabItem.push('<div class="tab-body-item">');
        tabItem.push('<iframe src="' + url + '" frameborder="0"></iframe>');
        tabItem.push('</div>');
        $('.tab-body-item').removeClass('tab-body-selected');
        $('.vmsui-tabs-body').append(tabItem.join(''));
        $('.tab-body-item:last').addClass('tab-body-selected');
    }
    function bindLastTabEvent() {
        $('.tab-title-item:last').unbind('click').bind('click', function (event) {
            var index = $(this).index();
            if (index >= 0) {
                selectTab(index);
            }
        });
        $('.tab-title-item>.tab-close:last').unbind('click').bind('click', function (event) {
            event.stopPropagation();
            var index = $(this).parent('li').index();
            var lastIndex = $('.tab-title-item:last').index();
            if (index == 0 && index == lastIndex) return;
            $(this).parent('li').remove();
            $('.tab-body-item').eq(index).remove();
            if (lastIndex > index) {
                selectTab(index);
            } else {
                selectTab(index-1);
            }
           
        })
    }
    function loadWelcome() {
        var title = '首页';
        var url = '/Login/Welcome';
        addTab(title, url);
    }
    function rollPage(type, tabindex) {
        var t = $(".tabs-title>ul")
          , l = t.children("li")
          , n = (t.prop("scrollWidth"),
        t.outerWidth())
          , s = parseFloat(t.css("left"));
        if ("left" === type) {
            if (!s && s <= 0)
                return;
            var r = -s - n;
            l.each(function(index, el) {
                var l = $(el)
                  , n = l.position().left;
                if (n >= r)
                    return t.css("left", -n),
                    !1
            })
        } else
            "auto" === type ? !function() {
                var type, r = l.eq(tabindex);
                if (r[0]) {
                    if (type = r.position().left,
                    type < -s)
                        return t.css("left", -type);
                    if (type + r.outerWidth() >= n - s) {
                        var o = type + r.outerWidth() - (n - s);
                        l.each(function(index, el) {
                            var l = $(el)
                              , n = l.position().left;
                            if (n + s > 0 && n - s > o)
                                return t.css("left", -n),
                                !1
                        })
                    }
                }
            }() : l.each(function(index, el) {
                var l = $(el)
                  , r = l.position().left;
                if (r + l.outerWidth() >= n - s)
                    return t.css("left", -r),
                    !1
            })
    }

    var allMenus = [];
    function bindEvents() {
       //退出
        $('#btnOut').bind('click', function () {
            //$('#msg').modal('show');
            //return;
            var url = "../../api/SystemApi/Logout";
            $.post(url, {}, 'json')
            .success(function(ret){
                if (ret.success) {
                    window.location.reload();
                } else {

                }
            });
        });
        //菜单隐藏按钮
        $('.vmsui-header-item i.glyphicon-list').bind('click', function (event) {
            $('#vmsui_container').toggleClass('vms-side-spread-sm');
        });
        //刷新
        $('.vmsui-header-item i.glyphicon-refresh').bind('click', function (event) {
            var tabSelected = $('.tab-body-selected');
            if (tabSelected.length > 0) {
                var iframe = tabSelected.children('iframe').get(0);
                iframe.contentWindow.location.reload();
            }
        });
        //搜索
        $('.vmsui-header-search-list').unbind('click').bind('click', function (el) {
            var target = el.target;
            if (target != undefined) {
                var val = $(target).html();
                var findOne = $(allMenus).filter(function (inx, el) {
                    var cite = $(el).find('cite').html();
                    if (cite.indexOf(val) != -1) {
                        return true;
                    }
                    return false;
                });
                if (findOne != undefined) {
                    $(findOne).find('cite').click();
                    $('.vmsui-header-search input').val('');
                    $(this).hide();
                }
            }
        })
        $('.vmsui-header-item .vmsui-header-search input').off('keydown').on('keydown', function (e) {
            if (allMenus.length<=0) {
                var items = $('.vmsui-side-menu .vmsui-nav-item>a').filter(function (inx, el) {
                    var url = $(el).attr('data-url');
                    if (url != undefined && $.trim(url) != '') {
                        return true;
                    }
                    return false;
                });
                allMenus = items;
            }
            $('.vmsui-header-search-list>li').remove();
            var val = $(this).val();
            if (val != undefined && val.replace(/\s/g, "") && 13 === e.keyCode) {
                console.log(val);
                var filters = $(allMenus).filter(function (inx, el) {
                    var cite = $(el).find('cite').html();
                    if (cite.indexOf(val) != -1) {
                        return true;
                    }
                    return false;
                });
                var li = [];
                console.dir(filters);
                $.each(filters, function (inx, el) {
                    var cite = $(el).find('cite').html();
                    li.push('<li>'+cite+'</li>');
                })
                if (li.length > 0)
                    $('.vmsui-header-search-list').append(li.join('')).show();
                else {
                    $('.vmsui-header-search-list>li').remove();
                    $('.vmsui-header-search-list').hide();
                }
                    
            }
        });
        //全屏
        function fullscreen(e) {
            var a = "full-screen"
                , i = "small-screen"
              , t = e;
            if (t.hasClass(a)) {
                var l = document.body;
                l.webkitRequestFullScreen ? l.webkitRequestFullScreen() : l.mozRequestFullScreen ? l.mozRequestFullScreen() : l.requestFullScreen && l.requestFullscreen(),
                t.addClass(i).removeClass(a)
            } else {
                var l = document;
                l.webkitCancelFullScreen ? l.webkitCancelFullScreen() : l.mozCancelFullScreen ? l.mozCancelFullScreen() : l.cancelFullScreen ? l.cancelFullScreen() : l.exitFullscreen && l.exitFullscreen(),
                t.addClass(a).removeClass(i)
            }
        }
        $('.vmsui-header-item .full-screen').bind('click', function () {
            var e = $(this);
            fullscreen(e);
        });
        //tab翻页
        $('.left-page').bind('click', function () {
            rollPage('left');
        });
        $('.right-page').bind('click', function () {
            rollPage();
        });
        //菜单折叠
        $('.vmsui-side-menu span.vmsui-nav-more').bind('click', function () {
            var ul = $(this).parent().parent().children('ul');
            var display = ul.css('display');
            if (display == 'block') {
                ul.slideUp('fast');
            } else {
                ul.slideDown('fast');
            }
           
        });
        //菜单点击
        $('.vmsui-nav-item cite').unbind('click').bind('click', function (event) {
            event.stopPropagation();
            var url = $(this).parent('a').attr('data-url');
            if (url != undefined) {
                $('.vmsui-nav-item').removeClass('vmsui-nav-item-selected');
                $(this).parents('li:first').addClass('vmsui-nav-item-selected');
                var title = $.trim($(this).text());
                var titles = $('.tab-title-item>span');
                var has = false;
                var index = 0;
                $.each(titles, function (idx, el) {
                    if ($(el).text() == title) {
                        has = true;
                        index = idx;
                        return false;
                    }
                });
                if (has) {
                    selectTab(index);
                } else {
                    addTab(title, url);
                    bindLastTabEvent();
                    index = $('.tabs-title>ul>li').length - 1;
                }
                rollPage('auto', index);
            } 
                if ($(this).next('span').hasClass('vmsui-nav-more')) {
                    $(this).next('span').trigger('click');
                }


            
        });

        //关闭页签
        $('.tabs-control').bind('click', function (event) {
            event.stopPropagation();
            var block = $(this).find('.tabs-block');
            var display = block.css('display');
            if (display == 'block') {
                block.slideUp('fast');
            } else {
                block.slideDown('fast');
            }
        });
        $('.tabs-block>li').bind('click', function (event) {
            event.stopPropagation();
            var id = $(this).attr('id');
            if (id == 'close-all') {
                $('.tabs-title>ul>li,.vmsui-tabs-body>div').remove();
            } else if (id == 'close-left') {
                $('.tab-title-selected').prevAll().remove();
                $('.tab-body-selected').prevAll().remove();
            } else if (id == 'close-right') {
                $('.tab-title-selected').nextAll().remove();
                $('.tab-body-selected').nextAll().remove();
            }
            $('.tabs-control').trigger('click');
        });

    }
    function loadMenu() {
        function parseMenu(menus) {
            var dom = [];
            var topMenu = $.map(menus, function (ele, inx) {
                if (ele.pid == null) return ele;
            });
            $.each(topMenu, function (inx,ele) {
                dom.push(' <li class="vmsui-nav-item">');
                dom.push(' <a href="javascript:;">');
                dom.push(' <i class="' + ele.res_img + '"></i>');
                dom.push(' <cite>' + ele.res_desc + '</cite>');
                dom.push(' <span class="vmsui-nav-more"></span>');
                dom.push(' </a>');
            
                var secondMenu = $.map(menus, function (ele1, inx) {
                    if (ele1.pid == ele.id) return ele1;
                });
                if(secondMenu.length>0){
                    dom.push('<ul class="vmsui-nav-child">');
                }
                $.each(secondMenu, function ( inx1,el) {
                    dom.push(' <li class="vmsui-nav-item">');
                    dom.push(' <a href="javascript:;" data-url="' + el.res_uri + '">');
                    dom.push(' <i class="vmsui-space"></i>');
                    dom.push(' <cite>' + el.res_desc + '</cite>');
                    dom.push(' <span class=""></span>');
                    dom.push(' </a>');
                    dom.push(' </li>');
                });
                if(secondMenu.length>0){
                    dom.push('</ul>');
                }
                dom.push(' </li>');
            });
            return dom.join('');
        }
        var url = "../../api/SystemApi/LoadMenu";
        $.post(url, {}, 'json')
           .success(function (ret) {
               if (ret.success) {
                   $('.vmsui-nav-tree').empty().append(parseMenu(ret.data));
               } else {

               }
               loadWelcome();
               bindEvents();
               //bindLastTabEvent();
           });
    }
    function ajustHeight() {
        //var allHeight = $(window.document).outerHeight(true);
        //var header = $('.vmsui-header').outerHeight(true);
        //var title = $('.vmsui-tabs-title').outerHeight(true);
        //console.log(allHeight+':'+title + '==' + header);
        //$('.vmsui-tabs-body').height(allHeight - 94);
    }
    loadMenu();
    //bindEvents();
    //bindLastTabEvent();
    ajustHeight();
    
});
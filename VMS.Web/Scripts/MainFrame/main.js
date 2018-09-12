$(function () {
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
        $('.tab-title-item:last').after(li.join(''));
        $('.tab-title-item:last').addClass('tab-title-selected');

        var tabItem = [];
        tabItem.push('<div class="tab-body-item">');
        tabItem.push('<iframe src="' + url + '" frameborder="0"></iframe>');
        tabItem.push('</div>');
        $('.tab-body-item').removeClass('tab-body-selected');
        $('.tab-body-item:last').after(tabItem.join(''));
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
    function bindEvents() {
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
        $('.vmsui-header-item .vmsui-header-search input').off('keypress').on('keypress', function (e) {
            if (this.value.replace(/\s/g, "") && 13 === e.keyCode) {


            }

        });
        //全屏
        function fullscreen(e) {
            var a = "glyphicon-resize-full"
              , i = "glyphicon-resize-small"
              , t = e.children("i");
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
        $('.vmsui-header-item i.glyphicon-resize-full').bind('click', function () {
            var e = $(this).parent();
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
                console.log(url)
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
            } else {
                if ($(this).next('span').hasClass('vmsui-nav-more')) {
                    $(this).next('span').trigger('click');
                }


            }
        });

    }
    function ajustHeight() {
        //var allHeight = $(window.document).outerHeight(true);
        //var header = $('.vmsui-header').outerHeight(true);
        //var title = $('.vmsui-tabs-title').outerHeight(true);
        //console.log(allHeight+':'+title + '==' + header);
        //$('.vmsui-tabs-body').height(allHeight - 94);
    }
   
    bindEvents();
    bindLastTabEvent();
    ajustHeight();

});
//微博图片查看插件

(function($) {
    $.fn.artZoom = function() {
        $(this).live('click', function() {
            if ($(this).hasClass('artZoomAll')) {
                var aRevv = $(this).attr('rev');
                var parentId = 'image_area_' + aRevv;
                var tidv = (aRevv.substr(0, aRevv.indexOf('_')));

                // 拉出评论框
                if (($.trim(($('javascript:void(0);reply_area_' + tidv).html()))).length < 1) {
                    $('javascript:void(0);topic_list_reply_' + tidv + '_aid').click();
                }

                // 放大多图
                if ($("javascript:void(0);" + parentId + " a.artZoomAll").length > 1) {
                    $("javascript:void(0);" + parentId + " a.artZoomAll").each(function() {
                        clickOne($(this), parentId);
                    });
                    return false;
                }
            }

            clickOne($(this));
            return false;
        });
        var clickOne = function(aObj, parentId) {

            var maxImg = $(aObj).attr('rel'),
            viewImg = $(aObj).attr('href').length === '0' ? maxImg : $(aObj).attr('href');

            if ($(aObj).find('.loading').length == 0) {
                $(aObj)
						.append(
								'<span class="loading" title="Loading..">Loading..</span>');
            }
            imgTool($(aObj), maxImg, viewImg, parentId);
        };

        var loadImg = function(url, fn) {
            var img = new Image();
            img.src = url;
            if (img.complete) {
                fn.call(img);
            } else {
                img.onload = function() {
                    fn.call(img);
                };
            }
            ;
        };

        var imgTool = function(on, maxImg, viewImg, parentId) {

            var width = 0, height = 0,
            tool = function() {

                on.find('.loading').remove();
                on.hide();

                var parentInnerWidth = on.parent().innerWidth();

                if (parentInnerWidth < 120) {
                    parentInnerWidth = 440;
                }

                var maxWidth = parentInnerWidth - 12; // 获取父级元素宽度



                if (width > maxWidth) {
                    height = maxWidth / width * height;
                    width = maxWidth;
                }

                // 显示页禁止收起和缩小 foxis
                if ($(on).hasClass('artZoom2')) {
                    var html = '<div class="artZoomBox"><div class="tool"><a class="imgRight" href="javascript:void(0);" title="向右转"></a><a class="imgLeft" href="javascript:void(0);" title="向左转"></a><a class="viewImg" href="'
							+ viewImg
							+ '" title="查看原图">查看原图</a></div><a href="'
							+ viewImg
							+ '"> <img class="maxImg" width="'
							+ width
							+ '" height="'
							+ height
							+ '" src="'
							+ maxImg + '" /></a></div>';
                } else if ($(on).hasClass('artZoom3')) {
                    var html = '<div class="artZoomBox"><div class="tool"><a class="imgRight" href="javascript:void(0);" title="向右转"></a><a class="imgLeft" href="javascript:void(0);" title="向左转"></a><a class="viewImg" href="'
							+ viewImg
							+ '" title="查看原图">查看原图</a></div><a rev="'
							+ aRevv
							+ '" href="'
							+ viewImg
							+ '" class="maxImgLink3"> <img class="maxImg" width="'
							+ width
							+ '" height="'
							+ height
							+ '" src="'
							+ maxImg + '" /></a></div>';
                } else if ($(on).hasClass('artZoomAll')) {
                    var html = '<div class="artZoomBox"><div class="tool"><a class="imgRight" href="javascript:void(0);" title="向右转"></a><a class="imgLeft" href="javascript:void(0);" title="向左转"></a><a class="viewImg" href="'
							+ viewImg
							+ '" title="查看原图">查看原图</a></div><a href="'
							+ viewImg
							+ '" class="maxImgLinkAll"> <img class="maxImg" width="'
							+ width
							+ '" height="'
							+ height
							+ '" src="'
							+ maxImg + '" /></a></div>';
                } else {
                    var html = '<div class="artZoomBox"><div class="tool"><a class="hideImg" href="javascript:void(0);" title="收起">收起</a><a class="imgRight" href="javascript:void(0);" title="向右转"></a><a class="imgLeft" href="javascript:void(0);" title="向左转"></a><a class="viewImg" href="'
							+ viewImg
							+ '" title="查看原图">查看原图</a></div><a href="'
							+ viewImg
							+ '" class="maxImgLink"> <img class="maxImg" width="'
							+ width
							+ '" height="'
							+ height
							+ '" src="'
							+ maxImg + '" /></a></div>';
                }
                if (on.next('.artZoomBox').length < 1) {
                    on.after(html);
                }

                var box = on.next('.artZoomBox');
                box.hover(function() {
                    box.addClass('js_hover');
                }, function() {
                    box.removeClass('js_hover');
                });
                box.find('a').unbind();
                box
						.find('a')
						.bind(
								'click',
								function() {
								    // 点击收起整个
								    if ($(this).hasClass('maxImgLink3')
											&& 'undefined' != typeof (aRevv)) {
								        view_topic_content(aRevv, aSidv, aTPT_v);
								    }
								    ;

								    // 收起所有
								    if ($(this).hasClass('maxImgLinkAll')) {
								        if ($('javascript:void(0);' + parentId + ' .artZoomBox').length > 1) {
								            $('javascript:void(0);' + parentId + ' .artZoomBox')
													.each(function() {
													    $(this).hide();
													    $(this).prev().show();
													});
								            window.location.hash = parentId;
								        } else {
								            box.hide();
								            box.prev().show();
								        }
								    }
								    ;

								    // 收起
								    if ($(this).hasClass('hideImg')
											|| $(this).hasClass('maxImgLink')) {
								        box.hide();
								        box.prev().show();
								    }
								    ;
								    // 左旋转
								    if ($(this).hasClass('imgLeft')) {
								        box.find('.maxImg').rotate('right');
								    }
								    ;
								    // 右旋转
								    if ($(this).hasClass('imgRight')) {
								        box.find('.maxImg').rotate('left');
								    }
								    ;
								    // 新窗口打开
								    if ($(this).hasClass('viewImg')) {
								        window.open(viewImg);
								    }
								    return false;
								});

                if (on.next('.artZoomBox').length != 0) {
                    return on.next('.artZoomBox').show();
                }



            };

            loadImg(maxImg, function() {
                width = this.width;
                height = this.height;
                tool();
            });

            $.fn.rotate = function(p) {

                var img = $(this)[0], n = img.getAttribute('step');

                if (!this.data('width') && !$(this).data('height')) {
                    this.data('width', img.width);
                    this.data('height', img.height);
                }
                ;

                if (n == null)
                    n = 0;
                if (p == 'left') {
                    (n == 3) ? n = 0 : n++;
                } else if (p == 'right') {
                    (n == 0) ? n = 3 : n--;
                }
                ;
                img.setAttribute('step', n);

                if (document.all) {
                    img.style.filter = 'progid:DXImageTransform.Microsoft.BasicImage(rotation='
							+ n + ')';
                    // IE8高度设置
                    if ($.browser.version == 8) {
                        switch (n) {
                            case 0:
                                this.parent().height('');
                                // this.height(this.data('height'));
                                break;
                            case 1:
                                this.parent().height(this.data('width') + 10);
                                // this.height(this.data('width'));
                                break;
                            case 2:
                                this.parent().height('');
                                // this.height(this.data('height'));
                                break;
                            case 3:
                                this.parent().height(this.data('width') + 10);
                                // this.height(this.data('width'));
                                break;
                        }
                        ;
                    }
                    ;

                } else {
                    var c = this.next('canvas')[0];
                    if (this.next('canvas').length == 0) {
                        this.css({
                            'visibility': 'hidden',
                            'position': 'absolute'
                        });
                        c = document.createElement('canvas');
                        c.setAttribute('class', 'maxImg canvas');
                        img.parentNode.appendChild(c);
                    }
                    var canvasContext = c.getContext('2d');
                    switch (n) {
                        default:
                        case 0:
                            c.setAttribute('width', img.width);
                            c.setAttribute('height', img.height);
                            canvasContext.rotate(0 * Math.PI / 180);
                            canvasContext.drawImage(img, 0, 0);
                            break;
                        case 1:
                            c.setAttribute('width', img.height);
                            c.setAttribute('height', img.width);
                            canvasContext.rotate(90 * Math.PI / 180);
                            canvasContext.drawImage(img, 0, -img.height);
                            break;
                        case 2:
                            c.setAttribute('width', img.width);
                            c.setAttribute('height', img.height);
                            canvasContext.rotate(180 * Math.PI / 180);
                            canvasContext.drawImage(img, -img.width, -img.height);
                            break;
                        case 3:
                            c.setAttribute('width', img.height);
                            c.setAttribute('height', img.width);
                            canvasContext.rotate(270 * Math.PI / 180);
                            canvasContext.drawImage(img, -img.width, 0);
                            break;
                    }
                    ;
                }
                ;
            };

        };
    };

    // $('a.artZoom').artZoom();
    $('a.artZoom').live('click', function() {
        var relv = $(this).attr('rel');
        var revv = $(this).attr('rev');
        var TPT_v = $(this).attr('TPT_');

        view_topic_content(relv, revv, TPT_v);

        return false;
    });

    // artZoom2 区分显示页和列表页
    $('a.artZoom2').artZoom();

    $('a.artZoom3').artZoom();

    // 点击时，同时放大缩小区域内的多张图片
    $('a.artZoomAll').artZoom();

})(jQuery);
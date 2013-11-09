/// <reference path="jquery-1.8.3.min.js" />
/**
Name    : 下拉选择框（select标签）美化
Version : 1.0.0
Author  : RockyMyx
Use     : 在select标签上添加class="beautify" ，引入js，调用$.jSelect();即可
Params  : scope：   指定美化的select
          height：  下拉框默认高度
          width：   下拉框默认宽度（如果不设置selClass，则需要设置inherit:false才能生效）
          selClass：select标签样式
          opClass： select中option标签的样式，会影响到美化下拉框中的文字样式
          inherit： 如果inherit为true，则下拉框与input的宽度一样，否则使用settings中定义的width
          noscroll：强制下拉框不出现滚动条
**/
(function ($) {
    $.mCombox = function (options) {
        var settings = {
            scope: '.js-combo',
            height: "200px",
            width: "100px",
            selClass: '',
            opClass: '',
            inherit: true,
            noscroll: false
        };
        $.extend(settings, options || {});

        var selects = $(settings.scope);

        if (selects.length > 0) {
            $("body").append("<div id='combo-data' style='position:absolute; display:none'></div>");

            selects.each(function () {
                var select = this;
                var input = $("<input type='text' readonly='readonly' class='combo-input' />")
                            .attr("disabled", this.disabled)
                            .css("display", $(this).css("display"))
                            .css("width", parseInt($(this).css("width")) + "px")
                            .css("height", parseInt($(this).css("height")) + "px")
                            .val(this.options[this.selectedIndex].text)
                            .insertAfter(this);

                this.style.display = "none";

                input.on('click', function () {
                    var div = $("#combo-data")
                              .empty();

                    //使用select标签的样式
                    if (settings.selClass) {
                        div.attr("class", select.className);
                    }

                    //如果inherit为true，则div与input的宽度一样，否则使用settings中定义的width
                    if (settings.inherit)
                        div.css("width", $(this).innerWidth());
                    else
                        div.css("width", settings.width);

                    var item, a;
                    for (var i = 0; i < select.options.length; i++) {
                        item = select.options[i];
                        a = $("<a href='javascript:void(0);'></a>")
                            .html(item.text)
                            .appendTo(div);

                        if (settings.opClass) {
                            a.addClass(item.className)
                        }

                        if (i == select.selectedIndex) {
                            a.addClass("selected");
                        }

                        a.on('click', function () {
                            var n = $(this).index();
                            select.selectedIndex = n;
                            input.val(select.options[n].text);
                            div.hide();
                            $(select).change();
                        });
                    }

                    var divHeight = settings.height ? settings.height : "200px";

                    //jquery是根据userAgent的version值正则判断的，但是如果在windows2008k中，会把windows NT 6.0误认为是ie6.0，导致将ie8误认成ie6。$.support.style在IE6和IE7中返回值为false
                    //if ($.browser.msie && ($.browser.version == "6.0"))
                    if ($.browser.msie && ($.browser.version == "6.0") && !$.support.style) {
                        div.css({ "height": settings.noscroll ? "auto" : divHeight, "overflow-y": noscroll ? "hidden" : "scroll" });
                    } else {
                        div.css("height", settings.noscroll ? "10000px" : divHeight);
                    }

                    //如果有.onside修饰，弹出的选项层将在侧面，否则是在下面
                    $(select).hasClass("onside")
                             ? div.locateBeside(this, -2)
                             : div.locateBelow(this);

                    if (window.activeSelect == select) {
                        div.slideToggle(100);
                    } else {
                        div.slideDown(100);
                        window.activeSelect = select;
                    }
                });
            });

            $(document).click(function (e) {
                if (!$(e.target).is(".combo-input") && !$(e.target).is("#combo-data")) {
                    $("#combo-data").hide();
                }
            });
        }
    }
})(jQuery);

$.fn.extend({
    locate: function (x, y) {
        if (this.css("position") == "fixed") {
            y -= $(document).scrollTop();
        }
        return this.css({ left: x, top: y });
    },
    locateBeside: function (el, adjustX) {
        var p = $(el).offset(),
                    w1 = $(el).outerWidth(),
                    w2 = this.outerWidth(),
                    h2 = this.outerHeight(),
                    x  = p.left + w1 + 5 + (adjustX || 0),
                    y  = p.top;
        if ($(document).width() < x + w2) {
            x = p.left - w2 - 5 - (adjustX || 0);
        }
        if ($(document).height() < y + h2) {
            y = p.top - (y + h2 + 15 - $(document).height());
        }
        return this.locate(x, y);
    },
    locateBelow: function (el, adjustY) {
        var p = $(el).offset();
        return this.locate(p.left, p.top + $(el).outerHeight() + (adjustY || 0));
    },
    locateCenter: function () {
        return this.locate(
                    ($(window).width() - this.width()) / 2,
                    ($(window).height() - this.height()) / 2 + $(document).scrollTop()
                );
    }
});
/**
* This jQuery plugin displays pagination links inside the selected elements.
*
* @author Gabriel Birke (birke *at* d-scribe *dot* de)
* @version 1.2
* @param {int} maxentries Number of entries to paginate
* @param {Object} opts Several options (see README for documentation)
* @return {Object} jQuery Object
*/

/* Modified by RockyMyx
1、增加显示“首页”和“尾页”
2、增加显示“跳转到__页”
3、Ajax无刷新请求数据，增加参数table_to控制数据返回时刷新的表格
4、增加函数参数，根据需要对分页按键各部分进行控制
5、修改默认请求连接为当前页面url，即location.href
6、按钮使用css3圆角美化
7、修复浏览器兼容性问题
8、添加编辑删除事件绑定，配合common.js
*/
jQuery.fn.pagination = function (maxentries, opts) {
    opts = jQuery.extend({
        items_per_page: 3,
        num_display_entries: 9,
        current_page: 0,
        num_edge_entries: 1,
        table_to: '.table',
        //增加分页控件信息，修改分页显示文字
        first_text: "首页",
        prev_text: "上一页",
        next_text: "下一页",
        last_text: "末页",
        ellipse_text: "...",
        //自定义显示分页控件各部分
        first_show_always: true,
        prev_show_always: true,
        next_show_always: true,
        last_show_always: true,
        goto_show_always: true,
        data_info__show_always: true,
        callback: function () { return false; }
    }, opts || {});

    return this.each(function () {
        /**
        * Calculate the maximum number of pages
        */
        function numPages() {
            return Math.ceil(maxentries / opts.items_per_page);
        }

        /**
        * Calculate start and end point of pagination links depending on
        * current_page and num_display_entries.
        * @return {Array}
        */
        function getInterval() {
            var ne_half = Math.ceil(opts.num_display_entries / 2);
            var np = numPages();
            var upper_limit = np - opts.num_display_entries;
            var start = current_page > ne_half ? Math.max(Math.min(current_page - ne_half, upper_limit), 0) : 0;
            var end = current_page > ne_half ? Math.min(current_page + ne_half, np) : Math.min(opts.num_display_entries, np);
            return [start, end];
        }

        /**
        * This is the event handling function for the pagination links.
        * @param {int} page_id The new page number
        */
        function pageSelected(page_id, evt) {
            current_page = page_id;
            //common.js记录页码
            paging.pageIndex = current_page;
            drawLinks();
            //AJAX请求数据
            $('.current-page').html('当前第' + (++page_id) + '页');
            var params = '{pageIndex : ' + page_id + '}';
            $.ajax({
                type: 'POST',
                //请求当前页面
                url: location.href,
                contentType: 'application/json',
                dataType: 'html',
                data: params,
                success: function (result) {
                    $(opts.table_to).html(result);
                    //绑定表格的编辑和删除点击事件等，依赖于common.js
                    bindTable();
                }
            });
            var continuePropagation = opts.callback(page_id, panel);
            if (!continuePropagation) {
                if (evt.stopPropagation) {
                    evt.stopPropagation();
                }
                else {
                    evt.cancelBubble = true;
                }
            }
            return continuePropagation;
        }

        /**
        * This function inserts the pagination links into the container element
        */
        function drawLinks() {
            panel.empty();
            var interval = getInterval();
            var np = numPages();
            // This helper function returns a handler function that calls pageSelected with the right page_id
            var getClickHandler = function (page_id) {
                return function (evt) { return pageSelected(page_id, evt); }
            }
            // Helper function for generating a single link (or a span tag if it's the current page)
            var appendItem = function (page_id, appendopts) {
                page_id = page_id < 0 ? 0 : (page_id < np ? page_id : np - 1); // Normalize page id to sane value
                appendopts = jQuery.extend({ text: page_id + 1, classes: "" }, appendopts || {});
                if (page_id == current_page) {
                    var lnk = jQuery("<span class='current br'>" + (appendopts.text) + "</span>");
                }
                else {
                    var lnk = jQuery('<a class="bor" id="page' + page_id + '">' + (appendopts.text) + '</a>')
						.bind("click", getClickHandler(page_id))
						.attr('href', '');
                    //去除默认链接
                }
                if (appendopts.classes) { lnk.addClass(appendopts.classes); }
                panel.append(lnk);
            }
            // 生成首页按钮
            if (opts.first_text && opts.first_show_always) {
                appendItem(0, { text: opts.first_text, classes: "prev bor" });
            }
            // Generate "Previous"-Link
            if (opts.prev_text && (current_page > 0 || opts.prev_show_always)) {
                appendItem(current_page - 1, { text: opts.prev_text, classes: "prev bor" });
            }
            // Generate starting points
            if (interval[0] > 0 && opts.num_edge_entries > 0) {
                var end = Math.min(opts.num_edge_entries, interval[0]);
                for (var i = 0; i < end; i++) {
                    appendItem(i);
                }
                if (opts.num_edge_entries < interval[0] && opts.ellipse_text) {
                    jQuery("<span>" + opts.ellipse_text + "</span>").appendTo(panel);
                }
            }
            // Generate interval links
            for (var i = interval[0]; i < interval[1]; i++) {
                appendItem(i);
            }
            // Generate ending points
            if (interval[1] < np && opts.num_edge_entries > 0) {
                if (np - opts.num_edge_entries > interval[1] && opts.ellipse_text) {
                    jQuery("<span>" + opts.ellipse_text + "</span>").appendTo(panel);
                }
                var begin = Math.max(np - opts.num_edge_entries, interval[1]);
                for (var i = begin; i < np; i++) {
                    appendItem(i);
                }

            }
            // Generate "Next"-Link
            if (opts.next_text && (current_page < np - 1 || opts.next_show_always)) {
                appendItem(current_page + 1, { text: opts.next_text, classes: "next bor" });
            }
            // 生成末页按钮
            if (opts.next_text && opts.last_show_always) {
                appendItem(np, { text: opts.last_text, classes: "next br" });
            }
            // 生成跳转页面按钮
            if (opts.goto_show_always) {
                panel.append('<span style="margin-top:-10px;_margin-top:-5px;" id="go-span">跳转到<input type="text" id="go-page" style="width:20px;height:15px;margin-top:9px;_margin-top:1px;margin-left:5px;margin-right:7px;" /></span>');
                // 绑定跳转事件
                $('<input type="button" id="btn-go-page" class="btn btn-small btn-primary"  style="_width:20px;_padding:0 5px;" value="GO" />').on('click', function () {
                    var goPage = $('#go-page').val();
                    if (goPage == '') {
                        alert("请输入需要跳转的页数！");
                        return;
                    }
                    else if (isNaN(goPage) || !/^\d+$/.test(goPage)) {
                        alert("输入不符合要求！请输入正整数");
                        return;
                    }
                    else if (goPage < 1) {
                        alert("输入页码不得小于1，请重新输入！");
                    }
                    else if (parseInt(goPage) <= parseInt(np)) {
                        pageSelected(goPage - 1);
                    }
                    else {
                        alert("输入页码超出最大页数，请重新输入");
                    }
                }).appendTo($('#go-span'));
            }
            //生成分页数据信息
            if (opts.data_info__show_always) {
                panel.append('<span style="float:right">共' + maxentries + '条数据</span>');
                panel.append('<span style="float:right">每页' + opts.items_per_page + '条数据</span>');
                panel.append('<span style="float:right">共' + np + '页</span>');
                panel.append('<span class="current-page" style="float:right">当前第1页</span>');
            }
        }

        // Extract current_page from options
        var current_page = opts.current_page;
        // Create a sane value for maxentries and items_per_page
        maxentries = (!maxentries || maxentries < 0) ? 1 : maxentries;
        opts.items_per_page = (!opts.items_per_page || opts.items_per_page < 0) ? 1 : opts.items_per_page;
        // Store DOM element for easy access from all inner functions
        var panel = jQuery(this);
        // Attach control functions to the DOM element
        this.selectPage = function (page_id) { pageSelected(page_id); }
        this.prevPage = function () {
            if (current_page > 0) {
                pageSelected(current_page - 1);
                return true;
            }
            else {
                return false;
            }
        }
        this.nextPage = function () {
            if (current_page < numPages() - 1) {
                pageSelected(current_page + 1);
                return true;
            }
            else {
                return false;
            }
        }
        // When all initialisation is done, draw the links
        drawLinks();
        // call callback function
        opts.callback(current_page, this);
    });
}



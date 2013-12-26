var headerHeight = $('#header').height();
var footerHeight = $('#footer').height();
var visibleHeight = document.body.clientHeight;
var left = document.getElementById('left');
left.style.height = frame.style.height = visibleHeight - headerHeight - footerHeight + "px";

/******************链接标签工具栏（可用，效果有待加强）*****************************/

/*function clearActive(obj) {
    var currentActive = obj.find('.tab-active');
    if (currentActive.length > 0) {
        currentActive.eq(0).removeClass('tab-active');
    }
}

var tabBar = $('.tab-ul');
var mainLi = tabBar.find('li').eq(0);
var distance = mainLi.outerWidth(true);
var tabToFrameOffset = tabBar.cssInt('left');
//var deleteCount = 0;

$('#left ul').find('li').on('click', function () {
    clearActive(tabBar);
    var tabText = $(this).text();
    if (tabBar.text().indexOf(tabText) == -1) {
        var href = $(this).find('a').attr('href');
        //<button type=\"button\" class=\"close\">×</button>
        $("<li class=\"tab-active\"><a target=\"content\" href=\"" + href + "\" class=\"tab-link\">" + tabText + "</a></li>").bind('click', function (e) {
            if (e.target.tagName == 'A') {
                clearActive(tabBar);
                $(this).addClass('tab-active');
            }
            //if (e.target.tagName == 'BUTTON') {
            //    $(this).hide();
            //    var tabLeft = tabBar.cssInt('left');
            //    if (tabLeft > 0 &&
            //        tabBar.width() + tabToFrameOffset < $('#frame').width()) {
            //        $('.tab-arrow').hideIfShow();
            //    }
            //    ++deleteCount
            //    $('.tab-right-arrow').hideIfShow();
            //}
        }).appendTo(tabBar);
    }
    else {
        tabBar.find('a:contains("' + tabText + '")').parent().addClass('tab-active').showIfHide();
    }

    if (tabBar.width() + tabToFrameOffset > $('#frame').width()) {
        var move = tabBar.width() + tabToFrameOffset - $('#frame').width();
        tabBar.animate({ left: -move }, 'fast', function () {
            $('.tab-arrow').show();
        });
    }
});

$('.tab-left-arrow').on('click', function () {
    var tabLeft = tabBar.cssInt('left');
    if (tabLeft < 0) {
        if (tabLeft + distance > tabToFrameOffset) {
            tabBar.animate({ left: tabToFrameOffset }, 'fast');
            if (tabBar.width() + tabToFrameOffset < $('#frame').width()) {
                $('.tab-arrow').hideIfShow();
            }
        }
        else {
            tabBar.animate({ left: tabLeft + distance }, 'fast');
        }
    }
});

$('.tab-right-arrow').on('click', function () {
    var move;
    var tabLeft = tabBar.cssInt('left');
    //if (deleteCount == 0) {
    var arrowRightLeft = $(this).offset().left;
    var currentIndex = Math.floor((arrowRightLeft - tabLeft) / distance);
    var liLength = tabBar.find('li:visible').length;
    if (currentIndex > liLength) return;
    if (currentIndex == liLength) {
        move = tabBar.find('li').eq(currentIndex - 1).offset().left + distance - arrowRightLeft;
        tabBar.animate({ left: tabLeft - move }, 'fast');
    }
    else {
        move = tabBar.find('li').eq(currentIndex).offset().left - arrowRightLeft;
        tabBar.animate({ left: tabLeft - move }, 'fast');
    }
    //}
    //else {
    //    var arrowRightLeft = $(this).offset().left;
    //    var currentIndex = Math.floor((arrowRightLeft - tabLeft) / distance);
    //    var liLength = tabBar.find('li:visible').length;
    //    currentIndex -= deleteCount;
    //    if (currentIndex >= liLength) return;
    //    if (tabLeft + distance > tabToFrameOffset) {
    //        move = $('.tab-ul').find('li:visible').eq(currentIndex).offset().left - arrowRightLeft + 10;
    //              tabBar.animate({ left: -move }, 'fast');
    //    }
    //    else {
    //        move = $('.tab-ul').find('li:visible').eq(currentIndex).offset().left - arrowRightLeft;
    //        tabBar.animate({ left: tabLeft + move }, 'fast');
    //    }
    //}
});*/
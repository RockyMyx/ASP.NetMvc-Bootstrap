//用于在编辑时将当前表格行内容变成可编辑的表单时进行验证
var mValidate = {
    notEmpty: function (value) {
        return !/^\s*$/.test(value);
    },
    name: function (value) {
        return /^[\w]{6,15}$/.test(value);
    },
    pwd: function (value) {
        return /^.{6,15}$/.test(value);
    },
    pwdLevel: function (value) {
        var charMode = function (c) {
            if (c >= 48 && c <= 57) //数字
                return 1;
            else if (c >= 65 && c <= 90) //大写字母
                return 2;
            else if (c >= 97 && c <= 122) //小写
                return 2;
            else
                return 4; //特殊字符
        }
        var modes, level = 0;
        for (var i = 0; i < value.length; i++) {
            modes |= charMode(value.charCodeAt(i));
        }
        for (var i = 0; i < 3; i++) {
            if (modes & 1) {
                level++;
            }
            modes >>>= 1;
        }
        return level;
    },
    email: function (value) {
        return /^([-_A-Za-z0-9\.]+)@([_A-Za-z0-9]+\.)+[A-Za-z0-9]{2,3}$/.test($.trim(value));
    },
    date: function (value, separator) {
        value = $.trim(value);
        if (value.length == 0) return false;
        //年月日之间的分隔符，现只支持为'-'或'/'
        if (validationInit.dateMode == 'ymd') {
            if (separator == '/') {
                var reg = /^(1|2)([\d]){3}\/[\d]{2}\/[\d]{2}/;
                if (!reg.test(value)) return false;
            }
            else {
                var reg = /^(1|2)([\d]){3}-[\d]{2}-[\d]{2}/;
                if (!reg.test(value)) return false;
            }
        }
        else if (validationInit.dateMode == 'mdy') {
            if (separator == '/') {
                var reg = /^[\d]{2}\/[\d]{2}\/(1|2)([\d]){3}/;
                if (!reg.test(value)) return false;
            }
            else {
                var reg = /^[\d]{2}-[\d]{2}-(1|2)([\d]){3}/;
                if (!reg.test(value)) return false;
            }
        }
        var formatDate = value.replace(/(-|\/)/g, '');
        var year = formatDate.substr(0, 4),
                month = formatDate.substr(4, 2),
                day = formatDate.substr(6, 2);
        return checkDate(year, month, day);
    },
    number: function (value) {
        return $.isNumeric($.trim(value));
    },
    integer: function (value) {
        return /^-?[1-9]\d*$/.test($.trim(value));
    },
    letter: function (value) {
        return /^[A-Za-z]+$/.test($.trim(value));
    },
    chinese: function (value) {
        var len = value.length, value = $.trim(value);
        for (var i = 0; i < len; i++) {
            if (value.charCodeAt(i) < 255)
                return false;
        }
        return true;
    },
    age: function (value) {
        return this.integer(value) && (parseInt(value) > 0 || parseInt(value) < 150);
    },
    phone: function (value) {
        return /^(([0\+]\\d{2,3}-)?(0\d{2,3})-)?(\d{7,8})(-(\d{3,}))?$/.test($.trim(value));
    },
    mobile: function (value) {
        return /^(13|14|15|18)[0-9]{9}$/.test($.trim(value));
    },
    qq: function (value) {
        return /^[1-9]\d{4,9}$/.test($.trim(value));
    },
    cid: function (value) {
        value = $.trim(value.toUpperCase());
        if (!(/(^\d{15}$)|(^\d{17}([0-9]|X)$)/.test(value))) return false;

        var len = value.length;
        var reg;
        var year, month, day;
        if (len == 15) {
            reg = new RegExp(/^(\d{6})(\d{2})(\d{2})(\d{2})(\d{3})$/);
            var arrSplit = value.match(reg);

            var year = '19' + arrSplit[2],
                    month = arrSplit[3],
                    day = arrSplit[4];
            if (!checkDate(year, month, day)) {
                return false;
            }
            else {
                //将15位身份证转成18位
                return value.substr(0, 6) + '19' + value.substr(6, 9) + 'X';
            }
        }
        if (len == 18) {
            reg = new RegExp(/^(\d{6})(\d{4})(\d{2})(\d{2})(\d{3})([0-9]|X)$/);
            var arrSplit = value.match(reg);

            var year = arrSplit[2],
                    month = arrSplit[3],
                    day = arrSplit[4];
            if (!checkDate(year, month, day))
                return false;
            else {
                //校验位按照ISO 7064:1983.MOD 11-2的规定生成，X可以认为是数字10。
                var arrInt = new Array(7, 9, 10, 5, 8, 4, 2, 1, 6, 3, 7, 9, 10, 5, 4, 2);
                var arrCh = new Array('1', '0', 'X', '9', '8', '7', '6', '5', '4', '3', '2');
                var temp = 0, i;
                for (i = 0; i < 17; i++) {
                    temp += value[i] * arrInt[i];
                }
                if (arrCh[temp % 11] != value.substr(17, 1)) {
                    return false;
                }
            }
        }
        return true;
    },
    compress: function (value) {
        return /(.+)\.(rar|zip|tgz|7zip|7z)$/.test($.trim(value));
    },
    pic: function (value) {
        return /(.+)\.(jpg|jpeg|gif|png|ico)/.test($.trim(value));
    },
    url: function (value) {
        return /(https?|ftp|mms):\/\/([A-z0-9]+[_\-]?[A-z0-9]+\.)*[A-z0-9]+\-?[A-z0-9]+\.[A-z]{2,}(\/.*)*\/?/.test($.trim(value));
    }
};

var validationTip = {
    notEmpty: '内容不得为空',
    name: '用户名长度在6到15位之间,且只能由字母、数字和_组成',
    pwd: '密码长度必须在6到15位之间',
    email: '邮箱输入格式有误，请检查后重新输入',
    date: '输入的日期格式不正确，请检查后重新输入',
    number: '您输入了非法字符，请输入纯数字',
    integer: '您输入了非法字符，请输入整数',
    letter: '您输入了非法字符，请输入纯英文字母',
    chinese: '您输入了非法字符，请输入纯中文',
    age: '年龄不得为空, 请输入0-150之间的整数',
    phone: '不是有效的座机电话，请检查后重新输入',
    mobile: '不是有效的手机号，请检查后重新输入',
    qq: '不是有效的QQ号，请检查后重新输入',
    cid: '不是有效的身份证号，请检查后重新输入',
    compress: '不是有效的压缩包文件，请检查后重新上传',
    pic: '不是有效的图片文件，请检查后重新上传',
    url: '个人主页输入格式有误，请检查后重新输入'
};
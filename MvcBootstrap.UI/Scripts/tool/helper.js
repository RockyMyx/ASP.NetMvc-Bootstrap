jQuery.fn.alternateRowColors = function () {
    $("tbody tr:odd", this).removeClass("even").addClass("odd");
    $("tbody tr:even", this).removeClass("odd").addClass("even");
    return this;
}

jQuery.fn.showIfHide = function () {
    if (this.is(':hidden')) {
        this.show();
    }
}

jQuery.fn.hideIfShow = function () {
    if (this.is(':visible')) {
        this.hide();
    }
}

jQuery.fn.cssInt = function (prop) {
    return parseInt(this.css(prop), 10) || 0;
}

jQuery.fn.cssFloat = function () {
    return parseFloat(this.css(prop), 10) || 0;
}

Array.prototype.contains = function () {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == arguments[0]) {
            return true;
        }
    }
    return false;
};

Array.prototype.remove = function (s) {
    if (this.indexOf(s) !== -1) {
        this.splice(this.indexOf(s), 1);
    }
}

String.prototype.capitalize = function () {
    return this.replace(/^\w/, function (s) {
        return s.toUpperCase();
    });
};

String.prototype.equal = function (str1, str2) {
    return str1.localeCompare(str2) == 0;
};

String.prototype.format = function () {
    if (arguments.length == 0) return "";
    var formatter = this;
    for (var i = 0; i < arguments.length; i++) {
        formatter = formatter.replace(new RegExp("\\{" + i + "\\}"), arguments[i]);
    }
    return formatter;
};
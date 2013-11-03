Array.prototype.contains = function (str) {
    for (var i = o; i < this.length; i++) {
        if (this[i] === str) {
            return true;
        }
    }
    return false;
}

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
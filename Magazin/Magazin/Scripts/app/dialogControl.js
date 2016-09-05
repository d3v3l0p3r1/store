var BaseDialogControl = kendo.Class.extend({
    init: function (id, desc) {
        this.id = id;
        this.desc = desc;
    },

    onControlFire: function (e) { },

    getUrlParametr: function (uri, key) {
        var pattern = '([?|&])' + key + '=.*?(&|$)';
        var re = new RegExp(pattern, 'i');
        var separator = uri.indexOf('?') !== -1 ? '&' : '?';
        var match = uri.match(re);
        if (match) {
            return match;
        }
        return null;
    },

    replaceUrlParametr: function (uri, key, value) {
        var pattern = '([?|&])' + key + '=.*?(&|$)';
        var re = new RegExp(pattern, 'i');
        var separator = uri.indexOf('?') !== -1 ? '&' : '?';

        if (uri.match(re)) {
            return uri.replace(re, '$1' + key + '=' + value + '$2');
        }

        return uri + separator + key + '=' + value;
    },

    resize: function () {}
});

var DialogControl = BaseDialogControl.extend({
    controls: {},
    init: function (id) {
        this.id = id;
        this.desc = "dialogControl";
        BaseDialogControl.fn.init.call(this, id, this.desc);
    },
    addControl: function (control) {
        if (control !== this) {
            this.controls[control.id] = control;


        }
    },

    controlFire: function (e) {
        for (var id in this.controls) {
            if (this.controls[id] !== e.sender) {
                this.controls[id].onControlFire(e);
            }
        }
    },

    resizeChilds: function () {
        for (var id in this.controls) {
            this.controls[id].resize();
        }
    }

});

var SimpleControl = BaseDialogControl.extend({
    init: function (id, desc) {
        BaseDialogControl.fn.init.call(this, id, desc);
    }
});
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

    resize: function (h) { }
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

    resizeChilds: function (h) {  //container height
        for (var id in this.controls) {
            this.controls[id].resize(h);
        }
    }

});

var SimpleControl = BaseDialogControl.extend({
    init: function (id, desc) {
        BaseDialogControl.fn.init.call(this, id, desc);
    }
});

var GridControl = SimpleControl.extend({
    init: function (id, desc) {
        SimpleControl.fn.init.call(this, id, desc);
    },

    changeCategory: function (cat) {
        var grid = $('#' + this.id).data('kendoGrid');
        var url = grid.dataSource.transport.options.read.url;

        grid.dataSource.transport.options.read.url = this.replaceUrlParametr(url, 'categoryId', cat.Id);
        grid.dataSource.read();
    },

    resize: function (h) {
        var $grid = $('#' + this.id);
        var $dataArea = $grid.find(".k-grid-content");

        h = h - ($grid.outerHeight() - $dataArea.outerHeight(true));

        $dataArea.height(h);
    },

    getSelectedItem: function () {
        var grid = $('#' + this.id).data('kendoGrid');
        var selected = grid.select();
        return grid.dataItem(selected);
    },

    readDataSource: function() {
        var grid = $('#' + this.id).data('kendoGrid');
        grid.dataSource.read();
    }

});
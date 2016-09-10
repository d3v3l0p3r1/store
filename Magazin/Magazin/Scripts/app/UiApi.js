window.UiApi = window.UiApi || {};


(function () {
    'use strict';

    UiApi.uid = function (prefix) {
        var uid = Math.random().toString(36).slice(2);

        if (prefix) {
            return prefix + '_' + uid;
        }

        return uid;
    };

    UiApi.openModalWindow = function (params, callback) {

        var _params = $.extend({
            contentUrl: null,
            width: 900,
            height: 600,
            title: null
        }, params);

        var wid = UiApi.uid("wid");

        $('body').append('<div id="' + wid + '"></div>');

        var $w = $('#' + wid);

        var wnd = $w.kendoWindow({
                width: _params.width,
                height: _params.height,
                content: _params.contentUrl,
                title: _params.title,
                close: function (e) {                    
                    callback();
                    this.destroy();
                }
            })
            .data('kendoWindow');

        wnd.center().open();
    }
})()
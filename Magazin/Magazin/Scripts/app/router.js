define([],
    function () {
        var router = new kendo.Router(),
            layout = new kendo.Layout("<div id='content'></div>");


        var loadView = function (viewModel, view, delegate) {

            var kendoView = new kendo.View(view, { model: viewModel });

            $('#content').empty();
            layout.showIn("#content", kendoView);

            if (delegate != undefined)
                delegate();
        };



        layout.render($("#app"));

        router.route("/Admin/Home/Index", function () {
            require(['text!/admin/home/index'], function (view) {
                loadView(null, view);
            });
        });

        router.route("/Admin/Product/Index", function () {
            require([
                'text!/admin/product/index'],
                function (view) {
                    loadView(null, view);
                });
        });

        router.route("/Admin/InCome/Index", function () {
            require([
                'text!/admin/income/index'],
                function (view) {
                    loadView(null, view);
                });
        });

        router.route("/Admin/Balance/Index", function () {
            require([
                'text!/admin/balance/index'],
                function (view) {
                    loadView(null, view);
                });
        });

        router.route("/", function () {
            require(['text!/Product/index'], function (view) {
                loadView(null, view)
            });
        });

        router.route("/Product/index", function () {
            require(['text!/Product/index'], function(view){
                loadView(null, view)
            });
        });

        router.route


        return router;
    });
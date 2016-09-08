define([],
    function () {
        var router = new kendo.Router(),
            layout = new kendo.Layout("<div id='content'></div>");


        var loadView = function (viewModel, view, delegate) {
            console.log('loadView');
            var kendoView = new kendo.View(view, { model: viewModel });
            kendo.fx($("#content")).slideInRight().reverse().then(function () {
                $('#content').empty();
                layout.showIn("#content", kendoView);

                if (delegate != undefined)
                    delegate();

                kendo.fx($("#content")).slideInRight().play();
            });
        };


        
        layout.render($("#app"));

        router.route("/Admin/Home/Index", function () {
            require(['text!/admin/home/index'], function (view) {
                loadView(null, view);
            });
        });

        router.route("/Admin/Product/Products", function () {            
            require([
                'text!/admin/product/products'],
                function (view) {
                    loadView(null, view);
                });
        });

        router.route("/Admin/Balance/Balances", function () {
            require([
                'text!/Admin/Balance/Balances'],
                function (view) {
                    loadView(null, view);
                });
        });

   
        return router;
    });
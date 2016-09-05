define([],
    function () {    
        var router = new kendo.Router(),
            layout = new kendo.Layout("<div id='content'></div>");

        
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

        var loadView = function (viewModel, view, delegate) {
            var kendoView = new kendo.View(view, { model: viewModel });
            kendo.fx($("#content")).slideInRight().reverse().then(function () {
                $('#content').empty();
                layout.showIn("#content", kendoView);

                if (delegate != undefined)
                    delegate();

                kendo.fx($("#content")).slideInRight().play();
            });
        };


        return router;
    });
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

            //kendo.fx($("#content")).flipHorizontal($('#content'), $('#content')).duration(1000).play(); todo если время будет перерисовать

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

        router.route("/Admin/Balance/Balances", function () {
            require([
                'text!/Admin/Balance/Balances'],
                function (view) {
                    loadView(null, view);
                });
        });


        return router;
    });
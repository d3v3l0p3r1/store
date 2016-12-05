require.config({
    paths: {
        // Пакеты       
        'jquery': "http://code.jquery.com/jquery-1.9.1.min",
        'jszip' : '/Scripts/vendor/jszip.min',
        'kendo': '/Scripts/vendor/kendo.all.min',
        'text': '/Scripts/text',
        'router': '/Scripts/app/router',
        
        //VM
        'productModel': '/Scripts/app/viewmodel/productModel',
        'bascetModel': '/Scripts/app/viewmodel/bascetModel',
        'bascetProductModel': '/Scripts/app/viewmodel/bascetProductModel',
        'bascetViewModel': '/Scripts/app/viewmodel/bascetViewModel'
    },
    shim: {
       'kendo' : { deps: ['jquery'] }
    },
    priority: ['text', 'router', 'app'],    
    waitSeconds: 30
});

require(['app', 'kendo'], function (app) {
    app.initialize();
});
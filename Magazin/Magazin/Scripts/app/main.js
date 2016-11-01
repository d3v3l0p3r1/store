require.config({
    paths: {
        // Пакеты       
        'jquery': '/scripts/vendor/jquery',
        'kendo': '/scripts/vendor/kendo.all',
        'text': '/scripts/text',
        'router': '/scripts/app/router'
    },    
    priority: ['text' ,'router', 'app'],
    jquery: '3.1.0'    
});
require([
    'app'    
], function (app) {
    app.initialize();
});
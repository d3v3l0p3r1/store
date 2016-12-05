define(['kendo', 'bascetProductModel'], function (kendo, bascetProductModel) {
    debugger;
    var vm = kendo.data.Model.define({
        products: [bascetProductModel]
    });

    return vm;

});
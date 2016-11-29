define(['kendo', 'bascetProductModel'], function(kendo, bascetProductModel) {
    var vm = kendo.data.Model.define({
        products: [bascetProductModel]
    });

    return vm;

});
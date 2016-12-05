define(['kendo', 'productModel'], function (kendo, productModel) {
    debugger;
    var bascetProduct = kendo.data.Model.define({
        product: productModel,
        count: number
    });

    return bascetProduct;
});
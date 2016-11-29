define(['kendo', 'productModel'], function (kendo, productModel) {
    var bascetProduct = kendo.data.Model.define({
        product: productModel,
        count: number
    });

    return bascetProduct;
});
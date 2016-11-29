define(['kendo', 'bascetModel'], function (kendo, bascetModel) {

    var bascetViewModel = kendo.data.Model.define({
        products: bascetModel,
        phone: string,
        address: string
    });

    return bascetViewModel;
});


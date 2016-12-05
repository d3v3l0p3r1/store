define(['kendo'], function (kendo) {
        debugger;
        var productModel = kendo.data.Model.define({
            price: number,
            title: string,
            description: string
        });

        return productModel;
    });

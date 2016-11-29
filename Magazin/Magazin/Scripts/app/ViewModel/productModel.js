define(['kendo'],
    function (kendo) {
        var productModel = kendo.data.Model.define({
            price: number,
            title: string,
            description: string
        });

        return productModel;
    });

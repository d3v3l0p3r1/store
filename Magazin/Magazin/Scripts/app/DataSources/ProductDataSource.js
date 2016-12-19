/// <reference path="../../typings/jquery/jquery.d.ts"/>
/// <reference path="../../typings/kendo/kendo.all.d.ts"/>
/// <reference path="../Models/ProductModel.ts"/>
import { ProductModel } from "../Models/ProductModel";
export class ProductDataSource extends kendo.data.DataSource {
    constructor(url, options) {
        super();
        const readUrl = url;
        options = $.extend({
            transport: {
                read: {
                    url: readUrl,
                    dataType: "json"
                }
            },
            schema: {
                model: ProductModel
            },
            sort: {
                field: "Id",
                dir: "desc"
            },
            serverPaging: true,
            pageSize: 20,
            serverFiltering: true,
            serverSorting: true
        }, options);
        super(options);
        this.transport.parameterMap = this.getparamap.bind(this);
    }
    getparamap(data, operation) {
        return data;
    }
    ;
}
//# sourceMappingURL=ProductDataSource.js.map
/// <reference path="../../typings/jquery/jquery.d.ts"/>
/// <reference path="../../typings/kendo/kendo.all.d.ts"/>
/// <reference path="../Models/ProductModel.ts"/>

import Model = kendo.data.Model;
import DataSourceOptions = kendo.data.DataSourceOptions;
import {ProductModel} from "../Models/ProductModel";


export class ProductDataSource extends kendo.data.DataSource {

    constructor(url: string | string[], options?: DataSourceOptions) {
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

        (this as any).transport.parameterMap = this.getparamap.bind(this);
    }

    protected getparamap(data, operation): Object {
        return data;
    };
}
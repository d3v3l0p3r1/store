﻿/// <reference path="../../typings/kendo/kendo.all.d.ts"/>
import Model = kendo.data.Model;
const ProductModelDefinition: typeof Model = Model.define({
    id: "Id",
    fields: {
        Id: {
            field: "Id",
            type: "number",
            defaultValue: 0
        },
        Title: {
            field: "Title",
            type: "string"
        },
        Description: {
            field: "Description",
            type: "string"
        },
        Price: {
            field: "Price",
            type: "number"
        }
    }
});


export class ProductModel extends ProductModelDefinition {

}
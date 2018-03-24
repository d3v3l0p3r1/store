import { Product } from "../../models/Product"
import { Action } from "redux"

export interface IProductGridState {
    readonly currentPage: number;
    readonly itemsOnPage: number;
    readonly products: ReadonlyArray<Product>;
}


export enum ProductGridActionKeys {
    NextPage = "NEXT_PAGE",
    PrevPage = "PREV_PAGE",
    ReadProducts = "READ_PRODUCTS"
}

export interface INextPageAction extends Action {
    readonly type: ProductGridActionKeys.NextPage;
    readonly payload: ReadonlyArray<Product>;
}

export interface IPrevPageAction extends Action {
    readonly type: ProductGridActionKeys.PrevPage;
    readonly payload: ReadonlyArray<Product>;
}

export interface IReadProductAction extends Action {
    readonly type: ProductGridActionKeys.ReadProducts;
    readonly payload: ReadonlyArray<Product>;
}

export type GridActions = INextPageAction | IPrevPageAction | IReadProductAction;
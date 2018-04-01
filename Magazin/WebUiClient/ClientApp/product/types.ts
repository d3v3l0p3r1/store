import { Product } from "../models/Product"
import { Action } from "redux"

export interface IProductGridState {
    readonly products: ReadonlyArray<Product>;
    readonly isBusy: boolean;
    readonly errorMessage: string;
}




export enum ProductGridActionKeys {
    ReadProducts = "READ_PRODUCTS",
    Fetching = "FETCHING",
    Error = "ERROR"
}

export interface IFetchingAction extends Action {
    readonly type: ProductGridActionKeys.Fetching,
    readonly payload: true,
}

export interface IErrorAction extends Action {
    readonly type: ProductGridActionKeys.Error,
    readonly payload: string,
}

export interface IReadProductAction extends Action {
    readonly type: ProductGridActionKeys.ReadProducts;
    readonly payload: ReadonlyArray<Product>;
}

export type GridActions = IReadProductAction | IFetchingAction | IErrorAction;
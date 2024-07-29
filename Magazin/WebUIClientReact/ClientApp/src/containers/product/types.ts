import Product from "../../models/Product"
import { Action } from "redux"
import { ApiActionKeys } from "../../stores/ApiActionKeys"

export interface IProductGridState {
    readonly products: ReadonlyArray<Product>;
    readonly isBusy: boolean;
    readonly errorMessage: string;
}

export interface IFetchingAction extends Action {
    readonly type: ApiActionKeys.Product_Fetching,
    readonly payload: true,
}

export interface IErrorAction extends Action {
    readonly type: ApiActionKeys.Product_Error,
    readonly payload: string,
}

export interface IReadProductAction extends Action {
    readonly type: ApiActionKeys.Product_Read;
    readonly payload: ReadonlyArray<Product>;
    readonly category: number;
}

export type GridActions = IReadProductAction | IFetchingAction | IErrorAction;
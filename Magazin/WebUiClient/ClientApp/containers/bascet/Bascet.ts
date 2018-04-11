import { Product } from "../../models/Product"
import { Action } from "redux"
import {ApiActionKeys} from "../../stores/ApiActionKeys"

export interface IBascetState {
    readonly products: ReadonlyArray<IBascetItem>;
}

export interface IBascetItem {
    product: Product;
    count: number;
}

export interface IAddToCardAction extends Action {
    readonly type: ApiActionKeys.Card_Add,
    readonly payload: Product,
}

export interface IRemoveFromCardAction extends Action {
    readonly type: ApiActionKeys.Card_Remove,
    readonly payload: number,
}


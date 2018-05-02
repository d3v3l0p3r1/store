import { Product } from "../../models/Product"
import { Action } from "redux"
import {ApiActionKeys} from "../../stores/ApiActionKeys"



export interface IAddToCardAction extends Action {
    readonly type: ApiActionKeys.Card_Add,
    readonly payload: Product,
}

export interface IRemoveFromCardAction extends Action {
    readonly type: ApiActionKeys.Card_Remove,
    readonly payload: number,
}

export interface IRemoveAllProduct extends Action {
    readonly type: ApiActionKeys.Card_Remove_Product,
    readonly payload: number,
}


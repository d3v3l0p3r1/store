import { ActionCreator, Dispatch } from "redux"
import { IAddToCardAction, IRemoveFromCardAction } from "./Bascet"
import { Product } from "../../models/Product"
import { ApiActionKeys } from "../../stores/ApiActionKeys"


export const addToCard: ActionCreator<IAddToCardAction> = (product: Product) => {
    return {
        type: ApiActionKeys.Card_Add,
        payload: product
    }
}

export const removeFromCard: ActionCreator<IRemoveFromCardAction> = (id: number) => {
    return {
        type: ApiActionKeys.Card_Remove,
        payload: id
    }
}
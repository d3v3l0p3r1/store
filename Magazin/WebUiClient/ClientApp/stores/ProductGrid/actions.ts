import { ActionCreator } from "redux"
import { INextPageAction, IPrevPageAction, IReadProductAction, ProductGridActionKeys } from "./types"
import { Product } from "../../models/Product"


export const nextPage: ActionCreator<INextPageAction> = (products: ReadonlyArray<Product>) => ({
    type: ProductGridActionKeys.NextPage,
    payload: products
});

export const prevPage: ActionCreator<IPrevPageAction> = (products: ReadonlyArray<Product>) => ({
    type: ProductGridActionKeys.PrevPage,
    payload: products
});

export const readData: ActionCreator<IReadProductAction> = (products: ReadonlyArray<Product>) => ({
    type: ProductGridActionKeys.ReadProducts,
    payload: products
});




import Product from "../../models/Product"

export interface IBascetState {
    readonly products: ReadonlyArray<IBascetItem>;
    readonly totalPrice: number;
    readonly totalCount: number;
}

export interface IBascetItem {
    product: Product;
    count: number;
}
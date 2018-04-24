﻿import { Reducer } from "redux"
import { IBascetState, IBascetItem, IAddToCardAction, IRemoveFromCardAction, IRemoveAllProduct } from "./Bascet"
import { ApiActionKeys } from "../../stores/ApiActionKeys"


export const initialState: IBascetState = {
    products: new Array(),
    total: 0
}


const AddToCard = (state = initialState.products, action: IAddToCardAction) => {

    var exist = state.filter((x) => x.product.id === action.payload.id);

    var item: IBascetItem;
    if (exist.length > 0) {
        item = exist[0];
        item.count += 1;
        return state.slice();
    }

    item = { count: 1, product: action.payload };

    return state.concat(item);
}

const RemoveFromCard = (state = initialState.products, action: IRemoveFromCardAction) => {
    var exist = state.filter((x) => x.product.id === action.payload);

    var item: IBascetItem;
    if (exist.length > 0) {
        item = exist[0];
        item.count -= 1;

        if (item.count === 0) {
            return state.filter(x => x.product.id !== action.payload);
        }
    }

    return state.slice();
}

const Calculate = (state = initialState.products) => {
    var s = state.map(x => x.product.price * x.count).reduce((a, b) => a + b, 0);
    return s;
}

const RemoveProductFormCard = (state = initialState.products, action: IRemoveAllProduct) => {
    return state.filter(x => x.product.id !== action.payload);
}

export const bascetReducer: Reducer<IBascetState> = (state: IBascetState = initialState, action) => {
    var items = state.products;
    var total = state.total;

    switch (action.type) {
        case ApiActionKeys.Card_Add:
            {
                items = AddToCard(state.products, action as IAddToCardAction);
                break;
            }
        case ApiActionKeys.Card_Remove:
            {
                items = RemoveFromCard(state.products, action as IRemoveFromCardAction);
                break;
            }
        case ApiActionKeys.Card_Remove_Product:
            {
                items = RemoveProductFormCard(state.products, action as IRemoveAllProduct);
                break;
            }
    }

    total = Calculate(items);    

    return { ...state, products: items, total: total };

}
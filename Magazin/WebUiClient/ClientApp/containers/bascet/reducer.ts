import { Reducer } from "redux"
import { IBascetState, IBascetItem, IAddToCardAction, IRemoveFromCardAction } from "./Bascet"
import { ApiActionKeys } from "../../stores/ApiActionKeys"


export const initialState: IBascetState = {
    products: new Array()
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

export const bascetReducer: Reducer<IBascetState> = (state: IBascetState = initialState, action) => {
    
    switch (action.type) {
        case ApiActionKeys.Card_Add:
            {                
                state = { ...state, products: AddToCard(state.products, action as IAddToCardAction) };
                console.log(state);
                break;
            }
        case ApiActionKeys.Card_Remove:
            {                
                state = { ...state, products: RemoveFromCard(state.products, action as IRemoveFromCardAction) };
                console.log(state);
                break;
            }
    }
    
    return state;

}
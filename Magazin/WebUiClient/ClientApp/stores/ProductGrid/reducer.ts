import { Reducer } from 'redux';
import { IProductGridState, GridActions, ProductGridActionKeys } from "./types";

export const initialState: IProductGridState = {
    currentPage: 0,
    itemsOnPage: 20,
    products: []
};

export const productGridReducer: Reducer<IProductGridState> = (state: IProductGridState = initialState, action) => {
    switch ((action as GridActions).type) {
        case ProductGridActionKeys.NextPage:
            return { ...state, currentPage: state.currentPage + 1 , products: action.payload};
        case ProductGridActionKeys.PrevPage:
            return { ...state, currentPage: state.currentPage - 1 , products: action.payload};
        case ProductGridActionKeys.ReadProducts:
            return { ...state , products : action.payload};
        default:
            return state;
    }
};


export default productGridReducer;
import { Reducer } from 'redux';
import { IProductGridState, GridActions, ProductGridActionKeys, IReadProductAction, IFetchingAction, IErrorAction } from "./types";

export const initialState: IProductGridState = {
    products: [],
    isBusy: false,
    errorMessage: "",
};

export const productGridReducer: Reducer<IProductGridState> = (state: IProductGridState = initialState, action) => {
    switch ((action as GridActions).type) {

        case ProductGridActionKeys.ReadProducts:
            {
                var payload = (action as IReadProductAction).payload;
                var array = payload;

                return { ...state, products: array, isBusy: false, errorMessage: "" };
            }

        case ProductGridActionKeys.Fetching:
            {
                return { ...state, products: [], isBusy: true, errorMessage: "" }
            }
        case ProductGridActionKeys.Error:
            {
                var error = (action as IErrorAction).payload;
                return { ...state, products: [], isBusy: false, errorMessage: error }
            }

        default:
            return state;
    }
};




export default productGridReducer;
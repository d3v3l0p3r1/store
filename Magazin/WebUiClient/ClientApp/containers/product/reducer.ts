import { Reducer } from 'redux';
import { IProductGridState, GridActions, IReadProductAction, IErrorAction } from "./types";
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { IChangeCategoryAction } from "../categories/types"

export const initialState: IProductGridState = {
    currentCategory: -1,
    products: [],
    isBusy: false,
    errorMessage: "",
};

export const productGridReducer: Reducer<IProductGridState> = (state: IProductGridState = initialState, action) => {
    var ret: IProductGridState;
    console.log('productGridReducer: ' , action);
    switch (action.type) {
        case ApiActionKeys.Product_Read:
            {
                var array = (action as IReadProductAction).payload;

                ret = { ...state, products: array, isBusy: false, errorMessage: "",};
                break;
            }

        case ApiActionKeys.Product_Fetching:
            {
                ret = { ...state, products: [], isBusy: true, errorMessage: "" };
                break;
            }
        case ApiActionKeys.Product_Error:
            {
                var error = (action as IErrorAction).payload;
                ret = { ...state, products: [], isBusy: false, errorMessage: error };
                break;
            }
        case ApiActionKeys.ChangeProductCategory:
            {
                var payload = (action as IChangeCategoryAction).payload;
                ret = { ...state, products: state.products, isBusy: false, currentCategory: payload };
                break;
            }

        default:
            ret = state;
    }

    return ret;
};

export default productGridReducer;
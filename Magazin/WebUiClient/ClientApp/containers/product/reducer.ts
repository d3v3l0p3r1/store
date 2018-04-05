import { Reducer } from 'redux';
import { IProductGridState, GridActions, IReadProductAction, IErrorAction } from "./types";
import { ApiActionKeys } from "../../stores/ApiActionKeys"

export const initialState: IProductGridState = {
    products: [],
    isBusy: false,
    errorMessage: "",
};

export const productGridReducer: Reducer<IProductGridState> = (state: IProductGridState = initialState, action) => {
    var ret: IProductGridState;

    switch ((action as GridActions).type) {

        case ApiActionKeys.Product_Read:
            {
                var payload = (action as IReadProductAction).payload;
                var array = payload;

                ret = { ...state, products: array, isBusy: false, errorMessage: "" };
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

        default:
            ret = state;
    }

    return ret;
};

export default productGridReducer;
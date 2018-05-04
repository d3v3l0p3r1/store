import { Reducer } from 'redux';
import { IOrderState, initialState } from "./OrderState"
import { ApiActionKeys } from "../../stores/ApiActionKeys"

export const orderReducer: Reducer<IOrderState> = (state: IOrderState = initialState, action) => {

    var ret = state;


    switch (action.type) {
        case ApiActionKeys.Order_Fetching:
            {
                ret = {
                    ...state,
                    fetching: true,
                    error: "",
                    complete: false
                };
                break;
            }
        case ApiActionKeys.Order_Error:
            {
                ret = {
                    ...state,
                    fetching: false,
                    error: action.payload,
                    complete: false
                };
                break;
            }
        case ApiActionKeys.Order_Complete:
            {
                ret = {
                    ...state,
                    fetching: false,
                    error: "",
                    complete: true
                };
                break;
            }            
    }


    return ret;
}


﻿import { Action, ActionCreator, Dispatch } from "redux"
import { ThunkAction } from "redux-thunk"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { IOrderState } from "./OrderState"
import { order } from "../../api"
import {IOrderModel} from "../../models/OrderModel"


export const OrderAction: ActionCreator<ThunkAction<Action, IOrderState, void>> = (model: IOrderModel) => {
    return (dispatch: Dispatch<IOrderState>) => {


        const result = order(model);

        result.then((response) => {

            return dispatch({
                type: ApiActionKeys.Order_Complete,
                payload: response
            });
        });

        result.catch((error) => {
            return dispatch({
                type: ApiActionKeys.Order_Error,
                payload: error
            });
        });
        
        return dispatch({
            type: ApiActionKeys.Order_Fetching,
            payload: true
        });
    }
}
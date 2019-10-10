import { Action, ActionCreator, Dispatch } from "redux"
import { ThunkAction } from "redux-thunk"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { IOrderState } from "./OrderState"
import { order } from "../../api"
import { IOrderModel, Order } from "../../models/OrderModel"
import { OrderActions } from "./types"
import { async } from "q"


export const OrderAction: ActionCreator<ThunkAction<Promise<OrderActions>, Order, null, OrderActions>> = (model: IOrderModel) => {
    return async (dispatch: Dispatch<OrderActions>) => {

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
            type: ApiActionKeys.Order_Fetching
        });
    }
}
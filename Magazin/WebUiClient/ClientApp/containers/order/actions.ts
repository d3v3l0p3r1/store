import { Action, ActionCreator, Dispatch } from "redux"
import { ThunkAction } from "redux-thunk"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { IOrderState } from "./OrderState"
import { IApplicationState } from "../../stores/IApplicationState"
import { order } from "../../api"


export const OrderAction: ActionCreator<ThunkAction<Action, IOrderState, void>> = (state: IApplicationState) => {
    return (dispatch: Dispatch<IOrderState>) => {


        const result = order(state.bascetState, state.userState);

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
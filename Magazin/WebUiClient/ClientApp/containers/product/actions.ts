import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { IProductGridState } from "./types";
import { readProducts } from "../../api"
import { ApiActionKeys } from "../../stores/ApiActionKeys"

export const readData: ActionCreator<ThunkAction<Action, IProductGridState, void>> = (category: number) => {

    return (dispatch: Dispatch<IProductGridState>) => {
        const result = readProducts(category);

        result.then((response) => {

            return dispatch({
                type: ApiActionKeys.Product_Read,
                payload: response,
            });
        });

        result.catch((error) => {
            return dispatch({
                type: ApiActionKeys.Product_Error,
                payload: error
            });
        });

        return dispatch({
            type: ApiActionKeys.Product_Fetching,
            payload: true
        });
    };
};

export const gridActions = {
    readData: readData,
}



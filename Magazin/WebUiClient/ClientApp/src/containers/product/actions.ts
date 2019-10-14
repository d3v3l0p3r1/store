import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { IProductGridState, GridActions } from "./types";
import { readProducts } from "../../api"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import Product from "../../models/Product"
import { async } from "q";

export const readData: ActionCreator<ThunkAction<Promise<GridActions>, Product[], null, GridActions>> = (category: number) => {

    return async (dispatch: Dispatch<GridActions>) => {
        const result = readProducts(category);

        result.then((response) => {

            return dispatch({
                type: ApiActionKeys.Product_Read,
                payload: response,
                category: category
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



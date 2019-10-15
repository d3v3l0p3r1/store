import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { IProductGridState, GridActions } from "./types";
import { readProducts } from "../../api"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import Product from "../../models/Product"

export const readData: ActionCreator<ThunkAction<Promise<GridActions>, Product[], null, GridActions>> = (category: number) => {

    return async (dispatch: Dispatch<GridActions>) => {

        dispatch({
            type: ApiActionKeys.Product_Fetching,
            payload: true
        });

        try {
            const result = await readProducts(category);
            return dispatch({
                type: ApiActionKeys.Product_Read,
                payload: result,
                category: category
            });
        }
        catch (error)
        {
            return dispatch({
                type: ApiActionKeys.Product_Error,
                payload: error
            });
        }
    };
};

export const gridActions = {
    readData: readData,
}



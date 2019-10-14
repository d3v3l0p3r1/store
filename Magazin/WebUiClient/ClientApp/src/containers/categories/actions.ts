﻿import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { ICategoriesState } from "./ICategoriesState"
import { readCategories } from "../../api"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import Category from "../../models/Category";
import { CategoriesActions } from "./types"
import { async } from "q";

export const readData: ActionCreator<ThunkAction<Promise<CategoriesActions>, Category[], null, CategoriesActions>> = () => {

    return async (dispatch: Dispatch<CategoriesActions>) => {
        try {
            const result = await readCategories();

            dispatch({
                type: ApiActionKeys.Category_Read,
                payload: result
            });
        } catch (error) {
            dispatch({
                type: ApiActionKeys.Category_Error,
                payload: error
            });
        }

        return dispatch({ type: ApiActionKeys.Category_Fetching, payload: true });
    };
};

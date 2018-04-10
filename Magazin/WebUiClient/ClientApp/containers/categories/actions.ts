import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { ICategoriesState } from "./ICategoriesState"
import { readCategories } from "../../api"
import { ApiActionKeys } from "../../stores/ApiActionKeys"

export const readData: ActionCreator<ThunkAction<Action, ICategoriesState, void>> = () => {

    return (dispatch: Dispatch<ICategoriesState>) => {
        const result = readCategories();

        result.then((response) => {

            return dispatch({
                type: ApiActionKeys.Category_Read,
                payload: response
            });

        });

        result.catch((error) => {
            return dispatch({
                type: ApiActionKeys.Category_Error,
                payload: error
            });
        });

        return dispatch({ type: ApiActionKeys.Category_Fetching, payload: true });
    };
};
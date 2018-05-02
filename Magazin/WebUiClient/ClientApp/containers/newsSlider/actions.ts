import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { ISliderState } from "./SliderState"
import { readNews } from "../../api"
import {ApiActionKeys} from "../../stores/ApiActionKeys"

export const readNewsAction: ActionCreator<ThunkAction<Action, ISliderState, void>> = () => {

    return (dispatch: Dispatch<ISliderState>) => {
        const result = readNews();

        result.then((response) => {

            return dispatch({
                type: ApiActionKeys.News_Read,
                payload: response
            });

        });

        result.catch((error) => {
            return dispatch({
                type: ApiActionKeys.News_Error,
                payload: error
            });
        });

        return dispatch({ type: ApiActionKeys.News_Fetching, payload: true });
    };
};
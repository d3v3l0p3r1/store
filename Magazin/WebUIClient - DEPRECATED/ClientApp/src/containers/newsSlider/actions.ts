import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { ISliderState } from "./SliderState"
import { readNews } from "../../api"
import { ApiActionKeys } from "../../stores/ApiActionKeys"
import { SliderActions } from "./types"
import News from "../../models/News";
import { async } from "q";

export const readNewsAction: ActionCreator<ThunkAction<Promise<SliderActions>, News[], null, SliderActions>> = () => {

    return async (dispatch: Dispatch<SliderActions>) => {
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
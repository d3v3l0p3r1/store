import { ActionCreator, Dispatch, Action } from "redux";
import { ThunkAction } from "redux-thunk";
import { ISliderState } from "./reducer"
import { readNews } from "../api"
import { SliderActionKeys } from "./types"

export const readData: ActionCreator<ThunkAction<Action, ISliderState, void>> = () => {

    return (dispatch: Dispatch<ISliderState>) => {
        const result = readNews();

        result.then((response) => {

            return dispatch({
                type: SliderActionKeys.Read,
                payload: response
            });

        });

        result.catch((error) => {
            return dispatch({
                type: SliderActionKeys.Error,
                payload: error
            });
        });

        return dispatch({ type: SliderActionKeys.Fetching, payload: true });
    };
};
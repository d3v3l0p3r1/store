import { Reducer } from "redux"
import { News } from "../models/News"
import { SliderActions, SliderActionKeys, IReadNewsAction } from "./types"

export interface ISliderState {
    readonly newsArray: ReadonlyArray<News>;
    readonly isBusy: boolean;
    readonly errorMessage: string;
}

export const initialState: ISliderState = {
    errorMessage: "",
    isBusy: false,
    newsArray: []
};

export const sliderReducer: Reducer<ISliderState> = (state: ISliderState = initialState, action) => {

    switch ((action as SliderActions).type) {
        case SliderActionKeys.Fetching:
            {
                return { ...state, newsArray: [], errorMessage: "", isBusy: true };
            }

        case SliderActionKeys.Error:
            {
                return { ...state, newsArray: [], errorMessage: action.payload };
            }
        case SliderActionKeys.Read:
            {
                var payload = (action as IReadNewsAction).payload;
                return { ...state, newsArray: payload, errorMessage: "" }
            }

        default:
            return state;
    }
}

export default sliderReducer;



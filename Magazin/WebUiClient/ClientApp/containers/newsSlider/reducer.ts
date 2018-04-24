﻿import { Reducer } from "redux"
import { News } from "../../models/News"
import { SliderActions, IReadNewsAction } from "./types"
import { IBaseComponentState } from "../../stores/IBaseComponentState"
import {ApiActionKeys} from "../../stores/ApiActionKeys"

export interface ISliderState extends IBaseComponentState {
    readonly newsArray: ReadonlyArray<News>;
 
}

export const initialState: ISliderState = {
    errorMessage: "",
    isBusy: false,
    newsArray: []
};

export const sliderReducer: Reducer<ISliderState> = (state: ISliderState = initialState, action) => {

    var retState: ISliderState = state;


    switch ((action as SliderActions).type) {
        case ApiActionKeys.News_Fetching:
            {
                retState = { ...state, newsArray: [], errorMessage: "", isBusy: true };
                break;
            }

        case ApiActionKeys.News_Error:
            {
                retState = { ...state, newsArray: [], errorMessage: action.payload };
                break;
            }
        case ApiActionKeys.News_Read:
            {
                var payload = (action as IReadNewsAction).payload;
                retState = { ...state, newsArray: payload, errorMessage: "" };
                break;
            }
    }

    return retState;
}

export default sliderReducer;

